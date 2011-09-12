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

using SemanticNetworkManager.App.Models.Wrappers;

namespace SemanticNetworkManager.App.Views
{
    public partial class RelationArcModifyForm : Form
    {
        public bool Succesful = false;
        public IArcWrapper ArcWrapper { get; set; }

        public RelationArcModifyForm(IArcWrapper arcWrapper)
        {
            InitializeComponent();
            this.ArcWrapper = arcWrapper.Clone() as IArcWrapper;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ArcWrapper.Head = this.ArcWrapper.graphWrapper.VertexWrappers.Find(v => v.Vertex.Value.Equals(this.headComboBox.SelectedItem));
            this.ArcWrapper.Tail = this.ArcWrapper.graphWrapper.VertexWrappers.Find(v => v.Vertex.Value.Equals(this.tailComboBox.SelectedItem));

            (this.ArcWrapper.Edge as RelationArc).Id = (this.ArcWrapper.graphWrapper.Graph as SemanticGraph).RelationsDefinitions.Find(rd => rd.Name == this.relationComboBox.SelectedItem.ToString()).Id;

            this.Succesful = true;
            Close();
        }

        private void ArcModifyForm_Load(object sender, EventArgs e)
        {
            this.headComboBox.Items.Clear();
            this.tailComboBox.Items.Clear();
            this.relationComboBox.Items.Clear();

            this.headComboBox.Items.AddRange(this.ArcWrapper.graphWrapper.Graph.GetVertices().Select(v => v.Value).ToArray());
            this.tailComboBox.Items.AddRange(this.ArcWrapper.graphWrapper.Graph.GetVertices().Select(v => v.Value).ToArray());
            this.relationComboBox.Items.AddRange((this.ArcWrapper.graphWrapper.Graph as SemanticGraph).RelationsDefinitions.Select(rd => rd.Name).ToArray());

            this.headComboBox.SelectedItem = this.ArcWrapper.Head.Vertex.Value;
            this.tailComboBox.SelectedItem = this.ArcWrapper.Tail.Vertex.Value;
            this.relationComboBox.SelectedItem = (this.ArcWrapper.Edge as RelationArc).Name;
        }
    }
}
