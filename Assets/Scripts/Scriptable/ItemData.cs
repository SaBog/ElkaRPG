using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Items/SlotItem", fileName = "New SlotItem")]
    public class ItemData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public EquipmentRarity Rarity;

        public string Description;

        [Header("Stack Information")]
        public int maxStack = 0;
        public bool IsStackable => maxStack > 1;
    }

    //We connect the editor with the Weapon SO class
    [CustomEditor(typeof(ItemData))]
    //We need to extend the Editor
    public class ItemDataEditor : Editor
    {
        //Here we grab a reference to our Weapon SO
        ItemData item;

        private void OnEnable()
        {
            //target is by default available for you
            //because we inherite Editor
            item = target as ItemData;
        }

        //Here is the meat of the script
        public override void OnInspectorGUI()
        {
            //Draw whatever we already have in SO definition
            base.OnInspectorGUI();
            //Guard clause
            if (item.Icon == null)
                return;

            //Convert the weaponSprite (see SO script) to Texture
            Texture2D texture = AssetPreview.GetAssetPreview(item.Icon);
            //We crate empty space 80x80 (you may need to tweak it to scale better your sprite
            //This allows us to place the image JUST UNDER our default inspector
            int size = 100;

            GUILayout.Label(string.Empty, GUILayout.Height(size), GUILayout.Width(size));
            //Draws the texture where we have defined our Label (empty space)
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }

}
