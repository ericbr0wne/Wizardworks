using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WizardWorks.Squares.API.Models
{
    public class SquareModel
    {
        public class Square
        {
            public int Id { get; set; }
            public Position Position { get; set; }
            public string Color { get; set; }
        }

        public class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}