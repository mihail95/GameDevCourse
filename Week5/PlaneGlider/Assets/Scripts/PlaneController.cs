using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] TextMeshProUGUI boostText;
    [SerializeField] Image boostImage;
    [SerializeField] TextMeshProUGUI respawnText;
    [SerializeField] float boostDecreaseRate;

    private float initAnimSpeed;
    private bool isBoosting;
    private Vector2 initVelocity;
    private float initAngle;
    private float boostAmount;
    private bool hasCrashed;
    private bool hasFuel;
    private bool missileHit;

    private void Awake()
    {
        missileHit = false;
        boostAmount = 100f;
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

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && !isBoosting && hasFuel && !hasCrashed && !missileHit)
        {
            rigidBody.velocity = Vector2.right * horizontalSpeed + Vector2.up * boostForce;
            isBoosting = true;
            float angle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            animator.speed = initAnimSpeed;
            engineSound.Play();
            StartCoroutine(nameof(DecreaseBoost));
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene("MainMenu"); }
        if (hasCrashed && !missileHit)
        {
            isBoosting = false;
            animator.speed = 0;
            engineSound.Stop();
            StopCoroutine(nameof(DecreaseBoost));
        }
        if (!hasFuel && boostAmount > 0f) { hasFuel = true; }
        if ((Input.GetKeyUp(KeyCode.Space) && isBoosting) || !hasFuel && !hasCrashed)
        {
            if (!missileHit) 
            { 
                rigidBody.velocity = initVelocity;
                transform.rotation = Quaternion.Euler(0f, 0f, initAngle);
                animator.speed = 0;
            }
            isBoosting = false;
            engineSound.Stop();
            StopCoroutine(nameof(DecreaseBoost));
        }
        if (Input.GetKeyDown(KeyCode.Space) && hasCrashed) 
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Physics2D.IgnoreLayerCollision(9, 7, false);
            Physics2D.IgnoreLayerCollision(9, 6, false);
            Physics2D.IgnoreLayerCollision(9, 11, false);
        }
        boostText.text = (boostAmount > 0f) ? Mathf.RoundToInt(boostAmount).ToString() : "EMPTY";
        boostImage.fillAmount = boostAmount / 100f;

        if (boostAmount <= 0f)
        {
            hasFuel = false;
        }
    }

    public float GetPlayerXCoord()
    {
        return transform.position.x;
    }

    public Vector2 GetPlayerVelocity()
    {
        return rigidBody.velocity;
    }

    private IEnumerator DecreaseBoost()
    {
        while (isBoosting && boostAmount > 0f) { 
            boostAmount -= boostDecreaseRate * Time.deltaTime;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.name.Contains("ground") || collision.gameObject.name.Contains("rock") || collision.gameObject.name.Contains("EnemyPlane") || collision.gameObject.name.Contains("Missile"))  && !hasCrashed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 3f);

            // Disable collisions and enable gravity and rotation for bounces
            rigidBody.gravityScale = 1f;
            rigidBody.constraints = RigidbodyConstraints2D.None;
            Physics2D.IgnoreLayerCollision(9, 7);
            Physics2D.IgnoreLayerCollision(9, 6);
            Physics2D.IgnoreLayerCollision(9, 11);
            respawnText.gameObject.SetActive(true);

            if (collision.gameObject.name.Contains("Missile"))
            {
                missileHit = true;
                Destroy(collision.gameObject);
                StartCoroutine(nameof(ExplodeAnimation));
            }
            else if (!collision.gameObject.name.Contains("Missile") && !missileHit)
            {
                hasCrashed = true;
                crashSound.PlayOneShot(crashSound.clip);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("boostPickup"))
        {
            Destroy(collision.gameObject);
            pickupSound.PlayOneShot(pickupSound.clip);
            boostAmount +=  Mathf.Min(50f, 100f - boostAmount);
        }
        else if (collision.gameObject.name.Contains("FinishLine"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private IEnumerator ExplodeAnimation()
    {
        animator.speed = 1;
        animator.SetBool("missileHit", true);
        explosionSound.PlayOneShot(explosionSound.clip);
        yield return new WaitForSecondsRealtime(1);
        animator.SetBool("missileHit", false);
        yield return new WaitForSecondsRealtime(0.5f);
        hasCrashed = true;
        missileHit = false;
    }

}
