using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1;
    public float laneDistance = 2.5f;

    public int distanceunit = 0;
    public TextMeshProUGUI distancetravelled;

    public float jumpForce;
    public float Gravity = -20;
    public GameObject PauseButton;

    public Animator animator;
    private bool isSliding = false;
    public TextMeshProUGUI Highscore;
    public bool isrunning=true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Highscore.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0).ToString();
        InvokeRepeating("Distances", 0, (5 / (forwardSpeed)));
    }

    void Update()
    {
        if (!playerManager.isGameStarted||!isrunning)
            return;
        
        
            //Increase Speed 
            if (forwardSpeed < maxSpeed)
                forwardSpeed += 0.3f * Time.deltaTime;


            animator.SetBool("isGameStarted", true);
            direction.z = forwardSpeed;


            if (controller.isGrounded)
            {
                animator.SetBool("jump", false);
                direction.y -= Gravity * Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.UpArrow) || SwipeManager.swipeUp)

                {


                    direction.y = jumpForce;

                }

                else if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding || SwipeManager.swipeDown)
                {
                    StartCoroutine(Slide());

                    FindObjectOfType<AudioManager>().PlaySound("Slide");
                }


            }
            else
            {
                animator.SetBool("jump", true);

                if (SwipeManager.swipeDown || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    direction.y = -jumpForce;
                }
                direction.y = direction.y + (Physics.gravity.y * Gravity);
            }

            //Gather the inputs on which lane we should be
            if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeManager.swipeRight)
            {
                desiredLane++;
                if (desiredLane == 3)
                    desiredLane = 2;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeManager.swipeLeft)
            {
                desiredLane--;
                if (desiredLane == -1)
                    desiredLane = 0;
            }

            //Calculate where we should be in the future
            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
            if (desiredLane == 0)
                targetPosition += Vector3.left * laneDistance;
            else if (desiredLane == 2)
                targetPosition += Vector3.right * laneDistance;


            //transform.position = targetPosition;
            if (transform.position != targetPosition)
            {
                Vector3 diff = targetPosition - transform.position;
                Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
                if (moveDir.sqrMagnitude < diff.magnitude)
                    controller.Move(moveDir);
                else
                    controller.Move(diff);
            }

            //Move Player
            controller.Move(direction * Time.deltaTime);
        }

    

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            isrunning = false;
            new WaitForSeconds(5f);
            playerManager.gameOver = true;
            PauseButton.SetActive(false);
            
             FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        animator.SetBool("sliding", true);
        controller.height /= 2;

        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
        yield return new WaitForSeconds(0.7f);


        animator.SetBool("sliding", false);

        controller.height *= 2;
        
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);

    }
    public void Distances()
    {
        if (!playerManager.isGameStarted)
            return;
        distanceunit = distanceunit + 1;
        distancetravelled.text = "score:  " + distanceunit.ToString();
        if (distanceunit > PlayerPrefs.GetInt("highscore", 0))
        {


            PlayerPrefs.SetInt("highscore", distanceunit);

            FindObjectOfType<AudioManager>().PlaySound("Applause");

            Highscore.text = "Highscore: " + distanceunit.ToString();
        }

    }
}
