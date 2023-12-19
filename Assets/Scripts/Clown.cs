using System.Collections;
using UnityEngine.AI;
using UnityEngine;

[SelectionBase]
public class Clown : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float catchRange;

    [SerializeField] GameObject visual;

    [SerializeField] bool drawGizmos;
    
    NavMeshAgent agent;
    bool chasing;
    Vector3 startPos;
    Quaternion startRot;

    Transform playerTransform;

    const float maxChaseEndTime = 5f;
    float chaseEndTime;

    Animator anim;

    AudioSource chaseMusicSource;
    float chaseVolume;



    void Awake()
    {
        anim = GetComponent<Animator>();
        chaseMusicSource = GetComponent<AudioSource>();
        chaseVolume = chaseMusicSource.volume;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Start()
    {
        playerTransform = GameData.Player.Transform;

        //GameManager.Singleton.OnLoadCheckpoint += () => EndChase();
    }

    void Update()
    {
        if (!chasing) return;

        agent.SetDestination(playerTransform.position);

        if (Vector3.Distance(transform.position, playerTransform.position) <= catchRange) CatchPlayer();
    }


    void CatchPlayer()
    {
        if (chasing) EndChase(true);
        GameManager.Singleton.LoadCheckpoint();
    }

    [ContextMenu("Start Chase")]
    public void StartChase()
    {
        //if (chaseMusicSource) chaseMusicSource.Play();
        if (chaseMusicSource) LerpChaseVolume(chaseVolume);

        agent.enabled = true;
        chasing = true;

        anim.SetTrigger("ChaseStart");
    }

    [ContextMenu("End Chase")]
    public void EndChase(bool catched = false)
    {
        //agent.enabled = false;
        //chasing = false;
        //
        //transform.position = startPos;
        //transform.rotation = startRot;

        //if (chaseMusicSource) chaseMusicSource.Stop();
        if (chaseMusicSource) LerpChaseVolume(0f);

        Debug.Log("End chase");

        anim.SetTrigger("ChaseEnd");
        StartCoroutine(EndChaseCor(catched));
    }

    IEnumerator EndChaseCor(bool catched)
    {
        chasing = false;

        if (catched)
        {
            visual.SetActive(false);

            agent.speed = speed;
            agent.enabled = false;

            transform.position = startPos;
            transform.rotation = startRot;

            yield return new WaitForSeconds(2f);
            visual.SetActive(true);
        }
        else
        {
            agent.destination = startPos;
            agent.speed = speed * 2.5f;

            while (Vector3.Distance(transform.position, startPos) > 0.3f)
            {
                chaseEndTime += Time.deltaTime;
                if (chaseEndTime >= maxChaseEndTime) break;

                yield return null;
            }

            agent.speed = speed;
            agent.enabled = false;

            transform.position = startPos;
            transform.rotation = startRot;
        }
    }

    IEnumerator LerpChaseVolume(float targetVolume)
    {
        float lerpTime = 0f;

        while (true)
        {
            lerpTime += Time.deltaTime * 0.4f;
            chaseMusicSource.volume = Mathf.Lerp(chaseMusicSource.volume, targetVolume, lerpTime);

            if (lerpTime >= 1f) break;

            yield return null;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Clown trigger");

        if (other.CompareTag("Clown"))
        {
            Debug.Log("Clown crouch");
            anim.SetTrigger("Crouch");
        }
    }


    void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        if (chasing) Gizmos.color = Color.red;
        else Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, catchRange);
    }
}
