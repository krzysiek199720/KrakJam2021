using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour, IRepeat
{
    public string sortingLayer;

    public GameObject bgFirst;

    public Sprite bottomSprite;
    public Sprite[] repeatSprites;

    public float bottomYMin = -5f;

    public int repeatObjects = 2;
    private SpriteRenderer bottom;
    private SpriteRenderer[] repeats;

    private int repeatIndex = -1;

    private void Start()
    {
        GameObject bottomgo = new GameObject("Bottom");
        bottomgo.transform.SetParent(transform);
        bottom = bottomgo.AddComponent<SpriteRenderer>();
        bottom.spriteSortPoint = SpriteSortPoint.Pivot;
        bottom.sortingLayerName = sortingLayer;
        bottom.sortingOrder = 1;
        bottom.sprite = bottomSprite;
        bottomgo.AddComponent<BoxCollider2D>();

        Vector3 botPos = bottomgo.transform.position;
        botPos.y = bottomYMin;
        bottomgo.transform.position = botPos;

        repeats = new SpriteRenderer[repeatObjects];

        GameObject repGo = new GameObject("BackgroundRep");
        repGo.transform.SetParent(transform);
        repeats[0] = repGo.AddComponent<SpriteRenderer>();
        repeats[0].spriteSortPoint = SpriteSortPoint.Pivot;
        repeats[0].sortingLayerName = sortingLayer;
        repeats[0].sprite = repeatSprites[0];
        repGo.AddComponent<BoxCollider2D>();

        Vector3 repPos = repGo.transform.position;
        repPos.y = bottom.transform.position.y + bottom.sprite.bounds.max.y;
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
        if (repeatIndex == -1)
        {
            bottom.gameObject.SetActive(false);
            bgFirst.SetActive(false);
            repeatIndex++;
            return;
        }

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
