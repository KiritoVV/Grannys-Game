using UnityEngine;

public class Playerinteraction : MonoBehaviour
{
    PlayerController playerController;

    Land selectedLand = null;

    //The interactable object the player is currently holding
    InteractableObject SelectedInteractable   = null;
    void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }


    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
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

        if(other.tag == "Item")
        {
            SelectedInteractable = other.GetComponent<InteractableObject>();
            return;
        }

        if(SelectedInteractable != null)
        {
            SelectedInteractable = null;
        }

        if (selectedLand != null)
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
        //The player should not be able to use his tools when he has his hands full with an item
        if (InventoryManager.Instance.equippedItem != null)
        {
            return;
        }


        // Check if the player is selecting any land
        if (selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }

        Debug.Log("not on any land");
    }

    //Triggerred when the player presses the item interact button
    public void ItemInteract()
    {
        if(InventoryManager.Instance.equippedItem != null)
        {
            InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Item);
            return;
        }

        if (SelectedInteractable != null)
        {
            SelectedInteractable.Pickup();
        }
    }
}

