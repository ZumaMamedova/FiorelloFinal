﻿using System.ComponentModel.DataAnnotations;

namespace Fiorello.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string Desc { get; set; }
        public List<Product>Products { get; set; }=new List<Product>();
    }
}
