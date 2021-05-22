using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon;
using Photon.Realtime;
using KartGame.KartSystems;
using Cinemachine;
public class RespawnPlayersMP : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private List<Transform> PointsRespawn = new List<Transform>();
    [HideInInspector] public List<ArcadeKart> karts = new List<ArcadeKart>();

    private void Awake() 
    {
        GameObject Player = PhotonNetwork.Instantiate("Jugador",PointsRespawn[PhotonNetwork.LocalPlayer.ActorNumber - 1].position,Quaternion.identity);
    }
}