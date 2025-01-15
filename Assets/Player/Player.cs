using System.Collections;
using UnityEngine;

public class Player: MonoBehaviour
{
    public float speed = 10f;
    public float forwardSpeed = 5f;
    public float turnSpeed = 5f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private Transform m_Transform;
    private Vector3 m_StartPosition;
    private float m_StartSpeed;
    private float m_StartForwardSpeed;

    public Transform Transform => m_Transform;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        m_Transform = transform;
        m_StartPosition = m_Transform.position;
        m_StartSpeed = speed;
        m_StartForwardSpeed = forwardSpeed;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 forwardMovement = m_Transform.forward * forwardSpeed * Time.deltaTime;
        Vector3 horizontalMovement = m_Transform.right * moveHorizontal * speed * Time.deltaTime;

        Vector3 movement = forwardMovement + horizontalMovement;
        controller.Move(movement);

        Vector3 direction = new Vector3(moveHorizontal, 0, 0);

        if (direction.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            m_Transform.rotation = Quaternion.Slerp(m_Transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }

    public void ResetPlayer()
    {
        controller.enabled = false;
        m_Transform.position = m_StartPosition;
        m_Transform.rotation = Quaternion.identity;
        velocity = Vector3.zero;
        speed = m_StartSpeed;
        forwardSpeed = m_StartForwardSpeed;
        controller.enabled = true;
        Debug.Log("Reset is working");
    }

    public void SpeedUp(float speedUp)
    {
        speed += speedUp;
        forwardSpeed += speedUp;
        StartCoroutine(SpeedUpDuration());
    }

    IEnumerator SpeedUpDuration()
    {
        yield return new WaitForSeconds(3f);
        speed = m_StartSpeed;
        forwardSpeed = m_StartForwardSpeed;
    }
}
