using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] private float speed;
    [HideInInspector]
    public Vector2 movement;
    [HideInInspector]
    public float lastHorizontalDeCoupledVector;
    [HideInInspector]
    public float lastVerticalDeCoupledVector; 
    [HideInInspector]
    public float lastHorizontalCoupledVector;
    [HideInInspector]
    public float lastVerticalCoupledVector;
    private float xInput;

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();
    }
    private void Start()
    {
        lastHorizontalDeCoupledVector = -1f;
        lastVerticalDeCoupledVector = 1f;

        lastHorizontalCoupledVector = -1f;
        lastVerticalCoupledVector = 1f;
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
        if (movement.x != 0 || movement.y != 0)
        {
            lastHorizontalCoupledVector = movement.x;
            lastVerticalCoupledVector = movement.y;
        }

        if (movement.x != 0)
        {
            lastHorizontalDeCoupledVector = movement.x;
        }
        if (movement.y != 0)
        {
            lastVerticalDeCoupledVector = movement.y;
        }

        rb.velocity = movement * speed;
    }
}
