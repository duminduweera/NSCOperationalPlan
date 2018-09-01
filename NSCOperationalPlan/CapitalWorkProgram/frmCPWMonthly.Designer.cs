namespace NSCOperationalPlan
{
    partial class frmCPWMonthly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCPWMonthly));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrevious = new System.Windows.Forms.ToolStripButton();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.dgv01 = new System.Windows.Forms.DataGridView();
            this.opt2 = new System.Windows.Forms.RadioButton();
            this.opt0 = new System.Windows.Forms.RadioButton();
            this.opt1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboManager = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboDirector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv01)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbSave,
            this.toolStripSeparator3,
            this.tsbPrevious,
            this.tsbClear,
            this.toolStripSeparator4,
            this.tsbPrint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1406, 54);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(40, 51);
            this.tsbClose.Text = "Close";
            this.tsbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::NSCOperationalPlan.Properties.Resources.Save_32;
            this.tsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(36, 51);
            this.tsbSave.Text = "Save";
            this.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbPrevious
            // 
            this.tsbPrevious.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrevious.Image")));
            this.tsbPrevious.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrevious.Name = "tsbPrevious";
            this.tsbPrevious.Size = new System.Drawing.Size(56, 51);
            this.tsbPrevious.Text = "Previous";
            this.tsbPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbPrevious.ToolTipText = "Load Data From Previous Month";
            this.tsbPrevious.Click += new System.EventHandler(this.tsbPrevious_Click);
            // 
            // tsbClear
            // 
            this.tsbClear.Image = ((System.Drawing.Image)(resources.GetObject("tsbClear.Image")));
            this.tsbClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClear.Name = "tsbClear";
            this.tsbClear.Size = new System.Drawing.Size(38, 51);
            this.tsbClear.Text = "Clear";
            this.tsbClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbClear.ToolTipText = "Clear All data on the Grid";
            this.tsbClear.Click += new System.EventHandler(this.tsbClear_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbPrint
            // 
            this.tsbPrint.Image = global::NSCOperationalPlan.Properties.Resources.printer_icon_32;
            this.tsbPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(36, 51);
            this.tsbPrint.Text = "Print";
            this.tsbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // dgv01
            // 
            this.dgv01.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv01.Location = new System.Drawing.Point(12, 144);
            this.dgv01.MultiSelect = false;
            this.dgv01.Name = "dgv01";
            this.dgv01.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv01.Size = new System.Drawing.Size(1382, 511);
            this.dgv01.TabIndex = 56;
            this.dgv01.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv01_CellClick);
            this.dgv01.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv01_CellEndEdit);
            this.dgv01.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv01_CellLeave);
            // 
            // opt2
            // 
            this.opt2.AutoSize = true;
            this.opt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt2.Location = new System.Drawing.Point(41, 117);
            this.opt2.Name = "opt2";
            this.opt2.Size = new System.Drawing.Size(248, 21);
            this.opt2.TabIndex = 109;
            this.opt2.TabStop = true;
            this.opt2.Text = "Show all Actions for all Directorates";
            this.opt2.UseVisualStyleBackColor = true;
            this.opt2.CheckedChanged += new System.EventHandler(this.opt2_CheckedChanged);
            // 
            // opt0
            // 
            this.opt0.AutoSize = true;
            this.opt0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt0.Location = new System.Drawing.Point(41, 64);
            this.opt0.Name = "opt0";
            this.opt0.Size = new System.Drawing.Size(152, 21);
            this.opt0.TabIndex = 110;
            this.opt0.TabStop = true;
            this.opt0.Text = "My Department only";
            this.opt0.UseVisualStyleBackColor = true;
            this.opt0.CheckedChanged += new System.EventHandler(this.opt0_CheckedChanged);
            // 
            // opt1
            // 
            this.opt1.AutoSize = true;
            this.opt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt1.Location = new System.Drawing.Point(41, 90);
            this.opt1.Name = "opt1";
            this.opt1.Size = new System.Drawing.Size(60, 21);
            this.opt1.TabIndex = 111;
            this.opt1.TabStop = true;
            this.opt1.Text = "Show";
            this.opt1.UseVisualStyleBackColor = true;
            this.opt1.CheckedChanged += new System.EventHandler(this.opt1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboManager);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboDirector);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(512, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 81);
            this.groupBox1.TabIndex = 116;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // cboManager
            // 
            this.cboManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboManager.FormattingEnabled = true;
            this.cboManager.Location = new System.Drawing.Point(181, 49);
            this.cboManager.Name = "cboManager";
            this.cboManager.Size = new System.Drawing.Size(316, 24);
            this.cboManager.TabIndex = 117;
            this.cboManager.SelectedIndexChanged += new System.EventHandler(this.cboManager_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(19, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 23);
            this.label7.TabIndex = 118;
            this.label7.Text = "Responsible Manager :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDirector
            // 
            this.cboDirector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDirector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDirector.FormattingEnabled = true;
            this.cboDirector.Location = new System.Drawing.Point(181, 19);
            this.cboDirector.Name = "cboDirector";
            this.cboDirector.Size = new System.Drawing.Size(316, 24);
            this.cboDirector.TabIndex = 116;
            this.cboDirector.SelectedIndexChanged += new System.EventHandler(this.cboDirector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(32, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 23);
            this.label1.TabIndex = 119;
            this.label1.Text = "Responsible Director :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmCPWMonthly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 667);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.opt2);
            this.Controls.Add(this.opt0);
            this.Controls.Add(this.opt1);
            this.Controls.Add(this.dgv01);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmCPWMonthly";
            this.Text = "Capital Work - Monthly Progress";
            this.Load += new System.EventHandler(this.frmCPWMonthly_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv01)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbPrevious;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.DataGridView dgv01;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.RadioButton opt2;
        private System.Windows.Forms.RadioButton opt0;
        private System.Windows.Forms.RadioButton opt1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboManager;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboDirector;
        private System.Windows.Forms.Label label1;
    }
}