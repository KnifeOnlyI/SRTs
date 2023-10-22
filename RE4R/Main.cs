using ProcessMemory;
using ProcessMemory.windows;
using RE4R.data;

var process = new ProcessWrapper("re4");

var playerHealthPtr = new MultiLevelPointer(0x0DBB88C0, new long[] { 0xA0, 0x40, 0x148, 0x10 });
var playerHealthAddress = process.ReadMultiLevelPointer(playerHealthPtr).GetValueOrDefault();
var playerHealth = process.Read<PlayerHealth>(playerHealthAddress).GetValueOrDefault();

Console.WriteLine($"Player HP: {playerHealth.Hp}/{playerHealth.MaxHP}");