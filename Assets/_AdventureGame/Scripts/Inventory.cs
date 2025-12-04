using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    public static Inventory Instance { get; private set; }


    public event EventHandler OnItemListChanged;


    private List<ItemSO> itemList;


    private void Awake() {
        Instance = this;

        itemList = new List<ItemSO>();
    }

    public List<ItemSO> GetItemList() {
        return itemList;
    }

    public void AddItemSO(ItemSO itemSO) {
        itemList.Add(itemSO);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool HasItem(ItemSO itemSO) {
        return itemList.Contains(itemSO);
    }

    public void RemoveItem(ItemSO itemSO) {
        itemList.Remove(itemSO);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

}