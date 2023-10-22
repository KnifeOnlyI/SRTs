using System.Runtime.InteropServices;

namespace RE4R.data;

[StructLayout(LayoutKind.Explicit)]
public struct PlayerHealth
{
    [FieldOffset(0x0)] public readonly int MaxHP;
    [FieldOffset(0x4)] public readonly int Hp;
}