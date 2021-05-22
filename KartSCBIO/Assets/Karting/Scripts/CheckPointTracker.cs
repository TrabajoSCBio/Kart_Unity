using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
public class CheckPointTracker : MonoBehaviour
{
    public bool isMultiplayer;
    private GameFlowManager GameFlowManager;
    private InGameMenuManagerMP GMMP;
    [HideInInspector] public int checkpointsPassed;
    [HideInInspector] public bool startCounting;
    [HideInInspector] public int currentCheckpoint;
    private List<Collider> CheckpointsRanks = new List<Collider>();
    Collider nextCheckpoint;
    private void Start() 
    {
        if(isMultiplayer) 
        {
            if(this.GetComponent<PhotonView>().IsMine)
            {
                Hashtable hash = new Hashtable();
                hash.Add("checkpointsPassed",0);
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            }
        }
        GMMP = FindObjectOfType<InGameMenuManagerMP>();
        GameFlowManager = FindObjectOfType<GameFlowManager>();
        checkpointsPassed = 0;
        currentCheckpoint = 0;
        CheckpointsRanks = GameFlowManager.CheckpointsRanks;
        nextCheckpoint = CheckpointsRanks[currentCheckpoint];
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(nextCheckpoint.transform == other.transform && other.CompareTag("Checkpoint")) 
        {
            if(currentCheckpoint < CheckpointsRanks.Count - 1) 
            {
                currentCheckpoint++;
            } else 
            {
                currentCheckpoint = 0;
            }
            nextCheckpoint = CheckpointsRanks[currentCheckpoint];
            if(!isMultiplayer)
            {
                checkpointsPassed++;
                GameFlowManager.RankRacers(isMultiplayer);
            } else
            {
                if(this.GetComponent<PhotonView>().IsMine)
                {
                    int checkpointsPassedMP = (int)PhotonNetwork.LocalPlayer.CustomProperties["checkpointsPassed"];
                    checkpointsPassedMP++;
                    Hashtable hash = new Hashtable();
                    hash.Add("checkpointsPassed",checkpointsPassedMP);
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
                    GMMP.RankRacers();
                }
            }
        }
    }
}
