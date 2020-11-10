using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Mobile.Models
{
    public enum MenuItemType
    {
        Welcome,
        Temperature,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
