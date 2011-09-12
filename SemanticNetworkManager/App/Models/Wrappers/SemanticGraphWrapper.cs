using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using GraphEditor.App.Models;

using MagicLibrary.MathUtils.Graphs;
using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

using SemanticNetworkManager.App.Views;

namespace SemanticNetworkManager.App.Models.Wrappers
{
    [Serializable]
    public class SemanticGraphWrapper : WFGraphWrapper
    {
        public SemanticGraphWrapper(SemanticGraph sGraph)
            : base(sGraph)
        {
            SemanticGraphWrapper.SetDefaultEventHandlers(this);
        }

        public static void SetDefaultEventHandlers(SemanticGraphWrapper gWrapper)
        {
            var sGraph = gWrapper.Graph as SemanticGraph;

            sGraph.OnVertexAdded += new EventHandler<VerticesModifiedEventArgs>(gWrapper.sGraph_OnVertexAdded);
            sGraph.OnEdgeAdded += new EventHandler<EdgesModifiedEventArgs>(gWrapper.sGraph_OnEdgeAdded);
        }

        void sGraph_OnEdgeAdded(object sender, EdgesModifiedEventArgs e)
        {
            if (e.Status == ModificationStatus.Successful)
            {
                WFArcWrapper last = this.ArcWrappers.Last() as WFArcWrapper;
                this.ArcWrappers.Add(new RelationArcWrapper(this, e.Edge as RelationArc) { Points = last.Points });
                this.ArcWrappers.Remove(last);
            }
        }

        void sGraph_OnVertexAdded(object sender, VerticesModifiedEventArgs e)
        {
            if (e.Status == ModificationStatus.Successful)
            {
                var last = this.VertexWrappers.Last();
                this.VertexWrappers.Remove(last);
                this.VertexWrappers.Add(new ConceptVertexWrapper(this, e.Vertex as ConceptVertex) { Coords = this.currentCoords });
            }
        }

        public void AddConcept(int id, string name, PointF point)
        {
            this.currentCoords = point;
            (this.Graph as SemanticGraph).AddConcept(id, name);
            this.currentCoords = new PointF();
        }

        public void AddConcept(string name, PointF point)
        {
            this.currentCoords = point;
            (this.Graph as SemanticGraph).AddConcept(name);
            this.currentCoords = new PointF();
        }

        public void AddConcept(PointF point)
        {
            this.currentCoords = point;
            (this.Graph as SemanticGraph).AddConcept("C" + (this.Graph as SemanticGraph).GetFreeConceptId());
        }

        public void AddRelationArc(int conceptId1, int relationId, int conceptId2, PointF[] points)
        {
            this.currentPoints = points;
            (this.Graph as SemanticGraph).AddRelation(conceptId1, relationId, conceptId2);
            this.currentPoints = null;
        }

        public override void CopyTo(IGraphWrapper graphWrapper)
        {
            base.CopyTo(graphWrapper);
            SemanticGraphWrapper.SetDefaultEventHandlers(graphWrapper as SemanticGraphWrapper);
        }

        public override object Clone()
        {
            SemanticGraphWrapper s = new SemanticGraphWrapper(this.Graph.Clone() as SemanticGraph);

            this.CopyTo(s);

            SemanticGraphWrapper.SetDefaultEventHandlers(s);

            return s;
        }

        public override void EditGraph()
        {
            SemanticGraphConstructorForm gc = new SemanticGraphConstructorForm(this);
            (gc as Form).ShowDialog();

            if (gc.Succesful)
            {
                gc.GraphWrapper.CopyTo(this);
            }
        }
    }
}
