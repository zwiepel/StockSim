using UnityEngine;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;

public class CompanyGraphLineUI : MonoBehaviour
{
    public UILineRenderer lineRenderer;

    public void ShowGraph(Company company)
    {
        List<Vector2> points = new List<Vector2>();

        if (company.priceHistory.Count < 2)
            return;

        float max = Mathf.Max(company.priceHistory.ToArray());
        float min = Mathf.Min(company.priceHistory.ToArray());

        float width = 500f; // Breite des Panels
        float height = 200f;
        float spacing = width / (company.priceHistory.Count - 1);

        for (int i = 0; i < company.priceHistory.Count; i++)
        {
            float x = i * spacing;
            float norm = Mathf.InverseLerp(min, max, company.priceHistory[i]);
            float y = norm * height;
            points.Add(new Vector2(x, y));
        }

        lineRenderer.Points = points.ToArray();
        lineRenderer.SetAllDirty();
    }
}
