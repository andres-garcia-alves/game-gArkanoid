/* 
 *  Created by:
 *      Juan Andrés Garcia Alves de Borba
 *  
 *  Date & Version:
 *      Feb-2010, version 1.1
 * 
 *  Contact:
 *      andres_garcia_ao@hotmail.com
 *      andres.garcia.ao@gmail.com
 *  
 *  Curse:
 *      Sistemas de Procesamiento de Datos
 *      Tecnicatura Superior en Programación
 *      Universidad Tecnológica Nacional (UTN) FRBA - Argentina
 *
 *  Licensing:
 *      This software is Open Source and are available under de GNU LGPL license.
 *      You can found a copy of the license at http://www.gnu.org/copyleft/lesser.html
 *  
 *  Enjoy playing!
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using Garkanoid.Aux;
using Garkanoid.Entities;
using Garkanoid.Miscelaneous;

namespace Garkanoid
{
    public partial class frmGame : Form
    {
        #region Enumerations

        private enum eCurrentKey { None, Esc, Space, Left, Right, F10 }

        #endregion

        bool bExit = false;
        bool bKeyPressed = false;
        eCurrentKey eKey = eCurrentKey.None;
        cBalls.eInputType eInput = cBalls.eInputType.Keyboard;
        Graphics oGraphics;

        cBalls oBalls;
        cBoard oBoard;
        cPlayerPad oPlayerPad;
        cOutputLine oOutputLine;
        cGameControl oGameControl;
        cScoreBoard oScoreBoard;
        cHighScores oHighScores;
        cParticlesSystem oParticlesSystem;
        cCollisionsSystem oCollisionsSystem;

        public frmGame()
        {
            InitializeComponent();

            try
            {
                // graphics quality
                oGraphics = this.CreateGraphics();
                oGraphics.SmoothingMode = SmoothingMode.HighQuality;
                oGraphics.CompositingMode = CompositingMode.SourceCopy;
                oGraphics.CompositingQuality = CompositingQuality.HighQuality;

                // background image
                string sPath = ConfigurationManager.AppSettings["pathImages"];
                this.BackgroundImage = Image.FromFile(@sPath + "GameBackground.png", false);

                // get the input type (keyboard / mouse) from settings
                string sInput = ConfigurationManager.AppSettings["input"];
                eInput = (sInput == "Mouse" ? cBalls.eInputType.Mouse : cBalls.eInputType.Keyboard);

                if (eInput == cBalls.eInputType.Mouse)
                    this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmGame_MouseMove);

                // world objects
                oBalls = new cBalls(eInput);
                oBoard = new cBoard(1);
                oPlayerPad = new cPlayerPad();
                oOutputLine = new cOutputLine();
                oGameControl = new cGameControl();
                oScoreBoard = new cScoreBoard();
                oHighScores = new cHighScores(); 
                oParticlesSystem = new cParticlesSystem();
                oCollisionsSystem = new cCollisionsSystem();

                // add the listeners for objects comunication
                oPlayerPad.DoubleBallRewardEvent += new cPlayerPad.DoubleBallEventHandler(oBalls.DoubleBallEvent);
                oPlayerPad.TripleBallRewardEvent += new cPlayerPad.TripleBallEventHandler(oBalls.TripleBallEvent);
                oPlayerPad.WinLevelRewardEvent += new cPlayerPad.WinLevelEventHandler(oGameControl.WinLevelEvent);
                oPlayerPad.SlowBallRewardEvent += new cPlayerPad.SlowBallEventHandler(oBalls.SlowBallEvent);
                oPlayerPad.DemolitionBallRewardEvent += new cPlayerPad.DemolitionBallEventHandler(oBalls.DemolitionBallEvent);
                
                // start
                this.tmrStart.Enabled = true;
            }

            catch (Exception ex) {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void tmrStart_Tick(object sender, EventArgs e)
        {
            this.tmrStart.Enabled = false;
            MainBucle();
        }

        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            bKeyPressed = true;

            if (e.KeyData == Keys.Left)
                eKey = eCurrentKey.Left;

            else if (e.KeyData == Keys.Right)
                eKey = eCurrentKey.Right;

            else if (e.KeyData == Keys.Space)
                eKey = eCurrentKey.Space;

            else if (e.KeyData == Keys.Escape)
                eKey = eCurrentKey.Esc;

            else if (e.KeyData == Keys.F10)
                eKey = eCurrentKey.F10;
        }

        private void frmGame_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            bKeyPressed = false;
        }

        private void frmGame_MouseMove(object sender, MouseEventArgs e)
        {
            oPlayerPad.SetPosition(e.Location.X - (oPlayerPad.GetWidth() / 2));
        }

        private void frmGame_Paint(object sender, PaintEventArgs e)
        {
            // redraw the game world
            oScoreBoard.Draw(e.Graphics, oGameControl.Lives, oGameControl.Level, oGameControl.Score);
            oBoard.Draw(e.Graphics);
            oBalls.Draw(e.Graphics);
            oPlayerPad.Draw(e.Graphics);
            oOutputLine.Draw(e.Graphics);
            oParticlesSystem.Draw(e.Graphics);
            oGameControl.DrawFPS(e.Graphics);
        }

        private void MainBucle()
        {
            while (!bExit)
            {
                Application.DoEvents();
                oGameControl.FrameStart();

                // input
                ProcessInput();

                // collision check
                oCollisionsSystem.CheckCollisions();

                // game update
                if (oGameControl.CheckLoseLive()) {
                    oBalls.Reset();
                    oPlayerPad.Reset();
                    oParticlesSystem.ResetEfects();
                    cBoard.ResetRewards();
                    cBoard.ResetShots();
                }

                if (oGameControl.CheckNoLivesRemaining()) {
                    oHighScores.CheckNewHighScore(oGameControl.Score);
                    bExit = true; this.Close();
                }

                if (oGameControl.CheckLevelComplete(oBoard)) {
                    oBalls.Reset();
                    oPlayerPad.Reset();
                    oParticlesSystem.ResetEfects();
                    cBoard.ResetRewards();
                    cBoard.ResetShots();

                    oBoard = new cBoard(oGameControl.Level);
                }

                if (oGameControl.CheckAllLevelsFinished()) {
                    oHighScores.CheckNewHighScore(oGameControl.Score);
                    bExit = true; this.Close();
                }

                // next move
                oBalls.Move();

                // repaint the screen
                this.Refresh();

                oGameControl.CalculateFPS();
                oGameControl.FrameFinish();
            }
        }

        private void ProcessInput()
        {
            if (bKeyPressed == true)
            {
                if (eKey == eCurrentKey.Left && eInput == cBalls.eInputType.Keyboard) {
                    oPlayerPad.MoveLeft(null);
                    this.Invalidate(oPlayerPad.GetPositionRectangle());

                } else if (eKey == eCurrentKey.Right && eInput == cBalls.eInputType.Keyboard) {
                    oPlayerPad.MoveRight(null);
                    this.Invalidate(oPlayerPad.GetPositionRectangle());
                
                } else if (eKey == eCurrentKey.Space) {
                    oPlayerPad.Fire();

                } else if (eKey == eCurrentKey.F10) {
                    oGameControl.ShowHideFPS();
                
                } else if (eKey == eCurrentKey.Esc) {
                    bExit = true;
                    this.Close();
                }
            }
        }
    }
}
