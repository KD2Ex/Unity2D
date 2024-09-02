using UnityEngine;

namespace UI_Toolkit.Inventory.Items
{
    public class UIItem
    {
        private string name;
        private Texture2D icon;

        public UIItem(string name, Texture2D icon)
        {
            this.name = name;
            this.icon = icon;
            
            
        }

        public void Init()
        {
             
        }
    }
}