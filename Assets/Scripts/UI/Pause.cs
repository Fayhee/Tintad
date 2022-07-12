using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject aimCanvas;
    public Button resumeButton;
    public Button mainMenuButton;



    void Awake()
    {
        pauseMenu.SetActive(false);
        aimCanvas.SetActive(true);
        resumeButton.onClick.AddListener(OnResumePressed);
        mainMenuButton.onClick.AddListener(OnMainMenuPressed);

        Time.timeScale = 1;
    }

    void OnResumePressed()
    {
        pauseMenu.SetActive(false);
        aimCanvas.SetActive(true);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    void OnMainMenuPressed()
    {
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            pauseMenu.SetActive(true);
            aimCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}



