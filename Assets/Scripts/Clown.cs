using UnityEngine.AI;
using UnityEngine;

[SelectionBase]
public class Clown : MonoBehaviour
{
    NavMeshAgent agent;
    bool chasing;

    Transform playerTransform;

    [SerializeField] float speed;
    [SerializeField] float catchRange;

    [SerializeField] bool drawGizmos;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Start()
    {
        playerTransform = GameData.Player.Transform;
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
    }


    void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.DrawWireSphere(transform.position, catchRange);
    }
}
