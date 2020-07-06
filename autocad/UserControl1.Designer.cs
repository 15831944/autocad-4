namespace autocad
{
    partial class UserControl1
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.Lunghezza_input = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Spessore_input = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Lunghezza_input
            // 
            this.Lunghezza_input.AccessibleDescription = "L";
            this.Lunghezza_input.AccessibleName = "L";
            this.Lunghezza_input.Location = new System.Drawing.Point(111, 66);
            this.Lunghezza_input.Name = "Lunghezza_input";
            this.Lunghezza_input.Size = new System.Drawing.Size(30, 20);
            this.Lunghezza_input.TabIndex = 4;
            this.Lunghezza_input.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::autocad.Properties.Resources.ucAsola;
            this.pictureBox1.Location = new System.Drawing.Point(62, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(122, 114);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::autocad.Properties.Resources.PickPoint;
            this.button2.Location = new System.Drawing.Point(150, 182);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 36);
            this.button2.TabIndex = 6;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Spessore_input
            // 
            this.Spessore_input.AccessibleDescription = "s";
            this.Spessore_input.AccessibleName = "s";
            this.Spessore_input.Location = new System.Drawing.Point(53, 94);
            this.Spessore_input.Name = "Spessore_input";
            this.Spessore_input.Size = new System.Drawing.Size(30, 20);
            this.Spessore_input.TabIndex = 8;
            this.Spessore_input.Tag = "Spessore";
            this.Spessore_input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Spessore_input.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Spessore_input);
            this.Controls.Add(this.Lunghezza_input);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(350, 295);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox Lunghezza_input;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox Spessore_input;
    }
}
