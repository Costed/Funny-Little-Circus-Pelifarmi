using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneManager : Manager
{
    [SerializeField] float waitTime;


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCor(sceneName));
    }

    IEnumerator LoadSceneCor(string sceneName)
    {
        GameManager.Singleton.UIManager.EnterTransition();
        yield return new WaitForSeconds(waitTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
