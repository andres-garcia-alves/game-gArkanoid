using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Timers;
using System.Configuration;

namespace Garkanoid.Miscelaneous
{
    public class cMusic
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, string lpstrReturnString, long uReturnLength, long hwndCallback);

        long lRet, lCB = 0;
        string sRetString = "";
        Timer tmr;

        public cMusic()
        {
            try
            {
                string sPath = ConfigurationManager.AppSettings["pathSounds"];

                lRet = mciSendString("Stop Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Close Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Open " + @sPath + "Music.mid Type Sequencer Alias Mid01 ", sRetString, 128, lCB);

                tmr = new Timer(30000);
                tmr.Elapsed += new System.Timers.ElapsedEventHandler(TimeElapsed);
            }
            catch (Exception ex) { throw ex; }
        }

        public cMusic(string sFileName)
        {
            try
            {
                string sPath = ConfigurationManager.AppSettings["pathSounds"];

                lRet = mciSendString("Stop Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Close Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Open " + @sPath + sFileName + " Type Sequencer Alias Mid01 ", sRetString, 128, lCB);

                tmr = new Timer(30000);
                tmr.Elapsed += new System.Timers.ElapsedEventHandler(TimeElapsed);
            }
            catch (Exception ex) { throw ex; }
        }

        ~cMusic()
        {
            try
            {
                lRet = mciSendString("Stop Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Close Mid01", sRetString, 128, lCB);
            }
            catch (Exception ex) { throw ex; }
        }

        public void Play()
        {
            try
            {
                lRet = mciSendString("Play Mid01", sRetString, 128, lCB);
                tmr.Start();
            }
            catch (Exception ex) { throw ex; }
        }

        public void Stop()
        {
            try
            {
                lRet = mciSendString("Stop Mid01", sRetString, 128, lCB);
                tmr.Stop();
            }
            catch (Exception ex) { throw ex; }
        }
        
        void TimeElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string sPath = ConfigurationManager.AppSettings["pathSounds"];

                lRet = mciSendString("Stop Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Close Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Open " + @sPath + "Music.mid Type Sequencer Alias Mid01 ", sRetString, 128, lCB);
                lRet = mciSendString("Play Mid01", sRetString, 128, lCB);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
