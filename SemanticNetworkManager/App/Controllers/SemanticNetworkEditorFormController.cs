using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

using GraphEditor.App.Controllers;

using SemanticNetworkManager.App.Views;

namespace SemanticNetworkManager.App.Controllers
{
    public class SemanticNetworkEditorFormController : GraphEditFormController
    {
        public SemanticNetworkEditorFormController(string name, SemanticNetworkEditorForm view)
            : base(name, view)
        {
        }

        public SemanticNetworkEditorForm sv { get { return this.MainView as SemanticNetworkEditorForm; } }

        public override GraphEditor.App.Views.GraphView NewGraphView()
        {
            return new SemanticGraphView(new PictureBox());
        }

        public void AddRelationDefinition()
        {
            var s = this.sv.selectedGraph.graphWrapper.Graph as SemanticGraph;

            var f = new AddRelationDefinitionForm();
            f.ShowDialog();
            if (!s.RelationsDefinitions.Exists(rd => rd.Name == f.RelationDefinition.Name || rd.Id == f.RelationDefinition.Id))
            {
                s.RelationsDefinitions.Add(f.RelationDefinition.Clone() as RelationDefinition);
            }
            sv.Refresh();
        }
    }
}
