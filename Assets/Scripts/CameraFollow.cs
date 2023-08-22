using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private int speed;
    
    public Transform player;
   
    private Vector3 playerVector;
   
    void Update()
    {
        playerVector = player.position;
        playerVector.z = -10;
        transform.position = Vector3.Lerp(transform.position, playerVector, speed * Time.deltaTime);
    }
}
