using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Raaay : MonoBehaviourPunCallbacks
{
    Camera cam;
    Camera fakecam;
    PhotonView _photonView;
    int ID = -1;
    int gID = -1;
    private bool grabed;
    private bool fIsPressed;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if (!_photonView.IsMine)
        {
            return;
        }
        cam = GetComponent<Camera>();
    }
    
    void Update()
    {
        if (!_photonView.IsMine)
        {
            GetComponent<AudioListener>().enabled = false;
            return;
        }
        if (Input.GetKey(KeyCode.F) || Input.GetKeyUp(KeyCode.F))
        {
            Grab();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerContainer.playerIsSit)
            {
                StandUp();
            }
            else
            {
                SitDown();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Touch();
        }
    }

    void Touch()
    {
        Debug.Log("нажал");
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitObjects, 20f) && hitObjects.collider.CompareTag("Button"))
        {
            hitObjects.transform.GetComponent<TbWay>().ActivateButton();
            Debug.Log("зашел");
        }
    }

    void Grab()
    {
        if (grabed)
        {
            Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray2, out var hitObj, 2f);
            PhotonView.Find(gID).transform.position = transform.position + ray2.direction * 1.5f;
        }
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitObjects, 2f) && hitObjects.collider.CompareTag("Interactive") && fIsPressed)
        {
            Debug.Log("Включил");
            if (!hitObjects.transform.GetComponent<PhotonView>().IsMine)
                hitObjects.transform.GetComponent<PhotonView>().RequestOwnership();
            gID = hitObjects.transform.GetComponent<PhotonView>().ViewID;
            _photonView.RPC("SyncToGravFalse", RpcTarget.All, gID);
            grabed = true;
            PhotonView.Find(gID).gameObject.transform.GetComponent<Rigidbody>().useGravity = false;
            PhotonView.Find(gID).gameObject.transform.GetComponent<Transform>().LookAt(transform.position);
            PhotonView.Find(gID).transform.position = transform.position + ray.direction * 1.5f;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            fIsPressed = true;
        }
        
        if (Input.GetKeyUp(KeyCode.F) && grabed)
        {
            fIsPressed = false;
            Debug.Log("Выключил");
            grabed = false;
            _photonView.RPC("SyncToGravTrue", RpcTarget.All, gID);
            PhotonView.Find(gID).gameObject.transform.GetComponent<Rigidbody>().useGravity = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            fIsPressed = false;
            grabed = false;
            _photonView.RPC("SyncToGravTrue", RpcTarget.All, gID);
            PhotonView.Find(gID).gameObject.transform.GetComponent<Rigidbody>().useGravity = true;
            hitObjects.rigidbody.AddForce(ray.direction * 500f);
        }
    }

    void SitDown()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitObjects, 1f) && hitObjects.collider.CompareTag("Chair") && PlayerContainer.playerIsSit == false) 
        {
            if (hitObjects.transform.GetComponent<ChairSitScript>().lockpos == false &&
                hitObjects.transform.GetComponentInChildren<ChairContainer>().chairIsBusy == false)
            {
                hitObjects.transform.GetComponentInChildren<ChairSitScript>().lockpos = true;
                PlayerContainer.playerIsSit = true;
                ID = hitObjects.transform.GetComponentInChildren<PhotonView>().ViewID;
            }
        }
    }

    void StandUp()
    {
        if (PhotonView.Find(ID).gameObject.transform.GetComponent<ChairContainer>().chairIsBusy == true &&
            PlayerContainer.playerIsSit && ID != -1)
        {
            PhotonView.Find(ID).gameObject.transform.GetComponentInParent<ChairSitScript>().lockpos = false;
            PlayerContainer.playerIsSit = false;
        }
    }

    [PunRPC]
    public void SyncToGravFalse(int gID)
    {
        PhotonView.Find(gID).gameObject.transform.GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
        PhotonView.Find(gID).gameObject.transform.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        PhotonView.Find(gID).gameObject.transform.GetComponent<Rigidbody>().useGravity = false;
        Debug.Log("usegrav = " + PhotonView.Find(gID).gameObject.transform.GetComponent<Rigidbody>().useGravity);
    }
    
    [PunRPC]
    public void SyncToGravTrue(int gID)
    {
        PhotonView.Find(gID).gameObject.transform.GetComponent<Rigidbody>().useGravity = true;
        Debug.Log("usegrav = " + PhotonView.Find(gID).gameObject.transform.GetComponent<Rigidbody>().useGravity);
    }
}