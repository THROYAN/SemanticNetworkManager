namespace SemanticNetworkManager.App.Views
{
    partial class AddRelationDefinitionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.idNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.inheritableChBox = new System.Windows.Forms.CheckBox();
            this.transitiveChBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.idNumUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Имя:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(50, 12);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(122, 20);
            this.nameTextBox.TabIndex = 2;
            this.nameTextBox.Text = "Имя";
            // 
            // idNumUpDown
            // 
            this.idNumUpDown.Location = new System.Drawing.Point(108, 38);
            this.idNumUpDown.Name = "idNumUpDown";
            this.idNumUpDown.Size = new System.Drawing.Size(64, 20);
            this.idNumUpDown.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Идентификатор:";
            // 
            // inheritableChBox
            // 
            this.inheritableChBox.AutoSize = true;
            this.inheritableChBox.Location = new System.Drawing.Point(6, 19);
            this.inheritableChBox.Name = "inheritableChBox";
            this.inheritableChBox.Size = new System.Drawing.Size(93, 17);
            this.inheritableChBox.TabIndex = 4;
            this.inheritableChBox.Text = "наследуемая";
            this.inheritableChBox.UseVisualStyleBackColor = true;
            this.inheritableChBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // transitiveChBox
            // 
            this.transitiveChBox.AutoSize = true;
            this.transitiveChBox.Location = new System.Drawing.Point(6, 42);
            this.transitiveChBox.Name = "transitiveChBox";
            this.transitiveChBox.Size = new System.Drawing.Size(96, 17);
            this.transitiveChBox.TabIndex = 4;
            this.transitiveChBox.Text = "транзитивная";
            this.transitiveChBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.inheritableChBox);
            this.groupBox1.Controls.Add(this.transitiveChBox);
            this.groupBox1.Location = new System.Drawing.Point(27, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 68);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип связи";
            // 
            // AddRelationDefinitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 174);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.idNumUpDown);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "AddRelationDefinitionForm";
            this.Text = "AddRelationDefinitionForm";
            this.Load += new System.EventHandler(this.AddRelationDefinitionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.idNumUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.NumericUpDown idNumUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox inheritableChBox;
        private System.Windows.Forms.CheckBox transitiveChBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}