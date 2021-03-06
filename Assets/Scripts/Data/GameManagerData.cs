﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "ScriptableObject/GameManagerData")]
public class GameManagerData : ScriptableObject
{
    public int pointsPerSecond = 0;
    public int pointsPerCoin = 0;
    public int pointsPerPowerup = 0;
    public float powerupMultiplier = 0f;

    public int lifelines = 1;

    public float scoreSpeedupInterval = 100f;
    public float speedupMultiplierPerInterval = 0.2f;

    public int coinsToUltimate = 2;
}
