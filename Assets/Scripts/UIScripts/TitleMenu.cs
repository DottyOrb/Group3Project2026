using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    void Start() 
    {
        Cursor.lockState = CursorLockMode.None; //set to confined to limit to only the game window
        Cursor.visible = true;
    }
    public void StartBTN() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("8s Test Scene");
    }
    public void QuitBTN() 
    { 
        Application.Quit();
    }
}
