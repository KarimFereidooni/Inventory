namespace Inventory.Models.DataModels
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Count { get; set; }
    }
}