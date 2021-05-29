using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace Piano
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
           
        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            playGame P = new playGame();
            P.ShowDialog();
            
        }

        private void menu_Load(object sender, EventArgs e)
        {
            Data.col_pol =4;
            Data.lvl = "Легкий";
        }

        #region ItemSelect_lvl_colCol
        private void легкийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.lvl = "Легкий";
        }

        private void среднийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.lvl = "Средний";
        }

        private void сложныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.lvl = "Сложный";
        }

        private void невозможныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.lvl = "Невозможный";
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Data.col_pol = 4;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Data.col_pol = 5;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Data.col_pol = 6;
        }

        #endregion


      
    }
}
