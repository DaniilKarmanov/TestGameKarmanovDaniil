using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GoldSpaw : MonoBehaviour
{
    [SerializeField] private GameObject gold;
    [SerializeField] private float minX, minY, maxX, maxY;

    private float timeSpawn = 0;
    private float spawn = 2;

    void Update()
    {
        timeSpawn += Time.deltaTime;
        if(timeSpawn > spawn)
        {
            timeSpawn = 0;
            Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            PhotonNetwork.Instantiate(gold.name, randomPosition, Quaternion.identity);
        }
    }
}
