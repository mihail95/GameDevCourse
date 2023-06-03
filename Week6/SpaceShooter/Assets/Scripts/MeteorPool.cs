using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorPool : MonoBehaviour
{
    public static MeteorPool meteorPoolInstance;
    [SerializeField] private GameObject pooledMeteor1;
    [SerializeField] private GameObject pooledMeteor2;
    private bool notEnoughMeteorsInPool = true;

    private List<GameObject> meteors;

    private void Awake()
    {
        meteorPoolInstance = this;
    }
    private void Start()
    {
        meteors = new List<GameObject>();
    }
    public GameObject GetBullet()
    {
        if (meteors.Count > 0)
        {
            for (int i = 0; i < meteors.Count; i++)
            {
                if (!meteors[i].activeInHierarchy)
                {
                    return meteors[i];
                }
            }
        }
        if (notEnoughMeteorsInPool)
        {
            GameObject bullet = (Random.Range(0,100) <= 50) ? Instantiate(pooledMeteor1) : Instantiate(pooledMeteor2);
            bullet.SetActive(false);
            meteors.Add(bullet);
            return bullet;
        }
        return null;
    }

}
