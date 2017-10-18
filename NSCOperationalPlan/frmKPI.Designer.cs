namespace NSCOperationalPlan
{
    partial class frmKPI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKPI));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tabkpi = new System.Windows.Forms.TabControl();
            this.tab01 = new System.Windows.Forms.TabPage();
            this.dgv01 = new System.Windows.Forms.DataGridView();
            this.tab02 = new System.Windows.Forms.TabPage();
            this.dgv02 = new System.Windows.Forms.DataGridView();
            this.tab03 = new System.Windows.Forms.TabPage();
            this.dgv03 = new System.Windows.Forms.DataGridView();
            this.cboManager = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboDirector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEffiYear = new System.Windows.Forms.TextBox();
            this.txtEffiID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboEffiUnits = new System.Windows.Forms.ComboBox();
            this.cboEffiPrefix = new System.Windows.Forms.ComboBox();
            this.txtEffiValue = new System.Windows.Forms.TextBox();
            this.txtEffiDes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblkpi = new System.Windows.Forms.Label();
            this.btnEffiAdd = new System.Windows.Forms.Button();
            this.cboServicePlan = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKPIID = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.tabkpi.SuspendLayout();
            this.tab01.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv01)).BeginInit();
            this.tab02.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv02)).BeginInit();
            this.tab03.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv03)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator1,
            this.tsbNew,
            this.tsbEdit,
            this.tsbDelete,
            this.toolStripSeparator2,
            this.tsbSave,
            this.toolStripSeparator3,
            this.toolStripButton6,
            this.toolStripSeparator4,
            this.tsbPrint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1348, 54);
            this.toolStrip1.TabIndex = 15;
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
            // tsbNew
            // 
            this.tsbNew.Enabled = false;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(36, 51);
            this.tsbNew.Text = "New";
            this.tsbNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbEdit
            // 
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(36, 51);
            this.tsbEdit.Text = "Edit";
            this.tsbEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Enabled = false;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(44, 51);
            this.tsbDelete.Text = "Delete";
            this.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
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
            // toolStripButton6
            // 
            this.toolStripButton6.Enabled = false;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(46, 51);
            this.toolStripButton6.Text = "Search";
            this.toolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            // tabkpi
            // 
            this.tabkpi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabkpi.Controls.Add(this.tab01);
            this.tabkpi.Controls.Add(this.tab02);
            this.tabkpi.Controls.Add(this.tab03);
            this.tabkpi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabkpi.ItemSize = new System.Drawing.Size(200, 23);
            this.tabkpi.Location = new System.Drawing.Point(10, 314);
            this.tabkpi.Multiline = true;
            this.tabkpi.Name = "tabkpi";
            this.tabkpi.SelectedIndex = 0;
            this.tabkpi.Size = new System.Drawing.Size(1326, 586);
            this.tabkpi.TabIndex = 16;
            this.tabkpi.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabkpi_DrawItem);
            this.tabkpi.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabkpi_Selected);
            // 
            // tab01
            // 
            this.tab01.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tab01.Controls.Add(this.dgv01);
            this.tab01.Location = new System.Drawing.Point(4, 27);
            this.tab01.Name = "tab01";
            this.tab01.Padding = new System.Windows.Forms.Padding(3);
            this.tab01.Size = new System.Drawing.Size(1318, 555);
            this.tab01.TabIndex = 0;
            this.tab01.Text = "Efficiency Measure";
            // 
            // dgv01
            // 
            this.dgv01.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv01.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv01.Location = new System.Drawing.Point(7, 6);
            this.dgv01.Name = "dgv01";
            this.dgv01.Size = new System.Drawing.Size(1305, 543);
            this.dgv01.TabIndex = 0;
            this.dgv01.DoubleClick += new System.EventHandler(this.dgv01_DoubleClick);
            // 
            // tab02
            // 
            this.tab02.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tab02.Controls.Add(this.dgv02);
            this.tab02.Location = new System.Drawing.Point(4, 27);
            this.tab02.Name = "tab02";
            this.tab02.Padding = new System.Windows.Forms.Padding(3);
            this.tab02.Size = new System.Drawing.Size(1318, 555);
            this.tab02.TabIndex = 1;
            this.tab02.Text = "Effectiveness Measure ";
            // 
            // dgv02
            // 
            this.dgv02.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv02.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv02.Location = new System.Drawing.Point(7, 6);
            this.dgv02.Name = "dgv02";
            this.dgv02.Size = new System.Drawing.Size(1305, 543);
            this.dgv02.TabIndex = 1;
            this.dgv02.DoubleClick += new System.EventHandler(this.dgv02_DoubleClick);
            // 
            // tab03
            // 
            this.tab03.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tab03.Controls.Add(this.dgv03);
            this.tab03.Location = new System.Drawing.Point(4, 27);
            this.tab03.Name = "tab03";
            this.tab03.Size = new System.Drawing.Size(1318, 555);
            this.tab03.TabIndex = 2;
            this.tab03.Text = "Workload Measure";
            // 
            // dgv03
            // 
            this.dgv03.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv03.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv03.Location = new System.Drawing.Point(7, 6);
            this.dgv03.Name = "dgv03";
            this.dgv03.Size = new System.Drawing.Size(1308, 546);
            this.dgv03.TabIndex = 1;
            this.dgv03.DoubleClick += new System.EventHandler(this.dgv03_DoubleClick);
            // 
            // cboManager
            // 
            this.cboManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboManager.FormattingEnabled = true;
            this.cboManager.Location = new System.Drawing.Point(213, 129);
            this.cboManager.Name = "cboManager";
            this.cboManager.Size = new System.Drawing.Size(316, 24);
            this.cboManager.TabIndex = 1;
            this.cboManager.SelectedIndexChanged += new System.EventHandler(this.cboManager_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(51, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 23);
            this.label7.TabIndex = 32;
            this.label7.Text = "Responsible Manager :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDirector
            // 
            this.cboDirector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDirector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDirector.FormattingEnabled = true;
            this.cboDirector.Location = new System.Drawing.Point(213, 99);
            this.cboDirector.Name = "cboDirector";
            this.cboDirector.Size = new System.Drawing.Size(316, 24);
            this.cboDirector.TabIndex = 0;
            this.cboDirector.SelectedIndexChanged += new System.EventHandler(this.cboDirector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(64, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 23);
            this.label1.TabIndex = 33;
            this.label1.Text = "Responsible Director :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEffiYear
            // 
            this.txtEffiYear.BackColor = System.Drawing.Color.White;
            this.txtEffiYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEffiYear.ForeColor = System.Drawing.Color.Blue;
            this.txtEffiYear.Location = new System.Drawing.Point(690, 103);
            this.txtEffiYear.Name = "txtEffiYear";
            this.txtEffiYear.Size = new System.Drawing.Size(135, 23);
            this.txtEffiYear.TabIndex = 2;
            // 
            // txtEffiID
            // 
            this.txtEffiID.BackColor = System.Drawing.Color.White;
            this.txtEffiID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEffiID.ForeColor = System.Drawing.Color.Blue;
            this.txtEffiID.Location = new System.Drawing.Point(1094, 211);
            this.txtEffiID.Name = "txtEffiID";
            this.txtEffiID.Size = new System.Drawing.Size(135, 23);
            this.txtEffiID.TabIndex = 57;
            this.txtEffiID.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Gray;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Window;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(512, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 23);
            this.label5.TabIndex = 56;
            this.label5.Text = "Units";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gray;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Window;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(362, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 23);
            this.label8.TabIndex = 55;
            this.label8.Text = "Estimate Value";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboEffiUnits
            // 
            this.cboEffiUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEffiUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEffiUnits.FormattingEnabled = true;
            this.cboEffiUnits.Location = new System.Drawing.Point(513, 234);
            this.cboEffiUnits.Name = "cboEffiUnits";
            this.cboEffiUnits.Size = new System.Drawing.Size(134, 24);
            this.cboEffiUnits.TabIndex = 7;
            // 
            // cboEffiPrefix
            // 
            this.cboEffiPrefix.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEffiPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEffiPrefix.FormattingEnabled = true;
            this.cboEffiPrefix.Location = new System.Drawing.Point(211, 236);
            this.cboEffiPrefix.Name = "cboEffiPrefix";
            this.cboEffiPrefix.Size = new System.Drawing.Size(134, 24);
            this.cboEffiPrefix.TabIndex = 5;
            // 
            // txtEffiValue
            // 
            this.txtEffiValue.BackColor = System.Drawing.Color.White;
            this.txtEffiValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEffiValue.ForeColor = System.Drawing.Color.Blue;
            this.txtEffiValue.Location = new System.Drawing.Point(362, 235);
            this.txtEffiValue.Name = "txtEffiValue";
            this.txtEffiValue.Size = new System.Drawing.Size(135, 23);
            this.txtEffiValue.TabIndex = 6;
            // 
            // txtEffiDes
            // 
            this.txtEffiDes.BackColor = System.Drawing.Color.White;
            this.txtEffiDes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEffiDes.ForeColor = System.Drawing.Color.Blue;
            this.txtEffiDes.Location = new System.Drawing.Point(211, 182);
            this.txtEffiDes.Name = "txtEffiDes";
            this.txtEffiDes.Size = new System.Drawing.Size(1026, 23);
            this.txtEffiDes.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Gray;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(210, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 23);
            this.label4.TabIndex = 49;
            this.label4.Text = "Prefix";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(62, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 23);
            this.label2.TabIndex = 48;
            this.label2.Text = "Description :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblkpi
            // 
            this.lblkpi.BackColor = System.Drawing.Color.Lime;
            this.lblkpi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblkpi.ForeColor = System.Drawing.Color.Black;
            this.lblkpi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblkpi.Location = new System.Drawing.Point(979, 237);
            this.lblkpi.Name = "lblkpi";
            this.lblkpi.Size = new System.Drawing.Size(331, 63);
            this.lblkpi.TabIndex = 58;
            this.lblkpi.Text = "Prefix";
            this.lblkpi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblkpi.Visible = false;
            // 
            // btnEffiAdd
            // 
            this.btnEffiAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEffiAdd.Image = global::NSCOperationalPlan.Properties.Resources.Efficiency;
            this.btnEffiAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEffiAdd.Location = new System.Drawing.Point(799, 213);
            this.btnEffiAdd.Name = "btnEffiAdd";
            this.btnEffiAdd.Size = new System.Drawing.Size(254, 81);
            this.btnEffiAdd.TabIndex = 8;
            this.btnEffiAdd.Text = "Add to Grid";
            this.btnEffiAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEffiAdd.UseVisualStyleBackColor = true;
            this.btnEffiAdd.Click += new System.EventHandler(this.btnEffiAdd_Click);
            // 
            // cboServicePlan
            // 
            this.cboServicePlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServicePlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboServicePlan.FormattingEnabled = true;
            this.cboServicePlan.Location = new System.Drawing.Point(690, 132);
            this.cboServicePlan.Name = "cboServicePlan";
            this.cboServicePlan.Size = new System.Drawing.Size(547, 24);
            this.cboServicePlan.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(535, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 23);
            this.label6.TabIndex = 59;
            this.label6.Text = "Service Plan :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(535, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 23);
            this.label3.TabIndex = 59;
            this.label3.Text = "Year Estimate :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKPIID
            // 
            this.txtKPIID.BackColor = System.Drawing.Color.White;
            this.txtKPIID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKPIID.ForeColor = System.Drawing.Color.Blue;
            this.txtKPIID.Location = new System.Drawing.Point(1102, 103);
            this.txtKPIID.Name = "txtKPIID";
            this.txtKPIID.Size = new System.Drawing.Size(135, 23);
            this.txtKPIID.TabIndex = 60;
            // 
            // frmKPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 912);
            this.Controls.Add(this.txtKPIID);
            this.Controls.Add(this.cboServicePlan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblkpi);
            this.Controls.Add(this.txtEffiID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnEffiAdd);
            this.Controls.Add(this.cboEffiUnits);
            this.Controls.Add(this.cboEffiPrefix);
            this.Controls.Add(this.txtEffiValue);
            this.Controls.Add(this.txtEffiDes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboManager);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboDirector);
            this.Controls.Add(this.txtEffiYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabkpi);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmKPI";
            this.Text = "KPI";
            this.Load += new System.EventHandler(this.frmKPI_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabkpi.ResumeLayout(false);
            this.tab01.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv01)).EndInit();
            this.tab02.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv02)).EndInit();
            this.tab03.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv03)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.TabControl tabkpi;
        private System.Windows.Forms.TabPage tab01;
        private System.Windows.Forms.TabPage tab02;
        private System.Windows.Forms.ComboBox cboManager;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboDirector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tab03;
        private System.Windows.Forms.DataGridView dgv01;
        private System.Windows.Forms.TextBox txtEffiYear;
        private System.Windows.Forms.DataGridView dgv02;
        private System.Windows.Forms.TextBox txtEffiID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnEffiAdd;
        private System.Windows.Forms.ComboBox cboEffiUnits;
        private System.Windows.Forms.ComboBox cboEffiPrefix;
        private System.Windows.Forms.TextBox txtEffiValue;
        private System.Windows.Forms.TextBox txtEffiDes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgv03;
        private System.Windows.Forms.Label lblkpi;
        private System.Windows.Forms.ComboBox cboServicePlan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKPIID;
    }
}