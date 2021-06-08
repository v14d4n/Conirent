using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DevelopmentRoomManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    
    void Start()
    {
        PhotonNetwork.NickName = "Developer " + Random.Range(1000, 9999);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "Develop";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 99 });
    }

    public override void OnJoinedRoom()
    {
        Vector3 pos = new Vector3(0, 0f, 0);
        PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity);
    }
}
