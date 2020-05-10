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
    public partial class FormSoldProduct : Form
    {
        public FormSoldProduct()
        {
            InitializeComponent();
            ShowTitle();
            ShowStaff();
            ShowSoldProduct();
        }

        void ShowTitle()
        {
            comboBoxProduct.Items.Clear();
            foreach(Product product in Program.magazinEntities.Product)
            {
                string[] item =
                {
                    product.ID.ToString() +".",
                    product.Title
                    
                };
                comboBoxProduct.Items.Add(string.Join(" ", item));
            }
        }
        void ShowStaff()
        {
            comboBoxStaff.Items.Clear();
            foreach (Staff staff in Program.magazinEntities.Staff)
            {
                string[] item =
                {
                    staff.ID.ToString()+". ",
                    staff.Position+": ",
                    staff.FirstName+" ",
                    staff.MiddleName+" ",
                    staff.LastName+" "
                };
                comboBoxStaff.Items.Add(string.Join("", item));

            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxProduct.SelectedItem != null && comboBoxStaff != null)
            {
                SoldProduct soldProduct = new SoldProduct();
                soldProduct.IDStaff = Convert.ToInt32(comboBoxStaff.SelectedItem.ToString().Split('.')[0]);
                soldProduct.IDProduct = Convert.ToInt32(comboBoxProduct.SelectedItem.ToString().Split('.')[0]);
                Program.magazinEntities.SoldProduct.Add(soldProduct);
                Program.magazinEntities.SaveChanges();
                ShowSoldProduct();

            }
            else
            {
                MessageBox.Show("Данные не выбраны", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void ShowSoldProduct()
        {
            listViewSoldProduct.Items.Clear();
            foreach(SoldProduct soldProduct in Program.magazinEntities.SoldProduct)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    soldProduct.ID.ToString(),
                    soldProduct.IDProduct.ToString(),
                    soldProduct.Product.Title,
                    soldProduct.Product.Part_Number,
                    soldProduct.Product.Description,
                    soldProduct.Product.Cost.ToString(),
                    soldProduct.IDStaff.ToString(),
                    soldProduct.Staff.FirstName+" "+ soldProduct.Staff.MiddleName+" "+ soldProduct.Staff.LastName
                }) ;
                item.Tag = soldProduct;
                listViewSoldProduct.Items.Add(item);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewSoldProduct.SelectedItems.Count == 1)
            {
                SoldProduct soldProduct = listViewSoldProduct.SelectedItems[0].Tag as SoldProduct;
                soldProduct.IDProduct = Convert.ToInt32(comboBoxProduct.SelectedItem.ToString().Split('.')[0]);
                soldProduct.IDStaff = Convert.ToInt32(comboBoxStaff.SelectedItem.ToString().Split('.')[0]);
                Program.magazinEntities.SaveChanges();
                ShowSoldProduct();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if(listViewSoldProduct.SelectedItems.Count == 1)
                {
                    SoldProduct soldProduct = listViewSoldProduct.SelectedItems[0].Tag as SoldProduct;
                    Program.magazinEntities.SoldProduct.Remove(soldProduct);
                    Program.magazinEntities.SaveChanges();
                    ShowSoldProduct();
                }
                comboBoxProduct.SelectedItem = null;
                comboBoxStaff.SelectedItem = null;
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewSoldProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listViewSoldProduct.SelectedItems.Count == 1)
            {
                SoldProduct soldProduct = listViewSoldProduct.SelectedItems[0].Tag as SoldProduct;
                comboBoxProduct.SelectedIndex = comboBoxProduct.FindString(soldProduct.IDProduct.ToString());
                comboBoxStaff.SelectedIndex = comboBoxStaff.FindString(soldProduct.IDStaff.ToString());
            }
            else
            {
                comboBoxProduct.SelectedItem = null;
                comboBoxStaff.SelectedItem = null;
            }
        }
    }
}
