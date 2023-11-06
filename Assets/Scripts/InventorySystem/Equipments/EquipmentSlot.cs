using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class EquipmentSlot : Slot
    {
        // use drag nad drop
        public EquipmentType SlotType;
        public GameObject PreviewIcon;

        public bool IsEquipped => Item != null;

        // drag and drop
        public override bool CanTakeItem(SlotItem item)
        {
            if (base.CanTakeItem(item))
            {
                var equipment = item.data as EquipmentData;

                return equipment != null &&
                    equipment.equipmentType == SlotType;
            }

            return false;
        }

        public override void AttachItem(SlotItem dragItem)
        {
            base.AttachItem(dragItem);

            var activePreview = dragItem == null;
            PreviewIcon.SetActive(activePreview);
        }

        public override void Ghost()
        {
            base.Ghost();
            PreviewIcon.SetActive(true);
        }
    }
}
