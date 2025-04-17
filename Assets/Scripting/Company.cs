using System.Collections.Generic;
using UnityEngine;

public enum CompanyCategory
{
    Tech,
    Pharma,
    Industrie,
    Energie,
    Finanzen,
    Lifestyle
}

[System.Serializable]
public class Company
{
    public string Name;
    public float CurrentPrice;
    public float ChangeRate;
    public CompanyCategory Category;

    public List<float> priceHistory = new List<float>();

    public void UpdatePrice()
    {
        float change = UnityEngine.Random.Range(-ChangeRate, ChangeRate);
        CurrentPrice += change;
        if (CurrentPrice < 0) CurrentPrice = 0.01f;

        priceHistory.Add(CurrentPrice);
        if (priceHistory.Count > 50)
            priceHistory.RemoveAt(0);
    }

    public float LastPrice => priceHistory.Count > 1 ? priceHistory[^2] : CurrentPrice;

    public float GetPriceChangePercent()
    {
        float previous = LastPrice;
        if (previous == 0) return 0;
        return ((CurrentPrice - previous) / previous) * 100f;
    }
}
