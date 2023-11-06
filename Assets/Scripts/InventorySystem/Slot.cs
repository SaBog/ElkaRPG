using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        public Inventory Inventory;
        [NonSerialized]
        public SlotItem Item;

        public bool IsFree => Item == null;

        public void OnDrop(PointerEventData eventData)
        {
            // Get the dropped item
            // Check if there is a dropped item
            if (eventData.pointerDrag
                .TryGetComponent<SlotItem>(out var dragItem))
            {
                // Check if there is no item in the slot
                if (CanTakeItem(dragItem))
                {
                    // attach item
                    Inventory.Move(dragItem, this);
                }
            }
        }

        public virtual bool CanTakeItem(SlotItem item)
        {
            if (item != null)
            {
                return Inventory.HasSpaceForItem(Item);
            }

            return false;
        }

        public virtual void AttachItem(SlotItem dragItem)
        {
            if (dragItem == null)
            {
                return;
            }
            // if SlotItem == null is unchecked - this make inventory
            // Register to new slot

            if (dragItem.Slot != this)
            {
                dragItem.Slot = this;
                Item = dragItem;
            }

            // Set parent of the dropped item to the slot
            var rectTransform = dragItem.GetComponent<RectTransform>();

            rectTransform.SetParent(transform);
            rectTransform.localPosition = Vector3.zero;
            rectTransform.sizeDelta = Vector3.one;
            rectTransform.anchoredPosition = Vector3.zero;

        }

        public virtual void DetachItem()
        {
            if (Item != null)
            {
                Item.Slot = null;
                Item = null;
            }
        }

        public virtual void Ghost()
        {

        }
    }

}