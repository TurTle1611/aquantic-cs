namespace ZBase
{
    partial class Menu
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
            this.BunnyhopCheck = new System.Windows.Forms.CheckBox();
            this.ESPCheck = new System.Windows.Forms.CheckBox();
            this.AimbotCheck = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BunnyhopCheck
            // 
            this.BunnyhopCheck.AutoSize = true;
            this.BunnyhopCheck.Location = new System.Drawing.Point(12, 25);
            this.BunnyhopCheck.Name = "BunnyhopCheck";
            this.BunnyhopCheck.Size = new System.Drawing.Size(194, 17);
            this.BunnyhopCheck.TabIndex = 2;
            this.BunnyhopCheck.Text = "Bunny Hop (Space) - Sample Cheat";
            this.BunnyhopCheck.UseVisualStyleBackColor = true;
            this.BunnyhopCheck.CheckedChanged += new System.EventHandler(this.BunnyhopCheck_CheckedChanged);
            // 
            // ESPCheck
            // 
            this.ESPCheck.AutoSize = true;
            this.ESPCheck.Location = new System.Drawing.Point(12, 48);
            this.ESPCheck.Name = "ESPCheck";
            this.ESPCheck.Size = new System.Drawing.Size(86, 17);
            this.ESPCheck.TabIndex = 7;
            this.ESPCheck.Text = "Overlay ESP";
            this.ESPCheck.UseVisualStyleBackColor = true;
            this.ESPCheck.CheckedChanged += new System.EventHandler(this.ESPCheck_CheckedChanged);
            // 
            // AimbotCheck
            // 
            this.AimbotCheck.AutoSize = true;
            this.AimbotCheck.Location = new System.Drawing.Point(12, 71);
            this.AimbotCheck.Name = "AimbotCheck";
            this.AimbotCheck.Size = new System.Drawing.Size(58, 17);
            this.AimbotCheck.TabIndex = 8;
            this.AimbotCheck.Text = "Aimbot";
            this.AimbotCheck.UseVisualStyleBackColor = true;
            this.AimbotCheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(236, 25);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Skeleton";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 400);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.AimbotCheck);
            this.Controls.Add(this.ESPCheck);
            this.Controls.Add(this.BunnyhopCheck);
            this.Name = "Menu";
            this.Text = "aquantic";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox BunnyhopCheck;
        private System.Windows.Forms.CheckBox ESPCheck;
        private System.Windows.Forms.CheckBox AimbotCheck;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

