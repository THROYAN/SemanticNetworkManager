using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MagicLibrary.MathUtils.SemanticNetworkUtils.Graphs;

namespace SemanticNetworkManager.App.Views
{
    public partial class AddRelationDefinitionForm : Form
    {
        public RelationDefinition RelationDefinition { get; set; }

        public AddRelationDefinitionForm()
        {
            InitializeComponent();
            this.RelationDefinition = new RelationDefinition(1, "связь");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            transitiveChBox.Checked = inheritableChBox.Checked ? inheritableChBox.Checked : transitiveChBox.Checked;
            transitiveChBox.Enabled = !inheritableChBox.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.RelationDefinition.Id = Convert.ToInt32(this.idNumUpDown.Value);
            this.RelationDefinition.Name = this.nameTextBox.Text;
            this.RelationDefinition.TypeId = this.inheritableChBox.Checked ?
                RelationDefinition.inheritableId : this.transitiveChBox.Checked ? RelationDefinition.transitiveId : RelationDefinition.newRelationId;
            Close();
        }

        private void AddRelationDefinitionForm_Load(object sender, EventArgs e)
        {
            this.nameTextBox.Text = this.RelationDefinition.Name;
            this.idNumUpDown.Value = Convert.ToDecimal(this.RelationDefinition.Id);

            this.inheritableChBox.Checked = this.RelationDefinition.IsInheritable();
            this.transitiveChBox.Checked = this.RelationDefinition.IsTransitive();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
