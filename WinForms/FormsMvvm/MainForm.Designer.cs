
namespace FormsMvvm
{
	partial class MainForm
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this._driveLabel = new System.Windows.Forms.Label();
			this._pathLabel = new System.Windows.Forms.Label();
			this._driveTextBox = new System.Windows.Forms.TextBox();
			this._pathTextBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this._dataGridView = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// _driveLabel
			// 
			this._driveLabel.AutoSize = true;
			this._driveLabel.Location = new System.Drawing.Point(35, 39);
			this._driveLabel.Name = "_driveLabel";
			this._driveLabel.Size = new System.Drawing.Size(32, 12);
			this._driveLabel.TabIndex = 1;
			this._driveLabel.Text = "Drive";
			// 
			// _pathLabel
			// 
			this._pathLabel.AutoSize = true;
			this._pathLabel.Location = new System.Drawing.Point(35, 72);
			this._pathLabel.Name = "_pathLabel";
			this._pathLabel.Size = new System.Drawing.Size(28, 12);
			this._pathLabel.TabIndex = 2;
			this._pathLabel.Text = "Path";
			// 
			// _driveTextBox
			// 
			this._driveTextBox.Location = new System.Drawing.Point(111, 36);
			this._driveTextBox.Name = "_driveTextBox";
			this._driveTextBox.Size = new System.Drawing.Size(100, 19);
			this._driveTextBox.TabIndex = 3;
			// 
			// _pathTextBox
			// 
			this._pathTextBox.Location = new System.Drawing.Point(111, 69);
			this._pathTextBox.Name = "_pathTextBox";
			this._pathTextBox.Size = new System.Drawing.Size(100, 19);
			this._pathTextBox.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(37, 410);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// _dataGridView
			// 
			this._dataGridView.AllowUserToAddRows = false;
			this._dataGridView.AllowUserToDeleteRows = false;
			this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dataGridView.Location = new System.Drawing.Point(37, 139);
			this._dataGridView.MultiSelect = false;
			this._dataGridView.Name = "_dataGridView";
			this._dataGridView.ReadOnly = true;
			this._dataGridView.RowTemplate.Height = 21;
			this._dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._dataGridView.Size = new System.Drawing.Size(360, 239);
			this._dataGridView.TabIndex = 6;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(558, 550);
			this.Controls.Add(this._dataGridView);
			this.Controls.Add(this.button1);
			this.Controls.Add(this._pathTextBox);
			this.Controls.Add(this._driveTextBox);
			this.Controls.Add(this._pathLabel);
			this.Controls.Add(this._driveLabel);
			this.Name = "MainForm";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label _driveLabel;
		private System.Windows.Forms.Label _pathLabel;
		private System.Windows.Forms.TextBox _driveTextBox;
		private System.Windows.Forms.TextBox _pathTextBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView _dataGridView;
	}
}

