using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioSource aspirationSound; // Reference to the AudioSource component for movement sound
    [SerializeField] private float speed = 10f;

    private Vector2 moveInput;
    private Rigidbody rb;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there's movement input and play or stop the movement sound accordingly
        if (moveInput.magnitude > 0 && !aspirationSound.isPlaying && !isMoving)
        {
            aspirationSound.Play();
            isMoving = true;
        }
        else if (moveInput.magnitude == 0 && aspirationSound.isPlaying && isMoving)
        {
            aspirationSound.Stop();
            isMoving = false;
        }

        Go();
    }

    void Go()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y * speed);
        rb.velocity = transform.TransformDirection(playerVelocity);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
