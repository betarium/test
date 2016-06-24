using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Betarium.Betapeta
{
    public partial class MemoForm : Form
    {
        public ProgramForm OwnerForm { get; set; }
        public string FileName { get; set; }

        public MemoForm()
        {
            InitializeComponent();
        }

        private void MemoForm_Load(object sender, EventArgs e)
        {

        }

        private void MemoText_DoubleClick(object sender, EventArgs e)
        {
            OwnerForm.EditMemo(FileName);
        }
    }
}
