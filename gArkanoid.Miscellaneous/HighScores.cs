using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace gArkanoid.Miscelaneous
{
    public class HighScores
    {
        #region Class HighScoreItem

        [Serializable]
        public class HighScoreItem
        {
            int points = 0;
            string name = "Empty";

            public HighScoreItem(int points, string name)
            {
                this.points = points;
                this.name = name;
            }

            public int Points
            {
                get { return this.points; }
                set { this.points = value; }
            }

            public string Name
            {
                get { return this.name; }
                set { this.name = value; }
            }
        }
        #endregion

        const int CANT_RANKING = 10;
        const int NAME_MAX_LENGHT = 10;

        List<HighScoreItem> highScores;

        public HighScores()
        {
            try
            {
                this.highScores = new List<HighScoreItem>();

                for (int i = 0; i < CANT_RANKING; i++)
                    this.highScores.Add(new HighScoreItem(0, "Empty"));

                string path = Application.StartupPath + "\\HighScores.dat";

                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                this.highScores = (List<HighScoreItem>)bf.Deserialize(fs);
                fs.Close();
            }

            catch (FileNotFoundException) {
                MessageBox.Show("File 'HightScore.dat' not found!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { throw ex; }
        }

        public List<HighScoreItem> GetHighScores()
        {
            return this.highScores;
        }

        /// <summary>
        /// Check if a value is a new high score
        /// </summary>
        /// <param name="iValue">Value to check</param>
        /// <returns>Return the position of the value against the high score list (-1 if no high score)</returns>
        public void CheckNewHighScore(int testValue)
        {
            for (int i = 0; i < this.highScores.Count; i++)
            {
                if (testValue > this.highScores[i].Points)
                {
                    int pos_x = (Screen.PrimaryScreen.Bounds.Width - 300) / 2;
                    int posy = (Screen.PrimaryScreen.Bounds.Height - 100) / 2;

                    string title = Languaje.GetFrmGameHighScore()[0];
                    string message1 = Languaje.GetFrmGameHighScore()[1];
                    string message2 = Languaje.GetFrmGameHighScore()[2];

                    string name = Microsoft.VisualBasic.Interaction.InputBox(message1 + NAME_MAX_LENGHT + message2, title, "Empty", pos_x, posy);
                    if (name.Length > NAME_MAX_LENGHT)
                        name = name.Substring(0, NAME_MAX_LENGHT);

                    this.highScores.Insert(i, new HighScoreItem(testValue, name));

                    if (this.highScores.Count > CANT_RANKING)
                        this.highScores.RemoveAt(CANT_RANKING);

                    this.PersistHighScores();
                    break;
                }
            }
        }

        private void PersistHighScores()
        {
            try
            {
                string path = Application.StartupPath + "\\HighScores.dat";

                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, this.highScores);
                fs.Close();
            }
            catch (Exception ex) { throw ex; }
        }

        public void ResetHightScores()
        {
            this.highScores = new List<HighScoreItem>();

            for (int i = 0; i < CANT_RANKING; i++)
                this.highScores.Add(new HighScoreItem(0, "Empty"));

            this.PersistHighScores();
        }
    }
}
