using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Coin : Activatable
{
    public CoinData coinData;

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

    public override void Action()
    {
        Debug.Log("Coin");
        if(tilemap != null)
        {
            GameManager.Instance.AddScore(coinData.coinsToAdd);
            toDestroy = true;
        }
    }
}
