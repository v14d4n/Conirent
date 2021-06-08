using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Threading;

public class CheckColliderPlayerScript : MonoBehaviourPunCallbacks
{
    public bool isPlayerIn;
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!isPlayerIn && transform.GetComponentInParent<ChairContainer>().chairIsBusy)
        {
            transform.GetComponentInParent<ChairContainer>().chairIsBusy = false;
        }
        else if (isPlayerIn && !transform.GetComponentInParent<ChairContainer>().chairIsBusy)
        {
            transform.GetComponentInParent<ChairContainer>().chairIsBusy = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && transform.GetComponentInParent<ChairContainer>().chairIsBusy)
        {
            isPlayerIn = true;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        isPlayerIn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerIn = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerIn = true;
        }
    }
}
