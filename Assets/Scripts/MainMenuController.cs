using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSettings()
    {
        
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
