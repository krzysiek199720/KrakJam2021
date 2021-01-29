using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : Powerup
{
    public UltimateData ultimateData;

    public override void PowerupStart(PlayerController playerController)
    {
        playerController.isUltimateActive = true;
        playerController.speedModifier = ultimateData.speedMultiplier;

        //remove other speed modifiers
        playerController.powerups.Remove(PowerupType.SLOWDOWN);
        playerController.powerups.Remove(PowerupType.SPEEDUP);
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.isUltimateActive = false;
        playerController.speedModifier = 1f;
    }

    public override void AddTime()
    {
        TimeActiveLeft += ultimateData.timeActive;
    }

    public float GetPercentDone()
    {
        return 100 * (TimeActiveLeft / ultimateData.timeActive);
    }
}