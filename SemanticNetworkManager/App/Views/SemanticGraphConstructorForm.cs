using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MagicLibrary.MathUtils.Graphs;
using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

using GraphEditor.App.Models;
using GraphEditor.App.Views;

using SemanticNetworkManager.App.Models.Wrappers;

namespace SemanticNetworkManager.App.Views
{
    public partial class SemanticGraphConstructorForm : Form, IGraphConstructor
    {
        public bool Succesful { get; set; }
        public int height;
        public WFGraphWrapper GraphWrapper { get; set; }

        public SemanticGraphConstructorForm(WFGraphWrapper graphWrapper)
        {
            InitializeComponent();
            this.Succesful = false;
            GraphWrapper = graphWrapper.Clone() as WFGraphWrapper;
            //graphWrapper.CopyTo(GraphWrapper);
            verticesCounter.Value = graphWrapper.Graph.Order;
        }

        private void verticesCount_ValueChanged(object sender, EventArgs e)
        {
            int newVerticesCount = Convert.ToInt32((sender as NumericUpDown).Value);
            int oldVerticesCount = GraphWrapper.Graph.Order;
            if (oldVerticesCount < newVerticesCount)
            {
                Random r = new Random();
                for (int i = oldVerticesCount; i < newVerticesCount; i++)
                {
                    PointF p = new PointF(r.Next(500),r.Next(500));
                    while (GraphWrapper.VertexWrappers.Any(
                            v =>
                                (v as WFVertexWrapper).RectangleF.IntersectsWith(
                                    new RectangleF(p, GraphWrapper.DefaultVertexSize)
                                )))
                    {
                        p.X += 10;
                        if (p.X >= 500)
                        {
                            p.Y += 10;
                            p.X = 0;
                        }
                    }
                    var sg = GraphWrapper as SemanticGraphWrapper;
                    
                    sg.AddConcept(MagicLibrary.Graphic.MGraphic.T(GraphWrapper.DefaultVertexSize.Width,GraphWrapper.DefaultVertexSize.Height) * p);
                }
            }
            if (oldVerticesCount > newVerticesCount)
            {
                for (int i = newVerticesCount; i < oldVerticesCount; i++)
                {
                    GraphWrapper.Graph.RemoveVertex(GraphWrapper.Graph.VerticesValues.Last());
                }
            }
            RefreshValues();
        }

        private void GraphConstructorForm_Resize(object sender, EventArgs e)
        {
            incidentsGrid.Height = this.Height - height;
        }

        private void GraphConstructorForm_Load(object sender, EventArgs e)
        {
            height = this.Height - incidentsGrid.Height;
            RefreshValues();
        }

        public void RefreshValues()
        {
            verticesComboBox.Items.Clear();
            verticesComboBox.Items.AddRange(GraphWrapper.Graph.VerticesValues);
            verticesComboBox.SelectedIndex = verticesComboBox.Items.Count - 1;

            LoadDataToGrid(
                incidentsGrid,
                GraphWrapper.Graph.IncidentsMatrixTopHeaders,
                GraphWrapper.Graph.IncidentsMatrixLeftHeaders,
                GraphWrapper.Graph.IncidentsMatrix
            );
        }

        public void LoadDataToGrid(DataGridView grid, object[] topHeaders, object[] leftHeaders, MagicLibrary.MathUtils.Matrix values)
        {
            grid.Columns.Clear();
            grid.Rows.Clear();
            if (topHeaders.Length == 0 && leftHeaders.Length == 0)
                return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grid.TopLeftHeaderCell.Value = "\\";
            grid.ShowEditingIcon = false;

            for (int i = 0; i < leftHeaders.Length; i++)
			{
                grid.Columns.Add("", leftHeaders[i].ToString());
                
                for (int j = 0; j < topHeaders.Length; j++)
                {
                    if(i == 0)
                        grid.Rows.Add(new DataGridViewRow() { 
                            HeaderCell = new DataGridViewRowHeaderCell() { 
                                Value = topHeaders[j]
                            } });
                    grid[i, j].Value = values[j, i].ToString();
                }
			}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (verticesComboBox.SelectedIndex == -1)
                return;
            //VertexModifyForm vm = new VertexModifyForm(this.GraphWrapper[verticesComboBox.SelectedIndex].Vertex);
            //vm.ShowDialog();
            //if (vm.Succesful)
            //{
            //    this.GraphWrapper[verticesComboBox.SelectedIndex].Vertex = vm.VertexWrapper.Clone() as MagicLibrary.MathUtils.Graphs.Vertex;
            //    RefreshValues();
            //    //vm.Vertex.CopyTo(out this.GraphWrapper[verticesComboBox.SelectedIndex].Vertex);
            //}
            this.GraphWrapper[verticesComboBox.SelectedIndex].EditVertex();
            this.RefreshValues();
        }

        private void GraphConstructorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult d = MessageBox.Show("Сохранить изменения?", "Внимание!", MessageBoxButtons.YesNoCancel);
            if (d == System.Windows.Forms.DialogResult.Cancel)
                e.Cancel = true;
            Succesful = d == System.Windows.Forms.DialogResult.Yes;
        }

        private void incidentsGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string tailName = incidentsGrid.Rows[e.RowIndex].HeaderCell.Value.ToString();
            string headName = incidentsGrid.Columns[e.ColumnIndex].HeaderText;

            if (incidentsGrid[e.ColumnIndex, e.RowIndex].Value == null)
            {
                (GraphWrapper.Graph as DirectedGraph).RemoveArc(tailName, headName);
                this.RefreshValues();
                return;
            }

            var s = (this.GraphWrapper.Graph as SemanticGraph);

            var r = s.RelationsDefinitions.Find(rd => rd.Name == incidentsGrid[e.ColumnIndex, e.RowIndex].Value.ToString());
            if (r == null)
            {
                var a = s[tailName, headName] as RelationArc;
                incidentsGrid[e.ColumnIndex, e.RowIndex].Value = a == null ? "" : a.Name;
                //this.RefreshValues();
                return;
            }

            if (GraphWrapper[tailName, headName] == null)
            {
                s.AddRelation((GraphWrapper.Graph[tailName] as ConceptVertex).Id, r.Id, (GraphWrapper.Graph[headName] as ConceptVertex).Id);
            }
            else
            {
                (s[tailName, headName] as RelationArc).Id = r.Id;
            }

            //this.RefreshValues();
        }
    }
}
