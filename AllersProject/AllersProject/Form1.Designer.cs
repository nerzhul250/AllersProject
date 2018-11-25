namespace AllersProject
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menu = new System.Windows.Forms.TabPage();
            this.clientes = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gruposClientes = new System.Windows.Forms.TabPage();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.ayuda = new System.Windows.Forms.TabPage();
            this.customerPredictionPane1 = new AllersProject.CustomerPredictionPane();
            this.menuPane1 = new AllersProject.MenuPane();
            this.customerPane1 = new AllersProject.CustomerPane();
            this.recommendationsPane1 = new AllersProject.RecommendationsPane();
            this.tabControl1.SuspendLayout();
            this.menu.SuspendLayout();
            this.clientes.SuspendLayout();
            this.gruposClientes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.menu);
            this.tabControl1.Controls.Add(this.clientes);
            this.tabControl1.Controls.Add(this.gruposClientes);
            this.tabControl1.Controls.Add(this.ayuda);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(821, 634);
            this.tabControl1.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menu.Controls.Add(this.customerPredictionPane1);
            this.menu.Controls.Add(this.menuPane1);
            this.menu.Location = new System.Drawing.Point(4, 25);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(3);
            this.menu.Size = new System.Drawing.Size(813, 605);
            this.menu.TabIndex = 0;
            this.menu.Text = "Menu";
            this.menu.UseVisualStyleBackColor = true;
            this.menu.Click += new System.EventHandler(this.menu_Click);
            // 
            // clientes
            // 
            this.clientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clientes.Controls.Add(this.statusStrip1);
            this.clientes.Controls.Add(this.customerPane1);
            this.clientes.Location = new System.Drawing.Point(4, 25);
            this.clientes.Name = "clientes";
            this.clientes.Padding = new System.Windows.Forms.Padding(3);
            this.clientes.Size = new System.Drawing.Size(813, 605);
            this.clientes.TabIndex = 1;
            this.clientes.Text = "Clientes";
            this.clientes.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Location = new System.Drawing.Point(3, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 8, 0);
            this.statusStrip1.Size = new System.Drawing.Size(805, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gruposClientes
            // 
            this.gruposClientes.AutoScroll = true;
            this.gruposClientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gruposClientes.Controls.Add(this.recommendationsPane1);
            this.gruposClientes.Controls.Add(this.zedGraphControl1);
            this.gruposClientes.Location = new System.Drawing.Point(4, 25);
            this.gruposClientes.Name = "gruposClientes";
            this.gruposClientes.Size = new System.Drawing.Size(813, 605);
            this.gruposClientes.TabIndex = 2;
            this.gruposClientes.Text = "Grupos de clientes";
            this.gruposClientes.UseVisualStyleBackColor = true;
            this.gruposClientes.Click += new System.EventHandler(this.gruposClientes_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 3);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(6);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(788, 433);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            this.zedGraphControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl1_MouseClick);
            // 
            // ayuda
            // 
            this.ayuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ayuda.Location = new System.Drawing.Point(4, 25);
            this.ayuda.Name = "ayuda";
            this.ayuda.Size = new System.Drawing.Size(813, 605);
            this.ayuda.TabIndex = 3;
            this.ayuda.Text = "Ayuda";
            this.ayuda.UseVisualStyleBackColor = true;
            // 
            // customerPredictionPane1
            // 
            this.customerPredictionPane1.AutoScroll = true;
            this.customerPredictionPane1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.customerPredictionPane1.customerId = null;
            this.customerPredictionPane1.Location = new System.Drawing.Point(26, 163);
            this.customerPredictionPane1.main = null;
            this.customerPredictionPane1.minConf = 0D;
            this.customerPredictionPane1.minSup = 0D;
            this.customerPredictionPane1.Name = "customerPredictionPane1";
            this.customerPredictionPane1.Size = new System.Drawing.Size(767, 422);
            this.customerPredictionPane1.TabIndex = 3;
            this.customerPredictionPane1.tipoPanel = '\0';
            this.customerPredictionPane1.Load += new System.EventHandler(this.customerPredictionPane1_Load_1);
            // 
            // menuPane1
            // 
            this.menuPane1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menuPane1.Location = new System.Drawing.Point(119, 6);
            this.menuPane1.Margin = new System.Windows.Forms.Padding(6);
            this.menuPane1.Name = "menuPane1";
            this.menuPane1.Size = new System.Drawing.Size(522, 148);
            this.menuPane1.TabIndex = 2;
            // 
            // customerPane1
            // 
            this.customerPane1.Location = new System.Drawing.Point(5, 6);
            this.customerPane1.Margin = new System.Windows.Forms.Padding(6);
            this.customerPane1.Name = "customerPane1";
            this.customerPane1.Size = new System.Drawing.Size(800, 591);
            this.customerPane1.TabIndex = 0;
            // 
            // recommendationsPane1
            // 
            this.recommendationsPane1.Location = new System.Drawing.Point(3, 442);
            this.recommendationsPane1.Margin = new System.Windows.Forms.Padding(6);
            this.recommendationsPane1.Name = "recommendationsPane1";
            this.recommendationsPane1.Size = new System.Drawing.Size(788, 280);
            this.recommendationsPane1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 635);
            this.Controls.Add(this.tabControl1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Allers";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.clientes.ResumeLayout(false);
            this.clientes.PerformLayout();
            this.gruposClientes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage menu;
        private System.Windows.Forms.TabPage clientes;
        private System.Windows.Forms.TabPage gruposClientes;
        private System.Windows.Forms.TabPage ayuda;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private MenuPane menuPane1;
        private CustomerPane customerPane1;
        private RecommendationsPane recommendationsPane1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private CustomerPredictionPane customerPredictionPane1;
    }
}

