using UnityEngine;

public class Gate : MonoBehaviour
{
    int unlockedLocks;


    public void UnlockLock()
    {
        unlockedLocks++;

        if (unlockedLocks == 4) GameManager.Singleton.SceneManager.LoadScene("Outside_Chase");
    }
}
