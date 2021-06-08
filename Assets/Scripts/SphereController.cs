using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private PhotonView photonView; // PhotonViev - �������� ����� ������ ���������� �� ������

    public float speed; // �������� ����
    
    void Start() 
    {
        photonView = GetComponent<PhotonView>(); // ����������� PhotonViev ������� ���������� ���
    }

    void Update()
    {
        if (!photonView.IsMine) return; // ��������� ���� �� photonViev (���� �� ��������� ��� ��������, �� ����� ��������� ��� ����)

        // ����������
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
