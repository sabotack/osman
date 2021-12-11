using System;
using System.Threading;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Media;
using System.Runtime.InteropServices;

namespace osman
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            const int SW_HIDE = 0;

            int sleepTime, repeat;

            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            SoundPlayer ndokowaoh = new SoundPlayer(Properties.Resource1.Ndokowaoh);
   
            defaultPlaybackDevice.Mute(false);
            defaultPlaybackDevice.Volume = 30;

            Console.Beep(2000, 250);

            Console.WriteLine("Enter number of seconds to wait:");
            sleepTime = int.Parse(Console.ReadLine())*1000;

            Console.WriteLine("Enter number of times to repeat:");
            repeat = int.Parse(Console.ReadLine());

            // Hide console window and taskbar icon
            ShowWindow(handle, SW_HIDE);

            // Wait
            Thread.Sleep(sleepTime);

            for (int i = 0; i < repeat; i++)
            {
                // Unmute default sound device, max volume and play ndokowaoh
                defaultPlaybackDevice.Mute(false);
                defaultPlaybackDevice.Volume = 100;

                ndokowaoh.Play();
                Thread.Sleep(3300);
            }
        }
    }
}
