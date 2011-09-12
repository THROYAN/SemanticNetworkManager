using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using MagicLibrary.MathUtils.SemanticNetworkUtils;
using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

using GraphEditor.App.Controllers;
using GraphEditor.App.Models;

using SemanticNetworkManager.App.Views;
using SemanticNetworkManager.App.Models.Wrappers;

namespace SemanticNetworkManager.App.Controllers
{
    public class SemanticGraphController : GraphEditController
    {
        private SemanticGraphView sgView { get { return this.View as SemanticGraphView; } }

        public SemanticGraphController(SemanticGraphView view, string name)
            : base(view, name)
        {
        }

        public override bool OpenGraph(string path)
        {
            var f = base.OpenGraph(path);
            sgView.SemanticNerwork = new SemanticNetwork(sgView.graphWrapper.Graph as SemanticGraph);
            return f;
        }

        public override void AddArc(System.Drawing.Point coords)
        {
            (this.sgView.graphWrapper as SemanticGraphWrapper).AddRelationArc(
                    (this.sgView.graphWrapper[this.sgView.selectedVertexIndex].Vertex as ConceptVertex).Id,
                    this.sgView.currentRelationId,
                    (this.sgView.graphWrapper[this.sgView.selectionVertexIndex].Vertex as ConceptVertex).Id,
                    this.sgView.points.ToArray()
            );
        }

        public override void AddVertex(System.Drawing.PointF coords)
        {
            (this.sgView.graphWrapper as SemanticGraphWrapper).AddConcept(sgView.TransformationMatrix.Reverse() * coords);
        }
    }
}
