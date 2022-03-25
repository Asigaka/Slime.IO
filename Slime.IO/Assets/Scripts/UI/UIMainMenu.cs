using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIObject
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button shopBtn;
    [SerializeField] private Button quitBtn;

    private UIManager uiManager;

    public static UIMainMenu Instance;

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        uiManager = UIManager.Instance;

        playBtn.onClick.AddListener(OnPlayBtnClick);
        settingsBtn.onClick.AddListener(OnSettingsBtnClick);
        shopBtn.onClick.AddListener(OnShopBtnClick);
        quitBtn.onClick.AddListener(OnQuitBtnClick);
    }

    private void OnPlayBtnClick()
    {
        uiManager.Toogle(UIType.Match);
    }

    private void OnShopBtnClick()
    {
        uiManager.Toogle(UIType.Shop);
    }

    private void OnSettingsBtnClick()
    {
        uiManager.Toogle(UIType.Settings);
    }

    private void OnQuitBtnClick()
    {
        Application.Quit();
    }
}
