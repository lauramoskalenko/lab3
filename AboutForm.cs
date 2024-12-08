using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsMovie
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Москаленко Лаура Олександрівна, 2 курс, К24, 2024 рік\r\n\r\nЦя програма дозволяє працювати з даними про фільми у форматі JSON. \r\nВи можете завантажувати фільми з файлу, додавати, редагувати, видаляти записи, а також шукати фільми \r\nза різними критеріями.\r\nВсі фільми відображаються в таблиці, зберігаються в файл, а пошук здійснюється за допомогою LINQ. \r\nТакож є форма з інформацією про саму програму";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            // Закриває форму "Про програму"
            this.Close();
        }
    }
}
