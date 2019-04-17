using System;
using System.Collections.Generic;
using System.Linq;

namespace SellerApp.UI
{
    public class Menu
    {
        private Dictionary<int, MenuItem> menuItems;
        private bool continueMenu;

        public Action OnPreRender
        {
            get;
            set;
        }

        private void RenderMenu()
        {
            List<int> keys = new List<int>(menuItems.Keys);
            Console.Clear();
            if (OnPreRender != null)
            {
                OnPreRender();
            }
            var keysList = keys.Where(currentValue => currentValue != 0)
                                .OrderBy(currentValue => currentValue);

            foreach (var key in keysList)
            {
                menuItems[key].RenderItem();
            }

            menuItems[0].RenderItem();

        }

        private int ReadOption()
        {
            Console.Write("\n\nEnter your option: ");
            var keyInfo = Console.ReadKey();
            var option = (keyInfo.KeyChar - '0');

            return option;
        }

        public Menu()
        {
            continueMenu = true;
            menuItems = new Dictionary<int, MenuItem>();
            SetMenuItem(0, "Back", () => continueMenu = false);
        }
        public void SetMenuItem(int shortcutKey, string message, Action actionToPerform)
        {
            menuItems[shortcutKey] = new MenuItem { ActivationNumber = shortcutKey, TextMessage = message, ActionToExecute = actionToPerform, SubMenu = null };
        }

        public void SetMenuItem(int shortcutKey, string message, Menu subMenu)
        {
            menuItems[shortcutKey] = new MenuItem { ActivationNumber = shortcutKey, TextMessage = message, SubMenu = subMenu, ActionToExecute = null };
        }
        public void SetMenuItem(int shortcutKey, string message, Menu subMenu, Action actionToPerform)
        {
            menuItems[shortcutKey] = new MenuItem { ActivationNumber = shortcutKey, TextMessage = message, SubMenu = subMenu, ActionToExecute = actionToPerform };
        }

        public void EnterMenu()
        {

            do
            {
                RenderMenu();

                bool validOption = false;
                int option = 0;
                do
                {
                    option = ReadOption();
                    if (menuItems.ContainsKey(option))
                    {
                        validOption = true;
                    }

                } while (!validOption);

                var menuItem = menuItems[option];
                if (menuItem.ActionToExecute != null)
                {
                    menuItem.ActionToExecute();
                }

                if (menuItem.SubMenu != null)
                {
                    menuItem.SubMenu.EnterMenu();
                }

            } while (continueMenu);
        }
    }
}