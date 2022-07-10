using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    

    public void Defender()
    {
        SceneManager.LoadScene("Shooter");
    }

    public void CityGame()
    {
        SceneManager.LoadScene("City Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
