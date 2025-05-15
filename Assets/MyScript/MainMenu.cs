using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CinemachineCamera MainMenuCam;
    public CinemachineCamera PlayerCam;


    void Start()
    {
        PlayerCam.gameObject.SetActive(false);
        MainMenuCam.gameObject.SetActive(true);

                // التأكد من أن الماوس مرئي ومتاح
        Cursor.visible = true;  // يجعل الماوس مرئي
        Cursor.lockState = CursorLockMode.None;  // يحرر الماوس من قفل الحركة
    }
    public void PlayGame()
    {
        PlayerCam.gameObject.SetActive(true);
        MainMenuCam.gameObject.SetActive(false);
        Cursor.visible = false;
Cursor.lockState = CursorLockMode.Locked;

Debug.Log("Click Play");

    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); 
    }
}
