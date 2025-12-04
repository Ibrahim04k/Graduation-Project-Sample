using UnityEngine;
using UnityEngine.UI;

public class InventoryUI_Slot : MonoBehaviour {


    [SerializeField] private Image image;


    public void SetItemSO(ItemSO itemSO) {
        if (itemSO == null) {
            image.enabled = false;
        } else {
            image.enabled = true;
            image.sprite = itemSO.sprite;
        }
    }

}