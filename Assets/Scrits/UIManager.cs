using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreTXT;
    [SerializeField] private GameObject _player;
    [SerializeField] private TMP_Text _bestScoreTXT;
    [SerializeField] private GameObject _crownLifeImage;

    // Start is called before the first frame update
    void Start()
    {
        if (_scoreTXT == null)
        {
            Debug.LogError("ETL");
        }
        else
        {
            _scoreTXT.text = "0";
        }

        _bestScoreTXT.text = "Best Score: " + PlayerPrefs.GetInt("BestScore");
    }

    // Update is called once per frame
    void Update()
    {
        _scoreTXT.text = (MathF.Max(0,(int)_player.transform.position.z - 2)).ToString();
        if(_player.GetComponent<mover>().GetLife() < 1)
        {
            _crownLifeImage.SetActive(false);
        }
    }
}
