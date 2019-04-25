using System;
namespace Lesson2.UI
{
    public class MenuItem
    {
        public int ActivationNumber { get; set; }
        public string TextMessage { get; set; }
        public Menu SubMenu { get; set;}
        public Action ActionToExecute { get; set; }

        public void RenderItem()
        {
            Console.WriteLine($"{ActivationNumber}. {TextMessage}");
        }
    }
}