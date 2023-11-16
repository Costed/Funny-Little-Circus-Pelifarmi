using UnityEngine;
using System;

public class CinematicCompletedActivator : MonoBehaviour, IActivatable
{
    [SerializeField] CinematicSO trackedCinematic;

    public event Action OnActivate;
    public void Activate()
    {
        OnActivate?.Invoke();
    }

    void CinematicComplete(int cinematicID)
    {
        if (cinematicID == trackedCinematic.ID) Activate();
    }

    void OnEnable()
    {
        GameManager.Singleton.CinematicManager.OnCinematicCompleted += CinematicComplete;
    }

    void OnDisable()
    {
        GameManager.Singleton.CinematicManager.OnCinematicCompleted -= CinematicComplete;
    }
}
