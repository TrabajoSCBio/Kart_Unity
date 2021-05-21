using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBots : MonoBehaviour
{
    [Header("ScriptableObjects")]
    public ConfigurationRace Configuration;
    [Header("Bots Prefabs")]
    public List<GameObject> Bots = new List<GameObject>();
    // Start is called before the first frame update
    void OnEnable()
    {
        for(int i = 0; i < Configuration.bots; i++) 
        {
            Bots[i].SetActive(true);
        }
    }
}
