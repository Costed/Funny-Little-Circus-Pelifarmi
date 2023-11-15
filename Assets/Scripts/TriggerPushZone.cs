using System.Collections.Generic;
using UnityEngine;

public class TriggerPushZone : MonoBehaviour
{
    [SerializeField] Vector3 worldDirection;
    [SerializeField] float pushForce;

    List<Rigidbody> rbs = new List<Rigidbody>();

    void FixedUpdate()
    {
        Vector3 direction = transform.right * worldDirection.x + transform.forward * worldDirection.z + transform.up * worldDirection.y;

        foreach (Rigidbody rb in rbs)
        {
            rb.AddForce(transform.InverseTransformDirection(direction) * pushForce * Time.deltaTime);
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
