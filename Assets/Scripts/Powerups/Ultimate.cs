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
        playerController.ultimate.gameObject.SetActive(true);

        AudioController.Instance.Play(SoundId.Ultitame_start);
        float temp = AudioController.Instance.GetSoundLength(SoundId.Ultitame_start);
        AudioController.Instance.Play(SoundId.Ultitame_loop, temp);
    }

    public override void PowerupEnd(PlayerController playerController)
    {
        playerController.isUltimateActive = false;
        playerController.speedModifier = 1f;
        playerController.ultimate.gameObject.SetActive(false);
        playerController.AddPowerup(PowerupType.SHIELD);
        AudioController.Instance.Stop(SoundId.Ultitame_loop);
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