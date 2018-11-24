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
    public partial class Form1 : Form
    {
        public static Form1 formInctance;

        Panel newPanel;
        //Graphics myGraphics = null;

        SolidBrush sBrush = new SolidBrush(Color.Black);

        Color colorToUse;
        Color backGroundColor;
        Color eraserColor;
        Graphics graphicsToUse = null;
        private int brushSize;

        private int layerWidth;
        private int layerHeight;
        private int layerNum = 0;
        private int selectedLayer = 0;

        bool mouseDown;

        List<Panel> layers = new List<Panel>();
        List<Graphics> graphics = new List<Graphics>();

        public Form1()
        {
            InitializeComponent();
            formInctance = this;
            backGroundColor = Color.White;
            eraserColor = Color.White;            
        }
        private void createNew_Click(object sender, EventArgs e)
        {
            SetSize newSize = new SetSize();
            newSize.Show();

        }

        /// <summary>
        /// Creates a baseLayer based on given parameters.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void CreatePanel(int width, int height)
        {
            Graphics myGraphics = null;
            graphics.Add(myGraphics);
            newPanel = new Panel();
            if (layerNum == 0)
            {
                newPanel.BackColor = Color.White;
                newPanel.Location = new Point(backGround.Width / 2 - width / 2, backGround.Height / 2 - height / 2);

                
            }

            if (layerNum >= 1)
            {
                //newPanel.BackColor = Color.Transparent;
                newPanel.Location = new Point(layers[selectedLayer].Width / 2 - width / 2, layers[selectedLayer].Height / 2 - height / 2);
            }

            newPanel.Name = "canvas";
          
            newPanel.Size = new Size(width, height);
            newPanel.AutoSize = false;
            newPanel.MouseMove += MMove;
            newPanel.MouseClick += MClick;
            newPanel.MouseUp += MUp;
            newPanel.MouseDown += MDown;
            layerHeight = height;
            layerWidth = width;
            layers.Add(newPanel);
            if (layerNum == 0)
            {
                backGround.Controls.Add(newPanel);
                newPanel.BringToFront();

            }

            if (layerNum >= 1)
            {
                newPanel.BringToFront();
                backGround.Controls.Add(newPanel);
                newPanel.BringToFront();
            }




            checkedListBox1.Items.Add(newPanel);
            graphics[layerNum] = layers[layerNum].CreateGraphics();
            graphicsToUse = graphics[selectedLayer];
            layerNum += 1;
        }

        public void MClick(object sender, MouseEventArgs e)
        {
            brushSize = sb_brushSize.Value;
            Point mClick = layers[selectedLayer].PointToClient(Cursor.Position);
            graphicsToUse.FillEllipse(sBrush, mClick.X-brushSize/2, mClick.Y-brushSize/2, brushSize, brushSize);
        }

        private void MDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        public void MMove(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point mPoint = layers[selectedLayer].PointToClient(Cursor.Position);
                graphicsToUse.FillEllipse(sBrush, mPoint.X - brushSize / 2, mPoint.Y - brushSize / 2, brushSize, brushSize);
            }           
        }

        private void MUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void sb_brushSize_Scroll(object sender, ScrollEventArgs e)
        {
            brushSize = sb_brushSize.Value;
            l_BrushSize.Text = brushSize.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            colorToUse = colorDialog1.Color;
            sBrush.Color = colorDialog1.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            backGroundColor = colorDialog1.Color;
            eraserColor = backGroundColor;
           graphicsToUse.Clear(backGroundColor);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sBrush.Color = eraserColor;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sBrush.Color = colorToUse;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CreatePanel(layerWidth, layerHeight);
        }

        private void SelectLayer()
        {
            selectedLayer = int.Parse(textBox1.Text);
            graphicsToUse = graphics[selectedLayer];
            layers[selectedLayer].BringToFront();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedLayer = checkedListBox1.SelectedIndex;
            //textBox1.Text = selectedLayer.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SelectLayer();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
    }
}
