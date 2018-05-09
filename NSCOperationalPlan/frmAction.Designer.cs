namespace NSCOperationalPlan
{
    partial class frmAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAction));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.cboDirector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAction = new System.Windows.Forms.TextBox();
            this.txtStrategyID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lstStrategy = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPartner = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboManager = new System.Windows.Forms.ComboBox();
            this.txtRank = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPIID = new System.Windows.Forms.TextBox();
            this.grdAction = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.lstStrategyObjective = new System.Windows.Forms.ListView();
            this.cbopi = new System.Windows.Forms.ComboBox();
            this.txtActionID = new System.Windows.Forms.TextBox();
            this.grdDelivery = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cboServicePlan = new System.Windows.Forms.ComboBox();
            this.cboSourceCouncilPlan = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTargertDate = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDelivery)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(1328, 54);
            this.toolStrip1.TabIndex = 13;
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
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
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
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // cboDirector
            // 
            this.cboDirector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDirector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDirector.FormattingEnabled = true;
            this.cboDirector.Location = new System.Drawing.Point(253, 452);
            this.cboDirector.Name = "cboDirector";
            this.cboDirector.Size = new System.Drawing.Size(316, 24);
            this.cboDirector.TabIndex = 31;
            this.cboDirector.SelectedIndexChanged += new System.EventHandler(this.cboDirector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(104, 450);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 23);
            this.label1.TabIndex = 30;
            this.label1.Text = "Responsible Director :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAction
            // 
            this.txtAction.BackColor = System.Drawing.Color.Ivory;
            this.txtAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAction.ForeColor = System.Drawing.Color.Blue;
            this.txtAction.Location = new System.Drawing.Point(254, 301);
            this.txtAction.Multiline = true;
            this.txtAction.Name = "txtAction";
            this.txtAction.Size = new System.Drawing.Size(916, 57);
            this.txtAction.TabIndex = 35;
            // 
            // txtStrategyID
            // 
            this.txtStrategyID.BackColor = System.Drawing.Color.Ivory;
            this.txtStrategyID.Enabled = false;
            this.txtStrategyID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStrategyID.ForeColor = System.Drawing.Color.Blue;
            this.txtStrategyID.Location = new System.Drawing.Point(253, 423);
            this.txtStrategyID.Name = "txtStrategyID";
            this.txtStrategyID.Size = new System.Drawing.Size(67, 22);
            this.txtStrategyID.TabIndex = 34;
            this.txtStrategyID.TextChanged += new System.EventHandler(this.txtStrategyID_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(139, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 23);
            this.label3.TabIndex = 32;
            this.label3.Text = "Action :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(137, 422);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 23);
            this.label2.TabIndex = 33;
            this.label2.Text = "Strategy ID :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(137, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 23);
            this.label4.TabIndex = 36;
            this.label4.Text = "Strategy :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstStrategy
            // 
            this.lstStrategy.AutoArrange = false;
            this.lstStrategy.CheckBoxes = true;
            this.lstStrategy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstStrategy.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lstStrategy.FullRowSelect = true;
            this.lstStrategy.GridLines = true;
            this.lstStrategy.Location = new System.Drawing.Point(253, 185);
            this.lstStrategy.Name = "lstStrategy";
            this.lstStrategy.Size = new System.Drawing.Size(916, 110);
            this.lstStrategy.TabIndex = 37;
            this.lstStrategy.UseCompatibleStateImageBehavior = false;
            this.lstStrategy.View = System.Windows.Forms.View.Details;
            this.lstStrategy.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstStrategy_ItemCheck);
            this.lstStrategy.SelectedIndexChanged += new System.EventHandler(this.lstStrategy_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(103, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 23);
            this.label5.TabIndex = 32;
            this.label5.Text = "Partner Organisation :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPartner
            // 
            this.txtPartner.BackColor = System.Drawing.Color.Ivory;
            this.txtPartner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartner.ForeColor = System.Drawing.Color.Blue;
            this.txtPartner.Location = new System.Drawing.Point(254, 395);
            this.txtPartner.Name = "txtPartner";
            this.txtPartner.Size = new System.Drawing.Size(315, 22);
            this.txtPartner.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(92, 364);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 23);
            this.label6.TabIndex = 36;
            this.label6.Text = "Performance Indicator :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(91, 479);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 23);
            this.label7.TabIndex = 30;
            this.label7.Text = "Responsible Manager :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboManager
            // 
            this.cboManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboManager.FormattingEnabled = true;
            this.cboManager.Location = new System.Drawing.Point(253, 479);
            this.cboManager.Name = "cboManager";
            this.cboManager.Size = new System.Drawing.Size(316, 24);
            this.cboManager.TabIndex = 31;
            // 
            // txtRank
            // 
            this.txtRank.BackColor = System.Drawing.Color.Ivory;
            this.txtRank.Enabled = false;
            this.txtRank.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRank.ForeColor = System.Drawing.Color.Blue;
            this.txtRank.Location = new System.Drawing.Point(506, 423);
            this.txtRank.Name = "txtRank";
            this.txtRank.Size = new System.Drawing.Size(63, 22);
            this.txtRank.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(339, 422);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 23);
            this.label9.TabIndex = 33;
            this.label9.Text = "Action ID :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPIID
            // 
            this.txtPIID.BackColor = System.Drawing.Color.Ivory;
            this.txtPIID.Enabled = false;
            this.txtPIID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPIID.ForeColor = System.Drawing.Color.Blue;
            this.txtPIID.Location = new System.Drawing.Point(95, 422);
            this.txtPIID.Name = "txtPIID";
            this.txtPIID.Size = new System.Drawing.Size(55, 22);
            this.txtPIID.TabIndex = 39;
            this.txtPIID.Visible = false;
            // 
            // grdAction
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.grdAction.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdAction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAction.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdAction.Location = new System.Drawing.Point(0, 522);
            this.grdAction.MultiSelect = false;
            this.grdAction.Name = "grdAction";
            this.grdAction.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdAction.Size = new System.Drawing.Size(1328, 349);
            this.grdAction.StandardTab = true;
            this.grdAction.TabIndex = 47;
            this.grdAction.TabStop = false;
            this.grdAction.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAction_CellContentClick);
            this.grdAction.DoubleClick += new System.EventHandler(this.grdAction_DoubleClick);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Location = new System.Drawing.Point(95, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(152, 23);
            this.label10.TabIndex = 36;
            this.label10.Text = "Strategy Objective :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstStrategyObjective
            // 
            this.lstStrategyObjective.AutoArrange = false;
            this.lstStrategyObjective.CheckBoxes = true;
            this.lstStrategyObjective.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstStrategyObjective.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lstStrategyObjective.FullRowSelect = true;
            this.lstStrategyObjective.GridLines = true;
            this.lstStrategyObjective.Location = new System.Drawing.Point(254, 68);
            this.lstStrategyObjective.Name = "lstStrategyObjective";
            this.lstStrategyObjective.Size = new System.Drawing.Size(916, 111);
            this.lstStrategyObjective.TabIndex = 37;
            this.lstStrategyObjective.UseCompatibleStateImageBehavior = false;
            this.lstStrategyObjective.View = System.Windows.Forms.View.Details;
            this.lstStrategyObjective.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstStrategyObjective_ItemCheck);
            // 
            // cbopi
            // 
            this.cbopi.BackColor = System.Drawing.Color.Ivory;
            this.cbopi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopi.Enabled = false;
            this.cbopi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbopi.FormattingEnabled = true;
            this.cbopi.Location = new System.Drawing.Point(254, 365);
            this.cbopi.Name = "cbopi";
            this.cbopi.Size = new System.Drawing.Size(515, 24);
            this.cbopi.TabIndex = 48;
            // 
            // txtActionID
            // 
            this.txtActionID.BackColor = System.Drawing.Color.Ivory;
            this.txtActionID.Enabled = false;
            this.txtActionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActionID.ForeColor = System.Drawing.Color.Blue;
            this.txtActionID.Location = new System.Drawing.Point(426, 422);
            this.txtActionID.Name = "txtActionID";
            this.txtActionID.Size = new System.Drawing.Size(74, 22);
            this.txtActionID.TabIndex = 34;
            // 
            // grdDelivery
            // 
            this.grdDelivery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDelivery.Location = new System.Drawing.Point(1015, 365);
            this.grdDelivery.Name = "grdDelivery";
            this.grdDelivery.Size = new System.Drawing.Size(154, 138);
            this.grdDelivery.TabIndex = 49;
            this.grdDelivery.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDelivery_CellClick);
            this.grdDelivery.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDelivery_CellContentClick);
            this.grdDelivery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdDelivery_KeyDown);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(864, 366);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(145, 23);
            this.label8.TabIndex = 32;
            this.label8.Text = "Delivery Program :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Location = new System.Drawing.Point(575, 453);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 23);
            this.label11.TabIndex = 30;
            this.label11.Text = "Service Plan :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboServicePlan
            // 
            this.cboServicePlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServicePlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboServicePlan.FormattingEnabled = true;
            this.cboServicePlan.Location = new System.Drawing.Point(575, 478);
            this.cboServicePlan.Name = "cboServicePlan";
            this.cboServicePlan.Size = new System.Drawing.Size(274, 24);
            this.cboServicePlan.TabIndex = 31;
            // 
            // cboSourceCouncilPlan
            // 
            this.cboSourceCouncilPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceCouncilPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSourceCouncilPlan.FormattingEnabled = true;
            this.cboSourceCouncilPlan.Location = new System.Drawing.Point(578, 420);
            this.cboSourceCouncilPlan.Name = "cboSourceCouncilPlan";
            this.cboSourceCouncilPlan.Size = new System.Drawing.Size(271, 24);
            this.cboSourceCouncilPlan.TabIndex = 51;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Location = new System.Drawing.Point(578, 395);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(152, 23);
            this.label12.TabIndex = 50;
            this.label12.Text = "Source Council Plan :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Location = new System.Drawing.Point(869, 453);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 23);
            this.label13.TabIndex = 50;
            this.label13.Text = "Target Date";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTargertDate
            // 
            this.txtTargertDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTargertDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtTargertDate.Location = new System.Drawing.Point(869, 476);
            this.txtTargertDate.Name = "txtTargertDate";
            this.txtTargertDate.Size = new System.Drawing.Size(140, 23);
            this.txtTargertDate.TabIndex = 52;
            // 
            // frmAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 871);
            this.ControlBox = false;
            this.Controls.Add(this.txtTargertDate);
            this.Controls.Add(this.cboSourceCouncilPlan);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.grdDelivery);
            this.Controls.Add(this.cbopi);
            this.Controls.Add(this.grdAction);
            this.Controls.Add(this.txtPIID);
            this.Controls.Add(this.txtRank);
            this.Controls.Add(this.lstStrategyObjective);
            this.Controls.Add(this.lstStrategy);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPartner);
            this.Controls.Add(this.txtAction);
            this.Controls.Add(this.txtActionID);
            this.Controls.Add(this.txtStrategyID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboServicePlan);
            this.Controls.Add(this.cboManager);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboDirector);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Actions";
            this.Load += new System.EventHandler(this.frmAction_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDelivery)).EndInit();
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
        private System.Windows.Forms.ComboBox cboDirector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAction;
        private System.Windows.Forms.TextBox txtStrategyID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lstStrategy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPartner;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboManager;
        private System.Windows.Forms.TextBox txtRank;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPIID;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.DataGridView grdAction;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView lstStrategyObjective;
        private System.Windows.Forms.ComboBox cbopi;
        private System.Windows.Forms.TextBox txtActionID;
        private System.Windows.Forms.DataGridView grdDelivery;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboServicePlan;
        private System.Windows.Forms.ComboBox cboSourceCouncilPlan;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker txtTargertDate;
    }
}