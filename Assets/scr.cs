using UnityEngine;
using UnityEngine.InputSystem;

public class scr : MonoBehaviour
{
    void Update()
    {
        Quit();
    }

    void Quit()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }
}