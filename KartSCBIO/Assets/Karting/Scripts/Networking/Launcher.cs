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
    private GameObject MapA;
    [SerializeField]
    private GameObject MapB;
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private TextMeshProUGUI TextStartGameInfo;
    [SerializeField]
    private List<TextMeshProUGUI> PlayerNames = new List<TextMeshProUGUI>();
    #endregion

    #region Private Fields
    string gameVersion = "1";
    #endregion

    #region MonoBehaviour CallBacks
    void Awake ()
    {
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
        if (!PhotonNetwork.IsMasterClient)
        {
            MapA.SetActive(false);
            MapB.SetActive(false);
        }
        if(PhotonNetwork.PlayerList.Length > 0) 
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
        if(PhotonNetwork.PlayerList.Length > 5) 
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
        PhotonNetwork.CurrentRoom.IsOpen = false;
        StartCoroutine(WaitLoadLevel());
    }
    IEnumerator WaitLoadLevel()
    {
        float time;
        time = 5f;
        TextStartGameInfo.gameObject.SetActive(true);
        while(time > 0f)
        {   
            time -= Time.deltaTime;
            TextStartGameInfo.text = "La partida empiza en " + Mathf.Ceil(time).ToString() + "...";
            yield return null;
        }
        PhotonNetwork.LoadLevel(configuration.SceneRacing);
    }
    #endregion
}
