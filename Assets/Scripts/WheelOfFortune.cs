using HietakissaUtils;
using UnityEngine;
using System;

public class WheelOfFortune : MonoBehaviour
{
    [SerializeField] PropCollection[] junkCollections;
    [SerializeField] Transform junkSpawnPos;

    void Spin()
    {
        PropCollection collection = junkCollections.RandomElement();

        foreach (GameObject prop in collection.props)
        {
            GameObject go = Instantiate(prop, junkSpawnPos.position, Quaternion.identity);
            Destroy(go, 60);
        }
    }
}

[Serializable]
class PropCollection
{
    [SerializeField] string collectionName;
    [field: SerializeField] public GameObject[] props { get; private set; }
}
