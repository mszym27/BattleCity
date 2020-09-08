using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OptionsControls : MonoBehaviour
{
    public InputAction moveInput;
    public InputAction chooseInput;
    public InputAction quitInput;
    public float inputDelay = 0.1f;

    private int currentVolume = 5;

    private const float keyPressedValue = 1;
    private float nextMoveTime = 0;
    private const float up = 1;
    private const float down = -1;

    private enum menuOptions
    {
        Sound = 2,
        Resolution = 1
    }

    private int menuTopPosition = (int)menuOptions.Sound;
    private int menuBottomPosition = (int)menuOptions.Resolution;

    private int currentCursorPosition;

    private void OnEnable()
    {
        //starting position is always top of the menu
        currentCursorPosition = menuTopPosition;

        var tm = (TextMesh)GameObject.Find("CurrentResolution").GetComponent<TextMesh>();
        tm.text = Screen.currentResolution.ToString();

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
            SceneManager.LoadScene("MainMenu");
        }
        else if (chooseInput.ReadValue<float>() == keyPressedValue)
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
                    currentCursorPosition += (int)input;

                    currentCursorPosition = currentCursorPosition < menuBottomPosition ? menuBottomPosition : currentCursorPosition;
                    currentCursorPosition = currentCursorPosition > menuTopPosition ? menuTopPosition : currentCursorPosition;

                    Vector3 newPosition = new Vector3(transform.position.x, 0, transform.position.z);

                    switch (currentCursorPosition)
                    {
                        case (int)menuOptions.Sound:
                            newPosition.y = 2.5f;
                            break;
                        case (int)menuOptions.Resolution:
                            newPosition.y = -2;
                            break;
                    }

                    transform.position = newPosition;

                    Debug.Log(currentCursorPosition);
                }

                nextMoveTime = Time.time + inputDelay;
            }
        }
    }

    private void choose()
    {
        switch (currentCursorPosition)
        {
            case (int)menuOptions.Sound:
                Debug.Log("Sound");
                break;
            case (int)menuOptions.Resolution:
                Debug.Log("Resolution");
                changeResolution();
                break;
        }
    }


    private void changeResolution()
    {
        //Screen.SetResolution(640, 480, true);
        //Screen.SetResolution(800, 600, true);
        //Screen.SetResolution(1280, 960, true);
    }
}