using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MarketUI : MonoBehaviour
{
    public GameObject panelMarket;
    public TMP_Dropdown categoryDropdown;
    public Transform companyListContent;
    public GameObject companyItemPrefab;

    private bool showingAll = true;
    private CompanyCategory currentCategory = CompanyCategory.Tech;

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
        UIManager.Instance.panelMainUI.SetActive(true);
    }

    void SetupCategoryDropdown()
    {
        categoryDropdown.ClearOptions();
        var options = new List<string>();

        options.Add("All"); // Wichtig: "All" hinzufügen

        foreach (var category in System.Enum.GetValues(typeof(CompanyCategory)))
        {
            options.Add(category.ToString());
        }

        categoryDropdown.AddOptions(options);
        categoryDropdown.onValueChanged.AddListener(FilterCompanies);

        categoryDropdown.value = 0; // Setze Dropdown auf "All" standardmäßig
    }

    void FilterCompanies(int selectedIndex)
    {
        foreach (Transform child in companyListContent)
        {
            Destroy(child.gameObject);
        }

        if (selectedIndex == 0)
        {
            // Zeige ALLE Firmen
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
            currentCategory = (CompanyCategory)(selectedIndex - 1); // Achtung: -1 weil "All" am Anfang eingefügt wurde

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
        item.GetComponent<CompanyItemUI>().SetCompany(company);
    }
}
