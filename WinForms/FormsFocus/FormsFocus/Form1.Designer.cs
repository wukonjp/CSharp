using System.Windows.Forms;

namespace FormsFocus
{
	partial class Form1
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
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.panel4 = new System.Windows.Forms.Panel();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.activeControlCaptionLabel = new System.Windows.Forms.Label();
			this.activeControlNameLabel = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.unselectButton1 = new FormsFocus.UnselectButton();
			this.unselectButton2 = new FormsFocus.UnselectButton();
			this.unselectCheckBox1 = new FormsFocus.UnselectCheckBox();
			this.unselectCheckBox2 = new FormsFocus.UnselectCheckBox();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(376, 156);
			this.panel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(376, 156);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// button1
			// 
			this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
			this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.button1.Location = new System.Drawing.Point(81, 63);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.TabStop = false;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = false;
			// 
			// button2
			// 
			this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
			this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(198, 63);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.TabStop = false;
			this.button2.Text = "button2";
			this.button2.UseVisualStyleBackColor = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.groupBox2);
			this.panel2.Location = new System.Drawing.Point(394, 12);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(376, 156);
			this.panel2.TabIndex = 1;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBox1);
			this.groupBox2.Controls.Add(this.checkBox2);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(376, 156);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "groupBox2";
			// 
			// checkBox1
			// 
			this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox1.Location = new System.Drawing.Point(81, 63);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(71, 22);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.TabStop = false;
			this.checkBox1.Text = "checkBox1";
			this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox1.UseVisualStyleBackColor = false;
			// 
			// checkBox2
			// 
			this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBox2.Location = new System.Drawing.Point(198, 63);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(71, 22);
			this.checkBox2.TabIndex = 3;
			this.checkBox2.TabStop = false;
			this.checkBox2.Text = "checkBox2";
			this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox2.UseVisualStyleBackColor = false;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.groupBox3);
			this.panel3.Location = new System.Drawing.Point(12, 174);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(376, 156);
			this.panel3.TabIndex = 2;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.unselectButton1);
			this.groupBox3.Controls.Add(this.unselectButton2);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(376, 156);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "groupBox3";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.groupBox4);
			this.panel4.Location = new System.Drawing.Point(394, 174);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(376, 156);
			this.panel4.TabIndex = 3;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.unselectCheckBox1);
			this.groupBox4.Controls.Add(this.unselectCheckBox2);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Location = new System.Drawing.Point(0, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(376, 156);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "groupBox4";
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// activeControlCaptionLabel
			// 
			this.activeControlCaptionLabel.AutoSize = true;
			this.activeControlCaptionLabel.Dock = System.Windows.Forms.DockStyle.Left;
			this.activeControlCaptionLabel.Location = new System.Drawing.Point(0, 0);
			this.activeControlCaptionLabel.Name = "activeControlCaptionLabel";
			this.activeControlCaptionLabel.Size = new System.Drawing.Size(77, 12);
			this.activeControlCaptionLabel.TabIndex = 4;
			this.activeControlCaptionLabel.Text = "ActiveControl:";
			// 
			// activeControlNameLabel
			// 
			this.activeControlNameLabel.AutoSize = true;
			this.activeControlNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.activeControlNameLabel.Location = new System.Drawing.Point(77, 0);
			this.activeControlNameLabel.Name = "activeControlNameLabel";
			this.activeControlNameLabel.Size = new System.Drawing.Size(23, 12);
			this.activeControlNameLabel.TabIndex = 5;
			this.activeControlNameLabel.Text = "null";
			// 
			// panel5
			// 
			this.panel5.AutoSize = true;
			this.panel5.Controls.Add(this.activeControlNameLabel);
			this.panel5.Controls.Add(this.activeControlCaptionLabel);
			this.panel5.Location = new System.Drawing.Point(12, 338);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(376, 33);
			this.panel5.TabIndex = 6;
			// 
			// unselectButton1
			// 
			this.unselectButton1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.unselectButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
			this.unselectButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.unselectButton1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.unselectButton1.Location = new System.Drawing.Point(81, 63);
			this.unselectButton1.Name = "unselectButton1";
			this.unselectButton1.Size = new System.Drawing.Size(75, 33);
			this.unselectButton1.TabIndex = 1;
			this.unselectButton1.TabStop = false;
			this.unselectButton1.Text = "unselect\r\nButton1";
			this.unselectButton1.UseVisualStyleBackColor = false;
			// 
			// unselectButton2
			// 
			this.unselectButton2.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.unselectButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
			this.unselectButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			this.unselectButton2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.unselectButton2.Location = new System.Drawing.Point(198, 63);
			this.unselectButton2.Name = "unselectButton2";
			this.unselectButton2.Size = new System.Drawing.Size(75, 33);
			this.unselectButton2.TabIndex = 0;
			this.unselectButton2.TabStop = false;
			this.unselectButton2.Text = "unselect\r\nButton2";
			this.unselectButton2.UseVisualStyleBackColor = false;
			// 
			// unselectCheckBox1
			// 
			this.unselectCheckBox1.Appearance = System.Windows.Forms.Appearance.Button;
			this.unselectCheckBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.unselectCheckBox1.Location = new System.Drawing.Point(81, 63);
			this.unselectCheckBox1.Name = "unselectCheckBox1";
			this.unselectCheckBox1.Size = new System.Drawing.Size(73, 34);
			this.unselectCheckBox1.TabIndex = 3;
			this.unselectCheckBox1.TabStop = false;
			this.unselectCheckBox1.Text = "unselect\r\nCheckBox1";
			this.unselectCheckBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.unselectCheckBox1.UseVisualStyleBackColor = false;
			// 
			// unselectCheckBox2
			// 
			this.unselectCheckBox2.Appearance = System.Windows.Forms.Appearance.Button;
			this.unselectCheckBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.unselectCheckBox2.Location = new System.Drawing.Point(198, 62);
			this.unselectCheckBox2.Name = "unselectCheckBox2";
			this.unselectCheckBox2.Size = new System.Drawing.Size(73, 34);
			this.unselectCheckBox2.TabIndex = 2;
			this.unselectCheckBox2.TabStop = false;
			this.unselectCheckBox2.Text = "unselect\r\nCheckBox2";
			this.unselectCheckBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.unselectCheckBox2.UseVisualStyleBackColor = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.panel5);
			this.Name = "Form1";
			this.Text = "Form1";
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBox2;
		private Button button2;
		private Button button1;
		private CheckBox checkBox2;
		private CheckBox checkBox1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.GroupBox groupBox3;
		private UnselectButton unselectButton1;
		private UnselectButton unselectButton2;
		private Panel panel4;
		private GroupBox groupBox4;
		private UnselectCheckBox unselectCheckBox1;
		private UnselectCheckBox unselectCheckBox2;
		private Timer timer1;
		private Label activeControlCaptionLabel;
		private Label activeControlNameLabel;
		private Panel panel5;
	}
}

