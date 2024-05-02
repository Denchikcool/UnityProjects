using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1; 
    public float laneDistance = 4;

    public bool isGrounded;
    [SerializeField] private LayerMask ground;
    public Transform groundCheck;
    private bool isSliding = false;


    public float jumpForce;
    public float gravity = -20;

    public Animator animator;



    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {

        if (!PlayerManager.isGameStarted)
        {
            return;
        }
        animator.SetBool("isGameStarted", true);

        controller.Move(direction * Time.deltaTime);

        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }

        if (forwardSpeed > 20)
        {
            animator.SetBool("isFaster", true);
            animator.SetBool("isGameStarted", false);
        }
        else
        {
            animator.SetBool("isFaster", false);
            animator.SetBool("isGameStarted", true);
        }



        direction.z = forwardSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, ground);
        Debug.Log(isGrounded);
        animator.SetBool("isGrounded", isGrounded);


        if (isGrounded)
        {
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {    
            direction.y += gravity * Time.deltaTime;
        }

        if (SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }


        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
    
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        if(transform.position == targetPosition)
        {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }

      


    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        gravity = -60;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(0.8f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;

        animator.SetBool("isSliding", false);
        isSliding = false;
        gravity = -20;
    }
}
