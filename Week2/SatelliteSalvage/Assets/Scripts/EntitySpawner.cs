using UnityEngine;
using System.Collections.Generic;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private GameObject astronautPrefab;
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private GameObject satelliteFGPrefab;
    [SerializeField] private GameObject satelliteBGPrefab;
    [SerializeField] private GameObject asteroidFGPrefab;
    [SerializeField] private GameObject asteroidBGPrefab;
    [SerializeField] private GameObject toolBoxPrefab;
    [SerializeField] private GameObject playerPodPrefab;
    [SerializeField] private List<GameObject> spawnerList;


    public GameObject SpawnPlayerPod(Vector3 coords)
    {
        GameObject playerPodGO = Instantiate(playerPodPrefab, coords, Quaternion.Euler(0, 0, Random.Range(0, 360)));

        return playerPodGO;
    }
    public GameObject SpawnPlayerPodRandom()
    {
        GameObject playerPodGO = SpawnPlayerPod(GenerateRandomLocationInsideBorders());

        return playerPodGO;
    }
    public GameObject SpawnSatellite(Vector3 coords)
    {
        var prefab = (Random.value > 0.5f) ? satelliteBGPrefab : satelliteFGPrefab;
        GameObject satelliteGO = Instantiate(prefab, coords, Quaternion.identity);
        satelliteGO.GetComponent<Satellite>().satelliteState = Satellite.SatelliteState.OFF;

        return satelliteGO;
    }

    public GameObject SpawnPlayer(Player.PlayerState playerState, Vector3 coords)
    {
        GameObject prefab = (playerState == Player.PlayerState.ASTRONAUT) ? astronautPrefab : robotPrefab;
        GameObject playerGO =  Instantiate(prefab, coords, Quaternion.identity);

        return playerGO;
    }
    public GameObject SpawnPlayerRandom(Player.PlayerState playerState)
    {

        GameObject playerGO = SpawnPlayer(playerState, GenerateRandomLocationInsideBorders());

        return playerGO;
    }


    public GameObject SpawnAsteroid(Vector3 coords)
    {
        var prefab = (Random.value > 0.5f) ? asteroidFGPrefab : asteroidBGPrefab;
        GameObject asteroidGO = Instantiate(prefab, coords, Quaternion.identity);

        return asteroidGO;
    }

    public GameObject SpawnToolBox(Vector3 coords)
    {
        GameObject toolBoxGO = Instantiate(toolBoxPrefab, coords, Quaternion.identity);

        return toolBoxGO;
    }

    public List<GameObject> SpawnRandomSatellites(int satNum)
    {
        List<GameObject> satelliteList = new();

        for (int i = 0; i<satNum; i++)
        {
            Vector3 randomLocation = GenerateRandomLocationInsideBorders();
            GameObject satellite = SpawnSatellite(randomLocation);
            satelliteList.Add(satellite);
        }
        

        return satelliteList;
    }

    public List<GameObject> SpawnRandomInteractableObjects(int objNum)
    {
        List<GameObject> objectsList = new();
        for (int i = 0; i < objNum; i++)
        {
            var spawnerPosition = spawnerList[(int)Random.Range(0,4)].transform.position;
            GameObject objectToSpawn = new();
            if (Random.value > 0.5f) 
            {
                objectToSpawn = SpawnToolBox(spawnerPosition);
            }
            else 
            {
                objectToSpawn = SpawnAsteroid(spawnerPosition);
            }

            objectsList.Add(objectToSpawn);
        }

        return objectsList;
    }

    private Vector3 GenerateRandomLocationInsideBorders()
    {
        Vector3 ranomLocationInsideBorders = new(Random.Range(-9f, 21f), Random.Range(-4f, 12f), 0);
        return ranomLocationInsideBorders;
    }
}
