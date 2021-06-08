using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Networking;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField roomName;
    string office;
    public Text LogText, officeText;
    public InputField nickName;
    public static string[] arraylog = new string[5];

    void Start()
    {
        if (PhotonNetwork.NickName == "")
        {
            PhotonNetwork.NickName = "User " + Random.Range(1000, 9999); // Задает имя игроку
            Log("Player's name is set to " + PhotonNetwork.NickName); // Лог
            PhotonNetwork.AutomaticallySyncScene = true; // Автоматическая синхронизация сцены
            PhotonNetwork.GameVersion = "0.01"; // Версия игры
            PhotonNetwork.ConnectUsingSettings(); // Конечное задание настроек фотону и подключение к мастер серверу
        }
        
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(roomList.Count); // дрянь такая
    }

    public void ChangeOffice(int selectedOffice) // Меняет интерьер по нажатию кнопки
    {
        office = "room" + selectedOffice;
        //officeText.text = "Выбран интерьер: " + selectedOffice;
        Log("office changed to " + office);
    }

    public override void OnConnectedToMaster() // Вызывается при подключении к мастер-серверу
    {
        Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Log("Joined to lobby");
    }

    public void ChangeNickName(string nickname)
    {
        PhotonNetwork.NickName = nickName.text;
        if (PhotonNetwork.NickName == "") PhotonNetwork.NickName = "User " + Random.Range(1000, 9999);
        Log("Player's name is set to " + PhotonNetwork.NickName);
    }
    

    public void CreateRoom() // Создание комнаты
    {
        if (office != null)
        {
            PhotonNetwork.CreateRoom(roomName.text, new Photon.Realtime.RoomOptions { MaxPlayers = 10 });
        }
        else
        {
            Log("!!! Office dose not exist, can't create room");
        }
    }

    public void JoinRoom() // Подключение к комнате
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) // Вызывается если невозможно присоедениться к случайной комнате
    {
        Log("!!! Can't find a room");
    }

    public override void OnJoinedRoom() // Вызывается при подключении к комнате
    {
        Log("Joined the room");
        PhotonNetwork.LoadLevel(office);
    }

    private void Log(string message) // Лог
    {
        Debug.Log(message);

        for (int i = 0; i < 4; i++)
            arraylog[i] = arraylog[i + 1];

        arraylog[4] = message; LogText.text = null;

        for (int i = 0; i < 5; i++)
        {
            LogText.text += arraylog[i] + "\n";
        }
    }
    
    
    
    
    public enum MenuStates
    {
        Main,
        Avatar,
        Create,
        Options
    };

    public MenuStates currentstate;

    void Awake()
    {
        currentstate = MenuStates.Main;
    }

    public GameObject mainMenu;
    public GameObject avatarMenu;
    public GameObject createMenu;
    public GameObject optionsMenu;
    
    private void Update()
    {
        switch (currentstate)
        {
            
            case MenuStates.Main:
                mainMenu.SetActive(true);
                avatarMenu.SetActive(false);
                createMenu.SetActive(false);
                optionsMenu.SetActive(false);
                break;
            
            case MenuStates.Avatar:
                mainMenu.SetActive(false);
                avatarMenu.SetActive(true);
                createMenu.SetActive(false);
                optionsMenu.SetActive(false);
                break;
            
            case MenuStates.Create:
                mainMenu.SetActive(false);
                avatarMenu.SetActive(false);
                createMenu.SetActive(true);
                optionsMenu.SetActive(false);
                break;
                
            case MenuStates.Options:
                mainMenu.SetActive(false);
                avatarMenu.SetActive(false);
                createMenu.SetActive(false);
                optionsMenu.SetActive(true);
                break;
            
        }
    }

    public void OnMainMenu()
    {
        Debug.Log("You are in main menu");
        currentstate = MenuStates.Main;
    }

    public void OnAvatarMenu()
    {
        Debug.Log("You are in avatar menu");
        currentstate = MenuStates.Avatar;
    }

    public void OnCreateMenu()
    {
        Debug.Log("You are in create menu");
        currentstate = MenuStates.Create;
    }

    public void OnOptionsMenu()
    {
        Debug.Log("You are in options menu");
        currentstate = MenuStates.Options;
    }
}
