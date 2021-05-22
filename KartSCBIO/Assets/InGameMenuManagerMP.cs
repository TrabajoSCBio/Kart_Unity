using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class InGameMenuManagerMP : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("IntroMenu");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveRoom();
    }
}
