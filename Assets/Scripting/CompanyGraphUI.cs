using UnityEngine;

public class CompanyGraphUI : MonoBehaviour
{
    public GameObject panel;
    public CompanyGraph companyGraph;

    public void ShowGraph(Company company)
    {
        panel.SetActive(true);
        companyGraph.SetCompany(company);
    }

    public void HideGraph()
    {
        panel.SetActive(false);
        companyGraph.ClearGraph();
    }
}
