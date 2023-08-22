using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField createInput;
    [SerializeField] private InputField joinInput;
    [SerializeField] private InputField nickInput;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        nickInput.text = PlayerPrefs.GetString("name");
        PhotonNetwork.NickName = nickInput.text;
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }
   
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void SaveNick()
    {
        PlayerPrefs.SetString("name", nickInput.text);
        PhotonNetwork.NickName = nickInput.text;
    }
}
