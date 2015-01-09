namespace NoiseTest
{
    partial class ComparisonForm
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
            this.pictureBoxSimplex = new System.Windows.Forms.PictureBox();
            this.pictureBoxSimpletic = new System.Windows.Forms.PictureBox();
            this.pictureBoxOpenSimplex = new System.Windows.Forms.PictureBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.labelOctaves = new System.Windows.Forms.Label();
            this.textBoxScale = new System.Windows.Forms.TextBox();
            this.labelScale = new System.Windows.Forms.Label();
            this.textBoxOctaves = new System.Windows.Forms.TextBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.textBoxSize = new System.Windows.Forms.TextBox();
            this.labelPersistence = new System.Windows.Forms.Label();
            this.textBoxPersistence = new System.Windows.Forms.TextBox();
            this.labelLacunarity = new System.Windows.Forms.Label();
            this.textBoxLacunarity = new System.Windows.Forms.TextBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.labelPower = new System.Windows.Forms.Label();
            this.textBoxPower = new System.Windows.Forms.TextBox();
            this.checkBoxAbsoluteValue = new System.Windows.Forms.CheckBox();
            this.textBoxSimplex = new System.Windows.Forms.TextBox();
            this.textBoxSimpletic = new System.Windows.Forms.TextBox();
            this.textBoxOpenSimplex = new System.Windows.Forms.TextBox();
            this.checkBoxNormalize = new System.Windows.Forms.CheckBox();
            this.labelSimplexTitle = new System.Windows.Forms.Label();
            this.labelSimpleticTitle = new System.Windows.Forms.Label();
            this.labelOpenSimplexTitle = new System.Windows.Forms.Label();
            this.buttonSaveSimplex = new System.Windows.Forms.Button();
            this.buttonSaveSimpletic = new System.Windows.Forms.Button();
            this.buttonSaveOpenSimplex = new System.Windows.Forms.Button();
            this.buttonSaveComparison = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSimplex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSimpletic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOpenSimplex)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxSimplex
            // 
            this.pictureBoxSimplex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSimplex.Location = new System.Drawing.Point(12, 101);
            this.pictureBoxSimplex.Name = "pictureBoxSimplex";
            this.pictureBoxSimplex.Size = new System.Drawing.Size(350, 350);
            this.pictureBoxSimplex.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSimplex.TabIndex = 0;
            this.pictureBoxSimplex.TabStop = false;
            // 
            // pictureBoxSimpletic
            // 
            this.pictureBoxSimpletic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSimpletic.Location = new System.Drawing.Point(368, 101);
            this.pictureBoxSimpletic.Name = "pictureBoxSimpletic";
            this.pictureBoxSimpletic.Size = new System.Drawing.Size(350, 350);
            this.pictureBoxSimpletic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSimpletic.TabIndex = 1;
            this.pictureBoxSimpletic.TabStop = false;
            // 
            // pictureBoxOpenSimplex
            // 
            this.pictureBoxOpenSimplex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxOpenSimplex.Location = new System.Drawing.Point(724, 101);
            this.pictureBoxOpenSimplex.Name = "pictureBoxOpenSimplex";
            this.pictureBoxOpenSimplex.Size = new System.Drawing.Size(350, 350);
            this.pictureBoxOpenSimplex.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxOpenSimplex.TabIndex = 2;
            this.pictureBoxOpenSimplex.TabStop = false;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(985, 27);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(89, 36);
            this.buttonGenerate.TabIndex = 3;
            this.buttonGenerate.Text = "Generate Images";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // labelOctaves
            // 
            this.labelOctaves.AutoSize = true;
            this.labelOctaves.Location = new System.Drawing.Point(12, 27);
            this.labelOctaves.Name = "labelOctaves";
            this.labelOctaves.Size = new System.Drawing.Size(50, 13);
            this.labelOctaves.TabIndex = 5;
            this.labelOctaves.Text = "Octaves:";
            // 
            // textBoxScale
            // 
            this.textBoxScale.Location = new System.Drawing.Point(99, 42);
            this.textBoxScale.Name = "textBoxScale";
            this.textBoxScale.Size = new System.Drawing.Size(65, 20);
            this.textBoxScale.TabIndex = 6;
            this.textBoxScale.Text = "0.015";
            this.textBoxScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelScale
            // 
            this.labelScale.AutoSize = true;
            this.labelScale.Location = new System.Drawing.Point(96, 27);
            this.labelScale.Name = "labelScale";
            this.labelScale.Size = new System.Drawing.Size(37, 13);
            this.labelScale.TabIndex = 7;
            this.labelScale.Text = "Scale:";
            // 
            // textBoxOctaves
            // 
            this.textBoxOctaves.Location = new System.Drawing.Point(15, 42);
            this.textBoxOctaves.Name = "textBoxOctaves";
            this.textBoxOctaves.Size = new System.Drawing.Size(61, 20);
            this.textBoxOctaves.TabIndex = 8;
            this.textBoxOctaves.Text = "1";
            this.textBoxOctaves.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(360, 27);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(62, 13);
            this.labelSize.TabIndex = 10;
            this.labelSize.Text = "Image Size:";
            // 
            // textBoxSize
            // 
            this.textBoxSize.Location = new System.Drawing.Point(363, 42);
            this.textBoxSize.Name = "textBoxSize";
            this.textBoxSize.Size = new System.Drawing.Size(65, 20);
            this.textBoxSize.TabIndex = 9;
            this.textBoxSize.Text = "350";
            this.textBoxSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPersistence
            // 
            this.labelPersistence.AutoSize = true;
            this.labelPersistence.Location = new System.Drawing.Point(184, 27);
            this.labelPersistence.Name = "labelPersistence";
            this.labelPersistence.Size = new System.Drawing.Size(65, 13);
            this.labelPersistence.TabIndex = 12;
            this.labelPersistence.Text = "Persistence:";
            // 
            // textBoxPersistence
            // 
            this.textBoxPersistence.Location = new System.Drawing.Point(187, 42);
            this.textBoxPersistence.Name = "textBoxPersistence";
            this.textBoxPersistence.Size = new System.Drawing.Size(65, 20);
            this.textBoxPersistence.TabIndex = 11;
            this.textBoxPersistence.Text = "0.5";
            this.textBoxPersistence.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelLacunarity
            // 
            this.labelLacunarity.AutoSize = true;
            this.labelLacunarity.Location = new System.Drawing.Point(272, 27);
            this.labelLacunarity.Name = "labelLacunarity";
            this.labelLacunarity.Size = new System.Drawing.Size(59, 13);
            this.labelLacunarity.TabIndex = 14;
            this.labelLacunarity.Text = "Lacunarity:";
            // 
            // textBoxLacunarity
            // 
            this.textBoxLacunarity.Location = new System.Drawing.Point(275, 42);
            this.textBoxLacunarity.Name = "textBoxLacunarity";
            this.textBoxLacunarity.Size = new System.Drawing.Size(65, 20);
            this.textBoxLacunarity.TabIndex = 13;
            this.textBoxLacunarity.Text = "2.0";
            this.textBoxLacunarity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Items.AddRange(new object[] {
            "2D Sample",
            "3D Slice",
            "4D Slice",
            "Tileable (using 4D)"});
            this.comboBoxMode.Location = new System.Drawing.Point(724, 40);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(132, 21);
            this.comboBoxMode.TabIndex = 15;
            // 
            // labelPower
            // 
            this.labelPower.AutoSize = true;
            this.labelPower.Location = new System.Drawing.Point(448, 27);
            this.labelPower.Name = "labelPower";
            this.labelPower.Size = new System.Drawing.Size(40, 13);
            this.labelPower.TabIndex = 17;
            this.labelPower.Text = "Power:";
            // 
            // textBoxPower
            // 
            this.textBoxPower.Location = new System.Drawing.Point(451, 42);
            this.textBoxPower.Name = "textBoxPower";
            this.textBoxPower.Size = new System.Drawing.Size(65, 20);
            this.textBoxPower.TabIndex = 16;
            this.textBoxPower.Text = "1.0";
            this.textBoxPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxAbsoluteValue
            // 
            this.checkBoxAbsoluteValue.AutoSize = true;
            this.checkBoxAbsoluteValue.Location = new System.Drawing.Point(533, 44);
            this.checkBoxAbsoluteValue.Name = "checkBoxAbsoluteValue";
            this.checkBoxAbsoluteValue.Size = new System.Drawing.Size(97, 17);
            this.checkBoxAbsoluteValue.TabIndex = 18;
            this.checkBoxAbsoluteValue.Text = "Absolute Value";
            this.checkBoxAbsoluteValue.UseVisualStyleBackColor = true;
            // 
            // textBoxSimplex
            // 
            this.textBoxSimplex.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSimplex.Location = new System.Drawing.Point(12, 457);
            this.textBoxSimplex.Multiline = true;
            this.textBoxSimplex.Name = "textBoxSimplex";
            this.textBoxSimplex.ReadOnly = true;
            this.textBoxSimplex.Size = new System.Drawing.Size(350, 36);
            this.textBoxSimplex.TabIndex = 19;
            // 
            // textBoxSimpletic
            // 
            this.textBoxSimpletic.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSimpletic.Location = new System.Drawing.Point(368, 457);
            this.textBoxSimpletic.Multiline = true;
            this.textBoxSimpletic.Name = "textBoxSimpletic";
            this.textBoxSimpletic.ReadOnly = true;
            this.textBoxSimpletic.Size = new System.Drawing.Size(350, 36);
            this.textBoxSimpletic.TabIndex = 20;
            // 
            // textBoxOpenSimplex
            // 
            this.textBoxOpenSimplex.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOpenSimplex.Location = new System.Drawing.Point(724, 457);
            this.textBoxOpenSimplex.Multiline = true;
            this.textBoxOpenSimplex.Name = "textBoxOpenSimplex";
            this.textBoxOpenSimplex.ReadOnly = true;
            this.textBoxOpenSimplex.Size = new System.Drawing.Size(350, 36);
            this.textBoxOpenSimplex.TabIndex = 21;
            // 
            // checkBoxNormalize
            // 
            this.checkBoxNormalize.AutoSize = true;
            this.checkBoxNormalize.Location = new System.Drawing.Point(636, 44);
            this.checkBoxNormalize.Name = "checkBoxNormalize";
            this.checkBoxNormalize.Size = new System.Drawing.Size(72, 17);
            this.checkBoxNormalize.TabIndex = 22;
            this.checkBoxNormalize.Text = "Normalize";
            this.checkBoxNormalize.UseVisualStyleBackColor = true;
            // 
            // labelSimplexTitle
            // 
            this.labelSimplexTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSimplexTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSimplexTitle.Location = new System.Drawing.Point(12, 75);
            this.labelSimplexTitle.Name = "labelSimplexTitle";
            this.labelSimplexTitle.Size = new System.Drawing.Size(275, 23);
            this.labelSimplexTitle.TabIndex = 23;
            this.labelSimplexTitle.Text = "Simplex Noise";
            // 
            // labelSimpleticTitle
            // 
            this.labelSimpleticTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSimpleticTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSimpleticTitle.Location = new System.Drawing.Point(368, 75);
            this.labelSimpleticTitle.Name = "labelSimpleticTitle";
            this.labelSimpleticTitle.Size = new System.Drawing.Size(275, 23);
            this.labelSimpleticTitle.TabIndex = 24;
            this.labelSimpleticTitle.Text = "Simpletic Noise";
            // 
            // labelOpenSimplexTitle
            // 
            this.labelOpenSimplexTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelOpenSimplexTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOpenSimplexTitle.Location = new System.Drawing.Point(724, 75);
            this.labelOpenSimplexTitle.Name = "labelOpenSimplexTitle";
            this.labelOpenSimplexTitle.Size = new System.Drawing.Size(275, 23);
            this.labelOpenSimplexTitle.TabIndex = 25;
            this.labelOpenSimplexTitle.Text = "Open Simplex Noise";
            // 
            // buttonSaveSimplex
            // 
            this.buttonSaveSimplex.Enabled = false;
            this.buttonSaveSimplex.Location = new System.Drawing.Point(293, 75);
            this.buttonSaveSimplex.Name = "buttonSaveSimplex";
            this.buttonSaveSimplex.Size = new System.Drawing.Size(69, 23);
            this.buttonSaveSimplex.TabIndex = 26;
            this.buttonSaveSimplex.Text = "Save as...";
            this.buttonSaveSimplex.UseVisualStyleBackColor = true;
            this.buttonSaveSimplex.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveSimpletic
            // 
            this.buttonSaveSimpletic.Enabled = false;
            this.buttonSaveSimpletic.Location = new System.Drawing.Point(649, 75);
            this.buttonSaveSimpletic.Name = "buttonSaveSimpletic";
            this.buttonSaveSimpletic.Size = new System.Drawing.Size(69, 23);
            this.buttonSaveSimpletic.TabIndex = 27;
            this.buttonSaveSimpletic.Text = "Save as...";
            this.buttonSaveSimpletic.UseVisualStyleBackColor = true;
            this.buttonSaveSimpletic.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveOpenSimplex
            // 
            this.buttonSaveOpenSimplex.Enabled = false;
            this.buttonSaveOpenSimplex.Location = new System.Drawing.Point(1005, 75);
            this.buttonSaveOpenSimplex.Name = "buttonSaveOpenSimplex";
            this.buttonSaveOpenSimplex.Size = new System.Drawing.Size(69, 23);
            this.buttonSaveOpenSimplex.TabIndex = 28;
            this.buttonSaveOpenSimplex.Text = "Save as...";
            this.buttonSaveOpenSimplex.UseVisualStyleBackColor = true;
            this.buttonSaveOpenSimplex.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveComparison
            // 
            this.buttonSaveComparison.Enabled = false;
            this.buttonSaveComparison.Location = new System.Drawing.Point(886, 27);
            this.buttonSaveComparison.Name = "buttonSaveComparison";
            this.buttonSaveComparison.Size = new System.Drawing.Size(93, 36);
            this.buttonSaveComparison.TabIndex = 29;
            this.buttonSaveComparison.Text = "Save Comparison";
            this.buttonSaveComparison.UseVisualStyleBackColor = true;
            this.buttonSaveComparison.Click += new System.EventHandler(this.buttonSaveComparison_Click);
            // 
            // ComparisonForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1088, 507);
            this.Controls.Add(this.buttonSaveComparison);
            this.Controls.Add(this.buttonSaveOpenSimplex);
            this.Controls.Add(this.buttonSaveSimpletic);
            this.Controls.Add(this.buttonSaveSimplex);
            this.Controls.Add(this.labelOpenSimplexTitle);
            this.Controls.Add(this.labelSimpleticTitle);
            this.Controls.Add(this.labelSimplexTitle);
            this.Controls.Add(this.checkBoxNormalize);
            this.Controls.Add(this.textBoxOpenSimplex);
            this.Controls.Add(this.textBoxSimpletic);
            this.Controls.Add(this.textBoxSimplex);
            this.Controls.Add(this.checkBoxAbsoluteValue);
            this.Controls.Add(this.labelPower);
            this.Controls.Add(this.textBoxPower);
            this.Controls.Add(this.comboBoxMode);
            this.Controls.Add(this.labelLacunarity);
            this.Controls.Add(this.textBoxLacunarity);
            this.Controls.Add(this.labelPersistence);
            this.Controls.Add(this.textBoxPersistence);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.textBoxSize);
            this.Controls.Add(this.textBoxOctaves);
            this.Controls.Add(this.labelScale);
            this.Controls.Add(this.textBoxScale);
            this.Controls.Add(this.labelOctaves);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.pictureBoxOpenSimplex);
            this.Controls.Add(this.pictureBoxSimpletic);
            this.Controls.Add(this.pictureBoxSimplex);
            this.Name = "ComparisonForm";
            this.ShowIcon = false;
            this.Text = "Noise Comparison";
            this.Load += new System.EventHandler(this.ComparisonForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSimplex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSimpletic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOpenSimplex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSimplex;
        private System.Windows.Forms.PictureBox pictureBoxSimpletic;
        private System.Windows.Forms.PictureBox pictureBoxOpenSimplex;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Label labelOctaves;
        private System.Windows.Forms.TextBox textBoxScale;
        private System.Windows.Forms.Label labelScale;
        private System.Windows.Forms.TextBox textBoxOctaves;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.TextBox textBoxSize;
        private System.Windows.Forms.Label labelPersistence;
        private System.Windows.Forms.TextBox textBoxPersistence;
        private System.Windows.Forms.Label labelLacunarity;
        private System.Windows.Forms.TextBox textBoxLacunarity;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Label labelPower;
        private System.Windows.Forms.TextBox textBoxPower;
        private System.Windows.Forms.CheckBox checkBoxAbsoluteValue;
        private System.Windows.Forms.TextBox textBoxSimplex;
        private System.Windows.Forms.TextBox textBoxSimpletic;
        private System.Windows.Forms.TextBox textBoxOpenSimplex;
        private System.Windows.Forms.CheckBox checkBoxNormalize;
        private System.Windows.Forms.Label labelSimplexTitle;
        private System.Windows.Forms.Label labelSimpleticTitle;
        private System.Windows.Forms.Label labelOpenSimplexTitle;
        private System.Windows.Forms.Button buttonSaveSimplex;
        private System.Windows.Forms.Button buttonSaveSimpletic;
        private System.Windows.Forms.Button buttonSaveOpenSimplex;
        private System.Windows.Forms.Button buttonSaveComparison;
    }
}

