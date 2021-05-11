using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;

namespace Garkanoid.Miscelaneous
{
    public static class cLanguaje
    {
        static string sLanguaje = ConfigurationManager.AppSettings["languaje"];
        static string sLanguajesDir = ConfigurationManager.AppSettings["pathLanguajes"];

        static XmlDocument oDoc;
        //static bool bInitialized = false;

        public static bool Initialize()
        {
            try
            {
                oDoc = new XmlDocument();
                oDoc.Load(sLanguajesDir + sLanguaje + ".xml");
                //bInitialized = true;

                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmCreditsTitles()
        {
            try
            {
                string sTemp = oDoc.GetElementsByTagName("titles").Item(0).InnerText;
                return sTemp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmGameLevelString()
        {
            try
            {
                return oDoc.GetElementsByTagName("level").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmGameScoreString()
        {
            try
            {
                return oDoc.GetElementsByTagName("score").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameLoseLive()
        {
            try
            {
                string sTemp = oDoc.GetElementsByTagName("loseLive").Item(0).InnerText;
                return sTemp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameNoLive()
        {
            try
            {
                string sTemp = oDoc.GetElementsByTagName("noLive").Item(0).InnerText;
                return sTemp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameLevelUp()
        {
            try
            {
                string sTemp = oDoc.GetElementsByTagName("levelUp").Item(0).InnerText;
                sTemp = sTemp.Replace("\\n", "\n");
                return sTemp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameGameFinished()
        {
            try
            {
                string sTemp = oDoc.GetElementsByTagName("gameFinished").Item(0).InnerText;
                return sTemp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmGameHighScore()
        {
            try
            {
                string sTemp = oDoc.GetElementsByTagName("highScore").Item(0).InnerText;
                return sTemp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string[] GetFrmHighScoresHighScores()
        {
            try
            {
                string sTemp = oDoc.GetElementsByTagName("reset").Item(0).InnerText;
                return sTemp.Split('#');
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmHowToStep(int iStep)
        {
            try
            {
                return oDoc.GetElementsByTagName("step" + iStep.ToString()).Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmHowToLblLeyend()
        {
            try
            {
                return oDoc.GetElementsByTagName("lblLeyend").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmHowToBtnBack()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnBack").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmHowToBtnRepeat()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnRepeat").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnPlay()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnPlay").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnHowTo()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnHowTo").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnHighScores()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnHighScores").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnOptions()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnOptions").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnEditor()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnEditor").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnCredits()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnCredits").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmMenuBtnExits()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnExit").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblLanguaje()
        {
            try
            {
                return oDoc.GetElementsByTagName("lblLanguaje").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblLives()
        {
            try
            {
                return oDoc.GetElementsByTagName("lblLives").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblMusic()
        {
            try
            {
                return oDoc.GetElementsByTagName("lblMusic").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblInput()
        {
            try
            {
                return oDoc.GetElementsByTagName("lblInput").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsLblMenssaje()
        {
            try
            {
                return oDoc.GetElementsByTagName("lblMessaje").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsBtnOK()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnOK").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsBtnCancel()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnCancel").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }

        public static string GetFrmOptionsBtnRestart()
        {
            try
            {
                return oDoc.GetElementsByTagName("btnRestart").Item(0).InnerText;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
