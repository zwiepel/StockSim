using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject panelStartMessage;
    public GameObject panelMainUI;

    public TMP_Text balanceText;
    public Button continueButton;
    public Button marketButton;
    public Button upgradeButton;

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
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        panelStartMessage.SetActive(true);
        panelMainUI.SetActive(false);

        continueButton.onClick.AddListener(ShowMainUI);
        UpdateBalanceUI();
    }

    void ShowMainUI()
    {
        panelStartMessage.SetActive(false);
        panelMainUI.SetActive(true);
    }

    void UpdateBalanceUI()
    {
        balanceText.text = $"Balance: ${PlayerBalance:N2}";
    }

    // Beispielmethoden (später aufrufen beim Kaufen/Verkaufen etc.)
    public void SpendMoney(float amount)
    {
        PlayerBalance -= amount;
    }

    public void EarnMoney(float amount)
    {
        PlayerBalance += amount;
    }
}
