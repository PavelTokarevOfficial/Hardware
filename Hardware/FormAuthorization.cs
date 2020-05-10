using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hardware
{
    public struct User
    {
        public string login;
        public string password;
        public string type;

    }
    public partial class Authorization : System.Windows.Forms.Form
    {
        public static User users = new User();
        public Authorization()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "" && textBoxPassword.Text == "")
            {
                MessageBox.Show("Введите данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                bool key = false;
                foreach (Authorization authorization in Program.magazinEntities.Authorization)
                {
                    if(textBoxLogin.Text == authorization.Login && textBoxPassword.Text == authorization.Password)
                    {
                        key = true;
                        users.login = authorization.Login;
                        users.password = authorization.Password;
                        users.type = authorization.Type;
                    }
                }
                if (!key)
                {
                    MessageBox.Show("Проверьте данные", "Пользователь не найден", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxLogin.Text = "";
                    textBoxPassword.Text = "";

                }
                else
                {
                    FormMenu formMenu = new FormMenu();
                    formMenu.Show();
                    this.Hide();
                }
            }


        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
