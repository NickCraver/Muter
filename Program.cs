using System;
using System.Runtime.InteropServices;

namespace Muter
{
    class Program
    {
        static void Main(string[] args)
        {
            var deviceEnum = new MMDeviceEnumerator() as IMMDeviceEnumerator;
            IMMDevice defaultDevice;
            Marshal.ThrowExceptionForHR(deviceEnum.GetDefaultAudioEndpoint(0, 1, out defaultDevice));

            IAudioEndpointVolume master;
            var aevId = typeof(IAudioEndpointVolume).GUID;
            Marshal.ThrowExceptionForHR(defaultDevice.Activate(ref aevId, 23, 0, out master));

            var muting = !(args.Length > 0 && args[0] == "unmute");
            Console.WriteLine(muting ? "Muting..." : "Unmuting...");
            Marshal.ThrowExceptionForHR(master.SetMute(muting, Guid.Empty));
        }
    }

    [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    class MMDeviceEnumerator { }

    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceEnumerator
    {
        int f(); // You have to have this, don't ask questions.
        int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice endpoint);
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDevice
    {
        int Activate(ref Guid id, int clsCtx, int activationParams, out IAudioEndpointVolume pointer);
    }

    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioEndpointVolume
    {
        int f(); int g(); int h(); int i(); int j(); int k(); int l(); int m(); int n();
        int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);
        int GetMasterVolumeLevelScalar(out float pfLevel);
        int SetMute([MarshalAs(UnmanagedType.Bool)] Boolean bMute, Guid pguidEventContext);
        int GetMute(out bool pbMute);
    }
}
