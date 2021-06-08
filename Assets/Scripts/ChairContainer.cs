using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using Photon.Pun;
using Photon;
using Photon.Realtime;
using UnityEngine.UI;

public class ChairContainer : MonoBehaviourPunCallbacks
{
    private Text chairText;
    public bool chairIsBusy;
    private bool tabletIsSpawned;
    private bool textIsGreen = true;
    PhotonView _photonView;
    private Transform tabletPos;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        chairText = transform.parent.parent.GetChild(0).GetComponent<Text>();
    }

    private void Update()
    {
        if (chairIsBusy)
        {
            SetTextColorRed();
        }
        else
        {
            SetTextColorGreen();
        }
    }

    void SetTextColorRed()
    {
        if (!textIsGreen) return;
        textIsGreen = false;
        chairText.text = "Место\nзанято";
        chairText.color = Color.red;
    }
    void SetTextColorGreen()
    {
        if (textIsGreen) return;
        textIsGreen = true;
        chairText.text = "Место\nсвободно";
        chairText.color = Color.green;
    }
}
