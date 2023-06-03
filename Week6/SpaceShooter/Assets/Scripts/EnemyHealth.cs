using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemyhealth : MonoBehaviour
{
    public int maxHP;
    public GameObject damageText;
    public ParticleSystem explosionParticles;
    private int currentHP;
    private int damageToTake;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MusicManager.PlayEnemyHitSound();
        if (collision.gameObject.name.Contains("Normal"))
        {
            damageToTake = 1;
        }
        else if (collision.gameObject.name.Contains("Alt"))
        {
            damageToTake = 3;
        }
        else if (collision.gameObject.name.Contains("Special"))
        {
            damageToTake = 5;
        }

        currentHP -= damageToTake;
        ShowDamageText(damageToTake);

        if (currentHP <= 0)
        {
            Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }

    private void ShowDamageText(int damageToTake)
    {
        GameObject textGO = Instantiate(damageText, transform.position, Quaternion.identity, transform);
        TextMeshPro tmp = textGO.GetComponent<TextMeshPro>();
        tmp.text = $"{damageToTake}";
        tmp.color = (currentHP > (maxHP*2/3)) ? Color.white : (currentHP > maxHP / 3) ? Color.yellow : Color.red;
    }
}
