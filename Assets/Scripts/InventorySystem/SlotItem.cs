using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    // draggable item with count
    public class SlotItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        // wrapper
        public ItemData data;
        // container
        [HideInInspector]
        public Slot Slot;

        [SerializeField]
        private int count;
        public int Count
        {
            get => count; set
            {
                count = value;
                UpdateUI();
            }
        }

        public Image Icon;
        public TextMeshProUGUI textAmount;

        private void Start()
        {
            if (count > data.maxStack)
            {
                // split item realization
                Debug.LogError("count > data.maxStack - Not implementation");
            }

            UpdateUI();
        }

        public void UpdateUI()
        {
            textAmount.SetText(count > 1 ?
                count.ToString() :
                string.Empty);

            Icon.sprite = data.Icon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Set the slot's parent to the canvas so it doesn't get clipped
            transform.SetParent(transform.root);
            Slot.Ghost();

            // Disable the slot's canvas group to allow raycasts pass through
            Icon.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Move the slot with the pointer
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Enable the slot's canvas group
            Icon.raycastTarget = true;
            Slot.AttachItem(this);
        }


        bool doubleTap = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (doubleTap)
            {
                OnDoubleClick();
            }

            StopAllCoroutines();
            StartCoroutine(DoubleTap());
        }

        IEnumerator DoubleTap()
        {
            doubleTap = true;
            yield return new WaitForSeconds(.3f);
            doubleTap = false;
        }

        public virtual void OnDoubleClick()
        {
            if (data is EquipmentData equipment)
            {
                var inventory = Slot.Inventory;

                var slot = inventory.EquipmentList
                    .Find(slot => slot.SlotType == equipment.equipmentType);

                if (Slot == slot)
                {
                    inventory.Put(this);
                }
                else
                {
                    inventory.Move(this, slot);
                }

            }
        }

    }

}
