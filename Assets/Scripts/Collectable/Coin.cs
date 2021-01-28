using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    public CoinData coinData;

    protected override void Action()
    {
        Debug.Log("Coin");
        GameManager.Instance.AddScore(coinData.coinsToAdd);
    }
}
