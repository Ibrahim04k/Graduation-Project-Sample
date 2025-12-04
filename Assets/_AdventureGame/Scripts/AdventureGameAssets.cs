using System;
using UnityEngine;

public class AdventureGameAssets : MonoBehaviour {


    public static AdventureGameAssets Instance { get; private set; }


    private void Awake() {
        Instance = this;
    }


    public Items items;


    [Serializable]
    public class Items {

        public ItemSO cheese;

    }

}