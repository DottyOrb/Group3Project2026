using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public GameObject menuObj;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            TogglePause();
        }
    }
    public void TogglePause() 
    {
        if (isPaused) { 
            Time.timeScale = 0.0f;
            menuObj.SetActive(true);
            Cursor.lockState = CursorLockMode.None; //set to confined to limit to only the game window
            Cursor.visible = true;
        }
        if (!isPaused) { 
            Time.timeScale = 1.0f;
            menuObj.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ResumeBTN() 
    {
        isPaused = !isPaused;
        TogglePause();
    }
    public void TitleBTN() 
    {
        isPaused = !isPaused;
        TogglePause();
        SceneManager.LoadScene("TitleScreenScene"); 
    }
    public void QuitBTN() 
    { 
        Application.Quit();
    }
}
