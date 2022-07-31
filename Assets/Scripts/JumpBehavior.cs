using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : MonoBehaviour
{
    [SerializeField] float jumpSpeed;
    [Range(0,1)]
    [SerializeField] float fallSpeed;
    [SerializeField] Vector3 groundCheckOffcet;
    [SerializeField] Vector3 groundCheckSize;
    [SerializeField] LayerMask groundCheckMask;

    [SerializeField] int jumpCount=2;
    private int currentjumpCount=2;
    [SerializeField] float earlyTime=1;
    private float currentEarlyTime = 0;

    private Rigidbody rb;
    private bool _grounded = true;
    private bool leftGround = false;
    private bool touchedGround = false;
    private bool hasJumped = false;
    private void Start() => rb = GetComponent<Rigidbody>();

    private void FixedUpdate(){
        bool currentGroundCheck = IsGrounded();
        if(_grounded != currentGroundCheck)
        {
            touchedGround =  currentGroundCheck? true:false;
            leftGround = currentGroundCheck? false:true;
        }
        _grounded = currentGroundCheck;


    }
    private void Update()
    {
        if (touchedGround){
            currentjumpCount=jumpCount;
            touchedGround = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            currentEarlyTime = earlyTime;
        else
            currentEarlyTime -= Time.deltaTime;
        
        if (currentjumpCount>0 && currentEarlyTime>0 )
        {
            currentjumpCount--;
            currentEarlyTime=0;
            Jump();
        }
        else if (rb.velocity.y>0 && Input.GetKeyUp(KeyCode.Space))
        {
            EarlyFall();
        }

    }
    private bool IsGrounded() => Physics.CheckBox(transform.position + groundCheckOffcet, groundCheckSize, Quaternion.identity, groundCheckMask);
    private void Jump()=> rb.velocity += Vector3.up * jumpSpeed;
    private void EarlyFall() => rb.velocity += Vector3.down*rb.velocity.y * fallSpeed;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded() ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position + groundCheckOffcet, groundCheckSize*2);
    }
}
