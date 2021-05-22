using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using KartGame.KartSystems;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using System.Linq;
using UnityEngine.SceneManagement;

public class InGameMenuManagerMP : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text TextRank; 
    Player[] AllPlayersRank;
    private void Awake() 
    {
        AllPlayersRank = PhotonNetwork.PlayerList;
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("IntroMenu");
    }
    public void LeaveRoom()
    {
        Time.timeScale = 1f;
        PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveRoom();
    }
    public void RankRacers() 
    {
        bool swap = true;
        while(swap == true)
        {
            swap = false;
            for (int i = AllPlayersRank.Length - 1; i > 0; i--)
            {
                Debug.Log(AllPlayersRank[i].NickName);
                if((int)AllPlayersRank[i].CustomProperties["checkpointsPassed"] > (int)AllPlayersRank[i-1].CustomProperties["checkpointsPassed"]) 
                {
                    swap = true;
                    Player tempPlayer = AllPlayersRank[i-1];
                    AllPlayersRank[i-1] = AllPlayersRank[i];
                    AllPlayersRank[i] = tempPlayer;
                }
            }
        }
        int index = FindRankIndex(PhotonNetwork.LocalPlayer);
        TextRank.text = (index+1).ToString() + "º";
    }
    public int FindRankIndex(Player localPlayer) 
    {
        for (int i = 0; i < AllPlayersRank.Length; i++)
        {
            if(AllPlayersRank[i] == localPlayer)
            {
                return i;
            }
        }
        return 0;
    }
}
