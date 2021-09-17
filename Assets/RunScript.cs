using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class RunScript : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    [SerializeField]
    private GameObject ball;
    private DistanceGrabbable m_grabbable;
    private bool isGrabbed;
    private float runSpeed;
    private Vector3 ballPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //characterController = GetComponentInParent<CharacterController>();
        m_grabbable = ball.GetComponent<DistanceGrabbable>();
        runSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ballPosition = new Vector3(ball.transform.position.x, 0f, ball.transform.position.z);
        transform.LookAt(ballPosition);
        transform.position = Vector3.MoveTowards(transform.position, ballPosition, runSpeed);
        if (!m_grabbable.isGrabbed)
        {
            if (isGrabbed)
            {
                Debug.Log("Throw ball!");
                animator.SetBool("BallThrown", true);
                animator.SetBool("BallIdle", false);
                animator.SetBool("BallCatch", false);
                runSpeed = 0.02f;
                isGrabbed = false;
                return;
            }
            return;
        }
        if (!isGrabbed)
        {
            animator.SetBool("BallIdle", true);
            isGrabbed = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball")) {
            animator.SetBool("BallThrown", false);
            animator.SetBool("BallCatch", true);
            runSpeed = 0;
        }
    }
}
