using UnityEngine;

public class PlayCinematicOnActivation : ActionOnActivation
{
    [SerializeField] CinematicSO cinematicToPlay;

    public override void Activated()
    {
        PlayCinematic();
    }

    void PlayCinematic()
    {
        if (cinematicToPlay != null) GameManager.Singleton.CinematicManager.PlayCinematic(cinematicToPlay.ID);
    }
}
