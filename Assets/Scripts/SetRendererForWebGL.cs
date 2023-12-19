using UnityEngine;

public class SetRendererForWebGL : MonoBehaviour
{
    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        
    }
}
