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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gBoxAbout = new System.Windows.Forms.GroupBox();
            this.labAssociationRule = new System.Windows.Forms.Label();
            this.customerPredictionPane1 = new AllersProject.CustomerPredictionPane();
            this.menuPane1 = new AllersProject.MenuPane();
            this.customerPane1 = new AllersProject.CustomerPane();
            this.recommendationsPane1 = new AllersProject.RecommendationsPane();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.menu.SuspendLayout();
            this.clientes.SuspendLayout();
            this.gruposClientes.SuspendLayout();
            this.ayuda.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gBoxAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.menu);
            this.tabControl1.Controls.Add(this.clientes);
            this.tabControl1.Controls.Add(this.gruposClientes);
            this.tabControl1.Controls.Add(this.ayuda);
            this.tabControl1.Location = new System.Drawing.Point(22, 22);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1505, 1170);
            this.tabControl1.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menu.Controls.Add(this.customerPredictionPane1);
            this.menu.Controls.Add(this.menuPane1);
            this.menu.Location = new System.Drawing.Point(4, 36);
            this.menu.Margin = new System.Windows.Forms.Padding(6);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(6);
            this.menu.Size = new System.Drawing.Size(1497, 1130);
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
            this.clientes.Location = new System.Drawing.Point(4, 36);
            this.clientes.Margin = new System.Windows.Forms.Padding(6);
            this.clientes.Name = "clientes";
            this.clientes.Padding = new System.Windows.Forms.Padding(6);
            this.clientes.Size = new System.Drawing.Size(1497, 1130);
            this.clientes.TabIndex = 1;
            this.clientes.Text = "Clientes relevantes";
            this.clientes.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Location = new System.Drawing.Point(6, 1100);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 15, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1483, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gruposClientes
            // 
            this.gruposClientes.AutoScroll = true;
            this.gruposClientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gruposClientes.Controls.Add(this.recommendationsPane1);
            this.gruposClientes.Controls.Add(this.zedGraphControl1);
            this.gruposClientes.Location = new System.Drawing.Point(4, 36);
            this.gruposClientes.Margin = new System.Windows.Forms.Padding(6);
            this.gruposClientes.Name = "gruposClientes";
            this.gruposClientes.Size = new System.Drawing.Size(1497, 1130);
            this.gruposClientes.TabIndex = 2;
            this.gruposClientes.Text = "Grupos de clientes";
            this.gruposClientes.UseVisualStyleBackColor = true;
            this.gruposClientes.Click += new System.EventHandler(this.gruposClientes_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(9, 10);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(11);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(1445, 799);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            this.zedGraphControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zedGraphControl1_MouseClick);
            // 
            // ayuda
            // 
            this.ayuda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ayuda.Controls.Add(this.groupBox2);
            this.ayuda.Controls.Add(this.gBoxAbout);
            this.ayuda.Location = new System.Drawing.Point(4, 36);
            this.ayuda.Margin = new System.Windows.Forms.Padding(6);
            this.ayuda.Name = "ayuda";
            this.ayuda.Size = new System.Drawing.Size(1497, 1130);
            this.ayuda.TabIndex = 3;
            this.ayuda.Text = "Ayuda";
            this.ayuda.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(3, 450);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1490, 283);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Acerca de";
            // 
            // gBoxAbout
            // 
            this.gBoxAbout.Controls.Add(this.label6);
            this.gBoxAbout.Controls.Add(this.label4);
            this.gBoxAbout.Controls.Add(this.label3);
            this.gBoxAbout.Controls.Add(this.label2);
            this.gBoxAbout.Controls.Add(this.label1);
            this.gBoxAbout.Controls.Add(this.labAssociationRule);
            this.gBoxAbout.Location = new System.Drawing.Point(3, 3);
            this.gBoxAbout.Name = "gBoxAbout";
            this.gBoxAbout.Size = new System.Drawing.Size(1489, 441);
            this.gBoxAbout.TabIndex = 0;
            this.gBoxAbout.TabStop = false;
            this.gBoxAbout.Text = "Cómo funciona?";
            // 
            // labAssociationRule
            // 
            this.labAssociationRule.Location = new System.Drawing.Point(6, 25);
            this.labAssociationRule.Name = "labAssociationRule";
            this.labAssociationRule.Size = new System.Drawing.Size(1477, 56);
            this.labAssociationRule.TabIndex = 0;
            this.labAssociationRule.Text = resources.GetString("labAssociationRule.Text");
            this.labAssociationRule.Click += new System.EventHandler(this.label1_Click);
            // 
            // customerPredictionPane1
            // 
            this.customerPredictionPane1.AutoScroll = true;
            this.customerPredictionPane1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.customerPredictionPane1.customerId = null;
            this.customerPredictionPane1.Location = new System.Drawing.Point(48, 301);
            this.customerPredictionPane1.main = null;
            this.customerPredictionPane1.Margin = new System.Windows.Forms.Padding(11);
            this.customerPredictionPane1.minConf = 0D;
            this.customerPredictionPane1.minSup = 0D;
            this.customerPredictionPane1.Name = "customerPredictionPane1";
            this.customerPredictionPane1.Size = new System.Drawing.Size(1403, 776);
            this.customerPredictionPane1.TabIndex = 3;
            this.customerPredictionPane1.tipoPanel = '\0';
            // 
            // menuPane1
            // 
            this.menuPane1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menuPane1.Location = new System.Drawing.Point(218, 11);
            this.menuPane1.Margin = new System.Windows.Forms.Padding(11);
            this.menuPane1.Name = "menuPane1";
            this.menuPane1.Size = new System.Drawing.Size(955, 272);
            this.menuPane1.TabIndex = 2;
            // 
            // customerPane1
            // 
            this.customerPane1.Location = new System.Drawing.Point(9, 11);
            this.customerPane1.Margin = new System.Windows.Forms.Padding(11);
            this.customerPane1.Name = "customerPane1";
            this.customerPane1.Size = new System.Drawing.Size(1467, 1091);
            this.customerPane1.TabIndex = 0;
            // 
            // recommendationsPane1
            // 
            this.recommendationsPane1.Location = new System.Drawing.Point(9, 820);
            this.recommendationsPane1.Margin = new System.Windows.Forms.Padding(11);
            this.recommendationsPane1.Name = "recommendationsPane1";
            this.recommendationsPane1.Size = new System.Drawing.Size(1445, 517);
            this.recommendationsPane1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1477, 56);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1477, 56);
            this.label2.TabIndex = 2;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1477, 56);
            this.label3.TabIndex = 3;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1477, 80);
            this.label4.TabIndex = 4;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1477, 107);
            this.label5.TabIndex = 5;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1477, 56);
            this.label6.TabIndex = 5;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1591, 1201);
            this.Controls.Add(this.tabControl1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Allers";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.clientes.ResumeLayout(false);
            this.clientes.PerformLayout();
            this.gruposClientes.ResumeLayout(false);
            this.ayuda.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gBoxAbout.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gBoxAbout;
        private System.Windows.Forms.Label labAssociationRule;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

