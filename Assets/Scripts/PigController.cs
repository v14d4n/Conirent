using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PigController : MonoBehaviourPun, IPunOwnershipCallbacks
{
    private PhotonView _photonView;
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private float speed = 5f;
    void Update()
    {
        if (transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<ChairSitScript>().lockpos)
        {
            if (!_photonView.IsMine)
            {
                _photonView.RequestOwnership();
                return;
            }
            
            transform.rotation = Quaternion.Euler(
                0, 
                Camera.main.transform.rotation.eulerAngles.y + 90,
                Camera.main.transform.rotation.eulerAngles.x);

            if (Input.GetKey(KeyCode.W))
                transform.position = transform.position + Camera.main.transform.forward * Time.deltaTime * speed;
            if (Input.GetKey(KeyCode.S))
                transform.position = transform.position - Camera.main.transform.forward * Time.deltaTime * (speed * 0.5f);
            if (Input.GetKey(KeyCode.D))
                transform.position = transform.position + Camera.main.transform.right * Time.deltaTime * speed;
            if (Input.GetKey(KeyCode.A))
                transform.position = transform.position - Camera.main.transform.right * Time.deltaTime * speed;
        }
    }
    
    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
       _photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("Владение свином передано другому игроку");
    }
}
