namespace Reminder
{
    partial class RestFrm
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
        /// 控件坐标基于设计尺寸 905x486，运行时由 LayoutCenter 整体居中到实际屏幕。
        /// 布局自上而下：久坐提示 → 健康提示 → 解锁倒计时 → 操作提示 → 解锁按钮。
        /// 配色为深墨绿底(#1B5E20) + 白色文字，按钮与背景同色、白色细边框（悬停变亮）。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerRst = new System.Windows.Forms.Timer(this.components);
            this.lblText = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // timerRst
            //
            this.timerRst.Interval = 1000;
            this.timerRst.Tick += new System.EventHandler(this.TimerRst_Tick);
            //
            // lblText
            //
            this.lblText.Font = new System.Drawing.Font("楷体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblText.Location = new System.Drawing.Point(22, 68);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(860, 52);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "站起来活动下！";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblText.UseMnemonic = false;
            //
            // lblTip
            //
            this.lblTip.AutoEllipsis = true;
            this.lblTip.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.lblTip.Location = new System.Drawing.Point(22, 126);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(860, 32);
            this.lblTip.TabIndex = 1;
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTip.UseMnemonic = false;
            //
            // lblTime
            //
            this.lblTime.Font = new System.Drawing.Font("Consolas", 44F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(22, 196);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(860, 88);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblCaption
            //
            this.lblCaption.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            this.lblCaption.Location = new System.Drawing.Point(22, 288);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(860, 24);
            this.lblCaption.TabIndex = 3;
            this.lblCaption.Text = "解锁倒计时";
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCaption.UseMnemonic = false;
            //
            // lblHint
            //
            this.lblHint.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblHint.Location = new System.Drawing.Point(22, 352);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(860, 20);
            this.lblHint.TabIndex = 4;
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHint.UseMnemonic = false;
            //
            // btnUnlock
            //
            this.btnUnlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.btnUnlock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUnlock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnUnlock.FlatAppearance.BorderSize = 1;
            this.btnUnlock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(142)))), ((int)(((byte)(60)))));
            this.btnUnlock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnUnlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnlock.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnlock.ForeColor = System.Drawing.Color.White;
            this.btnUnlock.Location = new System.Drawing.Point(342, 384);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(220, 42);
            this.btnUnlock.TabIndex = 5;
            this.btnUnlock.TabStop = false;
            this.btnUnlock.Text = "立即解锁，继续工作";
            this.btnUnlock.UseVisualStyleBackColor = false;
            this.btnUnlock.Click += new System.EventHandler(this.BtnUnlock_Click);
            //
            // RestFrm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(905, 486);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.lblText);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RestFrm";
            this.Opacity = 0.85D;
            this.ShowInTaskbar = false;
            this.Text = "RestFrm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RestFrm_FormClosing);
            this.Load += new System.EventHandler(this.RestFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerRst;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnUnlock;
    }
}
