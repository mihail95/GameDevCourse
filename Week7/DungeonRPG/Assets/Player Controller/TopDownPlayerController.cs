using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TopDownPlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float runMultiplier = 1.5f;

    InputActions playerControls;

    Rigidbody2D rb;
    Collider2D col;
    Animator anim;

    Vector2 moveVector;
    bool isGod = false;
    bool isSprinting = false;
    bool isColliding = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        playerControls = new InputActions();
        gameObject.transform.position = GameManager.playerPos != null ? GameManager.playerPos: new Vector3(-4f, -5f, 0f);
    }

    private void OnEnable()
    {
        playerControls.Player.Sprint.performed += _ => isSprinting = true;
        playerControls.Player.Sprint.canceled += _ => isSprinting = false;
        playerControls.Player.GodMode.performed += _ => ToggleGodMode();
        playerControls.Player.EnterCombat.performed += _ => CheckEnterCombat();

        playerControls.Enable();
    }

    private void OnDisable() => playerControls.Disable();

    void ToggleGodMode()
    {
        isGod = !isGod;
        col.enabled = !isGod;
    }

    void CheckEnterCombat()
    {
        if (isColliding) 
        {
            GameManager.playerPos = gameObject.transform.position;
            SceneManager.LoadScene("BattleScene"); 
        }
    }

    private void FixedUpdate()
    {
        moveVector = playerControls.Player.Move.ReadValue<Vector2>();
        if (isGod) moveVector *= 5f;
        if (isSprinting) moveVector *= runMultiplier;

        rb.velocity = speed * moveVector;
    }
   
    private void Update() => SetAnimations();

    private void SetAnimations()
    { 
        if (moveVector != Vector2.zero)
        {
            anim.SetBool("IsMoving", true);
            anim.SetFloat("MoveX", moveVector.x);
            anim.SetFloat("MoveY", moveVector.y);
        }
        else
            anim.SetBool("IsMoving", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null) isColliding = false;
    }
}