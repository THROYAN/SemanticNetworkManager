using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

using GraphEditor.App.Models;

namespace SemanticNetworkManager.App.Views
{
    public partial class ConceptVertexModifyForm : Form
    {
        public bool Succesful = false;
        public IVertexWrapper VertexWrapper { get; set; }
        public ConceptVertexModifyForm(IVertexWrapper vertexWrapper)
        {
            this.VertexWrapper = vertexWrapper.Clone() as IVertexWrapper;

            InitializeComponent();
        }

        private void VertexModifyForm_Load(object sender, EventArgs e)
        {
            vertexNameTextBox.Text = VertexWrapper.Name;
            xTextBox.Text = (VertexWrapper as WFVertexWrapper).Coords.X.ToString();
            yTextBox.Text = (VertexWrapper as WFVertexWrapper).Coords.Y.ToString();
            numericUpDown1.Value = Convert.ToDecimal((VertexWrapper.Vertex as ConceptVertex).Id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VertexWrapper.Name = vertexNameTextBox.Text;
            (VertexWrapper.Vertex as ConceptVertex).Id = Convert.ToInt32(numericUpDown1.Value);
            float x;
            float y;
            if (!float.TryParse(xTextBox.Text, out x))
            {
                x = (VertexWrapper as WFVertexWrapper).Coords.X;
            }
            if (!float.TryParse(yTextBox.Text, out y))
            {
                y = (VertexWrapper as WFVertexWrapper).Coords.Y;
            }

            (VertexWrapper as WFVertexWrapper).Coords = new PointF(x, y);
            Succesful = true;
            Close();
        }
    }
}
