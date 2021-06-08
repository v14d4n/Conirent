using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ListenerScript : MonoBehaviour
{
    private PhotonView _photonView;
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!_photonView.IsMine)
        {
            Destroy(transform.GetComponent<AudioSource>());
            Destroy(transform.GetComponent<AudioListener>());
        }
    }
}
