using System;
using System.Windows.Threading;

namespace SeaBattle2._0
{
    internal class TimerModel // Таймер игры 
    {
        private DispatcherTimer timer;
        private DateTime starttime;

        public event EventHandler TimeElapsed;

        public string Timer { get; set; }

        public TimerModel()
        {
            starttime = DateTime.Now;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var dt = now - starttime;
            TimeElapsed?.Invoke(this, EventArgs.Empty);

            Timer = dt.ToString(@"mm\:ss");
        }
        
        public void Start() => timer.Start();
        public void Stop() => timer.Stop();
    }
}
