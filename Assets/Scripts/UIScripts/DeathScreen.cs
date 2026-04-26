using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreen;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RetryBTN() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Level-001");
    }
    public void MenuBTN() 
    {
        SceneManager.LoadScene("TitleScreenScene");
    }
    public void ExitBTN() 
    { 
        Application.Quit();
    }
}
