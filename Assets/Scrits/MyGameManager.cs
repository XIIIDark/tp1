using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private bool _startGame = true;
    public mover player;
    public SpawnManager spawnManager;
    public GameObject textStart;
    public GameObject textBestScore;

    void Start()
    {
        //PlayerPrefs.SetInt("BestScore", 0);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _startGame == true)
        {
            player.SetSpeed(6f);
            spawnManager.StartFloorsSpawn();
            textStart.SetActive(false);
            textBestScore.SetActive(false);
            _startGame = false;
        }
    }
}
