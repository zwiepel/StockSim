using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MarketUI : MonoBehaviour
{
    public static MarketUI Instance;

    [Header("Panels")]
    public GameObject panelMarket;

    [Header("Dropdown & Content")]
    public TMP_Dropdown categoryDropdown;
    public Transform companyListContent;
    public GameObject companyItemPrefab;

    [Header("Graph UI")]
    public CompanyGraphUI graphUI;

    private CompanyCategory currentCategory = CompanyCategory.Tech;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        panelMarket.SetActive(false);
        SetupCategoryDropdown();
    }

    public void ShowMarket()
    {
        panelMarket.SetActive(true);
        FilterCompanies(categoryDropdown.value);
    }

    public void HideMarket()
    {
        panelMarket.SetActive(false);
    }

    public void ShowCompanyDetails(Company company)
    {
        panelMarket.SetActive(false); // MarketPanel ausblenden
        graphUI.ShowGraph(company);   // GraphPanel anzeigen
    }

    void SetupCategoryDropdown()
    {
        categoryDropdown.ClearOptions();
        var options = new List<string> { "All" }; // "All" als erste Option

        foreach (var category in System.Enum.GetValues(typeof(CompanyCategory)))
        {
            options.Add(category.ToString());
        }

        categoryDropdown.AddOptions(options);
        categoryDropdown.onValueChanged.AddListener(FilterCompanies);
        categoryDropdown.value = 0; // Starte mit "All"
    }

    void FilterCompanies(int selectedIndex)
    {
        foreach (Transform child in companyListContent)
        {
            Destroy(child.gameObject);
        }

        if (selectedIndex == 0)
        {
            // Zeige alle Firmen aller Kategorien
            foreach (var categoryList in MarketManager.Instance.categorizedCompanies.Values)
            {
                foreach (var company in categoryList)
                {
                    CreateCompanyItem(company);
                }
            }
        }
        else
        {
            // Zeige Firmen einer bestimmten Kategorie
            currentCategory = (CompanyCategory)(selectedIndex - 1); // -1 wegen "All"

            if (MarketManager.Instance.categorizedCompanies.TryGetValue(currentCategory, out var companyList))
            {
                foreach (var company in companyList)
                {
                    CreateCompanyItem(company);
                }
            }
        }
    }

    void CreateCompanyItem(Company company)
    {
        var item = Instantiate(companyItemPrefab, companyListContent);

        var ui = item.GetComponent<CompanyItemUI>();
        var clickable = item.GetComponent<CompanyItemClickable>();

        if (ui == null || clickable == null)
        {
            Debug.LogError($"CompanyItemPrefab ist unvollständig für {company.Name}!");
            return;
        }

        ui.SetCompany(company);
        clickable.Setup(company);
    }
}
