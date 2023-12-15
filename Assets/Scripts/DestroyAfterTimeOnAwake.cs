using UnityEngine;

public class DestroyAfterTimeOnAwake : MonoBehaviour
{
    [SerializeField] float time = 60f;


    void Awake()
    {
        Destroy(gameObject, time);
    }
}
