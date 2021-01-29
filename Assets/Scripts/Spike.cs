using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spike : Activatable
{
    public SpikeData spikeData;

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
        Debug.Log("Spike");
        if (playerController.isUltimateActive)
        {
            if (tilemap != null)
            {
                toDestroy = true;
            }
            return;
        }

        if (playerController.isGhost)
            return;


        GameManager.Instance.TakeLifeline(spikeData.damage);
    }
}
