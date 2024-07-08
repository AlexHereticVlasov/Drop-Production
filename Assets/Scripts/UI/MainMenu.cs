using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;

    public void ShowPanel(GameObject panelToShow)
    {
        foreach (var panel in _panels)
            panel.SetActive(panel == panelToShow);
    }
     
    public void Quit() => Application.Quit();
}
