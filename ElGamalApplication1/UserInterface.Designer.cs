namespace ElGamalApplication
{
    partial class UserInterface
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.key_generate_p = new System.Windows.Forms.NumericUpDown();
            this.key_generate_g = new System.Windows.Forms.NumericUpDown();
            this.key_generate_x = new System.Windows.Forms.NumericUpDown();
            this.genKeyBtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.key_generate_p)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.key_generate_g)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.key_generate_x)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(429, 253);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(421, 227);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(421, 227);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.genKeyBtn);
            this.tabPage3.Controls.Add(this.key_generate_x);
            this.tabPage3.Controls.Add(this.key_generate_g);
            this.tabPage3.Controls.Add(this.key_generate_p);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(421, 227);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Key Generator";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "p";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "g";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "x";
            // 
            // key_generate_p
            // 
            this.key_generate_p.Location = new System.Drawing.Point(29, 18);
            this.key_generate_p.Name = "key_generate_p";
            this.key_generate_p.Size = new System.Drawing.Size(80, 20);
            this.key_generate_p.TabIndex = 3;
            // 
            // key_generate_g
            // 
            this.key_generate_g.Location = new System.Drawing.Point(29, 50);
            this.key_generate_g.Name = "key_generate_g";
            this.key_generate_g.Size = new System.Drawing.Size(80, 20);
            this.key_generate_g.TabIndex = 4;
            // 
            // key_generate_x
            // 
            this.key_generate_x.Location = new System.Drawing.Point(29, 83);
            this.key_generate_x.Name = "key_generate_x";
            this.key_generate_x.Size = new System.Drawing.Size(80, 20);
            this.key_generate_x.TabIndex = 5;
            // 
            // genKeyBtn
            // 
            this.genKeyBtn.Location = new System.Drawing.Point(29, 123);
            this.genKeyBtn.Name = "genKeyBtn";
            this.genKeyBtn.Size = new System.Drawing.Size(98, 23);
            this.genKeyBtn.TabIndex = 6;
            this.genKeyBtn.Text = "Generate Key";
            this.genKeyBtn.UseVisualStyleBackColor = true;
            this.genKeyBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 277);
            this.Controls.Add(this.tabControl1);
            this.Name = "UserInterface";
            this.Text = "ElGamal Application";
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.key_generate_p)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.key_generate_g)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.key_generate_x)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button genKeyBtn;
        private System.Windows.Forms.NumericUpDown key_generate_x;
        private System.Windows.Forms.NumericUpDown key_generate_g;
        private System.Windows.Forms.NumericUpDown key_generate_p;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}