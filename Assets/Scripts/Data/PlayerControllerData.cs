using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/Player")]
public class PlayerControllerData : ScriptableObject
{
    public float climbSpeed = 1f;
    public float speed = 1f;

    public float positionMinX = -4f;
    public float positionMaxX = 5f;
}
