namespace OrderSortingBy50.Frm
{
    partial class BoardInit
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.txtSeq = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSlaveComfirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtServiceIP = new System.Windows.Forms.TextBox();
            this.btnServiceIPComfirm = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNetGateIP = new System.Windows.Forms.TextBox();
            this.btnNetGateComfirm = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.Msg = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCOM = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // txtSeq
            // 
            this.txtSeq.Location = new System.Drawing.Point(62, 93);
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.Size = new System.Drawing.Size(87, 21);
            this.txtSeq.TabIndex = 0;
            this.txtSeq.Text = "01";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置从机地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "序号：";
            // 
            // btnSlaveComfirm
            // 
            this.btnSlaveComfirm.Location = new System.Drawing.Point(187, 90);
            this.btnSlaveComfirm.Name = "btnSlaveComfirm";
            this.btnSlaveComfirm.Size = new System.Drawing.Size(75, 23);
            this.btnSlaveComfirm.TabIndex = 3;
            this.btnSlaveComfirm.Text = "确定";
            this.btnSlaveComfirm.UseVisualStyleBackColor = true;
            this.btnSlaveComfirm.Click += new System.EventHandler(this.btnSlaveComfirm_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "设置IP地址";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(-8, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1000, 2);
            this.label4.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(-28, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1000, 2);
            this.label5.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(-20, 222);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1000, 2);
            this.label6.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "IP：";
            // 
            // txtServiceIP
            // 
            this.txtServiceIP.Location = new System.Drawing.Point(64, 174);
            this.txtServiceIP.Name = "txtServiceIP";
            this.txtServiceIP.Size = new System.Drawing.Size(87, 21);
            this.txtServiceIP.TabIndex = 9;
            this.txtServiceIP.Text = "192.168.0.101";
            // 
            // btnServiceIPComfirm
            // 
            this.btnServiceIPComfirm.Location = new System.Drawing.Point(187, 174);
            this.btnServiceIPComfirm.Name = "btnServiceIPComfirm";
            this.btnServiceIPComfirm.Size = new System.Drawing.Size(75, 23);
            this.btnServiceIPComfirm.TabIndex = 10;
            this.btnServiceIPComfirm.Text = "确定";
            this.btnServiceIPComfirm.UseVisualStyleBackColor = true;
            this.btnServiceIPComfirm.Click += new System.EventHandler(this.btnServiceIPComfirm_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(14, 242);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 14);
            this.label8.TabIndex = 11;
            this.label8.Text = "设置网关地址";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 274);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "IP：";
            // 
            // txtNetGateIP
            // 
            this.txtNetGateIP.Location = new System.Drawing.Point(62, 271);
            this.txtNetGateIP.Name = "txtNetGateIP";
            this.txtNetGateIP.Size = new System.Drawing.Size(87, 21);
            this.txtNetGateIP.TabIndex = 13;
            this.txtNetGateIP.Text = "192.168.0.1";
            // 
            // btnNetGateComfirm
            // 
            this.btnNetGateComfirm.Location = new System.Drawing.Point(187, 269);
            this.btnNetGateComfirm.Name = "btnNetGateComfirm";
            this.btnNetGateComfirm.Size = new System.Drawing.Size(75, 23);
            this.btnNetGateComfirm.TabIndex = 14;
            this.btnNetGateComfirm.Text = "确定";
            this.btnNetGateComfirm.UseVisualStyleBackColor = true;
            this.btnNetGateComfirm.Click += new System.EventHandler(this.btnNetGateComfirm_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(12, 349);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 14);
            this.label10.TabIndex = 15;
            this.label10.Text = "传输状态";
            // 
            // Msg
            // 
            this.Msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Msg.Location = new System.Drawing.Point(15, 380);
            this.Msg.Multiline = true;
            this.Msg.Name = "Msg";
            this.Msg.Size = new System.Drawing.Size(304, 153);
            this.Msg.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(12, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 14);
            this.label11.TabIndex = 17;
            this.label11.Text = "COM：";
            // 
            // txtCOM
            // 
            this.txtCOM.Location = new System.Drawing.Point(64, 12);
            this.txtCOM.Name = "txtCOM";
            this.txtCOM.Size = new System.Drawing.Size(87, 21);
            this.txtCOM.TabIndex = 18;
            this.txtCOM.Text = "COM1";
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Location = new System.Drawing.Point(-335, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(1000, 2);
            this.label12.TabIndex = 19;
            // 
            // btnCom
            // 
            this.btnCom.Location = new System.Drawing.Point(187, 10);
            this.btnCom.Name = "btnCom";
            this.btnCom.Size = new System.Drawing.Size(75, 23);
            this.btnCom.TabIndex = 20;
            this.btnCom.Text = "连接";
            this.btnCom.UseVisualStyleBackColor = true;
            this.btnCom.Click += new System.EventHandler(this.btnCom_Click);
            // 
            // BoardInit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 565);
            this.Controls.Add(this.btnCom);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCOM);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Msg);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnNetGateComfirm);
            this.Controls.Add(this.txtNetGateIP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnServiceIPComfirm);
            this.Controls.Add(this.txtServiceIP);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSlaveComfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSeq);
            this.Name = "BoardInit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "下层初始化";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox txtSeq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSlaveComfirm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtServiceIP;
        private System.Windows.Forms.Button btnServiceIPComfirm;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNetGateIP;
        private System.Windows.Forms.Button btnNetGateComfirm;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Msg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCOM;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnCom;
    }
}