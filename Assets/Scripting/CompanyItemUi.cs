using TMPro;
using UnityEngine;

public class CompanyItemUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text priceText;

    public void SetCompany(Company company)
    {
        Debug.Log("SetCompany called for: " + company.Name);
        nameText.text = company.Name;

        float percentChange = company.GetPriceChangePercent();
        string sign = percentChange >= 0 ? "+" : "-";
        string color = percentChange >= 0 ? "green" : "red";

        priceText.text = $"${company.CurrentPrice:F2} <color={color}>({sign}{Mathf.Abs(percentChange):F2}%)</color>";
    }
}
