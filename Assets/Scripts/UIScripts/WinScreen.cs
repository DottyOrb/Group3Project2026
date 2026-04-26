using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TitleBTN()
    {
        SceneManager.LoadScene("TitleScreenScene");
    }
    public void ExitBTN()
    {
        Application.Quit();
    }
}
