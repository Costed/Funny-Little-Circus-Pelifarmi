using System.Collections;
using HietakissaUtils;
using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{
    [SerializeField] PropCollection[] junkCollections;
    [SerializeField] Transform junkSpawnPos;

    [SerializeField] InteractionActivator interactionActivator;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void Spin()
    {
        anim.Play("WheelOfFortune_Spin");
        StartCoroutine(SpawnJunk());
    }

    IEnumerator SpawnJunk()
    {
        interactionActivator.SetInteractable(false);

        yield return new WaitForSeconds(7.5f);

        PropCollection collection = junkCollections.RandomElement();

        foreach (GameObject prop in collection.props)
        {
            int propCount = Random.Range(1, collection.maxCount);
            for (int i = 0; i < propCount; i++)
            {
                GameObject go = Instantiate(prop, junkSpawnPos.position, Quaternion.identity);
                Destroy(go, 60);
                yield return new WaitForSeconds(0.35f);
            }
            yield return new WaitForSeconds(0.7f);
            
        }

        yield return new WaitForSeconds(1.5f);
        interactionActivator.SetInteractable(true);
    }
}

[System.Serializable]
class PropCollection
{
    [SerializeField] string collectionName;
    [field: SerializeField] public GameObject[] props { get; private set; }
    [field: SerializeField] public int maxCount { get; private set; }
}
