using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OptionsControls : MonoBehaviour
{
    public InputAction moveInput;
    public InputAction chooseInput;
    public InputAction quitInput;
    public float inputDelay = 0.1f;

    private int currentSoundVolume;
    private int currentSoundDirection = -1;

    private const float keyPressedValue = 1;
    private float nextInputTime = 0;
    private const float up = 1;
    private const float down = -1;

    private enum menuOptions
    {
        Sound = 2,
        Resolution = 1
    }

    //private GameObject gameSettings;

    private int menuTopPosition = (int)menuOptions.Sound;
    private int menuBottomPosition = (int)menuOptions.Resolution;

    private int currentResPosition;
    private ScreenMeasurements[] possibleResolutions;
    private GameObject[] bands;

    private int currentCursorPosition;

    private void OnEnable()
    {
        ////get object that is used to pass setting values between the scenes
        //gameSettings = GameObject.Find("GameSettings");

        ////...And prevent it from destruction
        //DontDestroyOnLoad(gameSettings);

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

        bands = new GameObject[5];

        for (int i = 1; i <= 5; i++)
        {
            bands[i - 1] = GameObject.Find("Band" + i);
        }

        currentSoundVolume = PlayerPrefs.GetInt("soundVolume", 5);

        currentSoundVolume = currentSoundVolume > 5 ? 5 : currentSoundVolume;

        showSound(currentSoundVolume);

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
        if (nextInputTime < Time.time)
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
                            newPosition.x = -27.5f;
                            newPosition.y = 8.5f;
                            break;
                        case (int)menuOptions.Resolution:
                            newPosition.x = -33.5f;
                            newPosition.y = -0.8f;
                            break;
                    }

                    transform.position = newPosition;
                }
            }

            nextInputTime = Time.time + inputDelay;
        }
    }

    private void choose()
    {
        switch (currentCursorPosition)
        {
            case (int)menuOptions.Sound:
                changeSound();
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

        Screen.SetResolution(possibleResolutions[currentResPosition].width, possibleResolutions[currentResPosition].height, true);

        var currentResolution = Screen.currentResolution;

        displayCurrResAsText(Screen.currentResolution);
    }


    private void changeSound()
    {
        // bounces the sound in the alternate direction on hitting the border
        currentSoundDirection = currentSoundVolume == 0 ? 1 : currentSoundDirection;
        currentSoundDirection = currentSoundVolume == 5 ? -1 : currentSoundDirection;

        currentSoundVolume = currentSoundVolume + currentSoundDirection;

        showSound(currentSoundVolume);

        PlayerPrefs.SetInt("soundVolume", currentSoundVolume == 0 ? currentSoundVolume : currentSoundVolume + 1);
        //PlayerPrefs.SetInt("soundVolume", currentSoundVolume);
    }

    private void showSound(int currentSoundVolume)
    {
        Debug.Log(currentSoundVolume);

        for (int i = 1; i <= 5; i++)
        {
            bands[i - 1].SetActive(false);
        }

        for (int i = 1; i <= currentSoundVolume; i++)
        {
            bands[i - 1].SetActive(true);
        }
    }

    private void displayCurrResAsText(Resolution currentResolution)
    {
        var tm = (TextMesh)GameObject.Find("CurrentResolution").GetComponent<TextMesh>();
        tm.text = currentResolution.width + " x " + currentResolution.height;
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
}