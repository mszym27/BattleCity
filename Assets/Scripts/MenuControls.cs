using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControls : MonoBehaviour
{
    public InputAction moveInput;
    public InputAction chooseInput;
    public float inputDelay = 0.1f;

    private float nextMoveTime = 0;
    private const float spaceKeyValue = 1;
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
    
    //public CharacterController controller;

    private void OnEnable()
    {
        moveInput.Enable();
        chooseInput.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        chooseInput.Disable();
    }

    //void Update()
    //{
    //    //Movement();
    //    //OnMove();
    //    Quit();
    //}

    //void Quit()
    //{
    //    if (Keyboard.current.escapeKey.isPressed)
    //    {
    //        Application.Quit();
    //    }
    //}

    void Create()
    {
        //starting position is always top of the menu
        currentCursorPosition = menuTopPosition;
    }

    public void Update()
    {
        if (chooseInput.ReadValue<float>() == spaceKeyValue)
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

                    Debug.Log(currentCursorPosition);
                }

                nextMoveTime = Time.time + inputDelay;
            }
            //Vector3 finalVector = new Vector3();

            //finalVector.x = inputVector.x;
            //finalVector.z = inputVector.y;

            //controller.Move(finalVector * Time.deltaTime * playerMovementSpeed);

            //// left
            //if (finalVector.x == -1)
            //{
            //    var target = new Quaternion(0, -0.7f, 0, 0.7f);

            //    this.transform.rotation = target;
            //}
            //// right
            //if (finalVector.x == 1)
            //{
            //    var target = new Quaternion(0, 0.7f, 0, 0.7f);

            //    this.transform.rotation = target;
            //}
            // down
            ////if (finalVector.z == -1)
            ////{
            ////    var target = new Quaternion(0, -0.7f, 0, 0);

            ////    this.transform.rotation = target;
            ////}
            ////// up
            ////if (finalVector.z == 1)
            ////{
            ////    var target = new Quaternion(0, 0, 0, 1);

            ////    this.transform.rotation = target;
            ////}
        }
    }

    private void choose()
    {
        switch (currentCursorPosition)
        {
            case (int) menuOptions.OnePlayer:
                Debug.Log("OnePlayer");
                break;
            case (int) menuOptions.TwoPlayers:
                Debug.Log("TwoPlayers");
                break;
            case (int) menuOptions.Options:
                Debug.Log("Options");
                break;
            case (int) menuOptions.Credits:
                Debug.Log("Credits");
                break;
            //default:
            //    Debug.Log("Choose!");
            //    break;
        }
    }
}