using UnityEngine;

[SelectionBase]
public class Lever : MonoBehaviour
{
    [SerializeField] BreakerBox breaker;
    [SerializeField] bool correctPulledState;
    [SerializeField] int id;
    bool pulled;

    [SerializeField] GameObject[] stateIndicators;
    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
        breaker.RegisterLever(id, this);
    }

    void Start()
    {
        breaker.PulledLever(id, pulled == correctPulledState);
    }


    public void PullLever()
    {
        pulled = !pulled;

        if (pulled) anim.CrossFade("LeverPull", 0.3f);
        else anim.CrossFade("LeverUnPull", 0.3f);

        breaker.PulledLever(id, pulled == correctPulledState);
    }

    public void SetVisuals()
    {
        bool[] pulledLevers = breaker.GetCorrectStates();

        for (int i = 0; i < pulledLevers.Length; i++)
        {
            stateIndicators[i].SetActive(pulledLevers[i]);
        }
    }
}
