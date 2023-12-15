using HietakissaUtils;
using UnityEngine;

public class UIManager : Manager
{
    [SerializeField] Animator anim;

    [SerializeField] Transform UI;
    [SerializeField] SliderSetting sensitivitySetting;
    [SerializeField] SliderSetting volumeSetting;

    [SerializeField] bool openMenuOnAwake;
    Menu[] menus;

    [SerializeField] GameObject[] itemDisplays;


    void Awake()
    {
        ExitTransition();

        if (UI == null) UI = transform;
        menus = UI.GetComponentsInChildren<Menu>();

        if (menus != null && menus.Length > 0)
        {
            if (openMenuOnAwake) OpenMenu(menus[0].GetMenuName);
            else OpenMenu("Null");
        }

        StopDisplayItem();
    }

    void OnEnable()
    {
        sensitivitySetting.OnValueChanged += SensitivityChanged;
        volumeSetting.OnValueChanged += VolumeChanged;

        GameData.OnPauseStateChanged += PauseMenu;
    }
    void OnDisable()
    {
        sensitivitySetting.OnValueChanged -= SensitivityChanged;
        volumeSetting.OnValueChanged -= VolumeChanged;

        GameData.OnPauseStateChanged -= PauseMenu;
    }


    void SensitivityChanged(float value)
    {
        GameData.Controls.SetSensitivity(value);
    }
    void VolumeChanged(float value)
    {
        float remappedValue = Maf.ReMap(0, 100, -80, 20, value);
        GameData.Settings.SetVolume((int)remappedValue);
    }


    void PauseMenu(bool state)
    {
        if (state) OpenMenu("Pause Menu");
        else OpenMenu("Null");
    }
    public void OpenMenu(string name)
    {
        Debug.Log($"Trying to open menu {name}");

        bool foundMenu = false;

        foreach (Menu menu in menus)
        {
            if (menu.GetMenuName == name)
            {
                Debug.Log($"Showing menu {menu.GetMenuName}");
                foundMenu = true;
                menu.ShowMenu();
            }
            else
            {
                Debug.Log($"Hiding menu {menu.GetMenuName}");
                menu.HideMenu();
            }
        }

        if (foundMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
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


    public void DisplayItem(int ID)
    {
        GameData.Player.Camera.Disable();
        GameData.Player.Movement.Disable();
        GameData.DisplayingItem = true;

        for (int i = 0; i < itemDisplays.Length; i++)
        {
            itemDisplays[i].SetActive(i == ID);
        }
    }
    public void StopDisplayItem()
    {
        GameData.Player.Camera.Enable();
        GameData.Player.Movement.Enable();
        GameData.DisplayingItem = false;

        for (int i = 0; i < itemDisplays.Length; i++)
        {
            itemDisplays[i].SetActive(false);
        }
    }
}
