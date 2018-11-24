using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperDraw
{
    public partial class SetSize : Form
    {
        private int width;
        private int height;

        public int pWidth
        {
            get { return width; }
        }

        public int pHeight
        {
            get { return height; }
        }

        Form1 thisForm;

        public SetSize()
        {
            
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            thisForm = Form1.formInctance;
            height = int.Parse(tb_height.Text);
            width = int.Parse(tb_width.Text);
            thisForm.CreatePanel(width, height);
            this.Close();
        }
    }
}
