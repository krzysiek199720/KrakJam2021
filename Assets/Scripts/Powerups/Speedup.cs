using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedup : Powerup
{
    public SpeedupData speedupData;
    public override void PowerupStart(PlayerController playerController)
    {
        if (playerController.powerups.ContainsKey(PowerupType.ULTIMATE))
        {
            playerController.powerups.Remove(PowerupType.SPEEDUP);
            return;
        }
        playerController.speedModifier = speedupData.speedModifier;
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.speedModifier = 1f;
    }

    public override void AddTime()
    {
        TimeActiveLeft += speedupData.timeActive;
    }
}
