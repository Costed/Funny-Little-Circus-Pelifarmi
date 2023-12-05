using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CattedWalk : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Transform targetPoint;
    [SerializeField][Range(0f, 1f)] float SliderLayerWeight;
    int animLayerIndex;
    float doorwayFactor;
    bool inDoorway;

    NavMeshAgent cattedAgent;

    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        cattedAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {

        if (Input.GetMouseButton(0))
            MoveCommand();

        if (inDoorway)
            DoDoorway(true);
        else
            DoDoorway(false);

        float clampedVelocity = Mathf.Clamp(cattedAgent.velocity.magnitude / 2f, 0f, 1f);
        anim.SetFloat("MoveBlend", clampedVelocity);
        anim.SetLayerWeight(animLayerIndex, doorwayFactor);
    }

    void MoveCommand()
    {
        Vector2 mousePosition = Input.mousePosition;

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        //QueryTriggerInteraction triggerInteraction = characterMovement.triggerInteraction;

        //if (Physics.Raycast(ray, out RaycastHit hitResult, Mathf.Infinity, groundMask, triggerInteraction))
        //MoveToLocation(hitResult.point);

        Plane floor = new Plane(Vector3.up, 0f); //This plane represents the floor for pointing and clicking to target spells

        //float enter = 0.0f; //Initialize enter float (or don't)
        if (floor.Raycast(ray, out float enter))
        {
            targetPoint.position = ray.GetPoint(enter);
            cattedAgent.destination = targetPoint.position;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        inDoorway = true;
        if (other.gameObject.name == "TriggerBox1")
            animLayerIndex = 1;
        if (other.gameObject.name == "TriggerBox2")
            animLayerIndex = 2;
    }
    void OnTriggerExit(Collider other)
    {
        inDoorway = false;
    }

    void DoDoorway(bool goingIn)
    {
        float speedMultiplier = 3f;
        if (goingIn)
        {
            doorwayFactor = Mathf.MoveTowards(doorwayFactor, 1f, Time.deltaTime * speedMultiplier);
        }
        else
        {
            doorwayFactor = Mathf.MoveTowards(doorwayFactor, 0f, Time.deltaTime * speedMultiplier);
        }
    }
}
