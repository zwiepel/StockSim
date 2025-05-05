using UnityEngine;
using System.Linq;

public class CompanyGraph : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private Company company;

    public void SetCompany(Company newCompany)
    {
        company = newCompany;
        DrawGraph();
    }

    public void DrawGraph()
    {
        if (company == null || company.priceHistory.Count < 2)
        {
            Debug.Log(" Nicht genug Daten für Graph.");
            return;
        }

        float xSpacing = 20f;
        float max = company.priceHistory.Max();
        float min = company.priceHistory.Min();
        float yScale = 100f / Mathf.Max(1f, max - min);

        lineRenderer.positionCount = company.priceHistory.Count;

        for (int i = 0; i < company.priceHistory.Count; i++)
        {
            float x = i * xSpacing;
            float y = (company.priceHistory[i] - min) * yScale;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }

        Debug.Log($" Graph gezeichnet für {company.Name} mit {lineRenderer.positionCount} Punkten.");
    }

    public void ClearGraph()
    {
        lineRenderer.positionCount = 0;
    }
}
