using Modelo;
using Modelo.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllersProject
{
    public partial class CustomerInfoForm : Form
    {
        public CustomerInfoForm(Recommendation re)
        {
            InitializeComponent();
            Customer cus = re.customer;
            label1.Text = "Codigo Cliente: " + cus.id;
            label2.Text = "Nombre del grupo: " + cus.groupName;
            label3.Text = "Ciudad: " + cus.cityName;
            label4.Text = "Region: "+cus.regionName;
            int control = re.groupColor;
            label5.ForeColor = Color.FromArgb(((control * 7) % 129) + 100, ((control * 101) % 129) + 100, ((control * 300) % 129) + 100);
            string recom = "";
            recom += "Podria comprar mas de:\n";
            for (int j = 0; j < re.recommendations.Count; j++)
            {
                recom += re.recommendations[j].Item1.itemName + " " + "ya que compra " + re.recommendations[j].Item2 + "unidades menos que el promedio de su grupo\n";
            }
            recommendationsPane1.setRecommendations(recom);
        }

        private void CustomerInfoForm_Load(object sender, EventArgs e)
        {

        }
    }
}
