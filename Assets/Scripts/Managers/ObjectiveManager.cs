using System.Text;
using UnityEngine;
using TMPro;

public class ObjectiveManager : Manager
{
    [SerializeField] ObjectiveSO[] objectives;
    ObjectiveSO currentObjective;

    int currentCompletion;

    int objectiveIndex;
    int taskIndex;

    ObjectiveTask currentTask;

    [SerializeField] TextMeshProUGUI objectiveTitleText;
    [SerializeField] TextMeshProUGUI objectiveContentText;

    void Awake()
    {
        currentObjective = objectives[0];
        currentTask = GetCurrentTask();
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (currentObjective == null) return;

            ProgressTask();
        }
    }


    public void ProgressTask()
    {
        if (currentObjective == null)
        {
            Debug.Log("No objective!");
            return;
        }

        currentCompletion++;

        Debug.Log($"Task completion: {currentCompletion}/{currentTask.requiredCompletion}");

        if (currentCompletion >= currentTask.requiredCompletion)
        {
            //Completed task
            currentCompletion = 0;
            taskIndex++;

            Debug.Log($"Completed task: {currentTask.taskName}");

            if (taskIndex >= currentObjective.tasks.Length)
            {
                //No more tasks
                objectiveIndex++;
                taskIndex = 0;

                currentTask = GetCurrentTask();

                if (objectiveIndex < objectives.Length)
                {
                    //Get next objective
                    currentObjective = objectives[objectiveIndex];
                    currentTask = GetCurrentTask();

                    Debug.Log($"Completed all tasks. Next objective is {currentObjective.objectiveName}, of which task: {currentTask.taskName}");
                }
                else currentObjective = null;
            }

            if (currentObjective != null) currentTask = GetCurrentTask();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (currentObjective == null)
        {
            objectiveTitleText.text = "";
            objectiveContentText.text = "";
            return;
        }

        objectiveTitleText.text = currentObjective.objectiveName;
        objectiveContentText.text = $"[{currentCompletion}/{currentTask.requiredCompletion}] {currentTask.taskName}";
    }


    ObjectiveTask GetCurrentTask()
    {
        return currentObjective.tasks[taskIndex];
    }
}
