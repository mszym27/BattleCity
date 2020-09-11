using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovementControls : MonoBehaviour
{
    public float playerMovementSpeed = 5.0f;
    public InputAction moveInput;
    public CharacterController controller;

    public InputAction shootInput;
    public GameObject bullet;
    public float playerFireRate;

    public InputAction quitInput;

    private float nextFireTime;

    private const float keyPressedValue = 1;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // short delay before allowing to shoot so the player 
        // does not abuse original game mechanics
        nextFireTime = Time.time + playerFireRate;
    }

    private void OnEnable()
    {
        moveInput.Enable();
        shootInput.Enable();
        quitInput.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        shootInput.Disable();
        quitInput.Disable();
    }

    public void Update()
    {
        if (shootInput.ReadValue<float>() == keyPressedValue)
        {
            shoot();
        }
        else if (quitInput.ReadValue<float>() == keyPressedValue)
        {
            SceneManager.LoadScene("MainMenu");
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
            // bullet is created some distance from the tank, so it does not destroy it
            var initialPosition = transform.position + (transform.forward * 0.25f);

            Instantiate(bullet, initialPosition, this.transform.rotation);

            nextFireTime = Time.time + playerFireRate;
        }
    }
}