using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("External references")]
    public Transform orientation;
    public Transform playerCamera;
    public PlayerCamera zoomCamera;
    private Rigidbody playerRB;
    private PlayerMovement playerMovement;

    [Header("Dashing variables")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;
    public float dashCoolDown;
    private float dashCoolDownTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    [Header("misc")]
    Camera cam;
    private float baseFieldOfView;

    private void Awake()
    {
        cam = Camera.main;
    }
    void Start()
    {
        baseFieldOfView = cam.fieldOfView;
        playerRB = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }

        if (dashCoolDownTimer > 0)
        {
            dashCoolDownTimer -= Time.deltaTime;
        }
    }

    private void Dash()
    {
        if (dashCoolDownTimer > 0) return;
        else dashCoolDownTimer = dashCoolDown;
        playerMovement.isDashing = true;

        zoomCamera.SetFOV(baseFieldOfView + 20f);

        Transform forwardTransform;

        forwardTransform = orientation;

        //calculating the force to apply to the player and in what direction
        Vector3 direction = GetDirection(forwardTransform);

        Vector3 appliedForce = direction * dashForce + orientation.up * dashUpwardForce;

        playerRB.useGravity = false;

        delayedAppliedForce = appliedForce;
        Invoke(nameof(DelayedDash), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedAppliedForce;

    private void DelayedDash()
    {
        playerRB.linearVelocity = Vector3.zero;
        playerRB.AddForce(delayedAppliedForce, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        playerMovement.isDashing = false;

        zoomCamera.SetFOV(baseFieldOfView);

        playerRB.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardTransform)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        direction = forwardTransform.forward * verticalInput + forwardTransform.right * horizontalInput;

        if (verticalInput == 0 && horizontalInput == 0)
        {
            direction = forwardTransform.forward;
        }

        return direction.normalized;
    }
}
