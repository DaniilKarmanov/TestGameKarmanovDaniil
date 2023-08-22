using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Photon.Pun.Demo.PunBasics;

public class GameEnd : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text nickWin;
    [SerializeField] private Text gold;

    private Player player;

    private float timeStart = 0;
    private float timeEnd = 5;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        timeStart += Time.deltaTime;

        if (timeStart > timeEnd && PhotonNetwork.CurrentRoom.PlayerCount == 1 )
        {         
            EndGame();
        }
    }

    private void EndGame()
    { 
        nickWin.text = player.nick.text;
        gold.text = player.goldPoint.ToString();
        panel.SetActive(true);
    }

    public void LeaveRoom() 
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.LoadLevel(0);
    }
}
