using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float minX, maxX, minY, maxY;
    void Start()
    {
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        sr.color = new Color(Random.value, Random.value, Random.value,1);
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
    }
}
