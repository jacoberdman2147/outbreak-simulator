using System;
using System.Drawing;
using System.Windows.Forms;

namespace OutbreakSimulator
{
    public partial class MainInterface : Form
    {
        const int tickInterval = 500;

        private SimulatorController controller;
        private Timer tick;

        public MainInterface()
        {
            this.WindowState = FormWindowState.Maximized;
            controller = SimulatorController.getInstance();
            controller.Initialize();
            controller.SetBoundaries(this.MaximizedBounds);
            this.tick = new Timer();
            tick.Interval = tickInterval;
            tick.Tick += new EventHandler(Step);
            tick.Start();
            this.Paint += new PaintEventHandler(Draw);
        }

        private void Draw(Object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(0, 0, 0));
            controller.DrawAll(e.Graphics);
        }

        private void Step(Object sender, EventArgs e)
        {
            controller.Step();
            this.Invalidate();
        }
    }
}
