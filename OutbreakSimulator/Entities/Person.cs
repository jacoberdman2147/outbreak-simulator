using System;
using System.Collections.Generic;
using System.Drawing;

namespace OutbreakSimulator.Entities
{
    class Person : Entity
    {
        public Person() : base("person"){
        }

        public override Entity GetInfected()
        {
            this.ChangeEntityString("infectedperson");
            infected = true;
            return this;
        }
    }
}
