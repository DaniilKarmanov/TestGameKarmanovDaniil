using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DamageDealler : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            PhotonNetwork.Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
