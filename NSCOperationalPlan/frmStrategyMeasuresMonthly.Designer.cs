﻿namespace NSCOperationalPlan
{
    partial class frmStrategyMeasuresMonthly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStrategyMeasuresMonthly));
            this.chk1 = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrevious = new System.Windows.Forms.ToolStripButton();
            this.tsbClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.chk2 = new System.Windows.Forms.CheckBox();
            this.opt2 = new System.Windows.Forms.RadioButton();
            this.opt0 = new System.Windows.Forms.RadioButton();
            this.opt1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboManager = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboDirector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chk1
            // 
            this.chk1.AutoSize = true;
            this.chk1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk1.Location = new System.Drawing.Point(1059, 67);
            this.chk1.Name = "chk1";
            this.chk1.Size = new System.Drawing.Size(198, 21);
            this.chk1.TabIndex = 59;
            this.chk1.Text = "Show all for the Directorate";
            this.chk1.UseVisualStyleBackColor = true;
            this.chk1.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
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
            this.toolStrip1.Size = new System.Drawing.Size(1895, 54);
            this.toolStrip1.TabIndex = 58;
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
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv.Location = new System.Drawing.Point(12, 227);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv.Size = new System.Drawing.Size(1871, 340);
            this.dgv.TabIndex = 61;
            // 
            // chk2
            // 
            this.chk2.AutoSize = true;
            this.chk2.Cursor = System.Windows.Forms.Cursors.Default;
            this.chk2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk2.Location = new System.Drawing.Point(1059, 94);
            this.chk2.Name = "chk2";
            this.chk2.Size = new System.Drawing.Size(199, 21);
            this.chk2.TabIndex = 62;
            this.chk2.Text = "Show all for all Directorates";
            this.chk2.UseVisualStyleBackColor = true;
            this.chk2.Visible = false;
            // 
            // opt2
            // 
            this.opt2.AutoSize = true;
            this.opt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt2.Location = new System.Drawing.Point(37, 119);
            this.opt2.Name = "opt2";
            this.opt2.Size = new System.Drawing.Size(248, 21);
            this.opt2.TabIndex = 109;
            this.opt2.TabStop = true;
            this.opt2.Text = "Show all Actions for all Directorates";
            this.opt2.UseVisualStyleBackColor = true;
            // 
            // opt0
            // 
            this.opt0.AutoSize = true;
            this.opt0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt0.Location = new System.Drawing.Point(37, 66);
            this.opt0.Name = "opt0";
            this.opt0.Size = new System.Drawing.Size(152, 21);
            this.opt0.TabIndex = 110;
            this.opt0.TabStop = true;
            this.opt0.Text = "My Department only";
            this.opt0.UseVisualStyleBackColor = true;
            // 
            // opt1
            // 
            this.opt1.AutoSize = true;
            this.opt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt1.Location = new System.Drawing.Point(37, 92);
            this.opt1.Name = "opt1";
            this.opt1.Size = new System.Drawing.Size(60, 21);
            this.opt1.TabIndex = 111;
            this.opt1.TabStop = true;
            this.opt1.Text = "Show";
            this.opt1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboManager);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboDirector);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(501, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 81);
            this.groupBox1.TabIndex = 118;
            this.groupBox1.TabStop = false;
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
            // frmStrategyMeasuresMonthly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1895, 579);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.opt2);
            this.Controls.Add(this.opt0);
            this.Controls.Add(this.opt1);
            this.Controls.Add(this.chk2);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.chk1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmStrategyMeasuresMonthly";
            this.Text = "frmStrategyMeasure";
            this.Load += new System.EventHandler(this.frmStrategyMeasuresMonthly_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chk1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbPrevious;
        private System.Windows.Forms.ToolStripButton tsbClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.CheckBox chk2;
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