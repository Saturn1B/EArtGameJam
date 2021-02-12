using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    float playerSpeed;
    public float sprintSpeed;
    public float walkSpeed;
    public float jumpPower;

    public float velocityY;
    public Vector3 momentum;
    [SerializeField] private float gravity;
    public CharacterController controller;

    public Transform groundCheck;
    public LayerMask groundMask;
    [SerializeField] private float groundDistance;

    public bool canMove = true;

    public Vector3 linearVelocity;

    //private GeometryTwist message;
    private Vector3 previousPosition = Vector3.zero;
    private Quaternion previousRotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
            Sprint();
            CalculateVelocity();
        }
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(IncreaseSpeed());
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StartCoroutine(DecreaseSpeed());
        }
    }

    /*private void Gravity()
    {
        if (isGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }*/

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x * playerSpeed + transform.forward * z * playerSpeed;

        if (controller.isGrounded)
        {
            velocityY = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocityY = jumpPower;
            }
        }

        float gravityDownForce = -60f;
        velocityY += gravityDownForce * Time.deltaTime;

        move.y = velocityY;

        move += momentum;
        
        controller.Move(move * Time.deltaTime);

        if(momentum.magnitude >= 0)
        {
            float momentumDrag = 3f;
            momentum -= momentum * momentumDrag * Time.deltaTime;
            if(momentum.magnitude < .0f)
            {
                momentum = Vector3.zero;
            }
        }
    }

    IEnumerator IncreaseSpeed()
    {
        while(playerSpeed <= sprintSpeed)
        {
            playerSpeed += 2;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        playerSpeed = sprintSpeed;
    }

    IEnumerator DecreaseSpeed()
    {
        while (playerSpeed >= walkSpeed)
        {
            playerSpeed -= 2;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        playerSpeed = walkSpeed;
    }

    void CalculateVelocity()
    {
        linearVelocity = (transform.position - previousPosition) / Time.deltaTime;
        Vector3 angularVelocity = (transform.rotation.eulerAngles - previousRotation.eulerAngles) / Time.deltaTime;
        //Debug.Log("Vlinear" + linearVelocity.magnitude);
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }
}
