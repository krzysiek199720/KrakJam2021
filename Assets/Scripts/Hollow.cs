using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hollow : Activatable
{
    public HollowData hollowData;

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


        GameManager.Instance.TakeLifeline(hollowData.damage);
        AudioController.Instance.Play(SoundId.Obstacle_hollow);
    }
}
