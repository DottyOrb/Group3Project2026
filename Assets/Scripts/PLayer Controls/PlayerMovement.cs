using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //handles all the player movement stuff

    //being lazy and making everything public for now so I can edit values in the editor, headers to make it more readable in editor

    [Header("Movement variables")]
    public float moveSpeed;
    public float groundDrag;
    public float maxVelocity;

    [Header("Jumping Variables")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float airDrag;
    bool canJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground check Variables")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool isGrounded;

    [Header("Slope handling")]
    public float maxSlopeAngle;
    public RaycastHit slopeHit;

    [Header("wall running")]
    public bool isWallrunning;
    public float wallRunSpeed;

    [Header("Dashing")]
    public bool isDashing;

    [Header("other")]
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody playerRB;
    HP playerHP;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.freezeRotation = true;
        canJump = true;
        playerHP = GetComponent<HP>();
    }

    private void Update()
    {
        //check if the player is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        //check players input every frame
        PlayerInput();
        SpeedCheck();

        //handle drag
        if (isGrounded == true && !isDashing)
        {
            playerRB.linearDamping = groundDrag;
        }
        else
        {
            playerRB.linearDamping = airDrag;
        }
    }

    private void FixedUpdate()
    {
        //move the player 60 times a second (this stops movement being linked to frame rate)
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when the player can jump
        if(Input.GetKey(jumpKey) && canJump && isGrounded)
        {
            canJump = false;
            Jump();

            //lets you hold down space to Bhop
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //Move direction math

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (isGrounded == true)
        {
            playerRB.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (isGrounded == false)
        {
            playerRB.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        //changes angle of force to make slop movement feel smoother
        if (OnSlope())
        {
            playerRB.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);
            playerRB.AddForce(Vector3.down * 80f, ForceMode.Force);
            
        }

        if (!isWallrunning)
        {
            //stops the player sliding down on slopes
            playerRB.useGravity = !OnSlope();
        }
       
        if (isWallrunning == true)
        {
            wallRunSpeed = moveSpeed;
        }
    }

    //checks if the player is on a slope
    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle !=0;
        }
        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }


    private void SpeedCheck()
    {
        //calculates players velocity can use this to scale healing later
        Vector3 flatvel = new Vector3(playerRB.linearVelocity.x, 0f, playerRB.linearVelocity.z);

        float speed = flatvel.magnitude;

        if (speed > 3f && playerHP.CanHeal())
        {
            float healAmount = speed / 10 * Time.deltaTime;

            playerHP.Heal(healAmount);
        }
    }


    private void Jump()
    {
        //reset vertical velocity when jump is called
        playerRB.linearVelocity = new Vector3(playerRB.linearVelocity.x, 0f, playerRB.linearVelocity.z);

        playerRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }
}
