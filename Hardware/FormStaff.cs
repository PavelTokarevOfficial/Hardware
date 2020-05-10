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
    public partial class FormStaff : Form
    {
        public FormStaff()
        {
            InitializeComponent();
            ShowStaff();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Staff staff = new Staff();
            staff.FirstName = textBoxFirstName.Text;
            staff.MiddleName = textBoxMiddleName.Text;
            staff.LastName = textBoxLastName.Text;
            staff.Position = textBoxPosition.Text;
            staff.Post = textBoxPost.Text;
            staff.Phone = textBoxPhone.Text;
            Program.magazinEntities.Staff.Add(staff);
            Program.magazinEntities.SaveChanges();
            ShowStaff();
        }
        void ShowStaff()
        {
            listViewStaff.Items.Clear();
            foreach(Staff staff in Program.magazinEntities.Staff)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    staff.ID.ToString(),
                    staff.FirstName,
                    staff.MiddleName,
                    staff.LastName,
                    staff.Position,
                    staff.Post,
                    staff.Phone
                });
                item.Tag = staff;
                listViewStaff.Items.Add(item);
            }
            listViewStaff.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewStaff.SelectedItems.Count == 1)
            {
                Staff staff = listViewStaff.SelectedItems[0].Tag as Staff;
                staff.FirstName = textBoxFirstName.Text;
                staff.MiddleName = textBoxMiddleName.Text;
                staff.LastName = textBoxLastName.Text;
                staff.Position = textBoxPosition.Text;
                staff.Post = textBoxPost.Text;
                staff.Phone = textBoxPhone.Text;
                Program.magazinEntities.SaveChanges();
                ShowStaff();
            }
        }

        private void listViewStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listViewStaff.SelectedItems.Count == 1)
            {
                Staff staff = listViewStaff.SelectedItems[0].Tag as Staff;
                textBoxFirstName.Text = staff.FirstName;
                textBoxMiddleName.Text = staff.MiddleName;
                textBoxLastName.Text = staff.LastName;
                textBoxPosition.Text = staff.Position;
                textBoxPost.Text = staff.Post;
                textBoxPhone.Text = staff.Phone;
            }
            else
            {
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPosition.Text = "";
                textBoxPost.Text = "";
                textBoxPhone.Text = "";
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewStaff.SelectedItems.Count == 1)
                {
                    Staff staff = listViewStaff.SelectedItems[0].Tag as Staff;
                    Program.magazinEntities.Staff.Remove(staff);
                    Program.magazinEntities.SaveChanges();
                    ShowStaff();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPosition.Text = "";
                textBoxPhone.Text = "";
                textBoxPost.Text = "";
            }
            catch
            {
                MessageBox.Show("Невозможно удалить, запись используется!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
