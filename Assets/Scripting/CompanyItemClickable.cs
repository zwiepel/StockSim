using UnityEngine;
using UnityEngine.UI;

public class CompanyItemClickable : MonoBehaviour
{
    private Company company;

    public void Setup(Company companyData)
    {
        company = companyData;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        MarketUI.Instance.ShowCompanyDetails(company);
    }
}
