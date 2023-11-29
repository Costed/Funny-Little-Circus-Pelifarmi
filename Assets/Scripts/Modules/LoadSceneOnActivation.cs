using UnityEngine;

public class LoadSceneOnActivation : ActionOnActivation
{
    [SerializeField] string sceneName;

    public override void Activated()
    {
        GameManager.Singleton.SceneManager.LoadScene(sceneName);
    }
}
