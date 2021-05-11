using System;
using System.Configuration;
using System.Xml;

namespace gArkanoid.Miscelaneous
{
    public static class Languaje
    {
        static string languaje = ConfigurationManager.AppSettings["languaje"];
        static string languajesDir = ConfigurationManager.AppSettings["pathLanguajes"];

        static XmlDocument xmlDocument;
        // static bool initialized = false;

        public static bool Initialize()
        {
            try
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(languajesDir + languaje + ".xml");
                // initialized = true;

                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmCreditsTitles()
        {
            try
            {
                string temp = xmlDocument.GetElementsByTagName("titles").Item(0).InnerText;
                return temp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmGameLevelString()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("level").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmGameScoreString()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("score").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameLoseLive()
        {
            try
            {
                string temp = xmlDocument.GetElementsByTagName("loseLive").Item(0).InnerText;
                return temp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameNoLive()
        {
            try
            {
                string temp = xmlDocument.GetElementsByTagName("noLive").Item(0).InnerText;
                return temp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameLevelUp()
        {
            try
            {
                string temp = xmlDocument.GetElementsByTagName("levelUp").Item(0).InnerText;
                temp = temp.Replace("\\n", "\n");
                return temp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameGameFinished()
        {
            try
            {
                string temp = xmlDocument.GetElementsByTagName("gameFinished").Item(0).InnerText;
                return temp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameHighScore()
        {
            try
            {
                string temp = xmlDocument.GetElementsByTagName("highScore").Item(0).InnerText;
                return temp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmHighScoresHighScores()
        {
            try
            {
                string temp = xmlDocument.GetElementsByTagName("reset").Item(0).InnerText;
                return temp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmHowToStep(int iStep)
        {
            try
            {
                return xmlDocument.GetElementsByTagName("step" + iStep.ToString()).Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmHowToLblLeyend()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("lblLeyend").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmHowToBtnBack()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnBack").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmHowToBtnRepeat()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnRepeat").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnPlay()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnPlay").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnHowTo()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnHowTo").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnHighScores()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnHighScores").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnOptions()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnOptions").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnEditor()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnEditor").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnCredits()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnCredits").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnExits()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnExit").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblLanguaje()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("lblLanguaje").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblLives()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("lblLives").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblMusic()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("lblMusic").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblInput()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("lblInput").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblMenssaje()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("lblMessaje").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsBtnOK()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnOK").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsBtnCancel()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnCancel").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsBtnRestart()
        {
            try
            {
                return xmlDocument.GetElementsByTagName("btnRestart").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
