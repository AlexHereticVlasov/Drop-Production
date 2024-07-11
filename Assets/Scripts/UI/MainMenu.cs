using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;

    //Hack: Temp Solution. Add Scene Loader
    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowPanel(GameObject panelToShow)
    {
        foreach (var panel in _panels)
            panel.SetActive(panel == panelToShow);
    }
     
    public void Quit() => Application.Quit();
}
