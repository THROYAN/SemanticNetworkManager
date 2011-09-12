using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using MagicLibrary.MathUtils.Graphs;
using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

using GraphEditor.App.Models;

using SemanticNetworkManager.App.Views;

namespace SemanticNetworkManager.App.Models.Wrappers
{
    [Serializable]
    public class ConceptVertexWrapper : WFVertexWrapper
    {
        public ConceptVertexWrapper(SemanticGraphWrapper sWrapper, ConceptVertex cVertex)
            : base(sWrapper, cVertex)
        {
        }

        public string Text { get { return this.Name; } }

        public Font Font = new Font("Arial", 8);

        public RectangleF GetRectangleF(Graphics g)
        {
            this.RectangleF = new RectangleF(this.Coords, g.MeasureString(this.Text, this.Font));
            return this.RectangleF;
        }

        public Rectangle GetRectangle(Graphics g)
        {
            RectangleF r = this.GetRectangleF(g);
            return new Rectangle(
                    (int)r.X,
                    (int)r.Y,
                    (int)r.Width,
                    (int)r.Height
                );
        }

        public override void Draw(Graphics g, Pen p)
        {
            g.DrawRectangle(p, this.GetRectangle(g));

            g.DrawString(this.Text, this.Font, new SolidBrush(p.Color), this.Position);
        }

        public override void EditVertex()
        {
            ConceptVertexModifyForm vm = new ConceptVertexModifyForm(this);
            vm.ShowDialog();
            if (vm.Succesful)
            {
                vm.VertexWrapper.CopyTo(this);// as MagicLibrary.MathUtils.Graphs.Vertex;
            }
        }
    }
}
