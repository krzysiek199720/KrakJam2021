using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Activatable
{
    public SpikeData spikeData;
    
     public override void Action(PlayerController playerController)
    {
        Debug.Log("Spike");
        if (playerController.isGhost)
            return;
        GameManager.Instance.TakeLifeline(spikeData.damage);
    }
}
