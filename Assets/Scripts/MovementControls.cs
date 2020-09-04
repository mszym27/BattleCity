using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControls : MonoBehaviour, Controls.IPlayerTankActions
{
    public float playerMovementSpeed = 10.0f;

    Controls controls;
    public CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        controls = new Controls();
        controls.PlayerTank.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.PlayerTank.Enable();
    }

    private void OnDisable()
    {
        controls.PlayerTank.Disable();
    }

    void Update()
    {
        //Movement();
        //OnMove();
        Quit();
    }

    void Quit()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }

    //void Update()
    //{
    //    float movementStep = movementSpeed * Time.deltaTime;
    //    float rotationStep = rotationSpeed;

    //    Vector3 directionToTarget = targetWaypoint.position - transform.position;
    //    Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

    //    transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

    //    float distance = Vector3.Distance(transform.position, targetWaypoint.position);
    //    CheckDistanceToWaypoint(distance);

    //    transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log(context.control);
        //Debug.Log(context.ReadValue<Vector2>().ToString());

        Vector2 inputVector = context.ReadValue<Vector2>();
        Vector3 finalVector = new Vector3();

        finalVector.x = inputVector.x;
        finalVector.z = inputVector.y;

        controller.Move(finalVector * Time.deltaTime * playerMovementSpeed);

        //Vector2 inputVector = controls.ReadValue<Vector2>();
        //Vector3 finalVector = new Vector3();

        //finalVector.x = inputVector.x;
        //finalVector.z = inputVector.y;

        ////controller.Move(finalVector.x * Time.deltaTime * 3.14f);
        //transform.position = Vector3.MoveTowards(transform.position, finalVector.x, Time.deltaTime * 3.14f);
    }
}