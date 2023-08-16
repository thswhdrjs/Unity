using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
[Serializable]
public struct Packet
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public bool[] isConnect;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public float[] posX;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public float[] posY;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public float[] posZ;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public float[] rotX;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public float[] rotY;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
    public float[] rotZ;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string ip;
}