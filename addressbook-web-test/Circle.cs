using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_test
{
    internal class Circle : Figure
    {
        private int radius;
        private bool colored = false;
        public Circle(int radius)
        {
            this.radius = radius;
        }
        
        public int Radius
        {
            get 
            {
                return radius;
            }
            set 
            {
                radius = value;
            }
        }

    }
}
