using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiceBot.UserControls
{
    public partial class ProgrammerModeControl : UserControl
    {
        public ProgrammerModeControl()
        {

            InitializeComponent();

            //pnlControlProgrammer.Dock = DockStyle.Fill;
            //pnlProgrammer.Dock = DockStyle.Fill;

        }

        private void ProgrammerModeControl_Resize(object sender, EventArgs e)
        {
            this.pnlProgrammerX.Width = this.Width;
            this.pnlProgrammerX.Height = this.Height;
        }
    }
}
