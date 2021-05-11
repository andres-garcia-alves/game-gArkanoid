using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Timers;

namespace gArkanoid.Miscelaneous
{
    public class Music
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, string lpstrReturnString, long uReturnLength, long hwndCallback);

        long lRet, lCB = 0;
        string sRetString = "";
        Timer tmr;

        public Music()
        {
            try
            {
                string path = ConfigurationManager.AppSettings["pathSounds"];

                lRet = mciSendString("Stop Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Close Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Open " + path + "Music.mid Type Sequencer Alias Mid01 ", sRetString, 128, lCB);

                tmr = new Timer(30000);
                tmr.Elapsed += new System.Timers.ElapsedEventHandler(TimeElapsed);
            }
            catch (Exception ex) { throw ex; }
        }

        public Music(string sFileName)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["pathSounds"];

                lRet = mciSendString("Stop Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Close Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Open " + path + sFileName + " Type Sequencer Alias Mid01 ", sRetString, 128, lCB);

                tmr = new Timer(30000);
                tmr.Elapsed += new System.Timers.ElapsedEventHandler(TimeElapsed);
            }
            catch (Exception ex) { throw ex; }
        }

        ~Music()
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
        
        void TimeElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["pathSounds"];

                lRet = mciSendString("Stop Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Close Mid01", sRetString, 128, lCB);
                lRet = mciSendString("Open " + path + "Music.mid Type Sequencer Alias Mid01 ", sRetString, 128, lCB);
                lRet = mciSendString("Play Mid01", sRetString, 128, lCB);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
