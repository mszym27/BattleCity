using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour
{
    public InputAction moveInput;
    public InputAction chooseInput;
    public InputAction quitInput;
    public float inputDelay = 0.1f;

    private const float keyPressedValue = 1;
    private float nextMoveTime = 0;
    private const float up = 1;
    private const float down= -1;

    private enum menuOptions
    {
        OnePlayer = 4,
        TwoPlayers = 3,
        Options = 2,
        Credits = 1
    }

    private int menuTopPosition = (int)menuOptions.OnePlayer;
    private int menuBottomPosition = (int)menuOptions.Credits;

    private int currentCursorPosition;

    private void OnEnable()
    {
        //starting position is always top of the menu
        currentCursorPosition = menuTopPosition;

        moveInput.Enable();
        chooseInput.Enable();
        quitInput.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        chooseInput.Disable();
        quitInput.Disable();
    }

    public void Update()
    {
        if (quitInput.ReadValue<float>() == keyPressedValue)
        {
            Application.Quit();
        }
        else if(chooseInput.ReadValue<float>() == keyPressedValue)
        {
            choose();
        }
        else
        {
            if (nextMoveTime < Time.time)
            {
                var input = moveInput.ReadValue<float>();

                if (input == 0)
                {
                    // nothing pressed: do nothing
                }
                else
                {
                    Debug.Log("Before change: " + currentCursorPosition);

                    currentCursorPosition += (int)input;

                    currentCursorPosition = currentCursorPosition < menuBottomPosition ? menuBottomPosition : currentCursorPosition;
                    currentCursorPosition = currentCursorPosition > menuTopPosition ? menuTopPosition : currentCursorPosition;

                    Vector3 newPosition = new Vector3(transform.position.x, 0, transform.position.z);

                    switch (currentCursorPosition)
                    {
                        case (int)menuOptions.OnePlayer:
                            newPosition.y = 2.5f;
                            break;
                        case (int)menuOptions.TwoPlayers:
                            newPosition.y = -2;
                            break;
                        case (int)menuOptions.Options:
                            newPosition.y = -6;
                            break;
                        case (int)menuOptions.Credits:
                            newPosition.y = -11;
                            break;
                    }

                    transform.position = newPosition;
                }

                nextMoveTime = Time.time + inputDelay;
            }
        }
    }

    private void choose()
    {
        switch (currentCursorPosition)
        {
            case (int) menuOptions.OnePlayer:
                SceneManager.LoadScene("Level");
                break;
            case (int) menuOptions.TwoPlayers:
                Debug.Log("TwoPlayers");
                break;
            case (int) menuOptions.Options:
                SceneManager.LoadScene("Options");
                break;
            case (int) menuOptions.Credits:
                SceneManager.LoadScene("Credits");
                break;
        }
    }
}