using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

using gArkanoid.Entities;
using gArkanoid.Miscelaneous;

namespace gArkanoid.Aux
{
    public class GameControl
    {
        public const int MAX_LEVEL = 20;

        private int _Lives;
        private int _CurrentLevel = 1;
        private static int _Score = 0;

        private bool isInitialized = false;
        private bool showFPS = false;
        private readonly int iMaxLevels;
        private int countFPS = 0;
        private int currentFPS = 0;
        private int iMinFrameDuration;
        private DateTime prevTick;
        private DateTime dtPrevious;

        public static bool loseLive = false;

        #region Properties

        public int Lives
        {
            get { return _Lives; }
            set { _Lives = value; }
        }

        public int Level
        {
            get { return _CurrentLevel; }
            set { _CurrentLevel = value; }
        }

        public int Score
        {
            get { return _Score; }
            set { _Score = value; }
        }

        #endregion

        #region Constructor

        public GameControl ()
        {
            int iAux;

            try
            {    
                iAux = Convert.ToInt32(ConfigurationManager.AppSettings["lives"]);
                if (iAux >= 1 && iAux <= 5) {
                    _Lives = iAux;
                } else {
                    _Lives = 3;
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

        public static void AddScorePoints(int points)
        {
            _Score += points;
        }

        public void ResetScorePoints()
        {
            _Score = 0;
        }

        public bool CheckLoseLive()
        {
            if (loseLive == true) {
                _Lives--;
                loseLive = false;

                string title = Languaje.GetFrmGameLoseLive()[0];
                string message = _Lives.ToString() + Languaje.GetFrmGameLoseLive()[1];
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Check if no lives remaing
        /// </summary>
        public bool CheckNoLivesRemaining()
        {
            if (_Lives == 0) {
                string title = Languaje.GetFrmGameNoLive()[0];
                string message = Languaje.GetFrmGameNoLive()[1];

                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Check if current level has been finished
        /// </summary>
        public bool CheckLevelComplete(Board board)
        {
            if (board.Bricks.Count == 0) {
                _CurrentLevel++;

                string title = Languaje.GetFrmGameLevelUp()[0];
                string message = Languaje.GetFrmGameLevelUp()[1] + _CurrentLevel.ToString() + ".";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Check if all level has been finished
        /// </summary>
        public bool CheckAllLevelsFinished()
        {
            if (_CurrentLevel >= MAX_LEVEL) {
                string title = Languaje.GetFrmGameGameFinished()[0];
                string message = Languaje.GetFrmGameGameFinished()[1];

                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            return false;
        }

        /// <summary>
        /// WinLevel reward has been picked
        /// </summary>
        public void WinLevelEvent(object sender, EventArgs e)
        {
            Board.ResetBricks();
        }

        #region FPS-stuff methods

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
            if (showFPS) {

                DateTime currentTick;

                if (!isInitialized) { // 1st time: set init tick
                    isInitialized = true;
                    prevTick = DateTime.Now;
                }

                countFPS++;
                currentTick = DateTime.Now;

                // If 1 second passed from dtPrevTick, recalculate current framerate
                if (currentTick.Subtract(prevTick).TotalMilliseconds > 1000) {
                    prevTick = prevTick.AddMilliseconds(1000);
                    currentFPS = countFPS;
                    countFPS = 0;
                }
            }
        }

        public void ShowHideFPS()
        {
            showFPS = !showFPS;
        }

        public void DrawFPS(Graphics graphics)
        {
            if (showFPS)
                graphics.DrawString("FPS: " + currentFPS.ToString(), new Font("Verdana", 8), Brushes.White, 10, 460);
        }

        #endregion
    }
}
