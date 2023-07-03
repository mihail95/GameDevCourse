using UnityEngine;
public class CoinScript : MonoBehaviour
{
    [SerializeField] GameObject coinsplosion;
    private GameManager gameManager;

    private Vector3 pos;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Contains("Sphere"))
        {
            pos = gameObject.transform.position;
            Destroy(gameObject);
            Instantiate(coinsplosion, pos, Random.rotation);
            MusicManager.PlayCoinSound();
            gameManager.GetCoin();
        }
    }
}
