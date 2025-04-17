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

    private CompanyCategory currentCategory = CompanyCategory.Tech;

    void Start()
    {
        panelMarket.SetActive(false);
        SetupCategoryDropdown();
    }

    public void ShowMarket()
    {
        panelMarket.SetActive(true);
        FilterCompanies((int)currentCategory);
    }

    public void HideMarket()
    {
        panelMarket.SetActive(false);
    }

    void SetupCategoryDropdown()
    {
        categoryDropdown.ClearOptions();
        var options = new List<string>();
        foreach (var category in System.Enum.GetValues(typeof(CompanyCategory)))
        {
            options.Add(category.ToString());
        }

        categoryDropdown.AddOptions(options);
        categoryDropdown.onValueChanged.AddListener(FilterCompanies);
    }

    void FilterCompanies(int selectedIndex)
    {
        currentCategory = (CompanyCategory)selectedIndex;

        foreach (Transform child in companyListContent)
        {
            Destroy(child.gameObject);
        }

        if (MarketManager.Instance.categorizedCompanies.TryGetValue(currentCategory, out var companyList))
        {
            foreach (var company in companyList)
            {
                var item = Instantiate(companyItemPrefab, companyListContent);
                var texts = item.GetComponentsInChildren<TMP_Text>();

                // [0] = Name, [1] = Preis + Prozent
                texts[0].text = company.Name;

                float percentChange = company.GetPriceChangePercent();
                string sign = percentChange >= 0 ? "+" : "-";
                string color = percentChange >= 0 ? "green" : "red";

                texts[1].text = $"${company.CurrentPrice:F2} <color={color}>({sign}{Mathf.Abs(percentChange):F2}%)</color>";
            }
        }
    }
}
