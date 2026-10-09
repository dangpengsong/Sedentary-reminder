namespace Reminder
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.btnRestNow = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numWrkTime = new System.Windows.Forms.NumericUpDown();
            this.numRstTime = new System.Windows.Forms.NumericUpDown();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.主窗体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exit_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.ckBoxInput = new System.Windows.Forms.CheckBox();
            this.ckBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.ckBoxWorkRange = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpRange1Start = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpRange1End = new System.Windows.Forms.DateTimePicker();
            this.ckBoxRange2 = new System.Windows.Forms.CheckBox();
            this.dtpRange2Start = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpRange2End = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtHealthTips = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numWrkTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRstTime)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "工作时间：";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(11, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "计时器：";
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "休息时间：";
            //
            // btn_start
            //
            this.btn_start.BackColor = System.Drawing.Color.Transparent;
            this.btn_start.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.Location = new System.Drawing.Point(90, 390);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 25);
            this.btn_start.TabIndex = 14;
            this.btn_start.Text = "开始";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.Btn_start_Click);
            //
            // btnRestNow
            //
            this.btnRestNow.BackColor = System.Drawing.Color.Transparent;
            this.btnRestNow.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnRestNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestNow.Location = new System.Drawing.Point(175, 390);
            this.btnRestNow.Name = "btnRestNow";
            this.btnRestNow.Size = new System.Drawing.Size(75, 25);
            this.btnRestNow.TabIndex = 15;
            this.btnRestNow.Text = "立即休息";
            this.btnRestNow.UseVisualStyleBackColor = false;
            this.btnRestNow.Click += new System.EventHandler(this.BtnRestNow_Click);
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(157, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "（分钟）";
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(157, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "（分钟）";
            //
            // numWrkTime
            //
            this.numWrkTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numWrkTime.Location = new System.Drawing.Point(100, 43);
            this.numWrkTime.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numWrkTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWrkTime.Name = "numWrkTime";
            this.numWrkTime.Size = new System.Drawing.Size(51, 21);
            this.numWrkTime.TabIndex = 16;
            this.numWrkTime.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            //
            // numRstTime
            //
            this.numRstTime.BackColor = System.Drawing.Color.White;
            this.numRstTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numRstTime.Location = new System.Drawing.Point(100, 70);
            this.numRstTime.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numRstTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRstTime.Name = "numRstTime";
            this.numRstTime.Size = new System.Drawing.Size(51, 21);
            this.numRstTime.TabIndex = 17;
            this.numRstTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            //
            // notifyIcon1
            //
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Reminder";
            this.notifyIcon1.Visible = true;
            //
            // contextMenuStrip1
            //
            this.contextMenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.主窗体ToolStripMenuItem,
            this.关于ToolStripMenuItem,
            this.exit_ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            //
            // 主窗体ToolStripMenuItem
            //
            this.主窗体ToolStripMenuItem.Name = "主窗体ToolStripMenuItem";
            this.主窗体ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.主窗体ToolStripMenuItem.Text = "首选项";
            this.主窗体ToolStripMenuItem.Click += new System.EventHandler(this.主窗体ToolStripMenuItem_Click);
            //
            // exit_ToolStripMenuItem
            //
            this.exit_ToolStripMenuItem.Name = "exit_ToolStripMenuItem";
            this.exit_ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exit_ToolStripMenuItem.Text = "退出";
            this.exit_ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            //
            // 关于ToolStripMenuItem
            //
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(11, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 18;
            this.label4.Text = "选项：";
            //
            // ckBoxInput
            //
            this.ckBoxInput.AutoSize = true;
            this.ckBoxInput.Checked = true;
            this.ckBoxInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBoxInput.Location = new System.Drawing.Point(26, 123);
            this.ckBoxInput.Name = "ckBoxInput";
            this.ckBoxInput.Size = new System.Drawing.Size(108, 16);
            this.ckBoxInput.TabIndex = 19;
            this.ckBoxInput.Text = "休息时锁定键盘";
            this.ckBoxInput.UseVisualStyleBackColor = true;
            //
            // ckBoxAutoStart
            //
            this.ckBoxAutoStart.AutoSize = true;
            this.ckBoxAutoStart.Location = new System.Drawing.Point(26, 146);
            this.ckBoxAutoStart.Name = "ckBoxAutoStart";
            this.ckBoxAutoStart.Size = new System.Drawing.Size(96, 16);
            this.ckBoxAutoStart.TabIndex = 20;
            this.ckBoxAutoStart.Text = "开机自动启动";
            this.ckBoxAutoStart.UseVisualStyleBackColor = true;
            //
            // ckBoxWorkRange
            //
            this.ckBoxWorkRange.AutoSize = true;
            this.ckBoxWorkRange.Location = new System.Drawing.Point(26, 169);
            this.ckBoxWorkRange.Name = "ckBoxWorkRange";
            this.ckBoxWorkRange.Size = new System.Drawing.Size(180, 16);
            this.ckBoxWorkRange.TabIndex = 21;
            this.ckBoxWorkRange.Text = "限定工作时间（非工作时段不计时）";
            this.ckBoxWorkRange.UseVisualStyleBackColor = true;
            this.ckBoxWorkRange.CheckedChanged += new System.EventHandler(this.RangeOption_CheckedChanged);
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 193);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "时段1：";
            //
            // dtpRange1Start
            //
            this.dtpRange1Start.CustomFormat = "HH:mm";
            this.dtpRange1Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRange1Start.Location = new System.Drawing.Point(95, 190);
            this.dtpRange1Start.Name = "dtpRange1Start";
            this.dtpRange1Start.ShowUpDown = true;
            this.dtpRange1Start.Size = new System.Drawing.Size(70, 21);
            this.dtpRange1Start.TabIndex = 23;
            this.dtpRange1Start.Value = new System.DateTime(2000, 1, 1, 9, 0, 0, 0);
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(170, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "至";
            //
            // dtpRange1End
            //
            this.dtpRange1End.CustomFormat = "HH:mm";
            this.dtpRange1End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRange1End.Location = new System.Drawing.Point(188, 190);
            this.dtpRange1End.Name = "dtpRange1End";
            this.dtpRange1End.ShowUpDown = true;
            this.dtpRange1End.Size = new System.Drawing.Size(70, 21);
            this.dtpRange1End.TabIndex = 25;
            this.dtpRange1End.Value = new System.DateTime(2000, 1, 1, 12, 0, 0, 0);
            //
            // ckBoxRange2
            //
            this.ckBoxRange2.AutoSize = true;
            this.ckBoxRange2.Location = new System.Drawing.Point(26, 219);
            this.ckBoxRange2.Name = "ckBoxRange2";
            this.ckBoxRange2.Size = new System.Drawing.Size(60, 16);
            this.ckBoxRange2.TabIndex = 26;
            this.ckBoxRange2.Text = "时段2";
            this.ckBoxRange2.UseVisualStyleBackColor = true;
            this.ckBoxRange2.CheckedChanged += new System.EventHandler(this.RangeOption_CheckedChanged);
            //
            // dtpRange2Start
            //
            this.dtpRange2Start.CustomFormat = "HH:mm";
            this.dtpRange2Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRange2Start.Location = new System.Drawing.Point(95, 216);
            this.dtpRange2Start.Name = "dtpRange2Start";
            this.dtpRange2Start.ShowUpDown = true;
            this.dtpRange2Start.Size = new System.Drawing.Size(70, 21);
            this.dtpRange2Start.TabIndex = 27;
            this.dtpRange2Start.Value = new System.DateTime(2000, 1, 1, 13, 30, 0, 0);
            //
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(170, 219);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "至";
            //
            // dtpRange2End
            //
            this.dtpRange2End.CustomFormat = "HH:mm";
            this.dtpRange2End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRange2End.Location = new System.Drawing.Point(188, 216);
            this.dtpRange2End.Name = "dtpRange2End";
            this.dtpRange2End.ShowUpDown = true;
            this.dtpRange2End.Size = new System.Drawing.Size(70, 21);
            this.dtpRange2End.TabIndex = 29;
            this.dtpRange2End.Value = new System.DateTime(2000, 1, 1, 18, 0, 0, 0);
            //
            // label10
            //
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 246);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(245, 12);
            this.label10.TabIndex = 30;
            this.label10.Text = "健康提示语（每行一条，留空则使用内置默认）：";
            //
            // txtHealthTips
            //
            this.txtHealthTips.AcceptsReturn = true;
            this.txtHealthTips.Location = new System.Drawing.Point(26, 268);
            this.txtHealthTips.Multiline = true;
            this.txtHealthTips.Name = "txtHealthTips";
            this.txtHealthTips.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHealthTips.Size = new System.Drawing.Size(288, 110);
            this.txtHealthTips.TabIndex = 31;
            //
            // MainFrm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(340, 428);
            this.Controls.Add(this.txtHealthTips);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtpRange2End);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpRange2Start);
            this.Controls.Add(this.ckBoxRange2);
            this.Controls.Add(this.dtpRange1End);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtpRange1Start);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ckBoxWorkRange);
            this.Controls.Add(this.ckBoxAutoStart);
            this.Controls.Add(this.ckBoxInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numRstTime);
            this.Controls.Add(this.numWrkTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btnRestNow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sedentary Reminder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numWrkTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRstTime)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btnRestNow;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numWrkTime;
        private System.Windows.Forms.NumericUpDown numRstTime;
        protected System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 主窗体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exit_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckBoxInput;
        private System.Windows.Forms.CheckBox ckBoxAutoStart;
        private System.Windows.Forms.CheckBox ckBoxWorkRange;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpRange1Start;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpRange1End;
        private System.Windows.Forms.CheckBox ckBoxRange2;
        private System.Windows.Forms.DateTimePicker dtpRange2Start;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpRange2End;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtHealthTips;
    }
}
