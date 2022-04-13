namespace Labs.CHM.Lab4Vizualizer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.graph = new System.Windows.Forms.PictureBox();
            this.calculate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.iterationsInputTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.graph.Location = new System.Drawing.Point(14, 16);
            this.graph.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(1119, 721);
            this.graph.TabIndex = 0;
            this.graph.TabStop = false;
            this.graph.Click += new System.EventHandler(this.graph_Click);
            // 
            // calculate
            // 
            this.calculate.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.calculate.Location = new System.Drawing.Point(979, 745);
            this.calculate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.calculate.Name = "calculate";
            this.calculate.Size = new System.Drawing.Size(153, 64);
            this.calculate.TabIndex = 3;
            this.calculate.Text = "Рассчитать";
            this.calculate.UseVisualStyleBackColor = true;
            this.calculate.Click += new System.EventHandler(this.calculate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(536, 759);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "Количество итераций";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(14, 757);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(24, 37);
            this.errorLabel.TabIndex = 6;
            this.errorLabel.Text = " ";
            // 
            // iterationsInputTextBox
            // 
            this.iterationsInputTextBox.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.iterationsInputTextBox.Location = new System.Drawing.Point(843, 769);
            this.iterationsInputTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.iterationsInputTextBox.Name = "iterationsInputTextBox";
            this.iterationsInputTextBox.Size = new System.Drawing.Size(114, 43);
            this.iterationsInputTextBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 825);
            this.Controls.Add(this.iterationsInputTextBox);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calculate);
            this.Controls.Add(this.graph);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox graph;
        private Button calculate;
        private Label label1;
        private Label errorLabel;
        private TextBox iterationsInputTextBox;
    }
}