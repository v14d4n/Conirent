using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FollowScript : MonoBehaviour
{
    PhotonView photonView;

    void Start() // Этот скрипт нужен для того чтобы камера всегда была в кубе.
    {
        //photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        transform.position = transform.parent.position;
    }
}
