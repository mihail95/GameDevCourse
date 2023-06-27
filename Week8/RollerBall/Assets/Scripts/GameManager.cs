using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject coinsGO;
    [SerializeField] private TextMeshProUGUI coinCountText;
    private int coinCount;
    private int gatheredCoins;

    private void Awake()
    {
        coinCount = coinsGO.transform.childCount;
        gatheredCoins = 0;
        coinCountText.SetText($"{gatheredCoins}/{coinCount}");
    }

    public void GetCoin()
    {
        gatheredCoins++;
        coinCountText.SetText($"{gatheredCoins}/{coinCount}");
        coinCountText.color = CheckAllCoinsCollected() ? Color.green : Color.white;
    }

    public bool CheckAllCoinsCollected()
    {
        return gatheredCoins == coinCount;
    }
    
    
}
