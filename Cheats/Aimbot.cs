using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using ZBase.Classes;
using ZBase.Utilities;

namespace ZBase.Cheats
{
    public class Aimbot
    {
        public static void Run()
        {
            Signatures sOffsets = new Signatures();
            Netvars nOffsets = new Netvars();

            var memory = Process.GetProcessesByName(Memory.WindName)[0];
            var client = Memory.GetModuleAddress("client.dll");
            var engine = Memory.GetModuleAddress("engine.dll");

            while (true)
            {
                if (Main.S.AimbotEnabled)
                {
                    Thread.Sleep(1);

                    if (!Tools.HoldingKey(Keys.VK_RBUTTON))
                        continue;

                    // Local Player
                    var localPlayer = Memory.ReadMemory<IntPtr>(client + sOffsets.dwLocalPlayer);
                    var localTeam = Memory.ReadMemory<int>(localPlayer + nOffsets.m_iTeamNum);

                    // Crosshair position
                    var localEyePosition = Memory.ReadMemory<Vector3>(localPlayer + nOffsets.m_vecOrigin) +
                                            Memory.ReadMemory<Vector3>(localPlayer + nOffsets.m_vecViewOffset);

                    var clientState = Memory.ReadMemory<IntPtr>(engine + sOffsets.dwClientState);
                    var localPlayerId = Memory.ReadMemory<int>(clientState + sOffsets.dwClientState_GetLocalPlayer);

                    var viewAngles = Memory.ReadMemory<Vector3>(clientState + sOffsets.dwClientState_ViewAngles);
                    var aimPunch = Memory.ReadMemory<Vector3>(localPlayer + nOffsets.m_aimPunchAngle) * 2;

                    // Aimbot fov
                    float bestFov = 5f;
                    var bestAngle = new Vector3();

                    for (int i = 1; i <= 32; i++)
                    {
                        var player = Memory.ReadMemory<IntPtr>(client + sOffsets.dwEntityList + i * 0x10);

                        if (Memory.ReadMemory<int>(player + nOffsets.m_iTeamNum) == localTeam ||
                            Memory.ReadMemory<bool>(player + sOffsets.m_bDormant) ||
                            Memory.ReadMemory<int>(player + nOffsets.m_lifeState) != 0)
                            continue;

                        if ((Memory.ReadMemory<int>(player + nOffsets.m_bSpottedByMask) & (1 << localPlayerId)) != 0)
                        {
                            var boneMatrix = Memory.ReadMemory<IntPtr>(player + nOffsets.m_dwBoneMatrix);

                            // pos 8 :: player head
                            var playerHeadPosition = new Vector3(
                                Memory.ReadMemory<float>(boneMatrix + 0x30 * 8 + 0x0C),
                                Memory.ReadMemory<float>(boneMatrix + 0x30 * 8 + 0x1C),
                                Memory.ReadMemory<float>(boneMatrix + 0x30 * 8 + 0x2C)
                            );

                            var angle = CalculateAngle(localEyePosition, playerHeadPosition, viewAngles + aimPunch);
                            var fov = Math.Sqrt(angle.X * angle.X + angle.Y * angle.Y);

                            if (fov < bestFov)
                            {
                                bestFov = (float)fov;
                                bestAngle = angle;
                            }
                        }
                    }

                    // Überprüfen, ob bestAngle nicht null ist, und dann die Speicheradresse aktualisieren
                    if (!bestAngle.IsZero())
                        Memory.WriteMemory(clientState + sOffsets.dwClientState_ViewAngles, viewAngles + bestAngle / 3f);
                }
            }
        }

        private static Vector3 CalculateAngle(Vector3 localPosition, Vector3 enemyPosition, Vector3 viewAngles)
        {
            return (enemyPosition - localPosition).ToAngle() - viewAngles;
        }
    }
}
