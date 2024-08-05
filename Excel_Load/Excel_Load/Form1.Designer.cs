namespace Excel_Load
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ExcelButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MSSQLbutton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ExcelButton2 = new System.Windows.Forms.Button();
            this.MSSQLbutton2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExcelButton
            // 
            this.ExcelButton.Font = new System.Drawing.Font("細明體-ExtB", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ExcelButton.Location = new System.Drawing.Point(85, 79);
            this.ExcelButton.Name = "ExcelButton";
            this.ExcelButton.Size = new System.Drawing.Size(385, 114);
            this.ExcelButton.TabIndex = 0;
            this.ExcelButton.Text = "讀取Excel";
            this.ExcelButton.UseVisualStyleBackColor = true;
            this.ExcelButton.Click += new System.EventHandler(this.ExcelButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(23, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 102;
            this.dataGridView1.RowTemplate.Height = 45;
            this.dataGridView1.Size = new System.Drawing.Size(2076, 1044);
            this.dataGridView1.TabIndex = 1;
            // 
            // MSSQLbutton
            // 
            this.MSSQLbutton.Font = new System.Drawing.Font("微軟正黑體", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MSSQLbutton.Location = new System.Drawing.Point(85, 233);
            this.MSSQLbutton.Name = "MSSQLbutton";
            this.MSSQLbutton.Size = new System.Drawing.Size(385, 114);
            this.MSSQLbutton.TabIndex = 2;
            this.MSSQLbutton.Text = "存回MSSQL";
            this.MSSQLbutton.UseVisualStyleBackColor = true;
            this.MSSQLbutton.Click += new System.EventHandler(this.MSSQLbutton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ExcelButton);
            this.groupBox1.Controls.Add(this.MSSQLbutton);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(2141, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 408);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "發票主檔";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ExcelButton2);
            this.groupBox2.Controls.Add(this.MSSQLbutton2);
            this.groupBox2.Font = new System.Drawing.Font("微軟正黑體", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(2141, 549);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(516, 408);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "發票明細";
            // 
            // ExcelButton2
            // 
            this.ExcelButton2.Font = new System.Drawing.Font("細明體-ExtB", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ExcelButton2.Location = new System.Drawing.Point(85, 79);
            this.ExcelButton2.Name = "ExcelButton2";
            this.ExcelButton2.Size = new System.Drawing.Size(385, 114);
            this.ExcelButton2.TabIndex = 0;
            this.ExcelButton2.Text = "讀取Excel";
            this.ExcelButton2.UseVisualStyleBackColor = true;
            this.ExcelButton2.Click += new System.EventHandler(this.ExcelButton2_Click);
            // 
            // MSSQLbutton2
            // 
            this.MSSQLbutton2.Font = new System.Drawing.Font("微軟正黑體", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MSSQLbutton2.Location = new System.Drawing.Point(85, 233);
            this.MSSQLbutton2.Name = "MSSQLbutton2";
            this.MSSQLbutton2.Size = new System.Drawing.Size(385, 114);
            this.MSSQLbutton2.TabIndex = 2;
            this.MSSQLbutton2.Text = "存回MSSQL";
            this.MSSQLbutton2.UseVisualStyleBackColor = true;
            this.MSSQLbutton2.Click += new System.EventHandler(this.MSSQLbutton2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2669, 1111);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ExcelButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button MSSQLbutton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ExcelButton2;
        private System.Windows.Forms.Button MSSQLbutton2;
    }
}

