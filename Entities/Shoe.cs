using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeSalesSystem.Entities
{
    public class Shoe
    {
        public string ImagePath{ get; set;}
        public string ShoeModel { get; set; }
        public int ShoeId { get; set; }
        public string Origin { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public Shoe() {
            ImagePath = Path.Combine("..", "..", "Resources", "Images", "OIP (1).jfif");
            //ImagePath = "..\\..\\Resources\\Images\\OIP (1).jfif";
        }
        public Shoe(string model,string image)
        {
            ShoeModel = model;
            ImagePath = image;
        }

        public Shoe(string model, string imagePath, string origin, decimal price, int stockQuantity)
        {
            ShoeModel = model;
            ImagePath = imagePath;
            Origin = origin;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}
