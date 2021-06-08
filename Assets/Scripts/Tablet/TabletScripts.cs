using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TabletScripts : MonoBehaviour
{
    public void StandUP()
    {
        PlayerContainer.playerIsSit = false;
        Debug.Log("прейрсит " + PlayerContainer.playerIsSit);
    }
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Cursor.lockState = CursorLockMode.None;
    }
}
