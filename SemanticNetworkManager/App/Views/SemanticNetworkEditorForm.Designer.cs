namespace SemanticNetworkManager.App.Views
{
    partial class SemanticNetworkEditorForm
    {
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.firstConceptComboBox = new System.Windows.Forms.ComboBox();
            this.relationComboBox = new System.Windows.Forms.ComboBox();
            this.secondConceptComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.firstConceptComboBox);
            this.panel1.Controls.Add(this.relationComboBox);
            this.panel1.Controls.Add(this.secondConceptComboBox);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.SetChildIndex(this.button1, 0);
            this.panel1.Controls.SetChildIndex(this.secondConceptComboBox, 0);
            this.panel1.Controls.SetChildIndex(this.relationComboBox, 0);
            this.panel1.Controls.SetChildIndex(this.firstConceptComboBox, 0);
            // 
            // firstConceptComboBox
            // 
            this.firstConceptComboBox.FormattingEnabled = true;
            this.firstConceptComboBox.Location = new System.Drawing.Point(211, 9);
            this.firstConceptComboBox.Name = "firstConceptComboBox";
            this.firstConceptComboBox.Size = new System.Drawing.Size(136, 21);
            this.firstConceptComboBox.TabIndex = 2;
            // 
            // relationComboBox
            // 
            this.relationComboBox.FormattingEnabled = true;
            this.relationComboBox.Location = new System.Drawing.Point(365, 9);
            this.relationComboBox.Name = "relationComboBox";
            this.relationComboBox.Size = new System.Drawing.Size(136, 21);
            this.relationComboBox.TabIndex = 2;
            this.relationComboBox.SelectedIndexChanged += new System.EventHandler(this.relationComboBox_SelectedIndexChanged);
            // 
            // secondConceptComboBox
            // 
            this.secondConceptComboBox.FormattingEnabled = true;
            this.secondConceptComboBox.Location = new System.Drawing.Point(516, 9);
            this.secondConceptComboBox.Name = "secondConceptComboBox";
            this.secondConceptComboBox.Size = new System.Drawing.Size(136, 21);
            this.secondConceptComboBox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(658, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Задать вопрос";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SemanticNetworkEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 523);
            this.count = 1;
            this.Name = "SemanticNetworkEditorForm";
            this.Text = "SemanticNetworkEditor";
            this.Load += new System.EventHandler(this.SemanticNetworkEditorForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox firstConceptComboBox;
        private System.Windows.Forms.ComboBox relationComboBox;
        private System.Windows.Forms.ComboBox secondConceptComboBox;
        private System.Windows.Forms.Button button1;
    }
}