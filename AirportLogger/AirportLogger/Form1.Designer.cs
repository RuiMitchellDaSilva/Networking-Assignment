namespace AirportLogger
{
    partial class Form1
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
            this.mainSummaryTextBox = new System.Windows.Forms.TextBox();
            this.planesLeftIntegerBox = new System.Windows.Forms.TextBox();
            this.bluePlaneObjBox = new System.Windows.Forms.TextBox();
            this.bluePlaneStatusBox = new System.Windows.Forms.TextBox();
            this.redPlaneObjBox = new System.Windows.Forms.TextBox();
            this.redPlaneStatusBox = new System.Windows.Forms.TextBox();
            this.yellowPlaneObjBox = new System.Windows.Forms.TextBox();
            this.yellowPlaneStatusBox = new System.Windows.Forms.TextBox();
            this.greenPlaneObjBox = new System.Windows.Forms.TextBox();
            this.greenPlaneStatusBox = new System.Windows.Forms.TextBox();
            


            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.blueNameLabel = new System.Windows.Forms.Label();
            this.blueObjectiveLabel = new System.Windows.Forms.Label();
            this.blueStatusLabel = new System.Windows.Forms.Label();
            this.redNameLabel = new System.Windows.Forms.Label();
            this.redObjectiveLabel = new System.Windows.Forms.Label();
            this.redStatusLabel = new System.Windows.Forms.Label();
            this.yellowNameLabel = new System.Windows.Forms.Label();
            this.yellowObjectiveLabel = new System.Windows.Forms.Label();
            this.yellowStatusLabel = new System.Windows.Forms.Label();
            this.greenNameLabel = new System.Windows.Forms.Label();
            this.greenObjectiveLabel = new System.Windows.Forms.Label();
            this.greenStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Airport Main Summary
            // 
            this.mainSummaryTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.mainSummaryTextBox.Enabled = false;
            this.mainSummaryTextBox.Location = new System.Drawing.Point(43, 40);
            this.mainSummaryTextBox.Multiline = true;
            this.mainSummaryTextBox.Name = "mainSummaryTextBox";
            this.mainSummaryTextBox.ReadOnly = true;
            this.mainSummaryTextBox.Size = new System.Drawing.Size(418, 124);
            this.mainSummaryTextBox.TabIndex = 0;


            // number box
            this.planesLeftIntegerBox.BackColor = System.Drawing.SystemColors.Info;
            this.planesLeftIntegerBox.Enabled = false;
            this.planesLeftIntegerBox.Location = new System.Drawing.Point(43, 180);
            this.planesLeftIntegerBox.Multiline = true;
            this.planesLeftIntegerBox.Name = "Number of plane takeoffs";
            this.planesLeftIntegerBox.ReadOnly = true;
            this.planesLeftIntegerBox.Size = new System.Drawing.Size(30, 20);
            this.planesLeftIntegerBox.TabIndex = 0;


            // Blue Plane
            // Objective
            this.bluePlaneObjBox.BackColor = System.Drawing.SystemColors.Info;
            this.bluePlaneObjBox.Enabled = false;
            this.bluePlaneObjBox.Location = new System.Drawing.Point(43, 275);
            this.bluePlaneObjBox.Multiline = true;
            this.bluePlaneObjBox.Name = "Blue Plane Objective";
            this.bluePlaneObjBox.ReadOnly = true;
            this.bluePlaneObjBox.Size = new System.Drawing.Size(200, 20);
            this.bluePlaneObjBox.TabIndex = 0;
            // Status
            this.bluePlaneStatusBox.BackColor = System.Drawing.SystemColors.Info;
            this.bluePlaneStatusBox.Enabled = false;
            this.bluePlaneStatusBox.Location = new System.Drawing.Point(bluePlaneObjBox.Location.X + 210, bluePlaneObjBox.Location.Y);
            this.bluePlaneStatusBox.Multiline = true;
            this.bluePlaneStatusBox.Name = "Blue Plane Status";
            this.bluePlaneStatusBox.ReadOnly = true;
            this.bluePlaneStatusBox.Size = new System.Drawing.Size(200, 20);
            this.bluePlaneStatusBox.TabIndex = 0;
            // Name Label
            this.blueNameLabel.AutoSize = true;
            this.blueNameLabel.Location = new System.Drawing.Point(bluePlaneObjBox.Location.X, bluePlaneObjBox.Location.Y - 40);
            this.blueNameLabel.Name = "Blue Plane";
            this.blueNameLabel.Size = new System.Drawing.Size(78, 13);
            this.blueNameLabel.TabIndex = 2;
            this.blueNameLabel.Text = "Blue Plane";
            this.blueNameLabel.Click += new System.EventHandler(this.blueLabel_Click);
            // Objective Label
            this.blueObjectiveLabel.AutoSize = true;
            this.blueObjectiveLabel.Location = new System.Drawing.Point(bluePlaneObjBox.Location.X, bluePlaneObjBox.Location.Y - 20);
            this.blueObjectiveLabel.Name = "Blue Plane Objective";
            this.blueObjectiveLabel.Size = new System.Drawing.Size(78, 13);
            this.blueObjectiveLabel.TabIndex = 2;
            this.blueObjectiveLabel.Text = "Objective";
            this.blueObjectiveLabel.Click += new System.EventHandler(this.blueLabel_Click);
            // Status Label
            this.blueStatusLabel.AutoSize = true;
            this.blueStatusLabel.Location = new System.Drawing.Point(bluePlaneStatusBox.Location.X, bluePlaneStatusBox.Location.Y - 20);
            this.blueStatusLabel.Name = "Blue Plane Status";
            this.blueStatusLabel.Size = new System.Drawing.Size(78, 13);
            this.blueStatusLabel.TabIndex = 2;
            this.blueStatusLabel.Text = "Status";
            this.blueStatusLabel.Click += new System.EventHandler(this.blueLabel_Click);



            // Red Plane
            // Objective
            this.redPlaneObjBox.BackColor = System.Drawing.SystemColors.Info;
            this.redPlaneObjBox.Enabled = false;
            this.redPlaneObjBox.Location = new System.Drawing.Point(43, 375);
            this.redPlaneObjBox.Multiline = true;
            this.redPlaneObjBox.Name = "Red Plane Objective";
            this.redPlaneObjBox.ReadOnly = true;
            this.redPlaneObjBox.Size = new System.Drawing.Size(200, 20);
            this.redPlaneObjBox.TabIndex = 0;
            // Status
            this.redPlaneStatusBox.BackColor = System.Drawing.SystemColors.Info;
            this.redPlaneStatusBox.Enabled = false;
            this.redPlaneStatusBox.Location = new System.Drawing.Point(redPlaneObjBox.Location.X + 210, redPlaneObjBox.Location.Y);
            this.redPlaneStatusBox.Multiline = true;
            this.redPlaneStatusBox.Name = "Red Plane Status";
            this.redPlaneStatusBox.ReadOnly = true;
            this.redPlaneStatusBox.Size = new System.Drawing.Size(200, 20);
            this.redPlaneStatusBox.TabIndex = 0;
            // Name Label
            this.redNameLabel.AutoSize = true;
            this.redNameLabel.Location = new System.Drawing.Point(redPlaneObjBox.Location.X, redPlaneObjBox.Location.Y - 40);
            this.redNameLabel.Name = "Red Plane";
            this.redNameLabel.Size = new System.Drawing.Size(78, 13);
            this.redNameLabel.TabIndex = 2;
            this.redNameLabel.Text = "Red Plane";
            this.redNameLabel.Click += new System.EventHandler(this.redLabel_Click);
            // Objective Label
            this.redObjectiveLabel.AutoSize = true;
            this.redObjectiveLabel.Location = new System.Drawing.Point(redPlaneObjBox.Location.X, redPlaneObjBox.Location.Y - 20);
            this.redObjectiveLabel.Name = "Red Plane Objective";
            this.redObjectiveLabel.Size = new System.Drawing.Size(78, 13);
            this.redObjectiveLabel.TabIndex = 2;
            this.redObjectiveLabel.Text = "Objective";
            this.redObjectiveLabel.Click += new System.EventHandler(this.redLabel_Click);
            // Status Label
            this.redStatusLabel.AutoSize = true;
            this.redStatusLabel.Location = new System.Drawing.Point(redPlaneStatusBox.Location.X, redPlaneStatusBox.Location.Y - 20);
            this.redStatusLabel.Name = "Red Plane Status";
            this.redStatusLabel.Size = new System.Drawing.Size(78, 13);
            this.redStatusLabel.TabIndex = 2;
            this.redStatusLabel.Text = "Status";
            this.redStatusLabel.Click += new System.EventHandler(this.redLabel_Click);



            // Yellow Plane
            // Objective
            this.yellowPlaneObjBox.BackColor = System.Drawing.SystemColors.Info;
            this.yellowPlaneObjBox.Enabled = false;
            this.yellowPlaneObjBox.Location = new System.Drawing.Point(43, 475);
            this.yellowPlaneObjBox.Multiline = true;
            this.yellowPlaneObjBox.Name = "Yellow Plane Objective";
            this.yellowPlaneObjBox.ReadOnly = true;
            this.yellowPlaneObjBox.Size = new System.Drawing.Size(200, 20);
            this.yellowPlaneObjBox.TabIndex = 0;
            // Status
            this.yellowPlaneStatusBox.BackColor = System.Drawing.SystemColors.Info;
            this.yellowPlaneStatusBox.Enabled = false;
            this.yellowPlaneStatusBox.Location = new System.Drawing.Point(yellowPlaneObjBox.Location.X + 210, yellowPlaneObjBox.Location.Y);
            this.yellowPlaneStatusBox.Multiline = true;
            this.yellowPlaneStatusBox.Name = "Yellow Plane Status";
            this.yellowPlaneStatusBox.ReadOnly = true;
            this.yellowPlaneStatusBox.Size = new System.Drawing.Size(200, 20);
            this.yellowPlaneStatusBox.TabIndex = 0;
            // Name Label
            this.yellowNameLabel.AutoSize = true;
            this.yellowNameLabel.Location = new System.Drawing.Point(yellowPlaneObjBox.Location.X, yellowPlaneObjBox.Location.Y - 40);
            this.yellowNameLabel.Name = "Yellow Plane";
            this.yellowNameLabel.Size = new System.Drawing.Size(78, 13);
            this.yellowNameLabel.TabIndex = 2;
            this.yellowNameLabel.Text = "Yellow Plane";
            this.yellowNameLabel.Click += new System.EventHandler(this.yellowLabel_Click);
            // Objective Label
            this.yellowObjectiveLabel.AutoSize = true;
            this.yellowObjectiveLabel.Location = new System.Drawing.Point(yellowPlaneObjBox.Location.X, yellowPlaneObjBox.Location.Y - 20);
            this.yellowObjectiveLabel.Name = "Yellow Plane OPbjective";
            this.yellowObjectiveLabel.Size = new System.Drawing.Size(78, 13);
            this.yellowObjectiveLabel.TabIndex = 2;
            this.yellowObjectiveLabel.Text = "Objective";
            this.yellowObjectiveLabel.Click += new System.EventHandler(this.yellowLabel_Click);
            // Status Label
            this.yellowStatusLabel.AutoSize = true;
            this.yellowStatusLabel.Location = new System.Drawing.Point(yellowPlaneStatusBox.Location.X, yellowPlaneStatusBox.Location.Y - 20);
            this.yellowStatusLabel.Name = "Yellow Plane Status";
            this.yellowStatusLabel.Size = new System.Drawing.Size(78, 13);
            this.yellowStatusLabel.TabIndex = 2;
            this.yellowStatusLabel.Text = "Status";
            this.yellowStatusLabel.Click += new System.EventHandler(this.yellowLabel_Click);

            // Green Plane
            // Objective
            this.greenPlaneObjBox.BackColor = System.Drawing.SystemColors.Info;
            this.greenPlaneObjBox.Enabled = false;
            this.greenPlaneObjBox.Location = new System.Drawing.Point(43, 575);
            this.greenPlaneObjBox.Multiline = true;
            this.greenPlaneObjBox.Name = "Green Plane Objective";
            this.greenPlaneObjBox.ReadOnly = true;
            this.greenPlaneObjBox.Size = new System.Drawing.Size(200, 20);
            this.greenPlaneObjBox.TabIndex = 0;
            // Status
            this.greenPlaneStatusBox.BackColor = System.Drawing.SystemColors.Info;
            this.greenPlaneStatusBox.Enabled = false;
            this.greenPlaneStatusBox.Location = new System.Drawing.Point(greenPlaneObjBox.Location.X + 210, greenPlaneObjBox.Location.Y);
            this.greenPlaneStatusBox.Multiline = true;
            this.greenPlaneStatusBox.Name = "Green Plane Status";
            this.greenPlaneStatusBox.ReadOnly = true;
            this.greenPlaneStatusBox.Size = new System.Drawing.Size(200, 20);
            this.greenPlaneStatusBox.TabIndex = 0;
            // Name Label
            this.greenNameLabel.AutoSize = true;
            this.greenNameLabel.Location = new System.Drawing.Point(greenPlaneObjBox.Location.X, greenPlaneObjBox.Location.Y - 40);
            this.greenNameLabel.Name = "Green Plane";
            this.greenNameLabel.Size = new System.Drawing.Size(78, 13);
            this.greenNameLabel.TabIndex = 2;
            this.greenNameLabel.Text = "Green Plane";
            this.greenNameLabel.Click += new System.EventHandler(this.greenLabel_Click);
            // Objective Label
            this.greenObjectiveLabel.AutoSize = true;
            this.greenObjectiveLabel.Location = new System.Drawing.Point(greenPlaneObjBox.Location.X, greenPlaneObjBox.Location.Y - 20);
            this.greenObjectiveLabel.Name = "Green Plane OPbjective";
            this.greenObjectiveLabel.Size = new System.Drawing.Size(78, 13);
            this.greenObjectiveLabel.TabIndex = 2;
            this.greenObjectiveLabel.Text = "Objective";
            this.greenObjectiveLabel.Click += new System.EventHandler(this.greenLabel_Click);
            // Status Label
            this.greenStatusLabel.AutoSize = true;
            this.greenStatusLabel.Location = new System.Drawing.Point(greenPlaneStatusBox.Location.X, greenPlaneStatusBox.Location.Y - 20);
            this.greenStatusLabel.Name = "Green Plane Status";
            this.greenStatusLabel.Size = new System.Drawing.Size(78, 13);
            this.greenStatusLabel.TabIndex = 2;
            this.greenStatusLabel.Text = "Status";
            this.greenStatusLabel.Click += new System.EventHandler(this.greenLabel_Click);



            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Summary";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of plane takeoffs";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 650);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainSummaryTextBox);
            this.Controls.Add(this.planesLeftIntegerBox);
            this.Controls.Add(this.bluePlaneObjBox);
            this.Controls.Add(this.bluePlaneStatusBox);
            this.Controls.Add(this.blueNameLabel);
            this.Controls.Add(this.blueObjectiveLabel);
            this.Controls.Add(this.blueStatusLabel);
            this.Controls.Add(this.redPlaneObjBox);
            this.Controls.Add(this.redPlaneStatusBox);
            this.Controls.Add(this.redNameLabel);
            this.Controls.Add(this.redObjectiveLabel);
            this.Controls.Add(this.redStatusLabel);
            this.Controls.Add(this.yellowPlaneObjBox);
            this.Controls.Add(this.yellowPlaneStatusBox);
            this.Controls.Add(this.yellowNameLabel);
            this.Controls.Add(this.yellowObjectiveLabel);
            this.Controls.Add(this.yellowStatusLabel);
            this.Controls.Add(this.greenPlaneObjBox);
            this.Controls.Add(this.greenPlaneStatusBox);
            this.Controls.Add(this.greenNameLabel);
            this.Controls.Add(this.greenObjectiveLabel);
            this.Controls.Add(this.greenStatusLabel);
            this.Name = "Form1";
            this.Text = "AirportLogger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AirportLoggerForm_FormClosing);
            this.Load += new System.EventHandler(this.AirportLoggerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mainSummaryTextBox;
        private System.Windows.Forms.TextBox planesLeftIntegerBox;
        private System.Windows.Forms.TextBox bluePlaneObjBox;
        private System.Windows.Forms.TextBox bluePlaneStatusBox;
        private System.Windows.Forms.TextBox redPlaneObjBox;
        private System.Windows.Forms.TextBox redPlaneStatusBox;
        private System.Windows.Forms.TextBox yellowPlaneObjBox;
        private System.Windows.Forms.TextBox yellowPlaneStatusBox;
        private System.Windows.Forms.TextBox greenPlaneObjBox;
        private System.Windows.Forms.TextBox greenPlaneStatusBox; 
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label blueNameLabel;
        private System.Windows.Forms.Label blueObjectiveLabel;
        private System.Windows.Forms.Label blueStatusLabel;
        private System.Windows.Forms.Label redNameLabel;
        private System.Windows.Forms.Label redObjectiveLabel;
        private System.Windows.Forms.Label redStatusLabel;
        private System.Windows.Forms.Label yellowNameLabel;
        private System.Windows.Forms.Label yellowObjectiveLabel;
        private System.Windows.Forms.Label yellowStatusLabel;
        private System.Windows.Forms.Label greenNameLabel;
        private System.Windows.Forms.Label greenObjectiveLabel;
        private System.Windows.Forms.Label greenStatusLabel;
    }
}
