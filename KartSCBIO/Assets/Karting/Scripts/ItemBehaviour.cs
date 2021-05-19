using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using KartGame.KartSystems;

public class ItemBehaviour : MonoBehaviour
{
    public bool canChase;
    public bool ant;
    public bool shell;
    [HideInInspector] public Transform PlayerObjective;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        if(!canChase)
        {
            Destroy(this.gameObject,20f);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(canChase) 
        {
            agent.destination = PlayerObjective.position;
            if(Vector3.Distance(transform.position,PlayerObjective.position) <= 2.5f)
            {
                PlayerObjective.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        ArcadeKart kart = null;
        if(other.CompareTag("Player") || other.gameObject.layer == 13) 
        {
            Debug.Log(other.gameObject);
            Debug.Log("COLISION");
            kart = other.GetComponentInParent<ArcadeKart>();
            if(ant)
            {
                kart.SetCanSlowSpeed(true);
                Destroy(this.gameObject);
            } else if(!shell) 
            {
                kart.SetIfHit(true);
                kart.SetCanMove(false);
                Destroy(this.gameObject);
            } else if(shell && kart.gameObject == PlayerObjective.gameObject) 
            {
                kart.SetIfHit(true);
                kart.SetCanMove(false);
                Destroy(this.gameObject);
            }
        }
    }
    private void OnDestroy() 
    {
        if(shell)
            PlayerObjective.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
    }
}
