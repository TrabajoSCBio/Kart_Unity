using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public ConfigurationRace configuration;
    public GameObject MysteryBox;
    public float timeSpawn;
    [HideInInspector] public bool canInstantiate = false;
    private GameObject MysteryBoxGame;
    private float time;
    private void OnEnable() 
    {
        if(!configuration.activeObjects) 
        {
            this.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MysteryBoxGame = Instantiate(MysteryBox, transform.position, transform.localRotation,transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(canInstantiate)
        {
            time += Time.deltaTime;
            if(time >= timeSpawn) {
                MysteryBoxGame = Instantiate(MysteryBox, transform.position, transform.localRotation,transform);
                canInstantiate = false;
            }
        } else 
        {
            time = 0f;
        }
    }
}
