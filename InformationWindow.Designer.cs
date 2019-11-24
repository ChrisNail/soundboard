namespace Soundboard
{
    partial class InformationWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.deviceGrid = new System.Windows.Forms.DataGridView();
            this.indexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceGroup = new System.Windows.Forms.GroupBox();
            this.optionGroup = new System.Windows.Forms.GroupBox();
            this.fileLabel = new System.Windows.Forms.Label();
            this.deviceLabel = new System.Windows.Forms.Label();
            this.volumeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.deviceGrid)).BeginInit();
            this.deviceGroup.SuspendLayout();
            this.optionGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceGrid
            // 
            this.deviceGrid.AllowUserToAddRows = false;
            this.deviceGrid.AllowUserToDeleteRows = false;
            this.deviceGrid.AllowUserToResizeRows = false;
            this.deviceGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.deviceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.deviceGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexColumn,
            this.nameColumn});
            this.deviceGrid.Location = new System.Drawing.Point(6, 19);
            this.deviceGrid.Name = "deviceGrid";
            this.deviceGrid.ReadOnly = true;
            this.deviceGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.deviceGrid.Size = new System.Drawing.Size(464, 175);
            this.deviceGrid.TabIndex = 0;
            // 
            // indexColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.indexColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.indexColumn.FillWeight = 1F;
            this.indexColumn.HeaderText = "Index";
            this.indexColumn.Name = "indexColumn";
            this.indexColumn.ReadOnly = true;
            this.indexColumn.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            // 
            // nameColumn
            // 
            this.nameColumn.FillWeight = 8F;
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            // 
            // deviceGroup
            // 
            this.deviceGroup.Controls.Add(this.deviceGrid);
            this.deviceGroup.Location = new System.Drawing.Point(12, 12);
            this.deviceGroup.Name = "deviceGroup";
            this.deviceGroup.Size = new System.Drawing.Size(476, 200);
            this.deviceGroup.TabIndex = 1;
            this.deviceGroup.TabStop = false;
            this.deviceGroup.Text = "Devices";
            // 
            // optionGroup
            // 
            this.optionGroup.Controls.Add(this.volumeLabel);
            this.optionGroup.Controls.Add(this.deviceLabel);
            this.optionGroup.Controls.Add(this.fileLabel);
            this.optionGroup.Location = new System.Drawing.Point(12, 218);
            this.optionGroup.Name = "optionGroup";
            this.optionGroup.Size = new System.Drawing.Size(476, 68);
            this.optionGroup.TabIndex = 2;
            this.optionGroup.TabStop = false;
            this.optionGroup.Text = "Options";
            // 
            // label3
            // 
            this.volumeLabel.AutoSize = true;
            this.volumeLabel.Location = new System.Drawing.Point(6, 42);
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.Size = new System.Drawing.Size(94, 13);
            this.volumeLabel.TabIndex = 2;
            this.volumeLabel.Text = "-v : Output volume";
            // 
            // label2
            // 
            this.deviceLabel.AutoSize = true;
            this.deviceLabel.Location = new System.Drawing.Point(6, 29);
            this.deviceLabel.Name = "deviceLabel";
            this.deviceLabel.Size = new System.Drawing.Size(228, 13);
            this.deviceLabel.TabIndex = 1;
            this.deviceLabel.Text = "-d : Output device numbers (comma-separated)";
            // 
            // label1
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Location = new System.Drawing.Point(6, 16);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(158, 13);
            this.fileLabel.TabIndex = 0;
            this.fileLabel.Text = "-f : Audio file path. (REQUIRED)";
            // 
            // InformationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(500, 298);
            this.Controls.Add(this.optionGroup);
            this.Controls.Add(this.deviceGroup);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "InformationWindow";
            this.Text = "Soundboard";
            ((System.ComponentModel.ISupportInitialize)(this.deviceGrid)).EndInit();
            this.deviceGroup.ResumeLayout(false);
            this.optionGroup.ResumeLayout(false);
            this.optionGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView deviceGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.GroupBox deviceGroup;
        private System.Windows.Forms.GroupBox optionGroup;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.Label volumeLabel;
        private System.Windows.Forms.Label deviceLabel;
    }
}