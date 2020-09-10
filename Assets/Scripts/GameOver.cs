using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void OnDestroy()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
