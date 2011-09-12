using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MagicLibrary.MathUtils.SemanticNetworkUtils;
using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

using GraphEditor.App.Views;

using SemanticNetworkManager.App.Controllers;
using SemanticNetworkManager.App.Models.Wrappers;

namespace SemanticNetworkManager.App.Views
{
    public class SemanticGraphView : GraphView
    {
        public int currentRelationId = -1;
        public SemanticNetwork SemanticNerwork { get; set; }

        public new SemanticGraphController MainController { get { return this.GetController("MainController") as SemanticGraphController; } set { base.MainController = value; } }

        SemanticGraphWrapper SemanticGraphWrapper {get{return this.graphWrapper as SemanticGraphWrapper;}}
        public SemanticGraphView(Control control)
            : base(control)
        {
            this.MainController = new SemanticGraphController(this, "MainController");

            this.graphWrapper = new SemanticGraphWrapper(new SemanticGraph());

            this.SemanticNerwork = new SemanticNetwork(this.graphWrapper.Graph as SemanticGraph);
        }
    }
}
