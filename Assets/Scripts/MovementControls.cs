using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControls : MonoBehaviour
{
    public float playerMovementSpeed = 10.0f;
    public float playerTurnSpeed = 100.0f;

    public InputAction input;
    public CharacterController controller;

    private int previousRotation = 0;

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

        ////Debug.Log(finalVector.z); // góra 1, dół -1
        ////Debug.Log(finalVector.x);// prawo 1,  lewo -1

        //// chcę tylko rotacji w osi y (x), ale o określony kąt, który wykryję dzięki zmianie w z

        ////Vector3 rotation = new Vector3(0, inputVector.x * playerTurnSpeed, 0);

        /*
góra: 0
dół: 3.14
lewo: -1.60
prawo: 1.60
         */


        Quaternion q = this.transform.rotation;
        Vector3 v = q.ToEulerAngles();
        //Debug.Log(v.y);
        float currentRotation = v.y;

        // left
        if (finalVector.x == -1)
        {
            var target = new Quaternion(0, -0.7f, 0, 0.7f);

            this.transform.rotation = target;
        }
        // right
        if (finalVector.x == 1)
        {
            var target = new Quaternion(0, 0.7f, 0, 0.7f);

            this.transform.rotation = target;
        }
        // down
        if (finalVector.z == -1)
        {
            var target = new Quaternion(0, 1, 0, 0.7f);

            this.transform.rotation = target;
        }
        // up
        if (finalVector.z == 1)
        {
            var target = new Quaternion(0, 0, 0, 1);

            this.transform.rotation = target;
        }

        ////Debug.Log(previousRotation + ", " + currentRotation);// prawo 1,  lewo -1

        //Debug.Log(this.transform.rotation);

        //previousRotation = currentRotation;

        ////// Rotate the cube by converting the angles into a quaternion.
        ////Quaternion target = Quaternion.Euler(0, finalVector.x * 1000, finalVector.z * 1000);

        ////this.transform.rotation = Quaternion.Slerp(transform.rotation, target, 10000);

        //float rotationStep = rotationSpeed;

        //Vector3 directionToTarget = targetWaypoint.position - transform.position;

        //Vector2 inputVector = controls.ReadValue<Vector2>();
        //Vector3 finalVector = new Vector3();

        //finalVector.x = inputVector.x;
        //finalVector.z = inputVector.y;

        ////controller.Move(finalVector.x * Time.deltaTime * 3.14f);
        //transform.position = Vector3.MoveTowards(transform.position, finalVector.x, Time.deltaTime * 3.14f);
    }
}