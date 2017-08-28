namespace OrderSortingBy50
{
    partial class LatticeConfig
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
            this.btnPut = new System.Windows.Forms.Button();
            this.txtPutNum = new System.Windows.Forms.TextBox();
            this.btnBlock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRemoveBlockError = new System.Windows.Forms.Button();
            this.btnPrintSingle = new System.Windows.Forms.Button();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.btnLatticeDetail = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(144, 16);
            this.btnPut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(100, 71);
            this.btnPut.TabIndex = 0;
            this.btnPut.Text = "投递";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // txtPutNum
            // 
            this.txtPutNum.Location = new System.Drawing.Point(66, 62);
            this.txtPutNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPutNum.Name = "txtPutNum";
            this.txtPutNum.Size = new System.Drawing.Size(71, 25);
            this.txtPutNum.TabIndex = 1;
            // 
            // btnBlock
            // 
            this.btnBlock.Location = new System.Drawing.Point(16, 131);
            this.btnBlock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBlock.Name = "btnBlock";
            this.btnBlock.Size = new System.Drawing.Size(100, 70);
            this.btnBlock.TabIndex = 2;
            this.btnBlock.Text = "阻挡";
            this.btnBlock.UseVisualStyleBackColor = true;
            this.btnBlock.Click += new System.EventHandler(this.btnBlock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "数量：";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(1, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(309, 2);
            this.label2.TabIndex = 53;
            this.label2.Text = "______________________________________________________";
            // 
            // btnRemoveBlockError
            // 
            this.btnRemoveBlockError.Location = new System.Drawing.Point(144, 131);
            this.btnRemoveBlockError.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemoveBlockError.Name = "btnRemoveBlockError";
            this.btnRemoveBlockError.Size = new System.Drawing.Size(100, 70);
            this.btnRemoveBlockError.TabIndex = 56;
            this.btnRemoveBlockError.Text = "解除阻挡";
            this.btnRemoveBlockError.UseVisualStyleBackColor = true;
            this.btnRemoveBlockError.Click += new System.EventHandler(this.btnRemoveBlockError_Click);
            // 
            // btnPrintSingle
            // 
            this.btnPrintSingle.Location = new System.Drawing.Point(16, 224);
            this.btnPrintSingle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrintSingle.Name = "btnPrintSingle";
            this.btnPrintSingle.Size = new System.Drawing.Size(100, 70);
            this.btnPrintSingle.TabIndex = 57;
            this.btnPrintSingle.Text = "单个打印";
            this.btnPrintSingle.UseVisualStyleBackColor = true;
            this.btnPrintSingle.Click += new System.EventHandler(this.btnPrintSingle_Click);
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Location = new System.Drawing.Point(144, 224);
            this.btnPrintAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(100, 70);
            this.btnPrintAll.TabIndex = 58;
            this.btnPrintAll.Text = "全部打印";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // btnLatticeDetail
            // 
            this.btnLatticeDetail.Location = new System.Drawing.Point(16, 301);
            this.btnLatticeDetail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLatticeDetail.Name = "btnLatticeDetail";
            this.btnLatticeDetail.Size = new System.Drawing.Size(228, 70);
            this.btnLatticeDetail.TabIndex = 59;
            this.btnLatticeDetail.Text = "格口详细信息";
            this.btnLatticeDetail.UseVisualStyleBackColor = true;
            this.btnLatticeDetail.Click += new System.EventHandler(this.btnLatticeDetail_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 60;
            this.label3.Text = "类型：";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(65, 24);
            this.txtType.Margin = new System.Windows.Forms.Padding(4);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(71, 25);
            this.txtType.TabIndex = 61;
            // 
            // LatticeConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 402);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLatticeDetail);
            this.Controls.Add(this.btnPrintAll);
            this.Controls.Add(this.btnPrintSingle);
            this.Controls.Add(this.btnRemoveBlockError);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBlock);
            this.Controls.Add(this.txtPutNum);
            this.Controls.Add(this.btnPut);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LatticeConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "格口设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPut;
        private System.Windows.Forms.TextBox txtPutNum;
        private System.Windows.Forms.Button btnBlock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRemoveBlockError;
        private System.Windows.Forms.Button btnPrintSingle;
        private System.Windows.Forms.Button btnPrintAll;
        private System.Windows.Forms.Button btnLatticeDetail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtType;
    }
}