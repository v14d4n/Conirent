using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ChairSitScript : MonoBehaviour
{
    public Transform pos;
    public bool lockpos;
    GameObject player;
    private bool tabletIsSpawned;
    PhotonView _photonView;
    private Transform tabletPos;
    private Text chairText;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (lockpos)
        {
            TabletSpawn();
            try
            {
                player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = pos.transform.position;
            }
            catch
            {
                lockpos = false;
                PlayerContainer.playerIsSit = false;
                int ID = transform.GetComponentInChildren<PhotonView>().ViewID;
                PhotonView.Find(ID).gameObject.transform.GetComponentInParent<ChairContainer>().chairIsBusy = false;
            }
        }
        else
        {
            TabletDelete();
        }


        if (!PlayerContainer.playerIsSit && lockpos)
        {
            lockpos = false;
        }
    }

    void TabletSpawn()
    {
        if (!tabletIsSpawned)
        {
            tabletIsSpawned = true;
            Instantiate(Resources.Load("Tablet2"), transform.GetChild(0).GetChild(1).transform.position, transform.GetChild(0).GetChild(1).transform.rotation);
            GameObject.Find("Tablet2(Clone)").transform.parent = gameObject.transform;
        }
    }
    
    void TabletDelete()
    {
        if (tabletIsSpawned)
        {
            tabletIsSpawned = false;
            Destroy(GameObject.Find("Tablet2(Clone)"));
        }
    }
}
