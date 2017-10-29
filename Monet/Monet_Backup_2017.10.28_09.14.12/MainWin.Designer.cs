namespace Monet
{
    ///-------------------------------------------------------------------------------------------------
    /// \class MainWin
    ///
    /// \brief The application's main form.
    ///-------------------------------------------------------------------------------------------------

    partial class MainWin
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusImageLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelText1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutWhole = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1_Home = new System.Windows.Forms.TabPage();
            this.tableLayoutTabPage1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutTabPage1_3 = new System.Windows.Forms.TableLayoutPanel();
            this.lineButton = new System.Windows.Forms.Button();
            this.pointerButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2_View = new System.Windows.Forms.TabPage();
            this.mainView = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.colorBoxButton1 = new System.Windows.Forms.PictureBox();
            this.colorBoxButton2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutWhole.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1_Home.SuspendLayout();
            this.tableLayoutTabPage1.SuspendLayout();
            this.tableLayoutTabPage1_3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBoxButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBoxButton2)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.statusBarLabel1,
            this.statusImageLabel1,
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.statusLabelText1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 702);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1099, 25);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // statusBarLabel1
            // 
            this.statusBarLabel1.Name = "statusBarLabel1";
            this.statusBarLabel1.Size = new System.Drawing.Size(0, 20);
            // 
            // statusImageLabel1
            // 
            this.statusImageLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.statusImageLabel1.Name = "statusImageLabel1";
            this.statusImageLabel1.Size = new System.Drawing.Size(0, 20);
            this.statusImageLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel2.Image = global::Monet.Properties.Resources.cross;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(20, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // statusLabelText1
            // 
            this.statusLabelText1.Name = "statusLabelText1";
            this.statusLabelText1.Size = new System.Drawing.Size(144, 20);
            this.statusLabelText1.Text = "这里写鼠标当前位置";
            // 
            // tableLayoutWhole
            // 
            this.tableLayoutWhole.ColumnCount = 1;
            this.tableLayoutWhole.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutWhole.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutWhole.Controls.Add(this.mainView, 0, 1);
            this.tableLayoutWhole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutWhole.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutWhole.Name = "tableLayoutWhole";
            this.tableLayoutWhole.RowCount = 2;
            this.tableLayoutWhole.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutWhole.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutWhole.Size = new System.Drawing.Size(1099, 702);
            this.tableLayoutWhole.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tableLayoutWhole.SetColumnSpan(this.tabControl1, 2);
            this.tabControl1.Controls.Add(this.tabPage1_Home);
            this.tabControl1.Controls.Add(this.tabPage2_View);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1093, 144);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1_Home
            // 
            this.tabPage1_Home.Controls.Add(this.tableLayoutTabPage1);
            this.tabPage1_Home.Location = new System.Drawing.Point(4, 25);
            this.tabPage1_Home.Name = "tabPage1_Home";
            this.tabPage1_Home.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1_Home.Size = new System.Drawing.Size(1085, 115);
            this.tabPage1_Home.TabIndex = 0;
            this.tabPage1_Home.Text = "Home";
            this.tabPage1_Home.UseVisualStyleBackColor = true;
            // 
            // tableLayoutTabPage1
            // 
            this.tableLayoutTabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.tableLayoutTabPage1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutTabPage1.ColumnCount = 5;
            this.tableLayoutTabPage1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.9407F));
            this.tableLayoutTabPage1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.70632F));
            this.tableLayoutTabPage1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.14126F));
            this.tableLayoutTabPage1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.208302F));
            this.tableLayoutTabPage1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.84062F));
            this.tableLayoutTabPage1.Controls.Add(this.tableLayoutTabPage1_3, 2, 0);
            this.tableLayoutTabPage1.Controls.Add(this.tableLayoutPanel1, 4, 0);
            this.tableLayoutTabPage1.Controls.Add(this.label1, 2, 1);
            this.tableLayoutTabPage1.Controls.Add(this.label2, 3, 1);
            this.tableLayoutTabPage1.Controls.Add(this.label3, 4, 1);
            this.tableLayoutTabPage1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutTabPage1.Name = "tableLayoutTabPage1";
            this.tableLayoutTabPage1.RowCount = 2;
            this.tableLayoutTabPage1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutTabPage1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutTabPage1.Size = new System.Drawing.Size(1079, 116);
            this.tableLayoutTabPage1.TabIndex = 0;
            // 
            // tableLayoutTabPage1_3
            // 
            this.tableLayoutTabPage1_3.ColumnCount = 6;
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutTabPage1_3.Controls.Add(this.lineButton, 1, 0);
            this.tableLayoutTabPage1_3.Controls.Add(this.pointerButton, 0, 0);
            this.tableLayoutTabPage1_3.Location = new System.Drawing.Point(389, 6);
            this.tableLayoutTabPage1_3.Name = "tableLayoutTabPage1_3";
            this.tableLayoutTabPage1_3.RowCount = 2;
            this.tableLayoutTabPage1_3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutTabPage1_3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutTabPage1_3.Size = new System.Drawing.Size(239, 74);
            this.tableLayoutTabPage1_3.TabIndex = 2;
            // 
            // lineButton
            // 
            this.lineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineButton.AutoSize = true;
            this.lineButton.BackColor = System.Drawing.Color.Transparent;
            this.lineButton.BackgroundImage = global::Monet.Properties.Resources.line;
            this.lineButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.lineButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lineButton.FlatAppearance.BorderSize = 0;
            this.lineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lineButton.Location = new System.Drawing.Point(43, 3);
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(34, 34);
            this.lineButton.TabIndex = 0;
            this.lineButton.UseVisualStyleBackColor = false;
            this.lineButton.Click += new System.EventHandler(this.lineButton_Click);
            // 
            // pointerButton
            // 
            this.pointerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pointerButton.AutoSize = true;
            this.pointerButton.BackColor = System.Drawing.Color.Transparent;
            this.pointerButton.BackgroundImage = global::Monet.Properties.Resources.arrow;
            this.pointerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pointerButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pointerButton.FlatAppearance.BorderSize = 0;
            this.pointerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pointerButton.ForeColor = System.Drawing.Color.Black;
            this.pointerButton.Location = new System.Drawing.Point(3, 3);
            this.pointerButton.Name = "pointerButton";
            this.pointerButton.Size = new System.Drawing.Size(34, 34);
            this.pointerButton.TabIndex = 1;
            this.pointerButton.UseVisualStyleBackColor = false;
            this.pointerButton.Click += new System.EventHandler(this.pointerButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(705, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(366, 74);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(146)))), ((int)(((byte)(146)))));
            this.label1.Location = new System.Drawing.Point(389, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 27);
            this.label1.TabIndex = 4;
            this.label1.Text = "Shape";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(146)))), ((int)(((byte)(146)))));
            this.label2.Location = new System.Drawing.Point(637, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 27);
            this.label2.TabIndex = 5;
            this.label2.Text = "Width";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(146)))), ((int)(((byte)(146)))));
            this.label3.Location = new System.Drawing.Point(705, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(368, 27);
            this.label3.TabIndex = 6;
            this.label3.Text = "Color";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2_View
            // 
            this.tabPage2_View.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.tabPage2_View.Location = new System.Drawing.Point(4, 25);
            this.tabPage2_View.Name = "tabPage2_View";
            this.tabPage2_View.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2_View.Size = new System.Drawing.Size(1085, 115);
            this.tabPage2_View.TabIndex = 1;
            this.tabPage2_View.Text = "View";
            // 
            // mainView
            // 
            this.tableLayoutWhole.SetColumnSpan(this.mainView, 2);
            this.mainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainView.Location = new System.Drawing.Point(3, 153);
            this.mainView.Name = "mainView";
            this.mainView.Size = new System.Drawing.Size(1093, 546);
            this.mainView.TabIndex = 3;
            this.mainView.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.colorBoxButton1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(44, 68);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.colorBoxButton2, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(53, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(44, 68);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // colorBoxButton1
            // 
            this.colorBoxButton1.BackColor = System.Drawing.Color.Black;
            this.colorBoxButton1.Location = new System.Drawing.Point(3, 3);
            this.colorBoxButton1.Name = "colorBoxButton1";
            this.colorBoxButton1.Size = new System.Drawing.Size(40, 40);
            this.colorBoxButton1.TabIndex = 0;
            this.colorBoxButton1.TabStop = false;
            // 
            // colorBoxButton2
            // 
            this.colorBoxButton2.BackColor = System.Drawing.Color.White;
            this.colorBoxButton2.Location = new System.Drawing.Point(3, 3);
            this.colorBoxButton2.Name = "colorBoxButton2";
            this.colorBoxButton2.Size = new System.Drawing.Size(38, 40);
            this.colorBoxButton2.TabIndex = 1;
            this.colorBoxButton2.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "1";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "2";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1099, 727);
            this.Controls.Add(this.tableLayoutWhole);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainWin";
            this.Text = "Monet";
            this.Load += new System.EventHandler(this.MainWin_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWin_Paint);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutWhole.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1_Home.ResumeLayout(false);
            this.tableLayoutTabPage1.ResumeLayout(false);
            this.tableLayoutTabPage1.PerformLayout();
            this.tableLayoutTabPage1_3.ResumeLayout(false);
            this.tableLayoutTabPage1_3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBoxButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBoxButton2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusImageLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelText1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.PictureBox mainView;
        private System.Windows.Forms.Button lineButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutWhole;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1_Home;
        private System.Windows.Forms.TabPage tabPage2_View;
        private System.Windows.Forms.TableLayoutPanel tableLayoutTabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutTabPage1_3;
        private System.Windows.Forms.Button pointerButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox colorBoxButton2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox colorBoxButton1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}