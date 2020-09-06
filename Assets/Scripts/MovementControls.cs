using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControls : MonoBehaviour
{
    public float playerMovementSpeed = 10.0f;
    public InputAction moveInput;
    public CharacterController controller;

    public InputAction shootInput;
    public GameObject bullet;
    public float playerFireRate = 0.5f;

    private float nextFireTime = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveInput.Enable();
        shootInput.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        shootInput.Disable();
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
        if(shootInput.ReadValue<float>() == 1)
        {
            shoot();
        }
        else
        {
            Vector2 inputVector = moveInput.ReadValue<Vector2>();
            Vector3 finalVector = new Vector3();

            finalVector.x = inputVector.x;
            finalVector.z = inputVector.y;

            controller.Move(finalVector * Time.deltaTime * playerMovementSpeed);

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
                var target = new Quaternion(0, -0.7f, 0, 0);

                this.transform.rotation = target;
            }
            // up
            if (finalVector.z == 1)
            {
                var target = new Quaternion(0, 0, 0, 1);

                this.transform.rotation = target;
            }
        }
    }

    private void shoot()
    {
        if (nextFireTime < Time.time)
        {
            Instantiate(bullet, transform.position, this.transform.rotation);

            nextFireTime = Time.time + playerFireRate;
        }
    }
}