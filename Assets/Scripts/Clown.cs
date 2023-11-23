using UnityEngine.AI;
using UnityEngine;

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


    void Awake()
    {
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
        if (chasing) agent.SetDestination(playerTransform.position);

        if (Vector3.Distance(transform.position, playerTransform.position) <= catchRange) CatchPlayer();
    }


    void CatchPlayer()
    {
        EndChase();
        GameManager.Singleton.LoadCheckpoint();
    }

    [ContextMenu("Start Chase")]
    public void StartChase()
    {
        agent.enabled = true;
        chasing = true;
    }

    [ContextMenu("End Chase")]
    public void EndChase()
    {
        agent.enabled = false;
        chasing = false;

        transform.position = startPos;
        transform.rotation = startRot;
    }


    void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        if (chasing) Gizmos.color = Color.red;
        else Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, catchRange);
    }
}
