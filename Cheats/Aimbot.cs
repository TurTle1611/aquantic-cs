using System;
using System.Threading;
using ZBase.Utilities;

namespace ZBase.Cheats
{
    public class Aimbot
    {
        public static void Run()
        {
            var memory = new Memory("csgo.exe");
            var client = memory.GetModuleAddress("client.dll");
            var engine = memory.GetModuleAddress("engine.dll");

            while (true)
            {
                Thread.Sleep(1);

                if (!Tools.HoldingKey(Keys.VK_SPACE))
                    continue;

                var localPlayer = memory.Read<IntPtr>(client + Offsets.dwClientState_GetLocalPlayer);
                var localTeam = memory.Read<int>(localPlayer + Offsets.m_iTeamNum);
                var localEyePosition = memory.Read<Vector3>(localPlayer + Offsets.m_vecOrigin) +
                                       memory.Read<Vector3>(localPlayer + Offsets.m_vecViewOffset);

                var clientState = memory.Read<IntPtr>(engine + Offsets.dwClientState);
                var localPlayerId = memory.Read<int>(clientState + Offsets.dwClientState_GetLocalPlayer);
                var viewAngles = memory.Read<Vector3>(clientState + Offsets.dwClientState_ViewAngles);
                var aimPunch = memory.Read<Vector3>(localPlayer + Offsets.m_aimPunchAngle) * 2;

                float bestFov = 5f;
                var bestAngle = new Vector3();

                for (int i = 1; i <= 32; i++)
                {
                    var player = memory.Read<IntPtr>(client + Offsets.dwEntityList + i * 0x10);

                    if (memory.Read<int>(player + Offsets.m_iTeamNum) == localTeam ||
                        memory.Read<bool>(player + Offsets.m_.m_bDormant) ||
                        memory.Read<int>(player + Offsets.m_lifeState) != 0)
                        continue;

                    if ((memory.Read<int>(player + Offsets.m_bSpottedByMask) & (1 << localPlayerId)) != 0)
                    {
                        var boneMatrix = memory.Read<IntPtr>(player + Offsets.m_dwBoneMatrix);
                        var playerHeadPosition = new Vector3(
                            memory.Read<float>(boneMatrix + 0x30 * 8 + 0x0C),
                            memory.Read<float>(boneMatrix + 0x30 * 8 + 0x1C),
                            memory.Read<float>(boneMatrix + 0x30 * 8 + 0x2C)
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

                if (!bestAngle.IsZero())
                    memory.Write(clientState + Offsets.dwClientState_ViewAngles, viewAngles + bestAngle / 3f);
            }
        }

        private static Vector3 CalculateAngle(Vector3 localPosition, Vector3 enemyPosition, Vector3 viewAngles)
        {
            return (enemyPosition - localPosition).ToAngle() - viewAngles;
        }
    }
}
