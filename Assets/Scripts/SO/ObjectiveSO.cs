using UnityEngine;

[CreateAssetMenu(menuName = "Game/Objective")]
public class ObjectiveSO : ScriptableObject
{
    [field:SerializeField] public string objectiveName { get; private set; }
    [field: SerializeField] public ObjectiveTask[] tasks { get; private set; }
}

[System.Serializable]
public class ObjectiveTask
{
    [field: SerializeField] public int requiredCompletion { get; private set; }
    [field:SerializeField] public string taskName { get; private set; }
}
