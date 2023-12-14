using UnityEngine.AI;
using UnityEngine;
using System.Collections;

[SelectionBase]
public class Clown : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float catchRange;

    [SerializeField] bool drawGizmos;
    
    NavMeshAgent agent;
    bool chasing;
    Vector3 startPos;
    Quaternion startRot;

    Transform playerTransform;

    const float maxChaseEndTime = 5f;
    float chaseEndTime;

    float timeShutOff;

    bool oldHasPath;

    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Start()
    {
        playerTransform = GameData.Player.Transform;

        GameManager.Singleton.OnLoadCheckpoint += () => EndChase();
    }

    void Update()
    {
        if (!chasing) return;

        agent.SetDestination(playerTransform.position);

        bool hasPath = agent.hasPath;

        if (hasPath == oldHasPath)
        {
            if (hasPath) timeShutOff = 0f;
            else timeShutOff += Time.deltaTime;
        }


        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(playerTransform.position, path);

        if (Vector3.Distance(transform.position, playerTransform.position) <= catchRange) CatchPlayer();

        oldHasPath = hasPath;
    }


    void CatchPlayer()
    {
        if (chasing) EndChase();
        GameManager.Singleton.LoadCheckpoint();
    }

    [ContextMenu("Start Chase")]
    public void StartChase()
    {
        agent.enabled = true;
        chasing = true;

        anim.SetTrigger("ChaseStart");
    }

    [ContextMenu("End Chase")]
    public void EndChase()
    {
        //agent.enabled = false;
        //chasing = false;
        //
        //transform.position = startPos;
        //transform.rotation = startRot;

        anim.SetTrigger("ChaseEnd");
        StartCoroutine(EndChaseCor());
    }

    IEnumerator EndChaseCor()
    {
        chasing = false;

        agent.destination = startPos;
        agent.speed = speed * 2.5f;

        while (Vector3.Distance(transform.position, startPos) > 0.3f)
        {
            chaseEndTime += Time.deltaTime;
            if (chaseEndTime >= maxChaseEndTime) break;

            yield return null;
        }

        agent.enabled = false;
        agent.speed = speed;

        transform.position = startPos;
        transform.rotation = startRot;
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
