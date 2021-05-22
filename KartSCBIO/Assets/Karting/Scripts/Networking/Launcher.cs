using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 9;
    [SerializeField]
    private ConfigurationRace configuration;
    [SerializeField]
    private GameObject MultiplayerPanel;
    [SerializeField]
    private GameObject DisconnectPanel;
    [SerializeField]
    private GameObject ButtonOK;
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private List<TextMeshProUGUI> PlayerNames = new List<TextMeshProUGUI>();
    [SerializeField]
    private byte minPlayersToStart;
    #endregion

    #region Private Fields
    string gameVersion = "1";
    #endregion

    #region MonoBehaviour CallBacks
    void Awake ()
    {
        MultiplayerPanel.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    #endregion

    #region Public Methods
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
        DisconnectPanel.GetComponentInChildren<Button>().gameObject.SetActive(false);
        DisconnectPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Conectando...";
        DisconnectPanel.SetActive(true);
        StartButton.SetActive(false);
        PhotonNetwork.GameVersion = gameVersion;
    }

    public void Disconnect() 
    {
        MultiplayerPanel.SetActive(false);
        PhotonNetwork.Disconnect();
        Debug.Log("Te has desconectado");
    }
    #endregion

    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("Te has conectado");
        DisconnectPanel.SetActive(false);
        MultiplayerPanel.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        if(PhotonNetwork.IsMasterClient)
        {
            foreach (var item in PlayerNames)
            {
                item.text = "Buscando jugadores...";
            }
        }
        PhotonNetwork.NickName = configuration.Nickname;
        Debug.Log("OnConnectedToMaster()");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat(" OnDisconnected() was called by PUN with reason {0}", cause);
        if(DisconnectPanel && ButtonOK) 
        {
            DisconnectPanel.SetActive(true);
            ButtonOK.SetActive(true);
            if(cause == DisconnectCause.DisconnectByClientLogic) 
            {
                DisconnectPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Te has desconectado";
            } else if(cause == DisconnectCause.DnsExceptionOnConnect) 
            {
                DisconnectPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Sin conexión a internet";
            } else if(cause == DisconnectCause.ClientTimeout)
            {
                DisconnectPanel.GetComponentInChildren<TextMeshProUGUI>().text = "No se ha podido conectar";
            }
        }
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("On JoinRandomFailed()");
        string room = "Sala " + (PhotonNetwork.CountOfRooms + 1).ToString();
        PhotonNetwork.CreateRoom(room, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(!newPlayer.IsMasterClient)
        {
            Debug.Log(newPlayer.NickName);
            PlayerNames[PhotonNetwork.PlayerList.Length - 1].text = newPlayer.NickName;
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        for (int i = 0; i < PlayerNames.Count; i++)
        {
            if(i < PhotonNetwork.PlayerList.Length)
            {
                PlayerNames[i].text = PhotonNetwork.PlayerList[i].NickName;
            } else 
            {
                PlayerNames[i].text = "Buscando jugadores...";
            }
        }
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LocalPlayer.NickName = configuration.Nickname;
        if(PhotonNetwork.PlayerList.Length > minPlayersToStart && PhotonNetwork.IsMasterClient) 
        {
            if(!StartButton.activeSelf)
                StartButton.SetActive(true);
        } else 
        {
            if(StartButton.activeSelf)
                StartButton.SetActive(false);
        }
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            PlayerNames[i].text = PhotonNetwork.PlayerList[i].NickName;
        }
    }
    public override void OnLeftRoom()
    {
        if(PhotonNetwork.PlayerList.Length > minPlayersToStart && PhotonNetwork.IsMasterClient) 
        {
            if(!StartButton.activeSelf)
                StartButton.SetActive(true);
        } else 
        {
            if(StartButton.activeSelf)
                StartButton.SetActive(false);
        }
    }
    public void SelectMap(string nameScene)
    {
        configuration.SceneRacing = nameScene;
    }
    public void LoadScene()
    {
        configuration.activeObjects = true;
        configuration.isTimed = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(configuration.SceneRacing);
    }
    #endregion
}
