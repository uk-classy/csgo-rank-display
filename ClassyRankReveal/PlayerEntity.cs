using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassyRankReveal
{
    public class PlayerEntity
    {

        public bool IsValid { get; set; } = false;
        public int MemAddress { get; set; } = 0;

        public int Index { get; set; } = 0;
        public PlayerEntity(int index)
        {
            this.Index = index;
            MemAddress = Program.Mem.ReadInt(MemOffsets.ClientDLLAddress + MemOffsets.EntityListPointer + (index * 0x10));
            if (MemAddress != 0)
            {
                IsValid = true;
            }
        }



        public int GetPlayerInformationStructure()
        {
            int CurrentClientState = Program.Mem.ReadInt(MemOffsets.EngineDLLAddress + MemOffsets.ClientStatePointer);
            int CurrentUserInformationPointer = Program.Mem.ReadInt(CurrentClientState + MemOffsets.ClientStatePlayerInfo);
            int One = Program.Mem.ReadInt(CurrentUserInformationPointer + 0x40);
            int Two = Program.Mem.ReadInt(One + 0xC);
            return Program.Mem.ReadInt(Two + 0x28 + (Index * 0x34));
        }

    

        public string GetSteamID()
        {
            return Program.Mem.ReadString(GetPlayerInformationStructure() + 148, 20, false);
        }

        public string GetName()
        {
            return Program.Mem.ReadString(GetPlayerInformationStructure() + 0x10, 128, false);
        }


        public int GetTeamNum()
        {
            return Program.Mem.ReadInt(MemAddress + MemOffsets.TeamOffset);
        }


        string[] TeamNums = new string[] { "Connecting", "Spectator",  "Terrorist" , "Counter-Terrorist"};

        public string GetTeamName()
        {
            int TeamNum = GetTeamNum();
            
            if (TeamNum >= 0 && TeamNum < TeamNums.Length)
            {
                return TeamNums[TeamNum];
            }

            return "Undefined";
        }


    }
}
