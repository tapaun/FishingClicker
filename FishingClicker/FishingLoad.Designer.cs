﻿namespace FishingClicker
{
    partial class FishingLoad
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
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(144, 55);
            label1.Name = "label1";
            label1.Size = new Size(500, 294);
            label1.TabIndex = 0;
            // 
            // FishingLoad
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Name = "FishingLoad";
            Text = "FishingLoad";
            Load += FishingLoad_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
    }
}