using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupActivatable : Activatable
{
    public PowerupType type;

    public override void Action(PlayerController playerController)
    {
        playerController.AddPowerup(type);
    }
}
