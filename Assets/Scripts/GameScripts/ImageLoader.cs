using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class ImageLoader : MonoBehaviour, IPunObservable
{
    int indexator = 0;
    private Texture2D[] slides;
    private Renderer thisRenderer;
    private PhotonView _photonView;
    
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        string url;
        int countOfFiles = new DirectoryInfo(Application.dataPath + "/StreamingAssets/").GetFiles("*.jpg").Length;
        slides = new Texture2D[countOfFiles];
        Debug.Log(countOfFiles + " !!!!!!!!!!!!!!!!!!!!фыв");
        
        for (int i = 0; i < countOfFiles; i++)
        {
            url = "file:///" + Application.dataPath + $"/StreamingAssets/{i}.jpg";
            StartCoroutine(LoadFromLikeCoroutine(i, url));
        }
        
        thisRenderer = GetComponent<Renderer>();
    }

    private IEnumerator LoadFromLikeCoroutine(int i, string url)
    {
        Debug.Log("Loading...");
        WWW wwwLoader = new WWW(url);   // Создает WWW файл из url.
        yield return wwwLoader;         // Начинает загрузку файла.
        
        Debug.Log("Loaded");
        slides[i] = wwwLoader.texture; // textures2D[i] = wwwLoader.texture
        thisRenderer.material.mainTexture = slides[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _photonView.RPC("SyncLeft", RpcTarget.MasterClient);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _photonView.RPC("SyncRight", RpcTarget.MasterClient);
        }

        thisRenderer.material.mainTexture = slides[indexator];
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(indexator);
        }
        else if (stream.IsReading)
        {
            indexator = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void SyncRight()
    {
        indexator++;
        if (indexator > slides.Length - 1) indexator = 0;
        thisRenderer.material.mainTexture = slides[indexator];
    }

    [PunRPC]
    void SyncLeft()
    {
        indexator--;
        if (indexator < 0) indexator = slides.Length - 1;
        thisRenderer.material.mainTexture = slides[indexator];
    }
}