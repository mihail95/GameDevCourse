using UnityEngine;

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

    public GameObject SpawnPlayerPod(Vector3 coords)
    {
        GameObject playerPodGO = Instantiate(playerPodPrefab, coords, Quaternion.Euler(0, 0, Random.Range(0, 360)));

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
}
