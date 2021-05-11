using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using gArkanoid.Aux;
using gArkanoid.Entities;
using gArkanoid.Miscelaneous;

namespace gArkanoid
{
    public partial class frmGame : Form
    {
        #region Enumerations

        private enum eCurrentKey { None, Esc, Space, Left, Right, F10 }

        #endregion

        bool exit = false;
        bool keyPressed = false;
        eCurrentKey key = eCurrentKey.None;

        readonly Graphics graphics;
        readonly Balls.eInputType inputType = Balls.eInputType.Keyboard;

        Board board;
        readonly Balls balls;
        readonly PlayerPad playerPad;
        readonly OutputLine outputLine;
        readonly GameControl gameControl;
        readonly ScoreBoard scoreBoard;
        readonly HighScores highScores;
        readonly ParticlesSystem particlesSystem;
        readonly CollisionsSystem collisionsSystem;

        public frmGame()
        {
            InitializeComponent();

            try
            {
                // graphics quality
                this.graphics = this.CreateGraphics();
                this.graphics.SmoothingMode = SmoothingMode.HighQuality;
                this.graphics.CompositingMode = CompositingMode.SourceCopy;
                this.graphics.CompositingQuality = CompositingQuality.HighQuality;

                // background image
                string path = ConfigurationManager.AppSettings["pathImages"];
                this.BackgroundImage = Image.FromFile(path + "GameBackground.png", false);

                // get the input method (keyboard/mouse) from settings
                string input = ConfigurationManager.AppSettings["input"];
                this.inputType = (input == "Mouse" ? Balls.eInputType.Mouse : Balls.eInputType.Keyboard);

                if (inputType == Balls.eInputType.Mouse)
                    this.MouseMove += new MouseEventHandler(this.frmGame_MouseMove);

                // world objects
                this.balls = new Balls(inputType);
                this.board = new Board(1);
                this.playerPad = new PlayerPad();
                this.outputLine = new OutputLine();
                this.gameControl = new GameControl();
                this.scoreBoard = new ScoreBoard();
                this.highScores = new HighScores();
                this.particlesSystem = new ParticlesSystem();
                this.collisionsSystem = new CollisionsSystem();

                // add the listeners for objects comunication
                this.playerPad.DoubleBallRewardEvent += new PlayerPad.DoubleBallEventHandler(balls.DoubleBallEvent);
                this.playerPad.TripleBallRewardEvent += new PlayerPad.TripleBallEventHandler(balls.TripleBallEvent);
                this.playerPad.WinLevelRewardEvent += new PlayerPad.WinLevelEventHandler(gameControl.WinLevelEvent);
                this.playerPad.SlowBallRewardEvent += new PlayerPad.SlowBallEventHandler(balls.SlowBallEvent);
                this.playerPad.DemolitionBallRewardEvent += new PlayerPad.DemolitionBallEventHandler(balls.DemolitionBallEvent);
                
                // start
                this.tmrStart.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void tmrStart_Tick(object sender, EventArgs e)
        {
            this.tmrStart.Enabled = false;
            this.MainBucle();
        }

        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            this.keyPressed = true;

            if (e.KeyData == Keys.Left)
                this.key = eCurrentKey.Left;

            else if (e.KeyData == Keys.Right)
                this.key = eCurrentKey.Right;

            else if (e.KeyData == Keys.Space)
                this.key = eCurrentKey.Space;

            else if (e.KeyData == Keys.Escape)
                this.key = eCurrentKey.Esc;

            else if (e.KeyData == Keys.F10)
                this.key = eCurrentKey.F10;
        }

        private void frmGame_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            this.keyPressed = false;
        }

        private void frmGame_MouseMove(object sender, MouseEventArgs e)
        {
            this.playerPad.SetPosition(e.Location.X - (playerPad.GetWidth() / 2));
        }

        private void frmGame_Paint(object sender, PaintEventArgs e)
        {
            // redraw the game world
            this.scoreBoard.Draw(e.Graphics, this.gameControl.Lives, this.gameControl.Level, this.gameControl.Score);
            this.board.Draw(e.Graphics);
            this.balls.Draw(e.Graphics);
            this.playerPad.Draw(e.Graphics);
            this.outputLine.Draw(e.Graphics);
            this.particlesSystem.Draw(e.Graphics);
            this.gameControl.DrawFPS(e.Graphics);
        }

        private void MainBucle()
        {
            while (!exit)
            {
                Application.DoEvents();
                this.gameControl.FrameStart();

                // input
                this.ProcessInput();

                // collision check
                this.collisionsSystem.CheckCollisions();

                // game update
                if (this.gameControl.CheckLoseLive()) {
                    this.balls.Reset();
                    this.playerPad.Reset();
                    this.particlesSystem.ResetEfects();
                    Board.ResetRewards();
                    Board.ResetShots();
                }

                if (this.gameControl.CheckNoLivesRemaining()) {
                    this.highScores.CheckNewHighScore(this.gameControl.Score);
                    exit = true; this.Close();
                }

                if (this.gameControl.CheckLevelComplete(board)) {
                    this.balls.Reset();
                    this.playerPad.Reset();
                    this.particlesSystem.ResetEfects();
                    Board.ResetRewards();
                    Board.ResetShots();

                    this.board = new Board(this.gameControl.Level);
                }

                if (this.gameControl.CheckAllLevelsFinished()) {
                    this.highScores.CheckNewHighScore(this.gameControl.Score);
                    exit = true; this.Close();
                }

                // next move
                this.balls.Move();

                // repaint the screen
                this.Refresh();

                this.gameControl.CalculateFPS();
                this.gameControl.FrameFinish();
            }
        }

        private void ProcessInput()
        {
            if (keyPressed == true)
            {
                if (key == eCurrentKey.Left && inputType == Balls.eInputType.Keyboard) {
                    this.playerPad.MoveLeft(null);
                    this.Invalidate(this.playerPad.GetPositionRectangle());

                } else if (key == eCurrentKey.Right && inputType == Balls.eInputType.Keyboard) {
                    this.playerPad.MoveRight(null);
                    this.Invalidate(this.playerPad.GetPositionRectangle());
                
                } else if (key == eCurrentKey.Space) {
                    this.playerPad.Fire();

                } else if (key == eCurrentKey.F10) {
                    this.gameControl.ShowHideFPS();
                
                } else if (key == eCurrentKey.Esc) {
                    exit = true;
                    this.Close();
                }
            }
        }
    }
}
