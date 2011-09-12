using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MagicLibrary.MathUtils.SemanticNetworkUtils;
using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

using GraphEditor.App.Views;

using SemanticNetworkManager.App.Controllers;
using SemanticNetworkManager.App.Models.Wrappers;

namespace SemanticNetworkManager.App.Views
{
    public partial class SemanticNetworkEditorForm : GraphEditForm
    {
        public new SemanticNetworkEditorFormController MainController
        {
            get { return this.GetController("MainController") as SemanticNetworkEditorFormController; }
            set { base.MainController = value; }
        }

        public SemanticNetworkEditorForm()
        {
            InitializeComponent();
            this.MainController = new SemanticNetworkEditorFormController("MainController", this);
        }

        private void SemanticNetworkEditorForm_Load(object sender, EventArgs e)
        {
            (this.файлToolStripMenuItem as ToolStripDropDownItem).DropDownItems.Insert(2,
                    new ToolStripMenuItem("Открыть кривой файл", null, OpenTextFile, Keys.Control | Keys.Shift | Keys.O));
            (this.menuStrip1.Items[1] as ToolStripDropDownItem).DropDownItems.Insert(3,
                    new ToolStripMenuItem("Добавить тип связи", null, AddRelationDefinition));
            //(this.menuStrip1.Items[2] as ToolStripDropDownItem).DropDownItems.Insert(

        }

        void AddRelationDefinition(object sender, EventArgs e)
        {
            MainController.AddRelationDefinition();
        }

        void OpenTextFile(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.MainController.CreateAction();
                SemanticNetwork sNetwork = (this.selectedGraph as SemanticGraphView).SemanticNerwork;

                StreamReader sr = new StreamReader(this.openFileDialog1.FileName);
                try
                {
                    Random r = new Random();
                    int i = 0;
                    string goalString = "";
                    string line;

                    while (!sr.EndOfStream && i <= 4)
                    {
                        line = sr.ReadLine();
                        if (line[0] == '#')
                        {
                            i++;
                        }
                        else
                        {
                            switch (i)
                            {
                                //load concepts
                                case 1:
                                    string[] concept = line.Split(':');
                                    (this.selectedGraph.graphWrapper as SemanticGraphWrapper).AddConcept(
                                        //sNetwork.Graph.AddConcept(
                                        Int32.Parse(concept[0], System.Globalization.NumberStyles.Any),
                                        concept[1],
                                        new PointF(r.Next(0, 500), r.Next(0, 500))
                                        );
                                    break;
                                //load relations definitions
                                case 2:
                                    string[] relationDefinition = line.Split(':');
                                    sNetwork.Graph.AddRelationType(
                                        Int32.Parse(relationDefinition[0], System.Globalization.NumberStyles.Any),
                                        relationDefinition[1],
                                        Int32.Parse(relationDefinition[2], System.Globalization.NumberStyles.Any));
                                    break;
                                //load relations
                                case 3:
                                    string[] relation = line.Split(':');
                                    sNetwork.Graph.AddRelation(
                                        Int32.Parse(relation[0], System.Globalization.NumberStyles.Any),
                                        Int32.Parse(relation[1], System.Globalization.NumberStyles.Any),
                                        Int32.Parse(relation[2], System.Globalization.NumberStyles.Any));
                                    break;
                                //goal
                                case 4:
                                    string[] goal = line.Split(':');
                                    goalString += "----------\n" + sNetwork.Question(
                                            goal[0] == SemanticNetwork.anyValueString ? SemanticNetwork.anyValue : Int32.Parse(goal[0], System.Globalization.NumberStyles.Any),
                                            goal[1] == SemanticNetwork.anyValueString ? SemanticNetwork.anyValue : Int32.Parse(goal[1], System.Globalization.NumberStyles.Any),
                                            goal[2] == SemanticNetwork.anyValueString ? SemanticNetwork.anyValue : Int32.Parse(goal[2], System.Globalization.NumberStyles.Any), true);
                                    this.selectedGraph.Debug(goalString);
                                    break;
                            }
                        }
                    }

                    this.Refresh();
                }
                catch
                {
                    this.MainController.CloseAction();
                    MessageBox.Show("Invalid format");
                }
                finally
                {
                    sr.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.selectedGraph.action == GraphEditActions.Edit)
            {
                if (firstConceptComboBox.SelectedIndex == -1 || relationComboBox.SelectedIndex == -1 || secondConceptComboBox.SelectedIndex == -1)
                {
                    return;
                }
                var f = this.selectedGraph.graphWrapper.Graph.GetVertex(v => v.Value == firstConceptComboBox.SelectedItem) as ConceptVertex;
                var s = this.selectedGraph.graphWrapper.Graph.GetVertex(v => v.Value == secondConceptComboBox.SelectedItem) as ConceptVertex;

                var r = (this.selectedGraph.graphWrapper.Graph as SemanticGraph).RelationsDefinitions.Find(rd => rd.Name == relationComboBox.SelectedItem.ToString());

                this.selectedGraph.Debug((this.selectedGraph as SemanticGraphView).SemanticNerwork.Question(
                    f == null ? SemanticNetwork.anyValue : f.Id,
                    r == null ? SemanticNetwork.anyValue : r.Id,
                    s == null ? SemanticNetwork.anyValue : s.Id));
            }
            else
            {
                var s = this.selectedGraph.graphWrapper.Graph as SemanticGraph;

                var r = s.RelationsDefinitions.Find(rd => rd.Name == relationComboBox.SelectedItem.ToString());

                if (r != null)
                {
                    var f = new AddRelationDefinitionForm() { RelationDefinition = r };
                    f.ShowDialog();
                    if (f.RelationDefinition.Id != r.Id && !s.RelationsDefinitions.Exists(rd => rd.Id == f.RelationDefinition.Id))
                        r.Id = f.RelationDefinition.Id;

                    if (f.RelationDefinition.Name != r.Name && !s.RelationsDefinitions.Exists(rd => rd.Name == f.RelationDefinition.Name))
                        r.Name = f.RelationDefinition.Name;

                    r.TypeId = f.RelationDefinition.TypeId;
                }
                else
                {
                    MainController.AddRelationDefinition();
                }

                this.Refresh();
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            if (this.selectedGraph == null)
                return;

            firstConceptComboBox.Items.Clear();
            relationComboBox.Items.Clear();
            secondConceptComboBox.Items.Clear();

            if (selectedGraph.action == GraphEditActions.Edit)
            {
                button1.Enabled = true;
                button1.Text = "Задать вопрос";

                firstConceptComboBox.Enabled = true;
                relationComboBox.Enabled = true;
                secondConceptComboBox.Enabled = true;
                
                firstConceptComboBox.Items.Add(SemanticNetwork.anyFirstConceptString);
                relationComboBox.Items.Add(SemanticNetwork.anyRelationString);
                secondConceptComboBox.Items.Add(SemanticNetwork.anySecondConceptString);

                firstConceptComboBox.Items.AddRange(this.selectedGraph.graphWrapper.Graph.GetVertices().Select(v => (v as ConceptVertex).Name).ToArray());
                relationComboBox.Items.AddRange((this.selectedGraph.graphWrapper.Graph as SemanticGraph).RelationsDefinitions.Select(rd => rd.Name).ToArray());
                secondConceptComboBox.Items.AddRange(this.selectedGraph.graphWrapper.Graph.GetVertices().Select(v => (v as ConceptVertex).Name).ToArray());

                firstConceptComboBox.SelectedIndex = 0;
                relationComboBox.SelectedIndex = 0;
                secondConceptComboBox.SelectedIndex = 0;
            }
            else
            {
                firstConceptComboBox.Enabled = false;
                secondConceptComboBox.Enabled = false;

                if (selectedGraph.action == GraphEditActions.AddArcSelectFirstVertex)
                {
                    button1.Text = "Изменить тип связи";

                    relationComboBox.Enabled = true;
                    relationComboBox.Items.AddRange((this.selectedGraph.graphWrapper.Graph as SemanticGraph).RelationsDefinitions.Select(rd => rd.Name).ToArray());

                    if (relationComboBox.Items.Count == 0)
                    {
                        button1.Enabled = false;
                        return;
                    }
                    relationComboBox.SelectedIndex = 0;

                    button1.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                    relationComboBox.Enabled = false;
                    button1.Text = "Задать вопрос";
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void relationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = this.selectedGraph.graphWrapper.Graph as SemanticGraph;
            var r = s.RelationsDefinitions.Find(rd => rd.Name == relationComboBox.SelectedItem.ToString());
            if (r != null)
                (this.selectedGraph as SemanticGraphView).currentRelationId = r.Id;
        }
    }
}
