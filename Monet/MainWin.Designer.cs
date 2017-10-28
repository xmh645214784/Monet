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
            this.statusLabelText1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutWhole = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1_Home = new System.Windows.Forms.TabPage();
            this.tabPage2_View = new System.Windows.Forms.TabPage();
            this.tableLayoutTabPage1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutTabPage1_3 = new System.Windows.Forms.TableLayoutPanel();
            this.mainView = new System.Windows.Forms.PictureBox();
            this.lineButton = new System.Windows.Forms.Button();
            this.pointerButton = new System.Windows.Forms.Button();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutWhole.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1_Home.SuspendLayout();
            this.tableLayoutTabPage1.SuspendLayout();
            this.tableLayoutTabPage1_3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainView)).BeginInit();
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
            this.tableLayoutWhole.Controls.Add(this.mainView, 1, 1);
            this.tableLayoutWhole.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutWhole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutWhole.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutWhole.Name = "tableLayoutWhole";
            this.tableLayoutWhole.RowCount = 2;
            this.tableLayoutWhole.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
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
            this.tabControl1.Size = new System.Drawing.Size(1093, 94);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1_Home
            // 
            this.tabPage1_Home.Controls.Add(this.tableLayoutTabPage1);
            this.tabPage1_Home.Location = new System.Drawing.Point(4, 25);
            this.tabPage1_Home.Name = "tabPage1_Home";
            this.tabPage1_Home.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1_Home.Size = new System.Drawing.Size(1085, 65);
            this.tabPage1_Home.TabIndex = 0;
            this.tabPage1_Home.Text = "Home";
            this.tabPage1_Home.UseVisualStyleBackColor = true;
            // 
            // tabPage2_View
            // 
            this.tabPage2_View.Location = new System.Drawing.Point(4, 25);
            this.tabPage2_View.Name = "tabPage2_View";
            this.tabPage2_View.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2_View.Size = new System.Drawing.Size(1085, 65);
            this.tabPage2_View.TabIndex = 1;
            this.tabPage2_View.Text = "View";
            this.tabPage2_View.UseVisualStyleBackColor = true;
            // 
            // tableLayoutTabPage1
            // 
            this.tableLayoutTabPage1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tableLayoutTabPage1.ColumnCount = 4;
            this.tableLayoutTabPage1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutTabPage1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutTabPage1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.42751F));
            this.tableLayoutTabPage1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.75837F));
            this.tableLayoutTabPage1.Controls.Add(this.tableLayoutTabPage1_3, 2, 0);
            this.tableLayoutTabPage1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutTabPage1.Name = "tableLayoutTabPage1";
            this.tableLayoutTabPage1.RowCount = 1;
            this.tableLayoutTabPage1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutTabPage1.Size = new System.Drawing.Size(1079, 60);
            this.tableLayoutTabPage1.TabIndex = 0;
            // 
            // tableLayoutTabPage1_3
            // 
            this.tableLayoutTabPage1_3.ColumnCount = 5;
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutTabPage1_3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutTabPage1_3.Controls.Add(this.lineButton, 1, 0);
            this.tableLayoutTabPage1_3.Controls.Add(this.pointerButton, 0, 0);
            this.tableLayoutTabPage1_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutTabPage1_3.Location = new System.Drawing.Point(542, 6);
            this.tableLayoutTabPage1_3.Name = "tableLayoutTabPage1_3";
            this.tableLayoutTabPage1_3.RowCount = 2;
            this.tableLayoutTabPage1_3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutTabPage1_3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutTabPage1_3.Size = new System.Drawing.Size(157, 48);
            this.tableLayoutTabPage1_3.TabIndex = 2;
            // 
            // mainView
            // 
            this.tableLayoutWhole.SetColumnSpan(this.mainView, 2);
            this.mainView.Location = new System.Drawing.Point(3, 103);
            this.mainView.Name = "mainView";
            this.mainView.Size = new System.Drawing.Size(1093, 578);
            this.mainView.TabIndex = 3;
            this.mainView.TabStop = false;
            // 
            // lineButton
            // 
            this.lineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineButton.AutoSize = true;
            this.lineButton.BackColor = System.Drawing.SystemColors.Window;
            this.lineButton.BackgroundImage = global::Monet.Properties.Resources.line;
            this.lineButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.lineButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lineButton.FlatAppearance.BorderSize = 0;
            this.lineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lineButton.Location = new System.Drawing.Point(35, 3);
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(26, 17);
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
            this.pointerButton.BackColor = System.Drawing.SystemColors.Window;
            this.pointerButton.BackgroundImage = global::Monet.Properties.Resources.arrow;
            this.pointerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pointerButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pointerButton.FlatAppearance.BorderSize = 0;
            this.pointerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pointerButton.Location = new System.Drawing.Point(3, 3);
            this.pointerButton.Name = "pointerButton";
            this.pointerButton.Size = new System.Drawing.Size(26, 17);
            this.pointerButton.TabIndex = 1;
            this.pointerButton.UseVisualStyleBackColor = false;
            this.pointerButton.Click += new System.EventHandler(this.pointerButton_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel2.Image = global::Monet.Properties.Resources.cross;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(20, 20);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
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
            this.Text = "MainWin";
            this.Load += new System.EventHandler(this.MainWin_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWin_Paint);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutWhole.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1_Home.ResumeLayout(false);
            this.tableLayoutTabPage1.ResumeLayout(false);
            this.tableLayoutTabPage1_3.ResumeLayout(false);
            this.tableLayoutTabPage1_3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainView)).EndInit();
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
    }
}