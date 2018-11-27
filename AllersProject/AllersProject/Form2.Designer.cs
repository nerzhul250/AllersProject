namespace AllersProject
{
    partial class Form2
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
            this.advancedFlowLayoutPanel1 = new MakarovDev.ExpandCollapsePanel.AdvancedFlowLayoutPanel();
            this.txtClientInfo = new System.Windows.Forms.RichTextBox();
            this.customerPredictionPane1 = new AllersProject.CustomerPredictionPane();
            this.advancedFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // advancedFlowLayoutPanel1
            // 
            this.advancedFlowLayoutPanel1.AutoScroll = true;
            this.advancedFlowLayoutPanel1.Controls.Add(this.txtClientInfo);
            this.advancedFlowLayoutPanel1.Controls.Add(this.customerPredictionPane1);
            this.advancedFlowLayoutPanel1.Location = new System.Drawing.Point(12, 35);
            this.advancedFlowLayoutPanel1.Name = "advancedFlowLayoutPanel1";
            this.advancedFlowLayoutPanel1.Size = new System.Drawing.Size(873, 448);
            this.advancedFlowLayoutPanel1.TabIndex = 0;
            // 
            // txtClientInfo
            // 
            this.txtClientInfo.Location = new System.Drawing.Point(3, 3);
            this.txtClientInfo.Name = "txtClientInfo";
            this.txtClientInfo.ReadOnly = true;
            this.txtClientInfo.Size = new System.Drawing.Size(850, 96);
            this.txtClientInfo.TabIndex = 0;
            this.txtClientInfo.Text = "";
            this.txtClientInfo.TextChanged += new System.EventHandler(this.txtClientInfo_TextChanged);
            // 
            // customerPredictionPane1
            // 
            this.customerPredictionPane1.AutoScroll = true;
            this.customerPredictionPane1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.customerPredictionPane1.customerId = null;
            this.customerPredictionPane1.Location = new System.Drawing.Point(6, 108);
            this.customerPredictionPane1.main = null;
            this.customerPredictionPane1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.customerPredictionPane1.minConf = 0D;
            this.customerPredictionPane1.minSup = 0D;
            this.customerPredictionPane1.Name = "customerPredictionPane1";
            this.customerPredictionPane1.Size = new System.Drawing.Size(844, 384);
            this.customerPredictionPane1.TabIndex = 1;
            this.customerPredictionPane1.tipoPanel = '\0';
            this.customerPredictionPane1.Load += new System.EventHandler(this.customerPredictionPane1_Load);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 567);
            this.Controls.Add(this.advancedFlowLayoutPanel1);
            this.Name = "Form2";
            this.Text = "Cliente buscado/seleccionado";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.advancedFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MakarovDev.ExpandCollapsePanel.AdvancedFlowLayoutPanel advancedFlowLayoutPanel1;
        public System.Windows.Forms.RichTextBox txtClientInfo;
        public CustomerPredictionPane customerPredictionPane1;
    }
}