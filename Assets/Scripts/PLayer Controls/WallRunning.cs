using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("wall running")]
    public LayerMask isWall;
    public LayerMask isGround;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    private float horizontalInput;
    private float verticalInput;

    [Header("detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("Exit wall run")]
    private bool leavingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("references")]
    public Transform orientation;
    private PlayerMovement playerMovement;
    private Rigidbody playerRB;
    public PlayerCamera playerCamera;

    [Header("misc")]
    Camera cam;
    private float baseFieldOfView;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        cam = Camera.main;
    }

    private void Start()
    {
        baseFieldOfView = cam.fieldOfView;
        Debug.Log(baseFieldOfView);
    }

    private void Update()
    {
        WallCheck();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if (playerMovement.isWallrunning)
        {
            WallRunningMovement();
        }
    }

    private void WallCheck()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, isWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, isWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, isGround);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //state 1 - wall running
        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !leavingWall)
        {
            if (!playerMovement.isWallrunning)
            {
                StartWallRun();
            }

            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }

        //state 2 - exiting

        else if (leavingWall)
        {
            if (playerMovement.isWallrunning)
            {
                StopWallRunning();
            }

            if (exitWallTimer > 0)
            {
                exitWallTimer -= Time.deltaTime;
            }

            if (exitWallTimer <= 0)
            {
                leavingWall = false;
            }
        }

        //state 3 - not
        else
        {
            if (playerMovement.isWallrunning)
            {
                StopWallRunning();
            }
        }
    }

    //kinda like slide here, wall run start, move and stop go here

    private void StartWallRun()
    {
        playerMovement.isWallrunning = true;
        playerRB.linearVelocity = new Vector3(playerRB.linearVelocity.x, 0, playerRB.linearVelocity.z);

        //apply camera affects
        playerCamera.SetFOV(baseFieldOfView + 5f);
        if (wallLeft)
        {
            playerCamera.cameraTilt(-10f);
        }
        else if (wallRight)
        {
            playerCamera.cameraTilt(10f);
        }
    }

    private void WallRunningMovement()
    {
        playerRB.useGravity = useGravity;


        //calculates the walls direction
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        //figure out the direction the player is facing
        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        //applies the movement
        playerRB.AddForce(wallForward * wallRunForce, ForceMode.Force);

        //push player into the wall
        if(!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            playerRB.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        if (useGravity)
        {
            playerRB.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        }
    }

    private void StopWallRunning()
    {
        playerMovement.isWallrunning = false;

        //apply camera affects
        playerCamera.SetFOV(baseFieldOfView);
        playerCamera.cameraTilt(0f);

    }

    private void WallJump()
    {
        //leave the wall so you don't stick too it
        leavingWall = true;
        exitWallTimer = exitWallTime;

        //calculate wall jump force
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        //reset Y velocity and apply wall jump
        playerRB.linearVelocity = new Vector3(playerRB.linearVelocity.x, 0, playerRB.linearVelocity.z);
        playerRB.AddForce(forceToApply, ForceMode.Impulse);
    }
}
