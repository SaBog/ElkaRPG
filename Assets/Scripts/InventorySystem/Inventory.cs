using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Inventory : MonoBehaviour
    {
        public Transform InventoryRoot;
        public Transform EquipmentRoot;
        public Slot SlotPrefab;

        public int capacity;
        public List<Slot> Slots;

        public List<EquipmentSlot> EquipmentList;

        private void Awake()
        {
            Slots = InventoryRoot.GetComponentsInChildren<Slot>()
                .Select(x =>
                {
                    x.Inventory = this;
                    return x;

                }).ToList();

            EquipmentList = EquipmentRoot.GetComponentsInChildren<EquipmentSlot>()
                .Select(x =>
                {
                    x.Inventory = this;
                    return x;

                }).ToList();

            // load saved data
            Load();
        }

        public bool HasSpaceForItem(SlotItem item)
        {
            return true;
            if (Slots.Count < capacity)
            {
            }

            if (item.data.IsStackable)
            {
                var itemsInInventory = Slots.FindAll(slot => slot.Equals(item));
                return itemsInInventory.Exists(slot => slot.IsFree);
                //itemInInventory.s
            }

            if (Slots.Count < capacity)
            {
                return true;
            }

            return false;

        }

        public bool Put(SlotItem item)
        {
            if (!item.data.IsStackable)
            {
                return PutUnStackableInFreeSlot(item);
            }

            // stackable realization
            // fill slots that already has this item
            var slotsWithItem = Slots.FindAll(slot => slot.Equals(item));
            var maxInStack = item.data.maxStack;
            var count = item.Count;

            foreach (var slot in slotsWithItem)
            {
                var delta = maxInStack - slot.Item.Count;

                if (count < delta)
                {
                    delta = count;
                }

                count -= delta;

                // animate

                slot.Item.Count += delta;

                if (count == 0)
                {
                    break;
                }
            }

            // update count 
            item.Count = count;

            if (count == 0)
            {
                return true;
            }

            // use free slots
            return PutStackableInFreeSlot(item);
        }

        private bool PutStackableInFreeSlot(SlotItem item)
        {
            var count = item.Count;
            var maxInStack = item.data.maxStack;

            if (count > 0)
            {
                var slots = Slots.FindAll(slot => slot.IsFree);

                // put in free slot

                foreach (var slot in slots)
                {
                    var newInstance = Instantiate(item);

                    var delta = count < maxInStack ? count : maxInStack;
                    count -= delta;

                    newInstance.Count = delta;
                    slot.AttachItem(newInstance);

                    if (count == 0)
                    {
                        break;
                    }
                }
            }

            return count > 0;

        }

        private bool PutUnStackableInFreeSlot(SlotItem item)
        {
            // always one (not stacked) - make item splitting by factory method
            // if set count more than max stack size 
            var slot = Slots.FirstOrDefault(slot => slot.IsFree);

            if (slot)
            {
                slot.AttachItem(item);
                return true;
            }

            return false;
        }

        public bool Pop(SlotItem item)
        {
            throw new System.NotImplementedException();
        }

        public void Swap(Slot slot1, Slot slot2)
        {
            var item1 = slot1.Item;
            var item2 = slot2.Item;

            slot1.DetachItem();
            slot2.DetachItem();

            slot1.AttachItem(item2);
            slot2.AttachItem(item1);
        }

        public void Move(SlotItem item, Slot to)
        {
            if (item.Slot == null || item.Slot == to)
            {
                return;
            }

            Swap(item.Slot, to);
        }

        #region Saving

        private readonly string SaveFileName = "InventoryItems";

        public void Save()
        {
            var items = Slots.Select(x => x.Item);
            SaveSystem.Save(items, SaveFileName);
        }

        public void Load()
        {
            var items = SaveSystem.Load<IEnumerable<SlotItem>>(SaveFileName);

            if (items is null || items.Count() == 0)
            {
                Debug.Log("Saved inventory was empty");
                return;
            }

            foreach (var item in items)
            {
                var putted = Put(item);

                if (!putted)
                {
                    Debug.LogWarning($"Player lost {item.data.name} x {item.Count}");
                }
            }
        }

        #endregion
    }

}
