using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;

public class OfficeManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab; // ������ ������

    void Start()
    {
        //for (int i = 0; i < 5; i++)
            //LogText.text += arraylog[i] + "\n";
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)); // ��������� ������� �� ����
        PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity); // Instantiate ��������� �� ���������������� �������� ��������
    }

    public void Leave() // ������� ��� ������ �� �������
    {
        PhotonNetwork.LeaveRoom();
    }
 
    public override void OnLeftRoom() // ����������� ����� ������� ����� (��) �������� �������
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) // ����������� ����� ����� ������ � �������
    {
        Debug.Log("Player " + newPlayer.NickName + " entered room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) // ����������� ����� ����� ������� �� �������
    {
        Debug.Log("Player " + otherPlayer.NickName + " left room");
    }
}
