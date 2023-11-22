using UnityEngine;
using System;

public class Key : MonoBehaviour
{
    public void LoadState(bool state)
    {
        gameObject.SetActive(state);
    }
}
