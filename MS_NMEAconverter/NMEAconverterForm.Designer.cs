namespace MS_NMEAconverter
{
    partial class NMEAconverterForm
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
            this.openFileDialogNMEA = new System.Windows.Forms.OpenFileDialog();
            this.buttonOpenNMEA = new System.Windows.Forms.Button();
            this.textBoxNMEApath = new System.Windows.Forms.TextBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.labelCounter = new System.Windows.Forms.Label();
            this.buttonSaveGPX = new System.Windows.Forms.Button();
            this.saveFileDialogGPX = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // openFileDialogNMEA
            // 
            this.openFileDialogNMEA.FileName = "openFileDialogNMEA";
            // 
            // buttonOpenNMEA
            // 
            this.buttonOpenNMEA.Location = new System.Drawing.Point(428, 41);
            this.buttonOpenNMEA.Name = "buttonOpenNMEA";
            this.buttonOpenNMEA.Size = new System.Drawing.Size(189, 37);
            this.buttonOpenNMEA.TabIndex = 0;
            this.buttonOpenNMEA.Text = "Load NMEA file";
            this.buttonOpenNMEA.UseVisualStyleBackColor = true;
            this.buttonOpenNMEA.Click += new System.EventHandler(this.buttonOpenNMEA_Click);
            // 
            // textBoxNMEApath
            // 
            this.textBoxNMEApath.Location = new System.Drawing.Point(12, 12);
            this.textBoxNMEApath.Name = "textBoxNMEApath";
            this.textBoxNMEApath.Size = new System.Drawing.Size(800, 22);
            this.textBoxNMEApath.TabIndex = 3;
            this.textBoxNMEApath.Text = "D:\\Dropbox\\Visual Studio 2015\\Projects\\MS_NMEAconverter\\MockData\\mala.nmea";
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutput.Location = new System.Drawing.Point(12, 84);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOutput.Size = new System.Drawing.Size(800, 221);
            this.textBoxOutput.TabIndex = 4;
            // 
            // labelCounter
            // 
            this.labelCounter.AutoSize = true;
            this.labelCounter.Location = new System.Drawing.Point(12, 51);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(77, 17);
            this.labelCounter.TabIndex = 5;
            this.labelCounter.Text = "Records: 0";
            // 
            // buttonSaveGPX
            // 
            this.buttonSaveGPX.Location = new System.Drawing.Point(623, 41);
            this.buttonSaveGPX.Name = "buttonSaveGPX";
            this.buttonSaveGPX.Size = new System.Drawing.Size(189, 37);
            this.buttonSaveGPX.TabIndex = 6;
            this.buttonSaveGPX.Text = "Save GPX file";
            this.buttonSaveGPX.UseVisualStyleBackColor = true;
            this.buttonSaveGPX.Click += new System.EventHandler(this.buttonSaveGPX_Click);
            // 
            // NMEAconverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 317);
            this.Controls.Add(this.buttonSaveGPX);
            this.Controls.Add(this.labelCounter);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.textBoxNMEApath);
            this.Controls.Add(this.buttonOpenNMEA);
            this.Name = "NMEAconverterForm";
            this.Text = "MS_NMEA_converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogNMEA;
        private System.Windows.Forms.Button buttonOpenNMEA;
        private System.Windows.Forms.TextBox textBoxNMEApath;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label labelCounter;
        private System.Windows.Forms.Button buttonSaveGPX;
        private System.Windows.Forms.SaveFileDialog saveFileDialogGPX;
    }
}

