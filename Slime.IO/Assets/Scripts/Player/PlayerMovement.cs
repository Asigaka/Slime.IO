using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Joystick moveJoys;
    [SerializeField] private Rigidbody rb;

    [Header("Gravity")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private LayerMask groundMask;

    [Space]
    [SerializeField] private float speed = 6;
    [SerializeField] private float jumpForce = 15;

    private float turnSmoothVelocity;
    private Vector3 moveDir;
    private Vector3 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveJoys = UIMatch.Instance.PlayerJoystick;
    }

    private void Update()
    {
        GroundCheck();

        Move();
    }

    private void Move()
    {
#if UNITY_EDITOR 
        //float moveHor = Input.GetAxisRaw("Horizontal");
        //float moveVer = Input.GetAxisRaw("Vertical");
#else
        //float moveHor = moveJoys.Horizontal;
        //float moveVer = moveJoys.Vertical;
#endif
        float moveHor = moveJoys.Horizontal;
        float moveVer = moveJoys.Vertical;

        moveDir = new Vector3(moveHor, 0, moveVer) * speed;

        if (GroundCheck() && moveDir.magnitude >= 0.1f)
        {
            float targetAngle;
            targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.2f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
        }

        if (GroundCheck() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void RotateToTarget(Transform target)
    {
        transform.LookAt(target);
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public bool isRunning()
    {
        return moveDir.magnitude >= 0.1f;
    }
}
