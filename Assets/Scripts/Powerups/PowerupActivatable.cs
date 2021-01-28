using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PowerupActivatable : Activatable
{
    public PowerupType type;

    private Tilemap tilemap;

    private bool toDestroy = false;

    private void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
    }

    private void Update()
    {
        // just to avoid error messages
        if (toDestroy)
        {
            tilemap.SetTile(tilemap.WorldToCell(transform.position), null);
        }
    }

    public override void Action(PlayerController playerController)
    {
        if (tilemap != null)
        {
            playerController.AddPowerup(type);
            toDestroy = true;
        }
    }
}
