using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTracker : MonoBehaviour
{
    private GameFlowManager GameFlowManager;
    [HideInInspector] public int checkpointsPassed;
    [HideInInspector] public bool startCounting;
    [HideInInspector] public int currentCheckpoint;
    private List<Collider> CheckpointsRanks = new List<Collider>();
    Collider nextCheckpoint;
    private void Start() 
    {
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
            checkpointsPassed++;
            GameFlowManager.RankRacers();
        }
    }
}
