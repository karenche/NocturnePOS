namespace CardHelper
{
    partial class CardEmulatorForm
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
            this.btnInsertIdCard = new System.Windows.Forms.Button();
            this.btnInsertRfidCard = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbRegCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRfid = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnWithoutRole = new System.Windows.Forms.Button();
            this.btnAminAndWorkerRole = new System.Windows.Forms.Button();
            this.btnAdminRole = new System.Windows.Forms.Button();
            this.btnWorkerRole = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInsertIdCard
            // 
            this.btnInsertIdCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInsertIdCard.Location = new System.Drawing.Point(239, 81);
            this.btnInsertIdCard.Name = "btnInsertIdCard";
            this.btnInsertIdCard.Size = new System.Drawing.Size(230, 32);
            this.btnInsertIdCard.TabIndex = 0;
            this.btnInsertIdCard.Text = "Emulate ID-card";
            this.btnInsertIdCard.UseVisualStyleBackColor = true;
            this.btnInsertIdCard.Click += new System.EventHandler(this.btnInsertIdCard_Click);
            // 
            // btnInsertRfidCard
            // 
            this.btnInsertRfidCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInsertRfidCard.Location = new System.Drawing.Point(239, 29);
            this.btnInsertRfidCard.Name = "btnInsertRfidCard";
            this.btnInsertRfidCard.Size = new System.Drawing.Size(230, 32);
            this.btnInsertRfidCard.TabIndex = 0;
            this.btnInsertRfidCard.Text = "Emulate RFID card";
            this.btnInsertRfidCard.UseVisualStyleBackColor = true;
            this.btnInsertRfidCard.Click += new System.EventHandler(this.btnInsertRfidCard_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tbRegCode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnInsertIdCard, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbLastName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbFirstName, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 64);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(472, 114);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tbRegCode
            // 
            this.tbRegCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRegCode.Location = new System.Drawing.Point(239, 3);
            this.tbRegCode.Name = "tbRegCode";
            this.tbRegCode.Size = new System.Drawing.Size(230, 20);
            this.tbRegCode.TabIndex = 7;
            this.tbRegCode.Text = "49010124259";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reg. code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "First name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Last name";
            // 
            // tbLastName
            // 
            this.tbLastName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLastName.Location = new System.Drawing.Point(239, 55);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(230, 20);
            this.tbLastName.TabIndex = 5;
            this.tbLastName.Text = "Jameson";
            // 
            // tbFirstName
            // 
            this.tbFirstName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFirstName.Location = new System.Drawing.Point(239, 29);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(230, 20);
            this.tbFirstName.TabIndex = 6;
            this.tbFirstName.Text = "Jenna";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnInsertRfidCard, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tbRfid, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(472, 64);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Rfid UID";
            // 
            // tbRfid
            // 
            this.tbRfid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRfid.Location = new System.Drawing.Point(239, 3);
            this.tbRfid.Name = "tbRfid";
            this.tbRfid.Size = new System.Drawing.Size(230, 20);
            this.tbRfid.TabIndex = 4;
            this.tbRfid.Text = "1020304050607";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.btnWithoutRole, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnAminAndWorkerRole, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnAdminRole, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnWorkerRole, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 178);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(472, 77);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnWithoutRole
            // 
            this.btnWithoutRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWithoutRole.Location = new System.Drawing.Point(239, 41);
            this.btnWithoutRole.Name = "btnWithoutRole";
            this.btnWithoutRole.Size = new System.Drawing.Size(230, 33);
            this.btnWithoutRole.TabIndex = 1;
            this.btnWithoutRole.Text = "Login (Without role)";
            this.btnWithoutRole.UseVisualStyleBackColor = true;
            this.btnWithoutRole.Click += new System.EventHandler(this.btnWithoutRole_Click);
            // 
            // btnAminAndWorkerRole
            // 
            this.btnAminAndWorkerRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAminAndWorkerRole.Location = new System.Drawing.Point(239, 3);
            this.btnAminAndWorkerRole.Name = "btnAminAndWorkerRole";
            this.btnAminAndWorkerRole.Size = new System.Drawing.Size(230, 32);
            this.btnAminAndWorkerRole.TabIndex = 1;
            this.btnAminAndWorkerRole.Text = "Login (Admin and Worker)";
            this.btnAminAndWorkerRole.UseVisualStyleBackColor = true;
            this.btnAminAndWorkerRole.Click += new System.EventHandler(this.btnAminAndWorkerRole_Click);
            // 
            // btnAdminRole
            // 
            this.btnAdminRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdminRole.Location = new System.Drawing.Point(3, 3);
            this.btnAdminRole.Name = "btnAdminRole";
            this.btnAdminRole.Size = new System.Drawing.Size(230, 32);
            this.btnAdminRole.TabIndex = 1;
            this.btnAdminRole.Text = "Login (Admin)";
            this.btnAdminRole.UseVisualStyleBackColor = true;
            this.btnAdminRole.Click += new System.EventHandler(this.btnAdminRole_Click);
            // 
            // btnWorkerRole
            // 
            this.btnWorkerRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWorkerRole.Location = new System.Drawing.Point(3, 41);
            this.btnWorkerRole.Name = "btnWorkerRole";
            this.btnWorkerRole.Size = new System.Drawing.Size(230, 33);
            this.btnWorkerRole.TabIndex = 2;
            this.btnWorkerRole.Text = "Login (Worker)";
            this.btnWorkerRole.UseVisualStyleBackColor = true;
            this.btnWorkerRole.Click += new System.EventHandler(this.btnWorkerRole_Click);
            // 
            // CardEmulatorForm
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(472, 255);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CardEmulatorForm";
            this.Text = "Card emulator for testing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardEmulatorForm_FormClosing);
            this.Load += new System.EventHandler(this.CardEmulatorForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsertIdCard;
        private System.Windows.Forms.Button btnInsertRfidCard;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbRegCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRfid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnAdminRole;
        private System.Windows.Forms.Button btnWithoutRole;
        private System.Windows.Forms.Button btnAminAndWorkerRole;
        private System.Windows.Forms.Button btnWorkerRole;
    }
}