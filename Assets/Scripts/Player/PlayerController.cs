using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 4f;
    public Camera followCamera;
    public float jumpHeight = 1f;
    public float pushForce = 2;
    public float stepHeight = 0.9f;
    public float stepSmooth = 0.15f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;
    public bool canMove = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(!canMove)
            return;
        Movement();
    }

    private void Movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -0.5f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;
        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }

        StepClimb(movementDirection);
        controller.Move(movementDirection * playerSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
        
   void StepClimb(Vector3 moveDir)
{
    if (moveDir == Vector3.zero) return;

    Vector3 origin = transform.position + Vector3.up * 0.05f;
    Vector3[] directions = new Vector3[]
    {
        moveDir,
        Quaternion.Euler(0, 30, 0) * moveDir,
        Quaternion.Euler(0, -30, 0) * moveDir
    };

    foreach (Vector3 dir in directions)
    {
        if (Physics.Raycast(origin, dir, out RaycastHit hitLow, 0.5f))
        {
            Vector3 stepCheckOrigin = transform.position + Vector3.up * stepHeight;

            if (!Physics.Raycast(stepCheckOrigin, dir, 0.5f))
            {
                controller.Move(Vector3.up * stepSmooth);
                return;
            }
        }
    }
}
}
