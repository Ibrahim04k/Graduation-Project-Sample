using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {


    [SerializeField] private InventoryUI_Slot[] slotArray;


    private void Start() {
        Inventory.Instance.OnItemListChanged += Inventory_OnItemListChanged;

        UpdateVisual();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        List<ItemSO> itemList = Inventory.Instance.GetItemList();

        foreach (InventoryUI_Slot slot in slotArray) {
            slot.SetItemSO(null);
        }

        for (int i = 0; i < itemList.Count; i++) {
            ItemSO itemSO = itemList[i];
            slotArray[i].SetItemSO(itemSO);
        }
    }

}