using UnityEngine;

public class CompanyGraph : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Company company;

    void Update()
    {
        if (company == null || company.priceHistory.Count == 0) return;

        lineRenderer.positionCount = company.priceHistory.Count;

        for (int i = 0; i < company.priceHistory.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(i * 0.2f, company.priceHistory[i] * 0.1f, 0));
        }
    }
}
