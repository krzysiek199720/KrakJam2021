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
        if (GameManager.Instance.Lifelines > 1)
        {
            playerController.powerups.Remove(PowerupType.SPEEDUP);
            GameManager.Instance.TakeLifeline(1);
            return;
        }

        playerController.speedModifier = speedupData.speedModifier;
        playerController.bees_swarm.gameObject.SetActive(true);
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.speedModifier = 1f;
        playerController.bees_swarm.gameObject.SetActive(false);
    }

    public override void AddTime()
    {
        TimeActiveLeft += speedupData.timeActive;
    }
}
