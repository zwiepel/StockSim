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
}
