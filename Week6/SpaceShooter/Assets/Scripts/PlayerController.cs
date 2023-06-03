using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    public float fireRate = 1f;
    public float altFireRate = 3f;
    public float specialFireRate = 7f;

    public PlayerControls playerControls;
    public Rigidbody2D rb;

    // Projectiles
    public GameObject projectileSpawner;
    public GameObject normalProjectile;
    public GameObject altProjectile;
    public GameObject specialProjectile;

    // Audio
    public AudioClip normalFireSound;
    public AudioClip altFireSound;
    public AudioClip specialFireSound;
    public AudioSource fireSoundEmitter;
    public AudioSource explosionSoundEmitter;

    // Icons
    public Image altFireImage;
    public Image specialFireImage;
    public GameObject specialFireTarget;
    public ParticleSystem explosionParticles;

    Vector2 moveDirection = Vector2.zero;
    private InputAction move;
    private InputAction fire;
    private InputAction altFire;
    private InputAction specialFire;
    private InputAction godMode;

    private bool isFiring;
    private bool hasNormalCD;
    private bool hasAltFireCD;
    private bool hasSpecialCD;
    private LayerMask enemyMask;
    private GameObject specialTargetCrosshair;
    private GameObject currentTarget;
    private bool godModeOn;

    private void Awake()
    {
        playerControls = new PlayerControls();
        enemyMask = LayerMask.GetMask("Enemies");
        godModeOn = false;
    }

    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        if (hasAltFireCD) { altFireImage.fillAmount += 1.0f / altFireRate * Time.deltaTime; }
        if (hasSpecialCD) { specialFireImage.fillAmount += 1.0f / specialFireRate * Time.deltaTime; }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, moveDirection.y * moveSpeed * Time.deltaTime);

        // Check for closest enemy
        if (!hasSpecialCD)
        {
            RaycastHit2D raycastHit = Physics2D.CircleCast(projectileSpawner.transform.position, 7f, Vector2.right, 17f, enemyMask);
            if (raycastHit)
            {
                if (specialTargetCrosshair == null)
                {
                    specialTargetCrosshair = Instantiate(specialFireTarget, raycastHit.collider.transform.position, Quaternion.identity);
                }
                else
                {
                    specialTargetCrosshair.transform.position = raycastHit.collider.transform.position;
                }
                currentTarget = raycastHit.collider.gameObject;
            }
            else
            {
                if (specialTargetCrosshair != null || this == null)
                {
                    Destroy(specialTargetCrosshair);
                }
            }
        }
        else
        {
            if (specialTargetCrosshair != null || this == null)
            {
                Destroy(specialTargetCrosshair);
            }
        }
    }
    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
        fire.canceled += StopFire;

        altFire = playerControls.Player.AltFire;
        altFire.Enable();
        altFire.performed += AlternateFire;

        specialFire = playerControls.Player.SpecialFire;
        specialFire.Enable();
        specialFire.performed += SpecialFire;

        godMode = playerControls.Player.GodMode;
        godMode.Enable();
        godMode.performed += SwitchGodMode;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        altFire.Disable();
        specialFire.Disable();
    }

    private IEnumerator NormalFire()
    {
        while (true)
        {
            if (isFiring && !hasNormalCD)
            {
                hasNormalCD = true;
                ShootNormal();
                yield return new WaitForSeconds(fireRate);
                hasNormalCD = false;
            }
            else yield break;
            
        }
    }

    private IEnumerator AltFireCD()
    {
        hasAltFireCD = true;
        yield return new WaitForSeconds(altFireRate);
        hasAltFireCD = false;
    }

    private IEnumerator SpecialCD()
    {
        hasSpecialCD = true;
        yield return new WaitForSeconds(specialFireRate);
        hasSpecialCD = false;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        isFiring = true;
        StartCoroutine(NormalFire());
    }

    private void StopFire(InputAction.CallbackContext context)
    {
        isFiring = false;
    }

    private void AlternateFire(InputAction.CallbackContext context)
    {
        if (!hasAltFireCD) 
        {
            ShootAlternate();
            StartCoroutine(AltFireCD());
        }
    }

    private void SpecialFire(InputAction.CallbackContext context)
    {
        if (!hasSpecialCD && currentTarget != null) 
        {
            ShootSpecial(currentTarget);
            StartCoroutine(SpecialCD());
        }
    }

    private void ShootNormal()
    {
        GameObject projectileGO = Instantiate(normalProjectile, projectileSpawner.transform.position, Quaternion.Euler(0, 0, -90));
        projectileGO.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 0.1f);
        fireSoundEmitter.PlayOneShot(normalFireSound);
    }

    private void ShootAlternate()
    {
        GameObject projectileGO = Instantiate(altProjectile, projectileSpawner.transform.position, Quaternion.identity);
        projectileGO.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 0.1f);
        fireSoundEmitter.PlayOneShot(altFireSound);
        altFireImage.fillAmount = 0;
    }

    private void ShootSpecial(GameObject target)
    {
        GameObject projectileGO = Instantiate(specialProjectile, projectileSpawner.transform.position, Quaternion.identity);
        projectileGO.GetComponent<SeekTarget>().SetTarget(target);
        fireSoundEmitter.PlayOneShot(specialFireSound);
        specialFireImage.fillAmount = 0;
    }
    private void SwitchGodMode(InputAction.CallbackContext context)
    {
        if (GameObject.Find("Player") != null && this != null)
        {
            godModeOn = !godModeOn;
            gameObject.transform.GetChild(1).gameObject.SetActive(godModeOn);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.name.Contains("EnemyProjectile") || collision.gameObject.name.Contains("Meteor")) && !godModeOn)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
            explosionSoundEmitter.PlayOneShot(explosionSoundEmitter.clip);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(GameOver());
        }
    }
    private IEnumerator GameOver()
    {
        int wave = GameObject.Find("WaveManager").GetComponent<WaveManager>().GetCurrentWave();
        PlayerPrefs.SetInt("Highscore", wave);
        MusicManager.FadeMusic();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        SceneManager.LoadScene("MenuScreen");
    }

}
