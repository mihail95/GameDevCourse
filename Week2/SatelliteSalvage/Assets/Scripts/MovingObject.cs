using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] GameObject planetObject;
    [SerializeField] private List<GameObject> objectWalls = new(4);
    [SerializeField] private Vector3 direction;
    private Collider2D objectCollider;
    private float timeOfSpawn;
    private float speedMod;
    
    // Start is called before the first frame update
    private void Start()
    {
        speedMod = Random.Range(2f, 6f);
        planetObject = GameObject.Find("Planet");
        if ((int)GameManager.GetGameState() < 3) { direction = new Vector3(0, 0, 0); }
        else 
        {
            Vector3 centerDirection = (planetObject.transform.position - gameObject.transform.position).normalized;
            Vector2 vec2centerDirection = new Vector2(centerDirection.x, centerDirection.y);
            Vector3 rotatedDirection = (Quaternion.Euler(0, 0, Random.Range(-30f, 30f))) * vec2centerDirection;
            direction =  new Vector3 (rotatedDirection.x, rotatedDirection.y, 0);
        }
        timeOfSpawn = Time.time;
        objectCollider = gameObject.GetComponent<Collider2D>();
        objectWalls[0] = GameObject.Find("Object Border 1");
        objectWalls[1] = GameObject.Find("Object Border 2");
        objectWalls[2] = GameObject.Find("Object Border 3");
        objectWalls[3] = GameObject.Find("Object Border 4");
    }

    // Update is called once per frame
    private void Update()
    {
        bool isTouchingPlayer = objectWalls.Exists(wall => wall.GetComponent<Collider2D>().IsTouching(objectCollider));
       
        if (isTouchingPlayer)
        { 
            float currentTime = Time.time;
            if (currentTime-timeOfSpawn >= 2f) { Destroy(gameObject); }
        }

        MoveObject();
    }

    private void MoveObject()
    {
        transform.position += speedMod * Time.deltaTime * direction;
    }

    public void SetDirection(Vector3 incDirection)
    {
        direction = incDirection;
    }
}
