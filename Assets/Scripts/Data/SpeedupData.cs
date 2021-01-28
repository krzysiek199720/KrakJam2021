using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedupData", menuName = "ScriptableObject/Powerups/Speedup")]
public class SpeedupData : ScriptableObject
{
    public float speedModifier = 1.5f;
    public float timeActive = 1f;
}
