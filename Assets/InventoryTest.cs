using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Inventory Inventory;
    public List<SlotItem> items;

    private void Start()
    {
        items.ForEach(item => Inventory.Put(item));
    }

}
