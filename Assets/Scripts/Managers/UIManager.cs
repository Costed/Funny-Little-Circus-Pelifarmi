using UnityEngine;

public class UIManager : Manager
{
    [SerializeField] Animator anim;

    void Awake()
    {
        ExitTransition();
    }


    public void LoadScene(string sceneName)
    {
        GameManager.Singleton.SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        if (Application.isEditor) Debug.Log("Quit game doesn't work in the editor.");
        else Application.Quit();
    }


    public void EnterTransition()
    {
        anim.Play("EnterTransition");
    }

    public void ExitTransition()
    {
        anim.Play("ExitTransition");
    }
}
