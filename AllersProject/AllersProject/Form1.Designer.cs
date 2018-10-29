﻿using System;
using Modelo.services;

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
            this.gruposClientes = new System.Windows.Forms.TabPage();
            this.ayuda = new System.Windows.Forms.TabPage();
            this.customerPredictionPane1 = new AllersProject.CustomerPredictionPane();
            this.menuPane1 = new AllersProject.MenuPane();
            this.customerPane1 = new AllersProject.CustomerPane();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
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
            this.tabControl1.Size = new System.Drawing.Size(776, 426);
            this.tabControl1.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menu.Controls.Add(this.customerPredictionPane1);
            this.menu.Controls.Add(this.menuPane1);
            this.menu.Location = new System.Drawing.Point(4, 25);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.menu.Size = new System.Drawing.Size(768, 397);
            this.menu.TabIndex = 0;
            this.menu.Text = "Menu";
            this.menu.UseVisualStyleBackColor = true;
            this.menu.Click += new System.EventHandler(this.menu_Click);
            // 
            // clientes
            // 
            this.clientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clientes.Controls.Add(this.customerPane1);
            this.clientes.Location = new System.Drawing.Point(4, 25);
            this.clientes.Name = "clientes";
            this.clientes.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.clientes.Size = new System.Drawing.Size(768, 397);
            this.clientes.TabIndex = 1;
            this.clientes.Text = "Clientes";
            this.clientes.UseVisualStyleBackColor = true;
            // 
            // gruposClientes
            // 
            this.gruposClientes.AutoScroll = true;
            this.gruposClientes.Controls.Add(this.zedGraphControl1);
            this.gruposClientes.Location = new System.Drawing.Point(4, 25);
            this.gruposClientes.Name = "gruposClientes";
            this.gruposClientes.Size = new System.Drawing.Size(768, 397);
            this.gruposClientes.TabIndex = 2;
            this.gruposClientes.Text = "Grupos de clientes";
            this.gruposClientes.UseVisualStyleBackColor = true;
            this.gruposClientes.Click += new System.EventHandler(this.gruposClientes_Click);
            // 
            // ayuda
            // 
            this.ayuda.Location = new System.Drawing.Point(4, 25);
            this.ayuda.Name = "ayuda";
            this.ayuda.Size = new System.Drawing.Size(768, 397);
            this.ayuda.TabIndex = 3;
            this.ayuda.Text = "Ayuda";
            this.ayuda.UseVisualStyleBackColor = true;
            // 
            // customerPredictionPane1
            // 
            this.customerPredictionPane1.AutoScroll = true;
            this.customerPredictionPane1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.customerPredictionPane1.Location = new System.Drawing.Point(6, 160);
            this.customerPredictionPane1.Margin = new System.Windows.Forms.Padding(6);
            this.customerPredictionPane1.Name = "customerPredictionPane1";
            this.customerPredictionPane1.Size = new System.Drawing.Size(756, 231);
            this.customerPredictionPane1.TabIndex = 1;
            this.customerPredictionPane1.Load += new System.EventHandler(this.customerPredictionPane1_Load);
            // 
            // menuPane1
            // 
            this.menuPane1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menuPane1.Location = new System.Drawing.Point(218, 6);
            this.menuPane1.Margin = new System.Windows.Forms.Padding(6);
            this.menuPane1.Name = "menuPane1";
            this.menuPane1.Size = new System.Drawing.Size(365, 148);
            this.menuPane1.TabIndex = 0;
            this.menuPane1.Load += new System.EventHandler(this.menuPane1_Load);
            // 
            // customerPane1
            // 
            this.customerPane1.Location = new System.Drawing.Point(3, 6);
            this.customerPane1.Margin = new System.Windows.Forms.Padding(6);
            this.customerPane1.Name = "customerPane1";
            this.customerPane1.Size = new System.Drawing.Size(758, 357);
            this.customerPane1.TabIndex = 0;
            this.customerPane1.Load += new System.EventHandler(this.customerPane1_Load_3);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 3);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(478, 433);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 443);
            this.Controls.Add(this.tabControl1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Allers";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.clientes.ResumeLayout(false);
            this.gruposClientes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage menu;
        private System.Windows.Forms.TabPage clientes;
        private System.Windows.Forms.TabPage gruposClientes;
        private System.Windows.Forms.TabPage ayuda;
        private CustomerPane customerPane1;
        private MenuPane menuPane1;
        private CustomerPredictionPane customerPredictionPane1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
    }
}

