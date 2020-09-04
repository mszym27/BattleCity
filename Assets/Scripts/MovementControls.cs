using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControls : MonoBehaviour
{
    public float playerMovementSpeed = 10.0f;
    public float playerTurnSpeed = 100.0f;

    public InputAction input;
    public CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
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

    public void Update()
    {
        //Debug.Log(context.control);
        //Debug.Log(context.ReadValue<Vector2>().ToString());

        Vector2 inputVector = input.ReadValue<Vector2>();
        Vector3 finalVector = new Vector3();

        finalVector.x = inputVector.x;
        finalVector.z = inputVector.y;

        controller.Move(finalVector * Time.deltaTime * playerMovementSpeed);
        //this.transform.Rotate(finalVector * 100);

        Vector3 rotation = new Vector3(finalVector.z * playerTurnSpeed, inputVector.x * playerTurnSpeed, 0);

        this.transform.Rotate(rotation);


        //Vector2 inputVector = controls.ReadValue<Vector2>();
        //Vector3 finalVector = new Vector3();

        //finalVector.x = inputVector.x;
        //finalVector.z = inputVector.y;

        ////controller.Move(finalVector.x * Time.deltaTime * 3.14f);
        //transform.position = Vector3.MoveTowards(transform.position, finalVector.x, Time.deltaTime * 3.14f);
    }
}