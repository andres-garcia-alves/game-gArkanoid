using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Configuration;

namespace Garkanoid.Miscelaneous
{
    public class cSound
    {
        [DllImport("winmm.dll")]
        private static extern bool PlaySound(string pszSound, UIntPtr hmod, SoundFlags fdwSound);

        private static string sPath = ConfigurationManager.AppSettings["pathSounds"] + "Brick.wav";

        public enum SoundFlags
        {
            SND_SYNC = 0x0000,          // play synchronously (default)
            SND_ASYNC = 0x0001,         // play asynchronously
            SND_NODEFAULT = 0x0002,     // silence
            SND_MEMORY = 0x0004,        // pszSound points to a memory file
            SND_LOOP = 0x0008,          // loop the sound until next sndPlaySound
            SND_NOSTOP = 0x0010,        // don't stop any currently playing sound
            SND_NOWAIT = 0x00002000,    // don't wait if the driver is busy
            SND_ALIAS = 0x00010000,     // name is a Registry alias
            SND_ALIAS_ID = 0x00110000,  // alias is a predefined ID
            SND_FILENAME = 0x00020000,  // name is file name
            SND_RESOURCE = 0x00040004,  // name is resource name or atom
            SND_PURGE = 0x0040,         // purge non-static events for task
            SND_APPLICATION = 0x0080    // look for application-specific association
        }

        public cSound()
        {
        }

        public void Play()
        {
            PlaySound(sPath, UIntPtr.Zero, SoundFlags.SND_ASYNC);
        }

        public void Stop()
        {
            PlaySound(null, UIntPtr.Zero, SoundFlags.SND_PURGE);
        }
    }
}
