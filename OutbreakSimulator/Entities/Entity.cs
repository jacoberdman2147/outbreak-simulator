using System;
using System.Collections.Generic;
using System.Drawing;


namespace OutbreakSimulator
{
    abstract class Entity
    {
        protected const int size = 10;
        protected const int maxMove = 50;
        protected string entityString;

        protected Brush brush;
        protected Point pos;
        private static Random rand = new Random();
        private static SimulatorController controller;
        protected bool infected;
        protected Rectangle bounds;

        protected Entity(string entityString){
            this.entityString = entityString;
            if (controller == null)
            {
                controller = SimulatorController.getInstance();
            }
            this.brush = controller.GetBrushByName(entityString);
            this.pos = RandomPosition();
            this.bounds = controller.GetBoundaries();
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(brush, pos.X, pos.Y, size, size);
        }

        public void Move()
        {
            double distance = rand.NextDouble() * maxMove;
            double angle = rand.NextDouble() * Math.PI * 2;
            int newXOffset = (int)(Math.Cos(angle) * distance);
            int newYOffset = (int)(Math.Sin(angle) * distance);
            pos = new Point(Math.Abs(pos.X+ newXOffset) % bounds.Width, Math.Abs(pos.Y + newYOffset) % bounds.Height);
        }

        public Point GetPos()
        {
            return this.pos;
        }

        public abstract Entity GetInfected();

        private Point RandomPosition()
        {
            Rectangle bounds = controller.GetBoundaries();
            int x = rand.Next(bounds.Width);
            int y = rand.Next(bounds.Height);
            return new Point(x, y);
        }

        protected void MoveTo(Point to)
        {
            this.pos = to;
        }
        
        protected void ChangeEntityString(string newStr)
        {
            this.entityString = newStr;
            this.brush = controller.GetBrushByName(entityString);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            if (obj is Entity)
            {
                Entity o = (Entity)obj;
                if (o.pos != this.pos || o.infected != this.infected || o.brush != this.brush) return false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetDistance(Entity o)
        {
            return Math.Sqrt(Math.Pow(this.pos.X - o.pos.X, 2) + Math.Pow(this.pos.Y - o.pos.Y, 2));
        }

        public bool isInfected()
        {
            return infected;
        }
    }
}
