using UnityEngine;

public class Item : MonoBehaviour, IInteractable {


    [SerializeField] private ItemSO itemSO;


    public string GetInteractText() {
        return "Pick up " + itemSO.itemName;
    }

    public void Interact(AdventurePlayer adventurePlayer) {
        adventurePlayer.GetInventory().AddItemSO(itemSO);
        Destroy(gameObject);
    }

    public Transform GetTransform() => transform;

}