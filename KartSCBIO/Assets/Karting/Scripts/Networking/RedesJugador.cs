using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RedesJugador : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] CodigosIgnorar;
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if(!photonView.IsMine)
        {
            foreach (var codigo in CodigosIgnorar)
            {
                codigo.enabled = false;
            }
        }   
    }
}
