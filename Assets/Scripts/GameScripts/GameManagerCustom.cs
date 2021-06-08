using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;

public class OfficeManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab; // Префаб игрока

    void Start()
    {
        //for (int i = 0; i < 5; i++)
            //LogText.text += arraylog[i] + "\n";
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-5f, 5f)); // Случайная позиция на поле
        PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity); // Instantiate отвечаети за синхронизируемое создание объектов
    }

    public void Leave() // Функция для выхода их комнаты
    {
        PhotonNetwork.LeaveRoom();
    }
 
    public override void OnLeftRoom() // Запускается когда текущий игрок (мы) покидает комнату
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) // Запускается когда игрок входит в комнату
    {
        Debug.Log("Player " + newPlayer.NickName + " entered room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) // Запускается когда игрок выходит из комнаты
    {
        Debug.Log("Player " + otherPlayer.NickName + " left room");
    }
}
