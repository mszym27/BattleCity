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

    private int currentResPosition;
    private ScreenMeasurements[] possibleResolutions;

    private int currentCursorPosition;

    private void OnEnable()
    {
        //starting position is always top of the menu
        currentCursorPosition = menuTopPosition;

        var currentResolution = Screen.currentResolution;

        displayCurrResAsText(currentResolution);
        
        possibleResolutions = new ScreenMeasurements[] {
            new ScreenMeasurements(640, 480),
            new ScreenMeasurements(800, 600),
            new ScreenMeasurements(1280, 960)
        };

        for (int i = 0; i < possibleResolutions.Length; i++)
        {
            currentResPosition = i;

            if(possibleResolutions[i].width == currentResolution.width && possibleResolutions[i].height == currentResolution.height)
            {
                break;
            }
        }

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
                    Debug.Log("Was: " + currentCursorPosition);

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

                    Debug.Log("Is: " + currentCursorPosition);
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
                changeResolution();
                break;
        }
    }


    private void changeResolution()
    {
        currentResPosition++;

        // cycles the indes back to the first position on the list
        currentResPosition = currentResPosition % possibleResolutions.Length;

        Debug.Log(currentResPosition);
    }

    private class ScreenMeasurements
    {
        public int width;
        public int height;

        public ScreenMeasurements(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }

    private void displayCurrResAsText (Resolution currentResolution) {
        var tm = (TextMesh)GameObject.Find("CurrentResolution").GetComponent<TextMesh>();
        tm.text = currentResolution.width + " x " + currentResolution.height;
    }
}