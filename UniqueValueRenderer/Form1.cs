using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UniqueValueRenderer
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FrmUniqueValueRenderer frmUniqueValueRenderer = new FrmUniqueValueRenderer(axMapControl1.Map);
            frmUniqueValueRenderer.Show();
        }
    }
}
