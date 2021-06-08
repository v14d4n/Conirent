using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private PhotonView photonView; // PhotonViev - основной класс фотона отвечающий за синхру

    public float speed; // —корость куба
    
    void Start() 
    {
        photonView = GetComponent<PhotonView>(); // ѕрисваивает PhotonViev клиенту создавшему его
    }

    void Update()
    {
        if (!photonView.IsMine) return; // ѕровер€ет свой ли photonViev (если не поставить эту проверку, то будут двигатьс€ все кубы)

        // ”правление
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.forward * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.back * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * Time.deltaTime * speed;

        if (transform.position.y < 0.45)
            transform.position = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f));
    }
}
