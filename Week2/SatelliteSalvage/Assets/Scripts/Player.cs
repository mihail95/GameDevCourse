using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public enum PlayerState { ASTRONAUT, ROBOT }
    public enum MovementState { NORMAL = 1, BOOST = 2, SUPER_BOOST = 4}
    [SerializeField] private PlayerState thisPlayerState;
    [SerializeField] private MovementState movementState;
    [SerializeField] private float speedMod;
    private bool turnedRight = true;
    private Dictionary<int, List<float>> originalColors = new();
    private bool hasOriginal = false;
    private Collider2D playerCollider;
    private static int partsCount = 0;

    public void MovePlayer(Vector3 direction)
    {
        transform.position += (int)movementState * speedMod * Time.deltaTime * direction;
    }
    public void TurnPlayer(Vector2 direction)
    {
        int posCorrection = thisPlayerState == PlayerState.ASTRONAUT ? 2 : 0;
        if (direction == Vector2.left && turnedRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            turnedRight = false;

            transform.position = new Vector3(transform.position.x - posCorrection, transform.position.y, 0);
        }
        else if (direction == Vector2.right && !turnedRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
            turnedRight = true;

            transform.position = new Vector3(transform.position.x + posCorrection, transform.position.y, 0);
        }
            
    }

    private void Start()
    {
        movementState = MovementState.NORMAL;
        speedMod = 1.75f;
    }

    private void Update()
    {
        if (thisPlayerState != GameManager.GetPlayerState()) { gameObject.SetActive(false); }
    }

    public void SetMovementState(MovementState movState)
    {
        movementState = movState;
    }
    public MovementState GetMovementState()
    {
        return movementState;
    }
    public void SetPlayerPosition(Vector3 pos)
    {
        gameObject.transform.position = pos;
    }
    public void SetRandomSpriteColors()
    {
        SpriteRenderer[] objectSprites = gameObject.transform.GetComponentsInChildren<SpriteRenderer>();
        Dictionary<int, List<float>> tempColors = !hasOriginal ? new Dictionary<int, List<float>>() : originalColors;
        int idx = 0;
        foreach (SpriteRenderer objectSprite in objectSprites)
        {
            if (objectSprite != null)
            {
                if(!hasOriginal) 
                {
                    tempColors.Add(idx, new List<float>());
                    tempColors[idx].Add(objectSprite.color.r);
                    tempColors[idx].Add(objectSprite.color.g);
                    tempColors[idx].Add(objectSprite.color.b);
                    tempColors[idx].Add(objectSprite.color.a);
                }
                objectSprite.color = Random.ColorHSV();
            }
            idx++;
        }
        
        originalColors = tempColors;
        hasOriginal = true;
    }
    public void ResetSpriteColors()
    {
        Debug.Log("ResetColors");
        SpriteRenderer[] objectSprites = gameObject.transform.GetComponentsInChildren<SpriteRenderer>();

        int idx = 0;
        foreach (SpriteRenderer objectSprite in objectSprites)
        {
            if (objectSprite != null)
            {
                var color = objectSprite.color;
                color.r = originalColors[idx][0];
                color.g = originalColors[idx][1];
                color.b = originalColors[idx][2];
                color.a = originalColors[idx][3];

                objectSprite.color = color;
            }
            idx++;
        }
    }

    public void UseAbility()
    {
        playerCollider = gameObject.GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>(); // Collisions array
        var playerContacts = playerCollider.OverlapCollider(filter, results); // Number of collisions
        List<Collider2D> interactableObjects = new List<Collider2D>(); // Filtered results

        if (thisPlayerState == PlayerState.ASTRONAUT)
        {            
            foreach (Collider2D contact in results)
            {
                if (contact.gameObject.name.Contains("BoxOfTools")) { CollectBox(contact); break; }
                else if (contact.gameObject.name.Contains("Satellite")) { FixSatellite(contact); break; }
            }

        }
        else if (thisPlayerState == PlayerState.ROBOT)
        {
            foreach (Collider2D contact in results)
            {
                if (contact.gameObject.name.Contains("Asteroid")) { MineAsteroid(contact); break; }
            }
        }
    }

    private void CollectBox(Collider2D boxCollider)
    {
        Destroy(boxCollider.gameObject);
        partsCount++;
    }
    private void FixSatellite(Collider2D satelliteCollider)
    {
        if (partsCount >= 1)
        {
            Satellite satelliteGO = satelliteCollider.gameObject.GetComponent<Satellite>();
            if (satelliteGO.GetSatelliteHealth() < 3)
            {
                satelliteGO.HealSatellite();
                partsCount--;
            }
        }
    }
    private void MineAsteroid(Collider2D asteroidCollider)
    {
        Destroy(asteroidCollider.gameObject);
        partsCount++;
    }
    public static int GetPartsCount()
    {
        return partsCount;
    }
}
