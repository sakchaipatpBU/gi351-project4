using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;      // ความเร็วพื้นฐาน
    [SerializeField] private float smoothTime = 0.1f;   // ความหน่วงเวลาทำให้ลื่น

    private bool upperWallContact= false;
    private bool lowerWallContact = false;

    private InputAction moveInput;
    private Rigidbody2D rb;

    private Vector2 move;
    private Vector2 currentVelocity; // ใช้สำหรับ SmoothDamp

    // skill action
    public Skill skill;
    private InputAction skillAction;

    // chest
    public Chest chestNear;
    private InputAction interactAction;


    private Player player;

    [Header("Animation")]
    public Animator animator;
    public string anim_walk = "Walk";
    public string anim_walkUp = "Up";
    public string anim_walkDown = "Down";
    public string anim_walkSide = "Side";
    public string anim_walkUpSide = "UpSide";
    public string anim_walkDownSide = "DownSide";
    public SpriteRenderer spriteRenderer;


    private void Awake()
    {
        moveInput = InputSystem.actions.FindAction("Move");
        skillAction = InputSystem.actions.FindAction("Skill");
        interactAction = InputSystem.actions.FindAction("OpenChest");
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        player = this.gameObject.GetComponent<Player>();
    }
    private void OnEnable()
    {
        moveInput.Enable();
    }

    private void OnDisable()
    {
        moveInput.Disable();
        rb.linearVelocity = Vector2.zero;
    }

    private void Update()
    {
        // อ่านค่าจาก Action
        move = moveInput.ReadValue<Vector2>();

        if (move.x > 0 && move.y > 0)
        {
            // UpRight
            spriteRenderer.flipX = false;
            animator.SetBool(anim_walk, true);
            animator.SetBool(anim_walkUp, false);
            animator.SetBool(anim_walkDown, false);
            animator.SetBool(anim_walkSide, false);
            animator.SetBool(anim_walkUpSide, true);
            animator.SetBool(anim_walkDownSide, false);
        }
        else if (move.x < 0 && move.y > 0)
        {
            // UpLeft
            spriteRenderer.flipX = true;
            animator.SetBool(anim_walk, true);
            animator.SetBool(anim_walkUp, false);
            animator.SetBool(anim_walkDown, false);
            animator.SetBool(anim_walkSide, false);
            animator.SetBool(anim_walkUpSide, true);
            animator.SetBool(anim_walkDownSide, false);
        }
        else if (move.x > 0 && move.y < 0)
        {
            // DownRight
            spriteRenderer.flipX = false;
            animator.SetBool(anim_walk, true);
            animator.SetBool(anim_walkUp, false);
            animator.SetBool(anim_walkDown, false);
            animator.SetBool(anim_walkSide, false);
            animator.SetBool(anim_walkUpSide, false);
            animator.SetBool(anim_walkDownSide, true);
        }
        else if (move.x < 0 && move.y < 0)
        {
            // DownLeft
            spriteRenderer.flipX = true;
            animator.SetBool(anim_walk, true);
            animator.SetBool(anim_walkUp, false);
            animator.SetBool(anim_walkDown, false);
            animator.SetBool(anim_walkSide, false);
            animator.SetBool(anim_walkUpSide, false);
            animator.SetBool(anim_walkDownSide, true);
        }
        else if (move.y > 0)
        {
            // Up
            spriteRenderer.flipX = false;
            animator.SetBool(anim_walk, true);
            animator.SetBool(anim_walkUp, true);
            animator.SetBool(anim_walkDown, false);
            animator.SetBool(anim_walkSide, false);
            animator.SetBool(anim_walkUpSide, false);
            animator.SetBool(anim_walkDownSide, false);
        }
        else if (move.y < 0)
        {
            // Down
            spriteRenderer.flipX = false;
            animator.SetBool(anim_walk, true);
            animator.SetBool(anim_walkUp, false);
            animator.SetBool(anim_walkDown, true);
            animator.SetBool(anim_walkSide, false);
            animator.SetBool(anim_walkUpSide, false);
            animator.SetBool(anim_walkDownSide, false);
        }
        else if (move.x > 0)
        {
            // Right
            spriteRenderer.flipX = false;
            animator.SetBool(anim_walk, true);
            animator.SetBool(anim_walkUp, false);
            animator.SetBool(anim_walkDown, false);
            animator.SetBool(anim_walkSide, true);
            animator.SetBool(anim_walkUpSide, false);
            animator.SetBool(anim_walkDownSide, false);
        }
        else if (move.x < 0)
        {
            // Left
            spriteRenderer.flipX = true;
            animator.SetBool(anim_walk, true);
            animator.SetBool(anim_walkUp, false);
            animator.SetBool(anim_walkDown, false);
            animator.SetBool(anim_walkSide, true);
            animator.SetBool(anim_walkUpSide, false);
            animator.SetBool(anim_walkDownSide, false);
        }
        else
        {
            // idle
            animator.SetBool(anim_walk, false);
            animator.SetBool(anim_walkUp, false);
            animator.SetBool(anim_walkDown, false);
            animator.SetBool(anim_walkSide, false);
            animator.SetBool(anim_walkUpSide, false);
            animator.SetBool(anim_walkDownSide, false);
        }
        if (skillAction.triggered)
        {
            skill.UseSkill();
        }
        if (interactAction.triggered)
        {
            Debug.Log("interact");

            if (chestNear != null)
            {
                chestNear.OpenChest();
                Debug.Log("Open from controller");
            }
        }
        if (upperWallContact == true && lowerWallContact == true)
        {
            GameManager.GetInstance().ShowLosePanel();
        }
    }

    private void FixedUpdate()
    {
        // ทำให้ลื่น: Interpolate ทิศทางการเคลื่อนไหว
        Vector2 targetVelocity = move * moveSpeed;
        rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref currentVelocity, smoothTime);
        
        
    }
    /*private void LateUpdate()
    {
        if (move.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(move.x), 1, 1);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            GameManager.GetInstance().ShowWinPanel();
            this.enabled = false;
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            SoundManager.Instance.PlaySFX("PlayerGetHit", 0.2f, 1); // Sound

            player.ReduceHp();
        }
        if (collision.gameObject.CompareTag("Laser"))
        {
            SoundManager.Instance.PlaySFX("PlayerGetHit", 0.2f, 1); // Sound

            player.ReduceHp();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("UpperWall"))
        {
            upperWallContact = true;
        }
        if (collision.gameObject.CompareTag("LowerWall"))
        {
            lowerWallContact = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("UpperWall"))
        {
            upperWallContact = false;
        }
        if (collision.gameObject.CompareTag("LowerWall"))
        {
            lowerWallContact = false;
        }
    }
}
