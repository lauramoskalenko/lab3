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
    public partial class EditForm : Form
    {
        public string MovieTitle { get; set; }
        public string MovieGenre { get; set; }
        public string MovieDirector { get; set; }
        public int MovieYear { get; set; }
        public int MovieDuration { get; set; }

        public EditForm()
        {
            InitializeComponent();
        }

        // Завантаження даних при редагуванні
        private void EditForm_Load(object sender, EventArgs e)
        {
            textBoxTitle.Text = MovieTitle;
            textBoxGenre.Text = MovieGenre;
            textBoxDirector.Text = MovieDirector;
            textBoxYear.Text = MovieYear.ToString();
            textBoxDuration.Text = MovieDuration.ToString();
        }

        // Обробник для кнопки Save
        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Оновлюємо значення властивостей відповідно до введених даних
            MovieTitle = textBoxTitle.Text;
            MovieGenre = textBoxGenre.Text;
            MovieDirector = textBoxDirector.Text;

            // Перевірка введеного року
            if (!int.TryParse(textBoxYear.Text, out int year))
            {
                MessageBox.Show("Будь ласка, введіть коректний рік.");
                return;
            }
            MovieYear = year;

            // Перевірка введеної тривалості
            if (!int.TryParse(textBoxDuration.Text, out int duration))
            {
                MessageBox.Show("Будь ласка, введіть коректну тривалість фільму.");
                return;
            }
            MovieDuration = duration;

            // Закриваємо форму та передаємо результат назад
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        
    }
}
