using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MagicLibrary.MathUtils.Graphs;
using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

using GraphEditor.App.Models;

using SemanticNetworkManager.App.Views;

namespace SemanticNetworkManager.App.Models.Wrappers
{
    [Serializable]
    public class RelationArcWrapper : WFArcWrapper
    {
        public RelationArcWrapper(SemanticGraphWrapper sWrapper, RelationArc rArc)
            : base(sWrapper, rArc)
        {
        }

        public override string Text
        {
            get
            {
                return (this.Edge as RelationArc).Name;
            }
        }

        public override void EditArc()
        {
            RelationArcModifyForm am = new RelationArcModifyForm(this);
            am.ShowDialog();
            if (am.Succesful)
            {
                am.ArcWrapper.CopyTo(this);// as MagicLibrary.MathUtils.Graphs.Vertex;
            }
        }
    }
}
