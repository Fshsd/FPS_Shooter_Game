using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Map"); // اسم مشهد اللعب
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // يشتغل فقط في النسخة التنفيذية (build)
    }
}