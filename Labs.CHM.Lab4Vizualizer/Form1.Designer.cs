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
            this.graph.Location = new System.Drawing.Point(12, 12);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(979, 541);
            this.graph.TabIndex = 0;
            this.graph.TabStop = false;
            this.graph.Click += new System.EventHandler(this.graph_Click);
            // 
            // calculate
            // 
            this.calculate.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.calculate.Location = new System.Drawing.Point(857, 559);
            this.calculate.Name = "calculate";
            this.calculate.Size = new System.Drawing.Size(134, 48);
            this.calculate.TabIndex = 3;
            this.calculate.Text = "Рассчитать";
            this.calculate.UseVisualStyleBackColor = true;
            this.calculate.Click += new System.EventHandler(this.calculate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(509, 568);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "Количество итераций";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(12, 568);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(19, 30);
            this.errorLabel.TabIndex = 6;
            this.errorLabel.Text = " ";
            // 
            // iterationsInputTextBox
            // 
            this.iterationsInputTextBox.Location = new System.Drawing.Point(738, 577);
            this.iterationsInputTextBox.Name = "iterationsInputTextBox";
            this.iterationsInputTextBox.Size = new System.Drawing.Size(100, 23);
            this.iterationsInputTextBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 619);
            this.Controls.Add(this.iterationsInputTextBox);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calculate);
            this.Controls.Add(this.graph);
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