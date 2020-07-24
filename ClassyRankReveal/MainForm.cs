using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassyRankReveal
{
    public partial class MainForm : Form
    {

        public void InitDataGrid()
        {
            dataGridView1.ColumnCount = 6;

            dataGridView1.Columns[0].Name = "Index";
            dataGridView1.Columns[1].Name = "Team";
            dataGridView1.Columns[2].Name = "Name";
            dataGridView1.Columns[3].Name = "SteamID";
            dataGridView1.Columns[4].Name = "Rank";
            dataGridView1.Columns[5].Name = "Wins";
        }
        public MainForm()
        {
            InitializeComponent();
            InitDataGrid();


          //  dataGridView1.Rows.Add( "Hi", "there" , "test");
        }

        Bitmap img;

        

        private void RefreshButton_Click(object sender, EventArgs e)
        {


            dataGridView1.Rows.Clear();

            int TotalCTs = 0;
            int TotalTs = 0;

            int CtRankCalc = 0;
            int TRankCalc = 0;

            foreach (PlayerEntity Player in Utility.GetEntities())
            {


                int PlayerResource = Program.Mem.ReadInt(MemOffsets.ClientDLLAddress + MemOffsets.PlayerResourcePointer);
                int Wins = Program.Mem.ReadInt(PlayerResource + MemOffsets.PlayerWinsOffset +   ( (Player.Index + 1) * 4));
                int Rank = Program.Mem.ReadInt(PlayerResource + MemOffsets.PlayerRankOffset + ((Player.Index + 1) * 4));

                string RankName = Utility.GetRankNameFromNumber(Rank);

                if (Player.GetSteamID() != "BOT")
                {
                    dataGridView1.Rows.Add(Player.Index ,Player.GetTeamName(),  Player.GetName(), Player.GetSteamID(), RankName, Wins);
                }



                int TeamNum = Player.GetTeamNum();
                if (TeamNum == 2)
                {
                    TotalTs++;
                    TRankCalc = TRankCalc +  Rank ;
                }
                if (TeamNum == 3)
                {
                    TotalCTs++;
                    CtRankCalc = CtRankCalc +  Rank  ;
                }
                Console.WriteLine("{0:X}", Player.GetPlayerInformationStructure());
            }


            int TAverage = 0;
            int CTAverage = 0;
            if (TotalTs != 0 && TRankCalc != 0)
            {
               TAverage = TRankCalc / TotalTs;
            }

            if (TotalCTs != 0 && CtRankCalc != 0)
            {
                CTAverage = CtRankCalc / TotalCTs;
            }
           

            CTRankAvLabel.Text = "CT Rank Average:" + Utility.GetRankNameFromNumber(CTAverage);
            TRankAvLabel.Text = "T Rank Average: " + Utility.GetRankNameFromNumber(TAverage);


            dataGridView1.AutoResizeColumns();
            Console.WriteLine("----");

        }
    }
}
