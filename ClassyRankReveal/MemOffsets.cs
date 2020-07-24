using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassyRankReveal
{
    public static class MemOffsets
    {

        public static int ClientDLLAddress { get; set; } = 0;
        public static int EngineDLLAddress { get; set; } = 0;


        public static int EntityListPointer { get; set; } = 0x4D5348C;


        public static int ClientStatePointer { get; set; } = 0x58ADD4;
        public static int ClientStatePlayerInfo { get; set; } = 0x52B8;

        public static int PlayerResourcePointer { get; set; } = 0x3182D70;


        public static int TeamOffset { get; set; } = 0xF4;


        public static int PlayerWinsOffset { get; set; } = 0x1B88;
        public static int PlayerRankOffset { get; set; } = 0x1A84;
    }
}
