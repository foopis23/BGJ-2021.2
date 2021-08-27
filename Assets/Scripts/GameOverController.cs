using CallbackEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
        EventSystem.Current.RegisterEventListener((OnPlayerDeathContext e) =>
        {
            canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        });
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    } 
}
