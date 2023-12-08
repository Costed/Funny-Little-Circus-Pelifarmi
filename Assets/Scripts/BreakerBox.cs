using UnityEngine.Events;
using UnityEngine;

public class BreakerBox : MonoBehaviour
{
    bool[] pulledLeverStates = new bool[4];
    Lever[] levers = new Lever[4];

    [SerializeField] UnityEvent onAllLeversPulled;


    public void RegisterLever(int id, Lever lever)
    {
        levers[id] = lever;
    }


    public void PulledLever(int index, bool stateIsCorrect)
    {
        pulledLeverStates[index] = stateIsCorrect;

        UpdateLeverVisuals();

        if (AllCorrect()) AllLeversCorrect();
    }

    void UpdateLeverVisuals()
    {
        foreach (Lever lever in levers) lever.SetVisuals();
    }

    void AllLeversCorrect()
    {
        Debug.Log("Correctly pulled all levers");
        onAllLeversPulled?.Invoke();
    }


    bool AllCorrect()
    {
        foreach (bool state in pulledLeverStates)
        {
            if (!state) return false;
        }

        return true;
    }

    public bool[] GetCorrectStates()
    {
        return pulledLeverStates;
    }
}
