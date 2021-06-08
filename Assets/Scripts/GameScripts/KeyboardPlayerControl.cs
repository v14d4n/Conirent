using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyboardPlayerControl : MonoBehaviour
{
    float speed = 3.5f;
    public Camera cam;
    PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (!photonView.IsMine) 
            Destroy(cam);
    }

    void Update()
    {
        if (!photonView.IsMine) return; // Проверка на свой фотон.

        if (!PlayerContainer.playerIsSit)
        {
            if (Input.GetKey(KeyCode.W))
                transform.position = transform.position + cam.transform.forward * Time.deltaTime * speed;
            if (Input.GetKey(KeyCode.S))
                transform.position = transform.position - cam.transform.forward * Time.deltaTime * (speed * 0.5f);
            if (Input.GetKey(KeyCode.D))
                transform.position = transform.position + cam.transform.right * Time.deltaTime * speed;
            if (Input.GetKey(KeyCode.A))
                transform.position = transform.position - cam.transform.right * Time.deltaTime * speed;

            transform.position = new Vector3(transform.position.x, 2.6f, transform.position.z); // Всегда держит игрока на высоте 2.6
        }

        if (!photonView.IsMine) return;
        transform.rotation = cam.transform.rotation; // Вращение головы
        cam.transform.position = transform.position; // 
    }
}
