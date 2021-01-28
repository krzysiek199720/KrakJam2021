using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlowdownData", menuName = "ScriptableObject/Powerups/Slowdown")]
public class SlowDownData : ScriptableObject
{
    public float speedModifier = 1.5f;
    public float timeActive = 1f;
}
