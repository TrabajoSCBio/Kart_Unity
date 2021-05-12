using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuración", menuName = "ScriptableObjects/ConfiguracionCircuito", order = 1)]
public class ConfigurationRace : ScriptableObject
{
    public int laps;
    public int bots;
    public int timeLap;
    public int extraTime;
    public float maxSpeed;
    public bool activeObjects;
    public bool isTimed;
    public string SceneRacing;
}
