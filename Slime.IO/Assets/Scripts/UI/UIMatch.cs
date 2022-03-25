using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMatch : UIObject
{
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Joystick playerJoystick;
    [SerializeField] private TextMeshProUGUI firstLeaderName;
    [SerializeField] private TextMeshProUGUI firstLeaderScore;
    [SerializeField] private TextMeshProUGUI secondLeaderName;
    [SerializeField] private TextMeshProUGUI secondLeaderScore;
    [SerializeField] private TextMeshProUGUI thirdLeaderName;
    [SerializeField] private TextMeshProUGUI thirdLeaderScore;

    private UIManager uiManager;

    public static UIMatch Instance;

    public Joystick PlayerJoystick { get => playerJoystick; }

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        uiManager = UIManager.Instance;

        mainMenuBtn.onClick.AddListener(OnMainMenuClick);
    }

    private void OnMainMenuClick()
    {
        uiManager.Toogle(UIType.MainMenu);
    }

    public void UpdateLeaderboard(string fName, string fScore, string sName, string sScore, string tName, string tScore)
    {
        firstLeaderName.text = fName;
        firstLeaderScore.text = fScore;
        secondLeaderName.text = sName;
        secondLeaderScore.text = sScore;
        thirdLeaderName.text = tName;
        thirdLeaderScore.text = tScore;
    }
}
