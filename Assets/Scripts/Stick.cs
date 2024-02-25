using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public string Name
    {
        get
        {
            return "Stick";
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
        //TODO: Make it so that stick is visible in front of player and that hasStone in movement is true
        gameObject.SetActive(false);
    }
}
