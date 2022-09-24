using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _floorPrefab;
    [SerializeField] private GameObject _FloorContainer;

    private bool _stopSpawning = false;
    private float _zPosition = 10f;

    public mover player;

    void Start()
    {
        //StartFloorsSpawn();
        
    }

    public void StartFloorsSpawn()
    {
        for(int i = 0; i < 2; i++)
        {
            SpawnFloor();
        }
        StartCoroutine(SpawnRountine());
    }
   
    GameObject SpawnFloor()
    {
        Vector3 posToSpawn = new Vector3(0, 0, _zPosition);
        GameObject newFloor = Instantiate(_floorPrefab, posToSpawn, Quaternion.identity);
        newFloor.transform.parent = _FloorContainer.transform;
        _zPosition += 10f;
        return newFloor;
    }


    IEnumerator SpawnRountine()
    {

        while (_stopSpawning == false)
        {

            GameObject newFloor = SpawnFloor();
            newFloor.transform.GetChild(Random.Range(0,3)).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.9f/player.ReturnSpeed());
        }
    }

    /*public void ResetFloorSpawning()
    {
        _stopSpawning = true;
        _zPosition = 10f;
        foreach(Transform floor in _FloorContainer.transform)
        {
            Destroy(floor.gameObject);
        }
        _stopSpawning = false;

        //StartFloorsSpawn();
    }*/

}
