using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public InputAction backInput;

    private const float keyPressedValue = 1;

    private void OnEnable()
    {
        backInput.Enable();
    }

    private void OnDisable()
    {
        backInput.Disable();
    }

    public void Update()
    {
        if (backInput.ReadValue<float>() == keyPressedValue)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}