using System.Collections;
using System.Collections.Generic;
using KartGame.KartSystems;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    public GameFlowManager gameFlowManager;
    List<Sprite> IconSprites = new List<Sprite>();
    ArcadeKart player;
    private void Start() 
    {
        player = GetComponent<ArcadeKart>();
        IconSprites = gameFlowManager.IconSprites;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("MysteryBox"))
        {
            int item = Random.Range(0,IconSprites.Count - 1);
            switch (item)
            {
                case 0:
                player.item = ArcadeKart.Items.Banana;
                break;
                case 1:
                player.item = ArcadeKart.Items.RedShell;
                break;
                case 2:
                player.item = ArcadeKart.Items.GreenShell;
                break;
                case 3:
                player.item = ArcadeKart.Items.BoostSpeed;
                break;
                case 4:
                player.item = ArcadeKart.Items.LowSpeed;
                break;
                default:
                player.item = ArcadeKart.Items.None;
                gameFlowManager.ImageItem.sprite = IconSprites[5];
                break;
            }
            if(gameObject.CompareTag("Player"))
                gameFlowManager.ImageItem.sprite = IconSprites[item];
            player.m_canThrow = true;
            Destroy(other.gameObject);
        }
    }
}
