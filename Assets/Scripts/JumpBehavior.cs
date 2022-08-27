using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 16;
    [SerializeField, Range(0f, 1f)] float fallSpeed = 1;
    [SerializeField] Vector3 groundCheckOffcet;
    [SerializeField] Vector3 groundCheckSize;
    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] float earlyJump = 0.5f;
    [SerializeField] int jumpCount=1;
    
    private Rigidbody rb;
    private float currentEarlyJump;
    private Vector3 jumpPosTest = Vector3.negativeInfinity;
    private bool _grounded;
    private int currentJumpCounter = 0;
    private bool touchedGround;

    private void Start() => rb = GetComponent<Rigidbody>();

    private void FixedUpdate() => SetGroundCeckFlags();

    private void SetGroundCeckFlags()
    {
        bool currentGroundCheck = IsGrounded();
        if (currentGroundCheck != _grounded) //if ground check status is diffrent
            touchedGround = currentGroundCheck ? true : false;
        _grounded = IsGrounded();
    }

    private void Update()
    {
        if (touchedGround)
        {
            currentJumpCounter = touchedGround ? jumpCount : currentJumpCounter;
            touchedGround = false;
        }

        // since this might glitch we set the counter on 0 y velocity 
        else if (Mathf.Abs(rb.velocity.y) <= 0.01f && currentJumpCounter == 0)
            currentJumpCounter = _grounded ? jumpCount : currentJumpCounter;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentEarlyJump = earlyJump;
            jumpPosTest = transform.position;
        }
        else
            currentEarlyJump -= Time.deltaTime;

        if (currentJumpCounter>0 && currentEarlyJump> 0 )
        {
            currentJumpCounter--;
            currentEarlyJump = 0;
            Jump();
        }
        else if (rb.velocity.y>0 && Input.GetKeyUp(KeyCode.Space))
        {
            Fall();
        }
    }

    private void Fall() => rb.velocity += Vector3.down * rb.velocity.y * fallSpeed;

    private void Jump() => rb.velocity += Vector3.up * jumpSpeed;

    private bool IsGrounded() => Physics.CheckBox(transform.position + groundCheckOffcet, groundCheckSize, Quaternion.identity, groundCheckMask);
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = IsGrounded() ? Color.red : Color.green;
    //    Gizmos.DrawWireCube(transform.position + groundCheckOffcet, groundCheckSize*2);

    //    //Gizmos.color = Color.yellow;
    //    //Gizmos.DrawWireCube(jumpPosTest + groundCheckOffcet, groundCheckSize * 2);
    //}
}
