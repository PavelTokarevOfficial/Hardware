using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            if (Authorization.users.type != "admin") buttonAddStaff.Enabled =false;
        }

        private void buttonAddStaff_Click(object sender, EventArgs e)
        {
            FormStaff formStaff = new FormStaff();
            formStaff.Show();
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            FormProduct formProduct = new FormProduct();
            formProduct.Show();
        }

        private void buttonSoldProduct_Click(object sender, EventArgs e)
        {
            FormSoldProduct formSoldProduct = new FormSoldProduct();
            formSoldProduct.Show();
        }

        
    }
}
