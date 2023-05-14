using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float boostForce;
    [SerializeField] Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource engineSound;
    [SerializeField] private AudioSource pickupSound;
    [SerializeField] private AudioSource crashSound;
    [SerializeField] TextMeshProUGUI boostText;
    [SerializeField] Image boostImage;
    [SerializeField] TextMeshProUGUI respawnText;

    private float initAnimSpeed;
    private bool isBoosting;
    private Vector2 initVelocity;
    private float initAngle;
    private int boostAmount;
    private bool hasCrashed;
    private bool hasFuel;

    private void Awake()
    {
        boostAmount = 100;
        initAnimSpeed = animator.speed;
        animator.speed = 0;
        isBoosting = false;
        initVelocity = Vector2.right * horizontalSpeed + Vector2.down * verticalSpeed;
        rigidBody.velocity = initVelocity;
        initAngle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, initAngle);
        hasCrashed = false;
        hasFuel = true;
    }

    private void Update()
    {
        if (hasCrashed)
        {
            isBoosting = false;
            animator.speed = 0;
            engineSound.Stop();
            StopCoroutine(DecreaseBoost());
        }
        if (!hasFuel && boostAmount > 0) { hasFuel = true; }
        if (Input.GetKey(KeyCode.Space) && !isBoosting && hasFuel && !hasCrashed)
        {
            rigidBody.velocity = Vector2.right * horizontalSpeed + Vector2.up * boostForce;
            isBoosting = true;
            float angle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            animator.speed = initAnimSpeed;
            engineSound.Play();
            StartCoroutine(DecreaseBoost());
        }
        if ((Input.GetKeyUp(KeyCode.Space) && isBoosting) || !hasFuel && !hasCrashed)
        {
            rigidBody.velocity = initVelocity;
            isBoosting = false;
            transform.rotation = Quaternion.Euler(0f, 0f, initAngle);
            animator.speed = 0;
            engineSound.Stop();
            StopCoroutine(DecreaseBoost());
        }
        if (Input.GetKeyDown(KeyCode.Space) && hasCrashed) 
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Physics2D.IgnoreLayerCollision(9, 7, false);
        }
        boostText.text = (boostAmount > 0) ? boostAmount.ToString() : "EMPTY";
        boostImage.fillAmount = boostAmount / 100f;

        if (boostAmount == 0)
        {
            hasFuel = false;
        }
    }

    public float GetPlayerXCoord()
    {
        return transform.position.x;
    }

    private IEnumerator DecreaseBoost()
    {
        while (isBoosting && boostAmount > 0) { 
            boostAmount--;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");
        if ((collision.gameObject.name.Contains("ground") || collision.gameObject.name.Contains("rock")) && !hasCrashed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 3f);
            crashSound.PlayOneShot(crashSound.clip);
            hasCrashed = true;

            // Disable collisions and enable gravity and rotation for bounces
            rigidBody.gravityScale = 1f;
            rigidBody.constraints = RigidbodyConstraints2D.None;
            Physics2D.IgnoreLayerCollision(9, 7);
            respawnText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collided with trigger {collision.gameObject.name}");
        if (collision.gameObject.name.Contains("boostPickup"))
        {
            Destroy(collision.gameObject);
            pickupSound.PlayOneShot(pickupSound.clip);
            boostAmount +=  Mathf.Min(50, 100 - boostAmount);
        }
    }

}
