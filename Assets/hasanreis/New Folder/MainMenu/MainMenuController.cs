using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // "GameScene" kısmına geçiş yapmak istediğiniz sahnenin ismini yazın
    }
}
