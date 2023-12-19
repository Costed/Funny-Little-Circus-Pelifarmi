using UnityEngine;

public class ChaseQuit : MonoBehaviour
{
    [SerializeField] float length;
    [SerializeField] CanvasGroup group;

    float currentTime;
    float alpha;

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > length)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            alpha += Time.deltaTime;
            group.alpha = alpha;
        }
    }
}
