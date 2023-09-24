using UnityEngine;

[System.Serializable]

public class BagData
{
    public Sprite[] LootImages;
    public int[] CountLoot;

    public BagData (BagController bagController)
    {
        LootImages = bagController.LootImages;
        CountLoot = bagController.CountLoot;
    }
}
