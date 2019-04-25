using System;
using System.Collections.Generic;
using Lesson2.Abstractions;
using Lesson2.Implementations;
using Lesson2.UI;

namespace Lesson2
{
    public class MenuController
    {
        private ShapeContainer container = new ShapeContainer();
        List<IShapePlugin> shapePlugins = new List<IShapePlugin>();    

        Menu mainMenu = new Menu();    
        Menu addShapesSubMenu = new Menu();

        public void Initialize()
        {
            mainMenu.SetMenuItem(1, "View Canvas", ()=> HandleViewCanvas());
            mainMenu.SetMenuItem(2, "Add Shape", addShapesSubMenu);
            mainMenu.SetMenuItem(3, "Remove Shape", () => HandleRemoveShape());

        }

        private void HandleRemoveShape()
        {
            
        }

        private void HandleViewCanvas()
        {
            Console.Clear();
            container.Draw();
            Console.ReadLine();
        }
        public void AddAvailableShape(IShapePlugin shapePlugin)
        {
            addShapesSubMenu.SetMenuItem(shapePlugins.Count + 1, shapePlugin.GetName(), () => container.Add(shapePlugin.ReadShape()));
            shapePlugins.Add(shapePlugin);
        }

        public void EnterMenu()
        {
            mainMenu.EnterMenu();
        } 

    }
}