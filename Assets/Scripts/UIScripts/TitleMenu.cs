using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    void Start() 
    {
        Cursor.lockState = CursorLockMode.None; //set this to confined to limit to only the game window
        Cursor.visible = true;
    }
    public void StartBTN() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Level-001");
    }
    public void QuitBTN() 
    { 
        Application.Quit();
    }
}
