using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject coinsGO;
    [SerializeField] private TextMeshProUGUI coinCountText;
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    private bool hasFinished;
    private int coinCount;
    private int gatheredCoins;
    private float currentTime;
    private float bestTime;

    private void Awake()
    {
        hasFinished = false;
        coinCount = coinsGO.transform.childCount;
        gatheredCoins = 0;
        coinCountText.SetText($"{gatheredCoins}/{coinCount}");
        currentTime = 0.00f;
        bestTime = PlayerPrefs.GetFloat("record_level1", 0.00f);
        if (bestTime > 0.00f)
        {
            bestTimeText.SetText(bestTime.ToString("F2"));
            bestTimeText.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (!hasFinished)
        {
            currentTime += Time.deltaTime;
            currentTimeText.SetText(currentTime.ToString("N2"));
        }
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

    public void EndLevel()
    {
        if (CheckAllCoinsCollected())
        {
            hasFinished = true;
            if (currentTime < bestTime || bestTime == 0.00f) { PlayerPrefs.SetFloat("record_level1", currentTime); }
            StartCoroutine(WaitForSecondsAndEnd(2f));
        }
        
    }

    private IEnumerator WaitForSecondsAndEnd(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
