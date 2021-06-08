using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Nickname : MonoBehaviourPunCallbacks, IPunObservable
{
    private Text thisIsNick;
    private string NickName;
    private PhotonView _photonView;

    void Start()
    {
        thisIsNick = GetComponent<Text>();
        _photonView = GetComponent<PhotonView>();
    }
    
    private void Update()
    {
        if (_photonView.IsMine)
        {
            NickName = PhotonNetwork.NickName;
        }
        thisIsNick.text = NickName;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(NickName);
        }
        else
        {
            NickName = (string)stream.ReceiveNext();
        }
    }
}
