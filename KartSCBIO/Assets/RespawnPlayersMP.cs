using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;
public class RespawnPlayersMP : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private List<Transform> PointsRespawn = new List<Transform>();

    private void Awake() 
    {
        GameObject Player = PhotonNetwork.Instantiate("Jugador",PointsRespawn[PhotonNetwork.LocalPlayer.ActorNumber - 1].position,Quaternion.identity);
    }
}