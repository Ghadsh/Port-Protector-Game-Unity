using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControllerFull : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 700f;
    private CharacterController controller;

    [Header("Animation & Combat")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject swordOnShoulder;
    private int isWalkingHash;
    private int isRunningHash;

    public bool isEquipping;
    public bool isEquipped;
    public bool isBlocking;
    public bool isAttacking;
    private float timeSinceAttack;
    public int currentAttack = 0;

    public int maxHealth = 100;
    public int currentHealth;
    private bool isDead = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    private void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleCombat();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = runPressed ? runSpeed : walkSpeed;
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.SimpleMove(move * currentSpeed);

        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        // Animation sync
        animator.SetBool(isWalkingHash, isMoving);
        animator.SetBool(isRunningHash, isMoving && runPressed);
    }

    private void HandleCombat()
    {
        animator.SetBool("Grounded", true);
        timeSinceAttack += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && !isEquipping)
        {
            Equip();
        }

        Attack();
        Block();

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        animator.SetFloat("MoveSpeed", new Vector2(moveX, moveZ).magnitude);
    }

    private void Equip()
    {
        isEquipping = true;
        animator.SetTrigger("Equip");
    }

    public void ActiveWeapon()
    {
        if (!isEquipped)
        {
            sword.SetActive(true);
            swordOnShoulder.SetActive(false);
            isEquipped = true;
        }
        else
        {
            sword.SetActive(false);
            swordOnShoulder.SetActive(true);
            isEquipped = false;
        }
    }

    public void Equipped()
    {
        isEquipping = false;
    }

    private void Block()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("Block", true);
            isBlocking = true;
        }
        else
        {
            animator.SetBool("Block", false);
            isBlocking = false;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && animator.GetBool("Grounded") && timeSinceAttack > 0.8f)
        {
            if (!isEquipped) return;

            currentAttack++;
            isAttacking = true;
            if (currentAttack > 2) currentAttack = 1;
            if (timeSinceAttack > 1.0f) currentAttack = 1;

            animator.SetTrigger("Attack" + currentAttack);
            timeSinceAttack = 0;
        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            PlayInjured();
        }
    }
    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");

        
        Invoke(nameof(LoadGameOver), 2f);

        
        this.enabled = false;
    }

    private void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver"); 
    }

    public void PlayInjured()
    {
        animator.SetTrigger("Injured");
    }

}
