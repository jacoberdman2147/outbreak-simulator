using System;
using System.Drawing;
using System.Windows.Forms;

namespace OutbreakSimulator
{
    public partial class MainInterface : Form
    {
        const int tickInterval = 500;

        private SimulatorController controller;
        private Graphics graphics;
        private Timer tick;

        public MainInterface()
        {
            this.WindowState = FormWindowState.Maximized;
            controller = SimulatorController.getInstance();
            controller.Initialize();
            controller.SetBoundaries(this.MaximizedBounds);
            this.graphics = this.CreateGraphics();
            this.tick = new Timer();
            tick.Interval = tickInterval;
            tick.Tick += new EventHandler(Step);
            tick.Start();
            this.Paint += new PaintEventHandler(Draw);
        }

        private void Draw(Object sender, PaintEventArgs e)
        {
            controller.DrawAll(graphics);
        }

        private void Step(Object sender, EventArgs e)
        {
            controller.Step(graphics);
        }
    }
}
