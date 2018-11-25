namespace AllersProject
{
    partial class CustomerPredictionPane
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textCodigosProductos = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Pre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GananciaMinimaAdicional = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GananciaMaximaAdicional = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Relevancia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Confiabilidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filtrar prediciones por mayor";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "",
            "Confianza",
            "Relevancia",
            "Ingreso minimo",
            "Ingreso maximo",
            "Cantidad minima",
            "Cantidad maxima"});
            this.comboBox1.Location = new System.Drawing.Point(213, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textCodigosProductos
            // 
            this.textCodigosProductos.Location = new System.Drawing.Point(404, 10);
            this.textCodigosProductos.Name = "textCodigosProductos";
            this.textCodigosProductos.Size = new System.Drawing.Size(232, 20);
            this.textCodigosProductos.TabIndex = 3;
            this.textCodigosProductos.Text = "Separate item codes by spaces";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(642, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Specific predictions";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pre,
            this.Pos,
            this.GananciaMinimaAdicional,
            this.GananciaMaximaAdicional,
            this.Relevancia,
            this.Confiabilidad});
            this.dataGridView1.Location = new System.Drawing.Point(3, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(750, 366);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Pre
            // 
            this.Pre.HeaderText = "Si compra:";
            this.Pre.Name = "Pre";
            this.Pre.ReadOnly = true;
            this.Pre.Width = 120;
            // 
            // Pos
            // 
            this.Pos.HeaderText = "Puede comprar:";
            this.Pos.Name = "Pos";
            this.Pos.ReadOnly = true;
            this.Pos.Width = 120;
            // 
            // GananciaMinimaAdicional
            // 
            this.GananciaMinimaAdicional.HeaderText = "GananciaMinimaAdicional";
            this.GananciaMinimaAdicional.MinimumWidth = 7;
            this.GananciaMinimaAdicional.Name = "GananciaMinimaAdicional";
            this.GananciaMinimaAdicional.ReadOnly = true;
            this.GananciaMinimaAdicional.Width = 135;
            // 
            // GananciaMaximaAdicional
            // 
            this.GananciaMaximaAdicional.HeaderText = "GananciaMaximaAdicional";
            this.GananciaMaximaAdicional.Name = "GananciaMaximaAdicional";
            this.GananciaMaximaAdicional.ReadOnly = true;
            this.GananciaMaximaAdicional.Width = 135;
            // 
            // Relevancia
            // 
            this.Relevancia.HeaderText = "Relevancia";
            this.Relevancia.Name = "Relevancia";
            this.Relevancia.ReadOnly = true;
            // 
            // Confiabilidad
            // 
            this.Confiabilidad.HeaderText = "Confiabilidad";
            this.Confiabilidad.Name = "Confiabilidad";
            this.Confiabilidad.ReadOnly = true;
            // 
            // CustomerPredictionPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textCodigosProductos);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Name = "CustomerPredictionPane";
            this.Size = new System.Drawing.Size(767, 405);
            this.Load += new System.EventHandler(this.CustomerPredictionPane_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textCodigosProductos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn GananciaMinimaAdicional;
        private System.Windows.Forms.DataGridViewTextBoxColumn GananciaMaximaAdicional;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relevancia;
        private System.Windows.Forms.DataGridViewTextBoxColumn Confiabilidad;
    }
}
