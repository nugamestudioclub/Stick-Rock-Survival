using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [Header("Reticle Stuff")]
    public Image reticleImage;
    public Color itemColor;
    public Color enemyColor;
    
    Color originalreticleColor;

    [Header("Sound Effects")]
    public AudioClip collectSFX;
    public AudioClip attackSFX;

    [Header("Inventory")]
    public Inventory inventory;

    void Start()
    {
        originalreticleColor = reticleImage.color;
    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    /**
     * Changes the color of the reticle based on what they are looking at (needs range other than Infinity)
     * If looking at item -> reticleColor = green
     * Else if looking at item -> reticleColor = red
     * Else -> reticleColor = normal
     */
    void ReticleEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Item"))
            {
                Debug.Log("Press left Click to collect the item");
                reticleImage.color = Color.Lerp(reticleImage.color, itemColor, Time.deltaTime * 2);
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Thats an enemy");
                reticleImage.color = Color.Lerp(reticleImage.color, enemyColor, Time.deltaTime * 2);
            }
            else
            {
                reticleImage.color = Color.Lerp(reticleImage.color, originalreticleColor, Time.deltaTime * 2);
            }
        }
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color, originalreticleColor, Time.deltaTime * 2);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
        }
    }
}
