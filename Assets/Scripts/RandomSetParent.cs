using HietakissaUtils;
using UnityEngine;

public class RandomSetParent : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] Transform target;

    void Awake()
    {
        Transform randomPos = positions.RandomElement();
        target.parent = randomPos;
        target.position = randomPos.position;
        target.rotation = randomPos.rotation;
    }
}
