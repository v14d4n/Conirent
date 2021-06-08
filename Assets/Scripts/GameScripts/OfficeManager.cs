using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManagerCustom : MonoBehaviourPunCallbacks
{
    public Text LogText; // текст для логов
    public GameObject PlayerPrefab; // Префаб игрока
    static string[] arraylog = new string[5];

    void Start()
    {
        arraylog = LobbyManager.arraylog;
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), 2.6f, Random.Range(-5f, 5f)); // Случайная позиция на поле
        PhotonNetwork.Instantiate(PlayerPrefab.name, pos, Quaternion.identity); // Instantiate отвечает за синхронизируемое создание объектов
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
        Log("Player " + newPlayer.NickName + " entered room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) // Запускается когда игрок выходит из комнаты
    {
        Log("Player " + otherPlayer.NickName + " left room");
    }

    private void Log(string message) // Лог
    {
        Debug.Log(message);

        for (int i = 0; i < 4; i++)
            arraylog[i] = arraylog[i + 1];

        arraylog[4] = message; LogText.text = null;

        for (int i = 0; i < 5; i++)
            LogText.text += arraylog[i] + "\n";
    }
}
