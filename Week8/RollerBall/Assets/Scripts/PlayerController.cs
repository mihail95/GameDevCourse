using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float maxVelocity = 250f;
    [SerializeField] Camera mainCam;
    [SerializeField] ParticleSystem ps;
    ParticleSystem.MainModule ps_main;
    RollerBall playerControls;

    Rigidbody rb;
    Collider col;
    Animator anim;

    Vector2 moveVector;
    Vector2 lookVector;
    float flyDirection;
    Vector3 torqueVector;
    Vector3 forceVector;
    bool isGod = false;
    bool isTouchingGround = true;
    bool hasDoubleJump = true;

    private void Awake()
    {
        ps_main = ps.main;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        playerControls = new RollerBall();
    }
    private void OnEnable()
    {
        playerControls.Player.GodMode.performed += _ => ToggleGodMode();
        playerControls.Player.Jump.performed += _ => Jump();

        playerControls.Enable();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude >= (maxVelocity*maxVelocity))
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }

        moveVector = playerControls.Player.Move.ReadValue<Vector2>();
        
        torqueVector = new Vector3(moveVector.y, 0, -moveVector.x);
        rb.AddTorque(torqueVector * speed);
     
        if(!isTouchingGround)
        {
            forceVector = new Vector3(moveVector.x, 0, moveVector.y);
            rb.AddForce(2 * speed * forceVector);
        }

        if (!new Vector3(rb.velocity.x, 0, rb.velocity.z).Equals(Vector3.zero) && isTouchingGround)
        {
            ps.gameObject.transform.SetPositionAndRotation(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f, gameObject.transform.position.z), Quaternion.LookRotation(-rb.velocity.normalized));
            float particleSize = rb.velocity.sqrMagnitude / (maxVelocity*maxVelocity);
            ps_main.startSize = particleSize;
            ps.gameObject.SetActive(true);
        }
        else { ps.gameObject.SetActive(false); }


        if (isGod)
        {
            flyDirection = playerControls.Player.Fly.ReadValue<float>();
            gameObject.transform.position += 3 * speed * Time.deltaTime * new Vector3(moveVector.x, flyDirection/2, moveVector.y);
        }

        mainCam.transform.position = new Vector3(gameObject.transform.position.x, 3f, gameObject.transform.position.z - 6);

        if (gameObject.transform.position.y <= -10 && !isGod) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    }
    private void ToggleGodMode()
    {
        isGod = !isGod;
        rb.isKinematic = isGod;
    }

    private void Jump()
    {
        if (rb != null &&(isTouchingGround || hasDoubleJump))
        {
            if (!isTouchingGround) { hasDoubleJump = false; }
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(new Vector3(0, 1, 0) * jumpForce);
            MusicManager.PlayJumpSound();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            isTouchingGround = true;
            hasDoubleJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("Ground"))
        {
            isTouchingGround = false;
        }
    }
}
