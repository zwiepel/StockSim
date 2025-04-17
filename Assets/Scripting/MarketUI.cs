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
                item.GetComponent<CompanyItemUI>().SetCompany(company);
            }
        }
    }
}
