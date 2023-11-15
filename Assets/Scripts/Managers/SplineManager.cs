using UnityEngine.Splines;
using UnityEngine;

public class SplineManager : Manager
{
    [SerializeField] SplineContainer[] splines;
    CinematicSO[] cinematics;

    public override void LateInit()
    {
        cinematics = GameManager.Singleton.CinematicManager.Cinematics;

        for (int i = 0; i < cinematics.Length; i++)
        {
            cinematics[i].SetSpline(splines[i]);
        }
    }
}
