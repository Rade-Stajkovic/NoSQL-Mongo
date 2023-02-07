using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBP___Mongo.Model
{
    public class Car
    {
        public Model Model { get; set; }

        public Mark Mark { get; set; }

        public  String ExteriorColor { get; set; }

        public String InteriorColor { get; set; }

        public String Drivetrain  { get; set; }

        public int Year { get; set; }


    }
}
