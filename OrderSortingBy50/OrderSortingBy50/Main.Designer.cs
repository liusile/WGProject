namespace OrderSortingBy50
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.StarMenu = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolWaveCancel = new System.Windows.Forms.ToolStripButton();
            this.ToolWavePowerComplete = new System.Windows.Forms.ToolStripButton();
            this.ToolSysManage = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolSysSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolWaveApiSync = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolLocalSocketService = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolCheckHardware = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBoardUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBoardInit = new System.Windows.Forms.ToolStripMenuItem();
            this.CurUserMenu = new System.Windows.Forms.ToolStripSeparator();
            this.ddlUser = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolLoginOut = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCurUser = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolSortingDetail = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.serialPort_Scan = new System.IO.Ports.SerialPort(this.components);
            this.toolStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip2
            // 
            this.toolStrip2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StarMenu,
            this.toolStripSeparator1,
            this.ToolWaveCancel,
            this.ToolWavePowerComplete,
            this.ToolSysManage,
            this.CurUserMenu,
            this.ddlUser,
            this.lblCurUser,
            this.toolStripLabel1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1360, 30);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // StarMenu
            // 
            this.StarMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StarMenu.Enabled = false;
            this.StarMenu.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StarMenu.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.StarMenu.Image = ((System.Drawing.Image)(resources.GetObject("StarMenu.Image")));
            this.StarMenu.ImageTransparentColor = System.Drawing.Color.MediumSpringGreen;
            this.StarMenu.Name = "StarMenu";
            this.StarMenu.Size = new System.Drawing.Size(97, 27);
            this.StarMenu.Text = "开始分拣";
            this.StarMenu.Click += new System.EventHandler(this.StarMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // ToolWaveCancel
            // 
            this.ToolWaveCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolWaveCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolWaveCancel.ForeColor = System.Drawing.Color.Black;
            this.ToolWaveCancel.Image = ((System.Drawing.Image)(resources.GetObject("ToolWaveCancel.Image")));
            this.ToolWaveCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolWaveCancel.Name = "ToolWaveCancel";
            this.ToolWaveCancel.Size = new System.Drawing.Size(97, 27);
            this.ToolWaveCancel.Text = "分播中断";
            this.ToolWaveCancel.ToolTipText = "分拣记录";
            this.ToolWaveCancel.Click += new System.EventHandler(this.ToolWaveCancel_Click);
            // 
            // ToolWavePowerComplete
            // 
            this.ToolWavePowerComplete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolWavePowerComplete.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolWavePowerComplete.ForeColor = System.Drawing.Color.Black;
            this.ToolWavePowerComplete.Image = ((System.Drawing.Image)(resources.GetObject("ToolWavePowerComplete.Image")));
            this.ToolWavePowerComplete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolWavePowerComplete.Name = "ToolWavePowerComplete";
            this.ToolWavePowerComplete.Size = new System.Drawing.Size(97, 27);
            this.ToolWavePowerComplete.Text = "强制完成";
            this.ToolWavePowerComplete.ToolTipText = "分拣记录";
            this.ToolWavePowerComplete.Click += new System.EventHandler(this.ToolWavePowerComplete_Click);
            // 
            // ToolSysManage
            // 
            this.ToolSysManage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolSysManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolSysSetting,
            this.ToolWaveApiSync,
            this.toolStripMenuItem1,
            this.ToolLocalSocketService,
            this.ToolCheckHardware,
            this.ToolBoardUpdate,
            this.ToolBoardInit});
            this.ToolSysManage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ToolSysManage.ForeColor = System.Drawing.Color.Black;
            this.ToolSysManage.Image = ((System.Drawing.Image)(resources.GetObject("ToolSysManage.Image")));
            this.ToolSysManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolSysManage.Name = "ToolSysManage";
            this.ToolSysManage.Size = new System.Drawing.Size(107, 27);
            this.ToolSysManage.Text = "系统管理";
            this.ToolSysManage.ToolTipText = "分拣记录";
            this.ToolSysManage.Visible = false;
            this.ToolSysManage.Click += new System.EventHandler(this.ToolSortLog_Click);
            // 
            // ToolSysSetting
            // 
            this.ToolSysSetting.Name = "ToolSysSetting";
            this.ToolSysSetting.Size = new System.Drawing.Size(210, 26);
            this.ToolSysSetting.Text = "系统设置";
            this.ToolSysSetting.Click += new System.EventHandler(this.ToolSysSetting_Click);
            // 
            // ToolWaveApiSync
            // 
            this.ToolWaveApiSync.Name = "ToolWaveApiSync";
            this.ToolWaveApiSync.Size = new System.Drawing.Size(210, 26);
            this.ToolWaveApiSync.Text = "分拣查询";
            this.ToolWaveApiSync.Click += new System.EventHandler(this.ToolWaveApiSync_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // ToolLocalSocketService
            // 
            this.ToolLocalSocketService.Name = "ToolLocalSocketService";
            this.ToolLocalSocketService.Size = new System.Drawing.Size(210, 26);
            this.ToolLocalSocketService.Text = "本地服务器";
            this.ToolLocalSocketService.Click += new System.EventHandler(this.ToolLocalSocketService_Click);
            // 
            // ToolCheckHardware
            // 
            this.ToolCheckHardware.Name = "ToolCheckHardware";
            this.ToolCheckHardware.Size = new System.Drawing.Size(210, 26);
            this.ToolCheckHardware.Text = "硬件检测";
            this.ToolCheckHardware.Click += new System.EventHandler(this.ToolCheckHardware_Click);
            // 
            // ToolBoardUpdate
            // 
            this.ToolBoardUpdate.Name = "ToolBoardUpdate";
            this.ToolBoardUpdate.Size = new System.Drawing.Size(210, 26);
            this.ToolBoardUpdate.Text = "下位机升级";
            this.ToolBoardUpdate.Click += new System.EventHandler(this.ToolBoardUpdate_Click);
            // 
            // ToolBoardInit
            // 
            this.ToolBoardInit.Name = "ToolBoardInit";
            this.ToolBoardInit.Size = new System.Drawing.Size(210, 26);
            this.ToolBoardInit.Text = "下位机初始化";
            this.ToolBoardInit.Click += new System.EventHandler(this.ToolBoardInit_Click);
            // 
            // CurUserMenu
            // 
            this.CurUserMenu.Name = "CurUserMenu";
            this.CurUserMenu.Size = new System.Drawing.Size(6, 30);
            // 
            // ddlUser
            // 
            this.ddlUser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ddlUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddlUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolLoginOut});
            this.ddlUser.Image = ((System.Drawing.Image)(resources.GetObject("ddlUser.Image")));
            this.ddlUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddlUser.Name = "ddlUser";
            this.ddlUser.Size = new System.Drawing.Size(34, 27);
            // 
            // ToolLoginOut
            // 
            this.ToolLoginOut.Name = "ToolLoginOut";
            this.ToolLoginOut.Size = new System.Drawing.Size(130, 32);
            this.ToolLoginOut.Text = "注销";
            this.ToolLoginOut.Click += new System.EventHandler(this.ToolLoginOut_Click);
            // 
            // lblCurUser
            // 
            this.lblCurUser.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblCurUser.Name = "lblCurUser";
            this.lblCurUser.Size = new System.Drawing.Size(72, 27);
            this.lblCurUser.Text = "未登录";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(98, 27);
            this.toolStripLabel1.Text = "当前用户：";
            // 
            // txtInput
            // 
            this.txtInput.Enabled = false;
            this.txtInput.Location = new System.Drawing.Point(64, 51);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(224, 25);
            this.txtInput.TabIndex = 2;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(4, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "单号：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.ToolSortingDetail});
            this.statusStrip1.Location = new System.Drawing.Point(0, 829);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1360, 25);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(142, 20);
            this.toolStripStatusLabel1.Text = "当前版本：V1.0.0.1";
            // 
            // ToolSortingDetail
            // 
            this.ToolSortingDetail.Name = "ToolSortingDetail";
            this.ToolSortingDetail.Size = new System.Drawing.Size(69, 20);
            this.ToolSortingDetail.Text = "分拣明细";
            this.ToolSortingDetail.Click += new System.EventHandler(this.ToolSortingDetail_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMsg.Location = new System.Drawing.Point(312, 61);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 15);
            this.lblMsg.TabIndex = 5;
            // 
            // serialPort_Scan
            // 
            this.serialPort_Scan.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_Scan_DataReceived);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 854);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.toolStrip2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分拨墙";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton StarMenu;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator CurUserMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton ddlUser;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lblCurUser;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ToolStripButton ToolWavePowerComplete;
        private System.Windows.Forms.ToolStripButton ToolWaveCancel;
        private System.Windows.Forms.ToolStripStatusLabel ToolSortingDetail;
        private System.Windows.Forms.ToolStripDropDownButton ToolSysManage;
        private System.Windows.Forms.ToolStripMenuItem ToolWaveApiSync;
        private System.Windows.Forms.ToolStripMenuItem ToolLocalSocketService;
        private System.Windows.Forms.ToolStripMenuItem ToolSysSetting;
        private System.Windows.Forms.ToolStripMenuItem ToolBoardUpdate;
        private System.Windows.Forms.ToolStripMenuItem ToolCheckHardware;
        private System.Windows.Forms.ToolStripMenuItem ToolBoardInit;
        private System.Windows.Forms.ToolStripMenuItem ToolLoginOut;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.IO.Ports.SerialPort serialPort_Scan;
    }
}

