using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour, IRepeat
{
    [SortingMagic.SortingLayer]
    public string sortingLayer;

    public Sprite[] repeatSprites;

    public float bottomYMin = -5f;

    public int repeatObjects = 2;
    private SpriteRenderer[] repeats;

    private int repeatIndex = 0;

    private void Start()
    {
        repeats = new SpriteRenderer[repeatObjects];

        GameObject repGo = new GameObject("BackgroundRep");
        repGo.transform.SetParent(transform);
        repeats[0] = repGo.AddComponent<SpriteRenderer>();
        repeats[0].spriteSortPoint = SpriteSortPoint.Pivot;
        repeats[0].sortingLayerName = sortingLayer;
        repeats[0].sprite = repeatSprites[0];
        repGo.AddComponent<BoxCollider2D>();

        Vector3 repPos = repGo.transform.position;
        repPos.y = bottomYMin;
        repGo.transform.position = repPos;

        GameObject prevGo = repGo;
        for (int i = 1; i < repeats.Length; i++)
        {
            GameObject newGo = GameObject.Instantiate(repGo);
            newGo.transform.SetParent(transform);
            repeats[i] = newGo.GetComponent<SpriteRenderer>();
            repeats[i].sprite = repeatSprites[Random.Range(0, repeatSprites.Length)];

            Vector3 newPos = prevGo.transform.position;
            newPos.y += repeats[i - 1].sprite.bounds.max.y;
            newGo.transform.position = newPos;

            prevGo = newGo;
        }
    }

    public void SwapRepeat()
    {
        int forPos = mod(repeatIndex - 1, repeats.Length);

        Vector3 newPos = repeats[forPos].transform.position;
        newPos.y += repeats[forPos].sprite.bounds.max.y;
        repeats[repeatIndex].transform.position = newPos;

        repeats[repeatIndex].sprite = repeatSprites[Random.Range(0, repeatSprites.Length)];

        repeatIndex = mod(repeatIndex + 1, repeats.Length);
    }

    private int mod(int value, int mod)
    {
        if (value >= 0)
            return value % mod;
        return (mod + (value % mod)) % mod;
    }
}
