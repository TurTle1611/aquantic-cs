using ZBase.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZBase.Classes
{
    public class Entity
    {
        public IntPtr EntityBase; // Ändere den Typ von int zu IntPtr

        public Entity(int Base)
        {
            this.EntityBase = (IntPtr)Base; // Konvertiere Base zu IntPtr
        }

        public int Health
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iHealth);
            }
        }

        public int GlowIndex
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iGlowIndex);
            }
        }

        public int Team
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iTeamNum);
            }
        }

        public int Flags
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_fFlags);
            }
        }

        public bool IsTeammate
        {
            get
            {
                return Team == G.Engine.LocalPlayer.Team;
            }
        }

        public Vector3 Position
        {
            get
            {
                return Memory.ReadMemory<Vector3>(EntityBase + Main.O.netvars.m_vecOrigin);
            }
        }

        public Vector3 EyePosition
        {
            get
            {
                return Position + Memory.ReadMemory<Vector3>(EntityBase + Main.O.netvars.m_vecViewOffset);
            }
        }

        public Vector3 HeadPosition
        {
            get
            {
                return GetBonePosition(8);
            }
        }

        public Vector3 GetBonePosition(int BoneID)
        {
            int bonematrix = Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_dwBoneMatrix);
            float x = Memory.ReadMemory<float>((IntPtr)(bonematrix + 0x30 * BoneID + 0x0C));
            float y = Memory.ReadMemory<float>((IntPtr)bonematrix + 0x30 * BoneID + 0x1C);
            float z = Memory.ReadMemory<float>((IntPtr)bonematrix + 0x30 * BoneID + 0x2C);
            return new Vector3(x, y, z);
        }

        public bool Spotted
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bSpotted);
            }
            set
            {
                Memory.WriteMemory<bool>(EntityBase + Main.O.netvars.m_bSpotted, Convert.ToByte(value));
            }
        }

        public bool IsLocalPlayer
        {
            get
            {
                return G.Engine.LocalPlayer.EntityBase == EntityBase;
            }
        }

        public int FlashDuration
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_flFlashDuration);
            }
            set
            {
                Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_flFlashDuration, value);
            }
        }

        public bool Dormant
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.signatures.m_bDormant);
            }
        }

        public int CrosshairID
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iCrosshairId);
            }
        }

        public void Glow(Color color)
        {
            GlowStruct GlowObj = Memory.ReadMemory<GlowStruct>((IntPtr)(G.Engine.GlowObjectManager + GlowIndex * 0x38));

            GlowObj.r = (float)color.R / 255;
            GlowObj.g = (float)color.G / 255;
            GlowObj.b = (float)color.B / 255;
            GlowObj.a = (float)color.A / 255;
            GlowObj.m_bRenderWhenOccluded = true;
            GlowObj.m_bRenderWhenUnoccluded = false;

            Memory.WriteMemory<GlowStruct>((IntPtr)(G.Engine.GlowObjectManager + GlowIndex * 0x38), GlowObj);
        }

        public void Cham(Color color)
        {
            // these only show while visible
            Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender, color.R);
            Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender + 1, color.G);
            Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender + 2, color.B);
            Memory.WriteMemory<int>(EntityBase + Main.O.netvars.m_clrRender + 3, color.A);
        }

        public void ResetChams()
        {
            Cham(Color.FromArgb(255, 255, 255, 0));
        }

        public bool Alive
        {
            get
            {
                return Health > 0 && Health <= 100;
            }
        }

        public bool Dead
        {
            get
            {
                return !Alive;
            }
        }

        public bool IsPlayer
        {
            get
            {
                return Team == 2 || Team == 3; // Assuming these are the player team numbers
            }
        }

        public WeaponID WeaponID
        {
            get
            {
                int weaponbase = Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_hActiveWeapon);
                int yes = weaponbase & 0xFFF;
                int ok = Memory.ReadMemory<int>((IntPtr)Memory.Client + Main.O.signatures.dwEntityList + (yes - 1) * 16);
                return (WeaponID)Memory.ReadMemory<int>((IntPtr)(ok + Main.O.netvars.m_iItemDefinitionIndex));
            }
        }

        public string WeaponName
        {
            get
            {
                switch (this.WeaponID)
                {
                    case WeaponID.DEAGLE: return "Desert Eagle";
                    case WeaponID.BERETTAS: return "Dual Berettas";
                    case WeaponID.FIVESEVEN: return "Five-SeveN";
                    case WeaponID.GLOCK: return "Glock-18";
                    case WeaponID.AK47: return "AK-47";
                    case WeaponID.AUG: return "AUG";
                    case WeaponID.AWP: return "AWP";
                    case WeaponID.FAMAS: return "FAMAS";
                    case WeaponID.G3SG1: return "G3SG1";
                    case WeaponID.GALILAR: return "Galil";
                    case WeaponID.M249: return "M249";
                    case WeaponID.M4A4: return "M4A1";
                    case WeaponID.MAC10: return "MAC-10";
                    case WeaponID.P90: return "P90";
                    case WeaponID.UMP45: return "UMP-45";
                    case WeaponID.XM1014: return "XM1014";
                    case WeaponID.BIZON: return "PP-Bizon";
                    case WeaponID.MAG7: return "MAG-7";
                    case WeaponID.NEGEV: return "Negev";
                    case WeaponID.SAWEDOFF: return "Sawed-Off";
                    case WeaponID.TEC9: return "Tec-9";
                    case WeaponID.TASER: return "Taser";
                    case WeaponID.P2000: return "P2000";
                    case WeaponID.MP7: return "MP7";
                    case WeaponID.MP9: return "MP9";
                    case WeaponID.NOVA: return "Nova";
                    case WeaponID.P250: return "P250";
                    case WeaponID.MP5SD: return "MP5-SD";
                    case WeaponID.SCAR20: return "SCAR-20";
                    case WeaponID.SG556: return "SG 553";
                    case WeaponID.SSG08: return "SSG 08";
                    case WeaponID.KNIFE: return "Knife";
                    case WeaponID.FLASHBANG: return "Flashbang";
                    case WeaponID.HEGRENADE: return "Grenade";
                    case WeaponID.SMOKEGRENADE: return "Smoke Grenade";
                    case WeaponID.MOLOTOV: return "Molotov";
                    case WeaponID.DECOY: return "Decoy";
                    case WeaponID.INCGRENADE: return "Incendiary Grenade";
                    case WeaponID.C4: return "C4";
                    case WeaponID.M4A1S: return "M4A1-S";
                    case WeaponID.USPS: return "USP-S";
                    case WeaponID.CZ75A: return "CZ75-Auto";
                    case WeaponID.REVOLVER: return "R8 Revolver";
                    default: return "Knife";
                }
            }
        }

        public bool Valid
        {
            get
            {
                return !Dormant && !Dead && IsPlayer;
            }
        }

        public Vector2 AimPunchAngle
        {
            get
            {
                return Memory.ReadMemory<Vector2>(EntityBase + Main.O.netvars.m_aimPunchAngle);
            }
        }

        public int ShotsFired
        {
            get
            {
                return Memory.ReadMemory<int>(EntityBase + Main.O.netvars.m_iShotsFired);
            }
        }

        public float Velocity
        {
            get
            {
                Vector3 vel = Memory.ReadMemory<Vector3>(EntityBase + Main.O.netvars.m_vecVelocity);
                return (float)Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);
            }
        }

        public bool Scoped
        {
            get
            {
                return Memory.ReadMemory<bool>(EntityBase + Main.O.netvars.m_bIsScoped);
            }
        }

        public float Distance
        {
            get
            {
                return Vector3.Distance(G.Engine.LocalPlayer.Position, Position);
            }
        }
    }
}
