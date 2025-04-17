using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance;

    public Dictionary<CompanyCategory, List<Company>> categorizedCompanies = new();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        // Kategorien vorbereiten
        foreach (CompanyCategory category in System.Enum.GetValues(typeof(CompanyCategory)))
        {
            categorizedCompanies[category] = new List<Company>();
        }

        // Beispiel-Firmen hinzufügen
        AddCompany("Nexora Dynamics", 100f, 2f, CompanyCategory.Tech);
        AddCompany("Genaxa BioCorp", 75f, 1.5f, CompanyCategory.Pharma);
        AddCompany("Stahlwerk Nord", 50f, 3f, CompanyCategory.Industrie);
        AddCompany("Solventra Energy", 120f, 2.5f, CompanyCategory.Energie);
        AddCompany("Credex Capital", 90f, 1.2f, CompanyCategory.Finanzen);

        // Preise regelmäßig aktualisieren
        InvokeRepeating(nameof(UpdateMarket), 1f, 2f);
    }

    void AddCompany(string name, float startPrice, float changeRate, CompanyCategory category)
    {
        var newCompany = new Company
        {
            Name = name,
            CurrentPrice = startPrice,
            ChangeRate = changeRate,
            Category = category
        };
        categorizedCompanies[category].Add(newCompany);
    }

    void UpdateMarket()
    {
        foreach (var category in categorizedCompanies.Values)
        {
            foreach (var company in category)
            {
                company.UpdatePrice();
            }
        }
    }
}
