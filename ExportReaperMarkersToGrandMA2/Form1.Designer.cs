﻿namespace ExportReaperMarkersToGrandMA2
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.gB_Open = new System.Windows.Forms.GroupBox();
            this.btn_Open = new System.Windows.Forms.Button();
            this.txt_Open = new System.Windows.Forms.TextBox();
            this.gB_Timecode = new System.Windows.Forms.GroupBox();
            this.gB_Exec = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gB_TimecodeItem = new System.Windows.Forms.GroupBox();
            this.num_TcFrameRate = new System.Windows.Forms.NumericUpDown();
            this.lbl_TcFrameRate = new System.Windows.Forms.Label();
            this.txt_TcName = new System.Windows.Forms.TextBox();
            this.lbl_TcName = new System.Windows.Forms.Label();
            this.num_TcItem = new System.Windows.Forms.NumericUpDown();
            this.lbl_TcItem = new System.Windows.Forms.Label();
            this.gB_Seq = new System.Windows.Forms.GroupBox();
            this.txt_SeqName = new System.Windows.Forms.TextBox();
            this.lbl_SeqName = new System.Windows.Forms.Label();
            this.num_SeqItem = new System.Windows.Forms.NumericUpDown();
            this.lbl_SeqItem = new System.Windows.Forms.Label();
            this.gB_Save = new System.Windows.Forms.GroupBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.txt_Save = new System.Windows.Forms.TextBox();
            this.num_ExecItem = new System.Windows.Forms.NumericUpDown();
            this.lbl_ExecItem = new System.Windows.Forms.Label();
            this.lbl_ExecPage = new System.Windows.Forms.Label();
            this.num_ExecPage = new System.Windows.Forms.NumericUpDown();
            this.gB_Open.SuspendLayout();
            this.gB_Timecode.SuspendLayout();
            this.gB_Exec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gB_TimecodeItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_TcFrameRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TcItem)).BeginInit();
            this.gB_Seq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_SeqItem)).BeginInit();
            this.gB_Save.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_ExecItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ExecPage)).BeginInit();
            this.SuspendLayout();
            // 
            // gB_Open
            // 
            this.gB_Open.Controls.Add(this.btn_Open);
            this.gB_Open.Controls.Add(this.txt_Open);
            this.gB_Open.Location = new System.Drawing.Point(12, 12);
            this.gB_Open.Name = "gB_Open";
            this.gB_Open.Size = new System.Drawing.Size(614, 48);
            this.gB_Open.TabIndex = 0;
            this.gB_Open.TabStop = false;
            this.gB_Open.Text = "Reaper Marker File auswählen";
            // 
            // btn_Open
            // 
            this.btn_Open.Location = new System.Drawing.Point(533, 16);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(75, 23);
            this.btn_Open.TabIndex = 1;
            this.btn_Open.Text = "Öffnen";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // txt_Open
            // 
            this.txt_Open.Location = new System.Drawing.Point(6, 19);
            this.txt_Open.Name = "txt_Open";
            this.txt_Open.Size = new System.Drawing.Size(521, 20);
            this.txt_Open.TabIndex = 0;
            // 
            // gB_Timecode
            // 
            this.gB_Timecode.Controls.Add(this.gB_Exec);
            this.gB_Timecode.Controls.Add(this.dataGridView1);
            this.gB_Timecode.Controls.Add(this.gB_TimecodeItem);
            this.gB_Timecode.Controls.Add(this.gB_Seq);
            this.gB_Timecode.Location = new System.Drawing.Point(12, 66);
            this.gB_Timecode.Name = "gB_Timecode";
            this.gB_Timecode.Size = new System.Drawing.Size(614, 235);
            this.gB_Timecode.TabIndex = 1;
            this.gB_Timecode.TabStop = false;
            this.gB_Timecode.Text = "Timecode Pool Item konfigurieren";
            this.gB_Timecode.Visible = false;
            // 
            // gB_Exec
            // 
            this.gB_Exec.Controls.Add(this.num_ExecItem);
            this.gB_Exec.Controls.Add(this.lbl_ExecItem);
            this.gB_Exec.Controls.Add(this.lbl_ExecPage);
            this.gB_Exec.Controls.Add(this.num_ExecPage);
            this.gB_Exec.Location = new System.Drawing.Point(276, 88);
            this.gB_Exec.Name = "gB_Exec";
            this.gB_Exec.Size = new System.Drawing.Size(332, 69);
            this.gB_Exec.TabIndex = 9;
            this.gB_Exec.TabStop = false;
            this.gB_Exec.Text = "Wähle den Executor der Timecode Spur";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(264, 210);
            this.dataGridView1.TabIndex = 8;
            // 
            // gB_TimecodeItem
            // 
            this.gB_TimecodeItem.Controls.Add(this.num_TcFrameRate);
            this.gB_TimecodeItem.Controls.Add(this.lbl_TcFrameRate);
            this.gB_TimecodeItem.Controls.Add(this.txt_TcName);
            this.gB_TimecodeItem.Controls.Add(this.lbl_TcName);
            this.gB_TimecodeItem.Controls.Add(this.num_TcItem);
            this.gB_TimecodeItem.Controls.Add(this.lbl_TcItem);
            this.gB_TimecodeItem.Location = new System.Drawing.Point(276, 160);
            this.gB_TimecodeItem.Name = "gB_TimecodeItem";
            this.gB_TimecodeItem.Size = new System.Drawing.Size(332, 69);
            this.gB_TimecodeItem.TabIndex = 7;
            this.gB_TimecodeItem.TabStop = false;
            this.gB_TimecodeItem.Text = "Wähle das Timecode Pool Item:";
            // 
            // num_TcFrameRate
            // 
            this.num_TcFrameRate.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_TcFrameRate.Location = new System.Drawing.Point(196, 14);
            this.num_TcFrameRate.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.num_TcFrameRate.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.num_TcFrameRate.Name = "num_TcFrameRate";
            this.num_TcFrameRate.Size = new System.Drawing.Size(50, 20);
            this.num_TcFrameRate.TabIndex = 15;
            this.num_TcFrameRate.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.num_TcFrameRate.ValueChanged += new System.EventHandler(this.num_TcFrameRate_ValueChanged);
            // 
            // lbl_TcFrameRate
            // 
            this.lbl_TcFrameRate.AutoSize = true;
            this.lbl_TcFrameRate.Location = new System.Drawing.Point(125, 16);
            this.lbl_TcFrameRate.Name = "lbl_TcFrameRate";
            this.lbl_TcFrameRate.Size = new System.Drawing.Size(65, 13);
            this.lbl_TcFrameRate.TabIndex = 14;
            this.lbl_TcFrameRate.Text = "Frame Rate:";
            // 
            // txt_TcName
            // 
            this.txt_TcName.Location = new System.Drawing.Point(50, 41);
            this.txt_TcName.Name = "txt_TcName";
            this.txt_TcName.Size = new System.Drawing.Size(276, 20);
            this.txt_TcName.TabIndex = 13;
            this.txt_TcName.Text = "Timecode 1";
            this.txt_TcName.TextChanged += new System.EventHandler(this.txt_TcName_TextChanged);
            // 
            // lbl_TcName
            // 
            this.lbl_TcName.AutoSize = true;
            this.lbl_TcName.Location = new System.Drawing.Point(6, 44);
            this.lbl_TcName.Name = "lbl_TcName";
            this.lbl_TcName.Size = new System.Drawing.Size(38, 13);
            this.lbl_TcName.TabIndex = 12;
            this.lbl_TcName.Text = "Name:";
            // 
            // num_TcItem
            // 
            this.num_TcItem.Location = new System.Drawing.Point(66, 14);
            this.num_TcItem.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_TcItem.Name = "num_TcItem";
            this.num_TcItem.Size = new System.Drawing.Size(50, 20);
            this.num_TcItem.TabIndex = 7;
            this.num_TcItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_TcItem.ValueChanged += new System.EventHandler(this.num_TcItem_ValueChanged);
            // 
            // lbl_TcItem
            // 
            this.lbl_TcItem.AutoSize = true;
            this.lbl_TcItem.Location = new System.Drawing.Point(6, 16);
            this.lbl_TcItem.Name = "lbl_TcItem";
            this.lbl_TcItem.Size = new System.Drawing.Size(54, 13);
            this.lbl_TcItem.TabIndex = 0;
            this.lbl_TcItem.Text = "Pool Item:";
            // 
            // gB_Seq
            // 
            this.gB_Seq.Controls.Add(this.txt_SeqName);
            this.gB_Seq.Controls.Add(this.lbl_SeqName);
            this.gB_Seq.Controls.Add(this.num_SeqItem);
            this.gB_Seq.Controls.Add(this.lbl_SeqItem);
            this.gB_Seq.Location = new System.Drawing.Point(276, 19);
            this.gB_Seq.Name = "gB_Seq";
            this.gB_Seq.Size = new System.Drawing.Size(332, 68);
            this.gB_Seq.TabIndex = 6;
            this.gB_Seq.TabStop = false;
            this.gB_Seq.Text = "Wähle die Sequenz der Timecode Spur:";
            // 
            // txt_SeqName
            // 
            this.txt_SeqName.Location = new System.Drawing.Point(50, 40);
            this.txt_SeqName.Name = "txt_SeqName";
            this.txt_SeqName.Size = new System.Drawing.Size(276, 20);
            this.txt_SeqName.TabIndex = 11;
            this.txt_SeqName.Text = "Sequenze 1";
            this.txt_SeqName.TextChanged += new System.EventHandler(this.txt_SeqName_TextChanged);
            // 
            // lbl_SeqName
            // 
            this.lbl_SeqName.AutoSize = true;
            this.lbl_SeqName.Location = new System.Drawing.Point(6, 43);
            this.lbl_SeqName.Name = "lbl_SeqName";
            this.lbl_SeqName.Size = new System.Drawing.Size(38, 13);
            this.lbl_SeqName.TabIndex = 10;
            this.lbl_SeqName.Text = "Name:";
            // 
            // num_SeqItem
            // 
            this.num_SeqItem.Location = new System.Drawing.Point(66, 14);
            this.num_SeqItem.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_SeqItem.Name = "num_SeqItem";
            this.num_SeqItem.Size = new System.Drawing.Size(50, 20);
            this.num_SeqItem.TabIndex = 9;
            this.num_SeqItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_SeqItem.ValueChanged += new System.EventHandler(this.num_SeqItem_ValueChanged);
            // 
            // lbl_SeqItem
            // 
            this.lbl_SeqItem.AutoSize = true;
            this.lbl_SeqItem.Location = new System.Drawing.Point(6, 16);
            this.lbl_SeqItem.Name = "lbl_SeqItem";
            this.lbl_SeqItem.Size = new System.Drawing.Size(54, 13);
            this.lbl_SeqItem.TabIndex = 8;
            this.lbl_SeqItem.Text = "Pool Item:";
            // 
            // gB_Save
            // 
            this.gB_Save.Controls.Add(this.btn_Save);
            this.gB_Save.Controls.Add(this.txt_Save);
            this.gB_Save.Location = new System.Drawing.Point(12, 307);
            this.gB_Save.Name = "gB_Save";
            this.gB_Save.Size = new System.Drawing.Size(614, 48);
            this.gB_Save.TabIndex = 2;
            this.gB_Save.TabStop = false;
            this.gB_Save.Text = "GrandMA2 Ordner auswählen";
            this.gB_Save.Visible = false;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(533, 16);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "Speichern";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // txt_Save
            // 
            this.txt_Save.Location = new System.Drawing.Point(6, 19);
            this.txt_Save.Name = "txt_Save";
            this.txt_Save.Size = new System.Drawing.Size(521, 20);
            this.txt_Save.TabIndex = 0;
            // 
            // num_ExecItem
            // 
            this.num_ExecItem.Location = new System.Drawing.Point(166, 15);
            this.num_ExecItem.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_ExecItem.Name = "num_ExecItem";
            this.num_ExecItem.Size = new System.Drawing.Size(50, 20);
            this.num_ExecItem.TabIndex = 17;
            this.num_ExecItem.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_ExecItem.ValueChanged += new System.EventHandler(this.num_ExecItem_ValueChanged);
            // 
            // lbl_ExecItem
            // 
            this.lbl_ExecItem.AutoSize = true;
            this.lbl_ExecItem.Location = new System.Drawing.Point(106, 17);
            this.lbl_ExecItem.Name = "lbl_ExecItem";
            this.lbl_ExecItem.Size = new System.Drawing.Size(54, 13);
            this.lbl_ExecItem.TabIndex = 16;
            this.lbl_ExecItem.Text = "Pool Item:";
            // 
            // lbl_ExecPage
            // 
            this.lbl_ExecPage.AutoSize = true;
            this.lbl_ExecPage.Location = new System.Drawing.Point(9, 17);
            this.lbl_ExecPage.Name = "lbl_ExecPage";
            this.lbl_ExecPage.Size = new System.Drawing.Size(35, 13);
            this.lbl_ExecPage.TabIndex = 15;
            this.lbl_ExecPage.Text = "Page:";
            // 
            // num_ExecPage
            // 
            this.num_ExecPage.Location = new System.Drawing.Point(50, 15);
            this.num_ExecPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_ExecPage.Name = "num_ExecPage";
            this.num_ExecPage.Size = new System.Drawing.Size(50, 20);
            this.num_ExecPage.TabIndex = 14;
            this.num_ExecPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_ExecPage.ValueChanged += new System.EventHandler(this.num_ExecPage_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 367);
            this.Controls.Add(this.gB_Save);
            this.Controls.Add(this.gB_Timecode);
            this.Controls.Add(this.gB_Open);
            this.Name = "Form1";
            this.Text = "Exporterie Reaper Marker nach GrandMA2";
            this.gB_Open.ResumeLayout(false);
            this.gB_Open.PerformLayout();
            this.gB_Timecode.ResumeLayout(false);
            this.gB_Exec.ResumeLayout(false);
            this.gB_Exec.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gB_TimecodeItem.ResumeLayout(false);
            this.gB_TimecodeItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_TcFrameRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TcItem)).EndInit();
            this.gB_Seq.ResumeLayout(false);
            this.gB_Seq.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_SeqItem)).EndInit();
            this.gB_Save.ResumeLayout(false);
            this.gB_Save.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_ExecItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ExecPage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gB_Open;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.TextBox txt_Open;
        private System.Windows.Forms.GroupBox gB_Timecode;
        private System.Windows.Forms.GroupBox gB_Save;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TextBox txt_Save;
        private System.Windows.Forms.GroupBox gB_TimecodeItem;
        private System.Windows.Forms.TextBox txt_TcName;
        private System.Windows.Forms.Label lbl_TcName;
        private System.Windows.Forms.NumericUpDown num_TcItem;
        private System.Windows.Forms.Label lbl_TcItem;
        private System.Windows.Forms.GroupBox gB_Seq;
        private System.Windows.Forms.TextBox txt_SeqName;
        private System.Windows.Forms.Label lbl_SeqName;
        private System.Windows.Forms.NumericUpDown num_SeqItem;
        private System.Windows.Forms.Label lbl_SeqItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.NumericUpDown num_TcFrameRate;
        private System.Windows.Forms.Label lbl_TcFrameRate;
        private System.Windows.Forms.GroupBox gB_Exec;
        private System.Windows.Forms.NumericUpDown num_ExecItem;
        private System.Windows.Forms.Label lbl_ExecItem;
        private System.Windows.Forms.Label lbl_ExecPage;
        private System.Windows.Forms.NumericUpDown num_ExecPage;
    }
}
