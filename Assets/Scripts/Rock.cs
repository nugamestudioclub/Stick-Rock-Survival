using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Rock";
        }
    }

    public Sprite image;

    public Sprite Image
    {
        get
        {
            return image;
        }
    }

    public void OnPickup()
    {
        //TODO: Make it so that rock is visible in front of player and that hasStone in movement is true
        gameObject.SetActive(false);
    }
}
