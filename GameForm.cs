using System.Security.Cryptography;
using System.Windows.Forms;
using CGoL_Set_Alive;
namespace CGoL_Set_Alive
{
    public partial class GameForm : Form
    {
        Grid _grid;
        private System.Windows.Forms.Timer _timer;
        private bool _isPaused = true;
        public GameForm()
        {
            InitializeComponent();
            _grid = new Grid(40, 40);
            _grid.LoadPattern("Glider", 10, 10);
            _grid.LoadPattern("Glider", 5, 5);
            _grid.LoadPattern("Glider", 15, 15);
            _grid.LoadPattern("Glider", 5, 15);
            _grid.LoadPattern("blinker", 13, 11);
            _grid.LoadPattern("block", 25, 20);
            _grid.LoadPattern("blinker", 17, 24);
            _grid.LoadPattern("block", 22, 23);
            this.ClientSize = new Size(_grid.Width * 20, _grid.Height * 20);
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 99;
            _timer.Tick += OnTick;
            //_timer.Start();
            this.DoubleBuffered = true;
            this.MouseClick += IfClick;
            this.Text = "Conway's Game of Life";
            this.KeyDown += Spacebar;
        }

        private void Spacebar(object? sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                if(_isPaused == false)
                {
                    _isPaused = true;
                    _timer.Start();
                }
                else
                {
                    _timer.Stop();
                    _isPaused = false;
                }
                
            }
            
        }
        private void IfClick(object? sender, MouseEventArgs e)
        {
            _grid.SetAlive((e.X)/20, (e.Y)/20) ;
            Invalidate();
        }
        private void OnTick(object? sender, EventArgs e)
        {
            _grid.NextGeneration();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //loop through grid
            for(int x = 0; x< _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.IsAlive(x, y))
                    {
                        e.Graphics.FillRectangle(Brushes.Red, x*20, y*20, 20, 20);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(Brushes.Black, x * 20, y * 20, 20, 20);
                    }
                }
            }
        }


    }
}
