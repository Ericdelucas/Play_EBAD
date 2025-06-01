using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


public class movimentoPlay1 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed = 100.0f;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveInput = (transform.right * x + transform.forward * z).normalized;

        // Rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }
    void FixedUpdate()
    {
        // Apply horizontal movement
        Vector3 velocity = moveInput * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        if (!isGrounded)
        {
            velocity.y -= gravity * Time.fixedDeltaTime;
        }

        rb.linearVelocity = velocity;
    }

}
