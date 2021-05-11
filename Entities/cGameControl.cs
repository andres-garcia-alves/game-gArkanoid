using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;

using Garkanoid.Miscelaneous;
using Garkanoid.Entities;

namespace Garkanoid.Aux
{
    public class cGameControl
    {
        public const int MAX_LEVEL = 20;

        private int m_iLives;
        private int m_iCurrentLevel = 1;
        private static int m_iScore = 0;

        private bool bInitialized = false;
        private bool bShowFPS = false;
        private int iMaxLevels;
        private int iCountFPS = 0;
        private int iCurrentFPS = 0;
        private int iMinFrameDuration;
        private DateTime dtPrevTick;
        private DateTime dtPrevious;

        public static bool bLoseLive = false;

        #region Properties

        public int Lives
        {
            get { return m_iLives; }
            set { m_iLives = value; }
        }

        public int Level
        {
            get { return m_iCurrentLevel; }
            set { m_iCurrentLevel = value; }
        }

        public int Score
        {
            get { return m_iScore; }
            set { m_iScore = value; }
        }

        #endregion

        #region Constructor

        public cGameControl ()
        {
            int iAux;

            try
            {    
                iAux = Convert.ToInt32(ConfigurationManager.AppSettings["lives"]);
                if (iAux >= 1 && iAux <= 5) {
                    m_iLives = iAux;
                } else {
                    m_iLives = 3;
                    throw new ApplicationException("Lives value in 'App.config' file must be between 1 to 5. Using default value: 3 lives.");
                }

                iAux = Convert.ToInt32(ConfigurationManager.AppSettings["fps"]);
                if (iAux >= 1 && iAux <= 100) {
                    iMinFrameDuration = 1000 / iAux;
                } else {
                    iMinFrameDuration = 30; // 1000ms / 30 = 33 FPS
                    throw new ApplicationException("FPS value in 'App.config' file must be between 1 to 100. Using default value: 33 FPS.");
                }

                iAux = Convert.ToInt32(ConfigurationManager.AppSettings["levels"]);
                if (iAux > 0) {
                    iMaxLevels = iAux;
                } else {
                    iMaxLevels = 10;
                    throw new ApplicationException("Levels value in 'App.config' file must be greather then 0. Using default value: 10 levels.");
                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        #endregion

        public static void AddScorePoints(int iPoints)
        {
            m_iScore += iPoints;
        }

        public void ResetScorePoints()
        {
            m_iScore = 0;
        }

        public bool CheckLoseLive()
        {
            if (bLoseLive == true) {
                m_iLives--;
                bLoseLive = false;

                string sTitle = cLanguaje.GetFrmGameLoseLive()[0];
                string sMsg = m_iLives.ToString() + cLanguaje.GetFrmGameLoseLive()[1];
                MessageBox.Show(sMsg, sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Check if no lives remaing
        /// </summary>
        public bool CheckNoLivesRemaining()
        {
            if (m_iLives == 0) {
                string sTitle = cLanguaje.GetFrmGameNoLive()[0];
                string sMsg = cLanguaje.GetFrmGameNoLive()[1];

                MessageBox.Show(sMsg, sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Check if current level has been finished
        /// </summary>
        public bool CheckLevelComplete(cBoard oBoard)
        {
            if (oBoard.Bricks.Count == 0) {
                m_iCurrentLevel++;

                string sTitle = cLanguaje.GetFrmGameLevelUp()[0];
                string sMsg = cLanguaje.GetFrmGameLevelUp()[1] + m_iCurrentLevel.ToString() + ".";
                MessageBox.Show(sMsg, sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Check if all level has been finished
        /// </summary>
        public bool CheckAllLevelsFinished()
        {
            if (m_iCurrentLevel >= MAX_LEVEL) {
                string sTitle = cLanguaje.GetFrmGameGameFinished()[0];
                string sMsg = cLanguaje.GetFrmGameGameFinished()[1];

                MessageBox.Show(sMsg, sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            return false;
        }

        /// <summary>
        /// WinLevel reward has been picked
        /// </summary>
        public void WinLevelEvent(object sender, EventArgs e)
        {
            cBoard.ResetBricks();
        }

        #region FPS stuff

        public void FrameStart()
        {
            dtPrevious = DateTime.Now;
        }

        /// <summary>
        /// Generate a delay to control the FPS rate
        /// </summary>
        public void FrameFinish()
        {
            TimeSpan tsInteval = DateTime.Now.Subtract(dtPrevious);

            if (tsInteval.Milliseconds < iMinFrameDuration)
                // sleep to complete at least iMinFrameDuration per frame
                System.Threading.Thread.Sleep(iMinFrameDuration - tsInteval.Milliseconds);
        }

        public void CalculateFPS()
        {
            if (bShowFPS) {

                DateTime dtCurrentTick;

                if (!bInitialized) { // 1st time: set init tick
                    bInitialized = true;
                    dtPrevTick = DateTime.Now;
                }

                iCountFPS++;
                dtCurrentTick = DateTime.Now;

                // If 1 second passed from dtPrevTick, recalculate current framerate
                if (dtCurrentTick.Subtract(dtPrevTick).TotalMilliseconds > 1000) {
                    dtPrevTick = dtPrevTick.AddMilliseconds(1000);
                    iCurrentFPS = iCountFPS;
                    iCountFPS = 0;
                }
            }
        }

        public void ShowHideFPS()
        {
            bShowFPS = !bShowFPS;
        }

        public void DrawFPS(Graphics oGraphics)
        {
            if (bShowFPS)
                oGraphics.DrawString("FPS: " + iCurrentFPS.ToString(), new Font("Verdana", 8), Brushes.White, 10, 460);
        }

        #endregion
    }
}
