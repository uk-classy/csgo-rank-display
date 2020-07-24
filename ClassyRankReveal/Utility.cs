using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassyRankReveal
{
    public static class Utility
    {
        public static string[] RankArray = new string[] { "Unranked", "Silver I", "Silver II", "Silver III", "Silver IV", "Silver Elite", "Silver Elite Master",
        "Gold Nova I", "Gold Nova II", "Gold Nova III","Gold Nova Master",
        "Master Guardian I", "Master Guardian II", "Master Guardian Elite", "Distinguished Master Guardian", "Legendary Eagle", "Legendary Eagle master", "Supreme", "Global Elite"};

        public static List<PlayerEntity> GetEntities()
        {
            List<PlayerEntity> Players = new List<PlayerEntity>();
            for (int i =0; i <32; i++)
            {
                PlayerEntity Ent = new PlayerEntity(i);
                if (Ent.IsValid)
                {
                    Players.Add(Ent);
                }
            }
            return Players;
        }


       
        public static void DisplayErrorMessage(string Message, string Caption)
        {
            MessageBox.Show(Message, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }






        public static string GetRankNameFromNumber(int RankID)
        {
            if (RankID >= 0 && RankID <= RankArray.Length)
            {
                return RankArray[RankID];
            }
           
            return "";
        }
    }
}
