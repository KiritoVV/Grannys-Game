using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinteraction : MonoBehaviour
{
    PlayerController playerController;

    Land selectedLand = null;

    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
    }

    // What happens when the ineractions raycast hits something interactable
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        
        if (other.tag == "Land")
        {
            Land land = other.GetComponent<Land>();

            SelectLand(land);
            return;
        }

        if(selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;
        }
    }

    void SelectLand(Land land)
    {
        if (selectedLand != null)
        {
            selectedLand.Select(false);
        }

        selectedLand = land;
        land.Select(true);
    }

    public void Interact()
    {
       // Check if the player is selecting any land
       if(selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }

        Debug.Log("not on any land");
    }
}
