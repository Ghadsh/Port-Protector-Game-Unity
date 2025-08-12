using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 700f;

    private CharacterController controller;
    private Animator animator;

    private int isWalkingHash;
    private int isRunningHash;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Update()
    {
        // Input axes (WASD + Arrow keys)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Is any movement key pressed?
        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // Determine current speed
        float currentSpeed = runPressed ? runSpeed : walkSpeed;

        // Movement direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.SimpleMove(move * currentSpeed);

        // Rotation (mouse only)
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        // Animation logic
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        // Walking state
        if (!isWalking && isMoving)
            animator.SetBool(isWalkingHash, true);
        if (isWalking && !isMoving)
            animator.SetBool(isWalkingHash, false);

        // Running state
        if (!isRunning && isMoving && runPressed)
            animator.SetBool(isRunningHash, true);
        if (isRunning && (!isMoving || !runPressed))
            animator.SetBool(isRunningHash, false);
    }


}
