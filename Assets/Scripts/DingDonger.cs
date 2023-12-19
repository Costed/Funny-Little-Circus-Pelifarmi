using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DingDonger : MonoBehaviour
{
    [SerializeField] AudioClip[] DingSounds;
    [SerializeField] AudioSource DingSource;


    void OnTriggerEnter()
    {
        DingSource.PlayOneShot(DingSounds[Random.Range(0, DingSounds.Length)], 1f);
    }
}
