using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] string menuName;


    public string GetMenuName => menuName;

    public void ShowMenu()
    {
        gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }
}
