using System;
using System.Collections.Generic;
using System.Drawing;
using OutbreakSimulator.Entities;

namespace OutbreakSimulator
{
    class SimulatorController
    {
        const double distanceInfectThreshold = 100;
        const int numberOfPeople = 10000;

        private static SimulatorController instance = new SimulatorController();
        private List<Entity> entities;
        private List<Entity> infectedEntities;
        private Dictionary<string, Brush> brushes;
        protected Rectangle bounds;
        Random r;

        private SimulatorController(){
            InitializeBrushes();
            this.bounds = new Rectangle(0, 0, 1920, 1080); //Default
            entities = new List<Entity>();
            infectedEntities = new List<Entity>();
            r = new Random();
        }

        public static SimulatorController getInstance()
        {
            return instance;
        }

        private void InitializeBrushes()
        {
            brushes = new Dictionary<string, Brush>();
            brushes.Add("person", new SolidBrush(Color.FromArgb(0,255,0)));
            brushes.Add("infectedperson", new SolidBrush(Color.FromArgb(255, 0, 0)));
        }

        public Brush GetBrushByName(string key)
        {
            Brush ret;
            brushes.TryGetValue(key, out ret);
            if (ret == null) throw new ArgumentException("Key does not exist in dictionary");
            return ret;
        }

        public Rectangle GetBoundaries()
        {
            return this.bounds;
        }

        public void SetBoundaries(Rectangle newBounds)
        {
            this.bounds = newBounds;
        }

        public void DrawAll(Graphics g)
        {
            g.Clear(Color.FromArgb(0,0,0));
            foreach (Entity e in entities)
            {
                e.Draw(g);
            }
        }

        private void MoveAll()
        {
            foreach (Entity e in entities)
            {
                e.Move();
            }
        }

        private void TryInfectAll()
        {
            foreach (Entity e in entities)
            {
                if (!e.isInfected())
                {
                    TryInfect(e);
                }
            }
        }

        public void Step(Graphics g)
        {
            MoveAll();
            TryInfectAll();
            DrawAll(g);
        }

        public void Initialize()
        {
            for (int i = 0; i < numberOfPeople; i++)
            {
                Person p = new Person();
                entities.Add(p);
            }
            InfectSuccess(entities[0]);
        }

        public void TryInfect(Entity toInfect)
        {
            //NEED TO FIX INFECTION BEING GENERATED WITHIN THE STEP BY ONES THAT GET INFECTED DURING THAT STEP
            bool infect = false;
            foreach(Entity infected in infectedEntities)
            {
                double distance = toInfect.GetDistance(infected);
                if ((r.NextDouble() * (distanceInfectThreshold - distance)) > r.NextDouble() * distanceInfectThreshold)
                {
                    infect = true;
                    break;
                }
            }
            if (infect) InfectSuccess(toInfect);
        }

        public void InfectSuccess(Entity toInfect)
        {
            toInfect.GetInfected();
            infectedEntities.Add(toInfect);
            
        }
    }
}
