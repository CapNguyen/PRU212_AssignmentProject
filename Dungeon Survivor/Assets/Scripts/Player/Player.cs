using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] private float speed;
    [HideInInspector]

    public Vector2 movement;
    public float lastHorizontalMove;
    public float lastVerticalMove;
    private float xInput;

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();
    }
    private void Start()
    {
        lastHorizontalMove = -1f;
        lastVerticalMove = 1f;
    }

    void Update()
    {
        AnimatorController();
        MovementController();
    }

    private void AnimatorController()
    {
        anim.SetFloat("xVelocity", xInput);
    }

    private void MovementController()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        movement.x = xInput;
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x != 0)
        {
            lastHorizontalMove = movement.x;
        }
        if(movement.y != 0)
        {
            lastVerticalMove = movement.y;
        }

        rb.velocity = movement * speed;
    }
}
