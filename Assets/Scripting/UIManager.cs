using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject panelStartMessage;
    public GameObject panelMainUI;

    [Header("UI Elements")]
    public TMP_Text balanceText;
    public Button continueButton;
    public Button marketButton;
    public Button upgradeButton;

    [Header("External UIs")]
    public MarketUI marketUI;

    private float _playerBalance = 10000f;

    public float PlayerBalance
    {
        get => _playerBalance;
        set
        {
            _playerBalance = value;
            UpdateBalanceUI();
        }
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        // Startbildschirm anzeigen
        panelStartMessage.SetActive(true);
        panelMainUI.SetActive(false);

        // Button-Events
        continueButton.onClick.AddListener(ShowMainUI);
        marketButton.onClick.AddListener(OpenMarket);


        // Anfangs-Balance anzeigen
        UpdateBalanceUI();
    }

    void ShowMainUI()
    {
        panelStartMessage.SetActive(false);
        panelMainUI.SetActive(true);
    }
    void OpenMarket()
    {
        panelMainUI.SetActive(false);
        marketUI.ShowMarket();
    }

    void UpdateBalanceUI()
    {
        balanceText.text = $"Balance: ${PlayerBalance:N2}";
    }

    public void SpendMoney(float amount)
    {
        PlayerBalance -= amount;
    }

    public void EarnMoney(float amount)
    {
        PlayerBalance += amount;
    }
}
