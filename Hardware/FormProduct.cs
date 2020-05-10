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
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
            ShowProduct();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Title = textBoxTitle.Text;
            product.Part_Number = textBoxPart_Number.Text;
            product.Description = richTextBoxDescription.Text;
            product.Cost = Convert.ToInt32(textBoxCost.Text);
            Program.magazinEntities.Product.Add(product);
            Program.magazinEntities.SaveChanges();
            ShowProduct();
        }
        void ShowProduct()
        {
            listViewProduct.Items.Clear();
            foreach(Product product in Program.magazinEntities.Product)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    product.ID.ToString(),
                    product.Title,
                    product.Part_Number,
                    product.Description,
                    product.Cost.ToString()
                });
                item.Tag = product;
                listViewProduct.Items.Add(item);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if(listViewProduct.SelectedItems.Count == 1)
            {
                Product product = listViewProduct.SelectedItems[0].Tag as Product;
                product.Title = textBoxTitle.Text;
                product.Part_Number = textBoxPart_Number.Text;
                product.Description = richTextBoxDescription.Text;
                product.Cost = Convert.ToInt32(textBoxCost.Text);
                Program.magazinEntities.SaveChanges();
                ShowProduct();
            }
        }

        private void listViewProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProduct.SelectedItems.Count == 1)
            {
                Product product = listViewProduct.SelectedItems[0].Tag as Product;
                textBoxTitle.Text = product.Title;
                textBoxPart_Number.Text = product.Part_Number;
                richTextBoxDescription.Text = product.Description;
                textBoxCost.Text = product.Cost.ToString();
            }
            else
            {
                textBoxTitle.Text = "";
                textBoxPart_Number.Text = "";
                richTextBoxDescription.Text = "";
                textBoxCost.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewProduct.SelectedItems.Count == 1)
                {
                    Product product = listViewProduct.SelectedItems[0].Tag as Product;
                    Program.magazinEntities.Product.Remove(product);
                    Program.magazinEntities.SaveChanges();
                    ShowProduct();
                }
                textBoxTitle.Text = "";
                textBoxPart_Number.Text = "";
                richTextBoxDescription.Text = "";
                textBoxCost.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
