using UnityEngine;

/// <summary>
/// This class inherits from TargetObject and represents a LapObject.
/// </summary>
public class LapObject : TargetObject
{
    [Header("LapObject")]
    [Tooltip("Is this the first/last lap object?")]
    public bool finishLap;

    [HideInInspector]
    public bool lapOverNextPass;

    void Start() {
        Register();
    }
    
    void OnEnable()
    {
        lapOverNextPass = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!((layerMask.value & 1 << other.gameObject.layer) > 0) && !other.CompareTag("Player"))
            return;

        /*KartGame.KartSystems.ArcadeKart m_kart = other.GetComponentInParent<KartGame.KartSystems.ArcadeKart>();
        if(finishLap) 
        {
            m_kart.m_currentCheckpoint = 0;
            m_kart.m_startCounting = true;
        }
        bool flag = this.gameObject == m_kart.CheckpointsRanks[m_kart.m_currentCheckpoint].gameObject;
        if(flag && m_kart.m_startCounting)
        {
            m_kart.m_currentCheckpoint += 1;
            m_kart.m_checkpointsReach += 1;
            Objective.OnUpdateRanks?.Invoke(this);
        }*/
        Objective.OnUnregisterPickup?.Invoke(this);
    }
}