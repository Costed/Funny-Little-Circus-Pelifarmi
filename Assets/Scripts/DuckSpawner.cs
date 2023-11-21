using UnityEngine.Splines;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    [SerializeField] SplineContainer spline;

    [SerializeField] int count;
    [SerializeField] DuckController duckPrefab;

    void Awake()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(duckPrefab, transform).SetSpline(spline);
        }
    }
}
