using System.Collections.Generic;
using UnityEngine;

public class DrawerOpener : MonoBehaviour
{
    [SerializeField] float offset;

    Drawer[] drawers;

    void Awake()
    {
        drawers = GetComponentsInChildren<Drawer>();

        time += offset;
    }

    float time;

    void Update()
    {
        time += Time.deltaTime;

        if (time >= 3f)
        {
            time -= 3f;
            ToggleRandomNum();
        }
    }

    List<Drawer> drawersList = new List<Drawer>();

    [ContextMenu("Toggle Random Drawers")]
    void ToggleRandomNum()
    {
        int numToToggle = Random.Range(0, drawers.Length + 1);

        for (int i = 0; i < numToToggle; i++)
        {
            int randIndex = Random.Range(0, drawers.Length);
            Drawer drawer = drawers[randIndex];

            if (!drawersList.Contains(drawer)) drawer.ToggleState();
        }

        drawersList.Clear();
    }
}
