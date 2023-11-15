using System.Collections.Generic;
using UnityEngine;

public class TriggerPushZone : MonoBehaviour
{
    [SerializeField] Vector3 worldDirection;
    [SerializeField] float pushForce;

    List<Rigidbody> rbs = new List<Rigidbody>();

    void FixedUpdate()
    {
        foreach (Rigidbody rb in rbs)
        {
            rb.AddForce(transform.InverseTransformDirection(worldDirection) * pushForce * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb) && !rbs.Contains(rb)) rbs.Add(rb);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb) && rbs.Contains(rb)) rbs.Remove(rb);
    }
}
