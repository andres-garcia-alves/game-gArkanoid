using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Garkanoid.Miscelaneous
{
    public class cHighScores
    {
        #region Class cHighScoreItem
        [Serializable]
        public class cHighScoreItem
        {
            int m_iPoints = 0;
            string m_sName = "Empty";

            public cHighScoreItem(int iPoints, string sName)
            {
                this.m_iPoints = iPoints;
                this.m_sName = sName;
            }

            public int Points
            {
                get { return this.m_iPoints; }
                set { this.m_iPoints = value; }
            }

            public string Name
            {
                get { return this.m_sName; }
                set { this.m_sName = value; }
            }
        }
        #endregion

        const int CANT_RANKING = 10;
        const int NAME_MAX_LENGHT = 10;

        List<cHighScoreItem> lstHighScores;

        public cHighScores()
        {
            try
            {
                lstHighScores = new List<cHighScoreItem>();

                for (int i = 0; i < CANT_RANKING; i++)
                    lstHighScores.Add(new cHighScoreItem(0, "Empty"));

                string sPath = Application.StartupPath + "\\HighScores.dat";

                FileStream fs = new FileStream(sPath, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                lstHighScores = (List<cHighScoreItem>)bf.Deserialize(fs);
                fs.Close();
            }

            catch (FileNotFoundException) {
                MessageBox.Show("File 'HightScore.dat' not found!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<cHighScoreItem> GetHighScores()
        {
            return lstHighScores;
        }

        /// <summary>
        /// Check if a value is a new high score
        /// </summary>
        /// <param name="iValue">Value to check</param>
        /// <returns>Return the position of the value against the high score list (-1 if no high score)</returns>
        public void CheckNewHighScore(int iTestValue)
        {
            for (int i = 0; i < lstHighScores.Count; i++)
            {
                if (iTestValue > lstHighScores[i].Points)
                {
                    int iPosX = (Screen.PrimaryScreen.Bounds.Width - 300) / 2;
                    int iPosY = (Screen.PrimaryScreen.Bounds.Height - 100) / 2;

                    string sTitle = cLanguaje.GetFrmGameHighScore()[0];
                    string sMsg1 = cLanguaje.GetFrmGameHighScore()[1];
                    string sMsg2 = cLanguaje.GetFrmGameHighScore()[2];

                    string sName = Microsoft.VisualBasic.Interaction.InputBox(sMsg1 + NAME_MAX_LENGHT + sMsg2, sTitle, "Empty", iPosX, iPosY);
                    if (sName.Length > NAME_MAX_LENGHT)
                        sName = sName.Substring(0, NAME_MAX_LENGHT);
                    
                    lstHighScores.Insert(i, new cHighScoreItem(iTestValue, sName));

                    if (lstHighScores.Count > CANT_RANKING)
                        lstHighScores.RemoveAt(CANT_RANKING);

                    PersistHighScores();
                    break;
                }
            }
        }

        private void PersistHighScores()
        {
            try
            {
                string sPath = Application.StartupPath + "\\HighScores.dat";

                FileStream fs = new FileStream(sPath, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, lstHighScores);
                fs.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public void ResetHightScores()
        {
            lstHighScores = new List<cHighScoreItem>();

            for (int i = 0; i < CANT_RANKING; i++)
                lstHighScores.Add(new cHighScoreItem(0, "Empty"));

            PersistHighScores();
        }
    }
}
