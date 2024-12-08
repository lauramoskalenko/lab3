using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace WindowsFormsMovie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Оголошення списку movies
        private List<Movie> movies = new List<Movie>();

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter search term...")
            {
                textBox1.Text = "";
                textBox1.ForeColor = SystemColors.WindowText; // Колір тексту змінюється на чорний
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Enter search term...";
                textBox1.ForeColor = SystemColors.GrayText; // Колір тексту стає сірим
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.ForeColor = SystemColors.GrayText; // Сірий колір для заповнювача
            textBox1.Text = "Enter search term...";    // Початковий текст
        }
        private void UpdateGrid()
        {
            dataGridView1.DataSource = null;  // Очищаємо поточне джерело даних
            dataGridView1.DataSource = movies;  // Встановлюємо нові дані
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    string json = File.ReadAllText(filePath);
                    movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    UpdateGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження файлу: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Відкриваємо діалогове вікно для збереження файлу
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    // Серіалізуємо список фільмів у формат JSON
                    string json = JsonConvert.SerializeObject(movies, Formatting.Indented);
                    // Зберігаємо у файл
                    File.WriteAllText(filePath, json);
                    MessageBox.Show("Фільми успішно збережені в файл.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка збереження файлу: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Створюємо нову форму для додавання фільму
            EditForm editForm = new EditForm();

            // Якщо користувач натиснув "OK" на формі редагування
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Створюємо новий об'єкт Movie
                var newMovie = new Movie
                {
                    ID = movies.Count > 0 ? movies.Max(m => m.ID) + 1 : 1,  // Генеруємо унікальний ID
                    Title = editForm.MovieTitle,
                    Genre = editForm.MovieGenre,
                    Director = editForm.MovieDirector,
                    Year = editForm.MovieYear,
                    DurationMinutes = editForm.MovieDuration
                };

                // Додаємо фільм до списку
                movies.Add(newMovie);
                // Оновлюємо таблицю
                UpdateGrid();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Перевіряємо, чи вибрано фільм у DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedMovie = dataGridView1.SelectedRows[0].DataBoundItem as Movie;

                if (selectedMovie != null)
                {
                    // Створюємо форму для редагування
                    EditForm editForm = new EditForm
                    {
                        MovieTitle = selectedMovie.Title,
                        MovieGenre = selectedMovie.Genre,
                        MovieDirector = selectedMovie.Director,
                        MovieYear = selectedMovie.Year,
                        MovieDuration = selectedMovie.DurationMinutes
                    };

                    // Якщо користувач натиснув "OK"
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Оновлюємо фільм
                        selectedMovie.Title = editForm.MovieTitle;
                        selectedMovie.Genre = editForm.MovieGenre;
                        selectedMovie.Director = editForm.MovieDirector;
                        selectedMovie.Year = editForm.MovieYear;
                        selectedMovie.DurationMinutes = editForm.MovieDuration;

                        // Оновлюємо таблицю
                        UpdateGrid();
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть фільм для редагування.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Перевіряємо, чи вибрано фільм у DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedMovie = dataGridView1.SelectedRows[0].DataBoundItem as Movie;

                if (selectedMovie != null)
                {
                    // Видаляємо фільм зі списку
                    movies.Remove(selectedMovie);
                    // Оновлюємо таблицю
                    UpdateGrid();
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть фільм для видалення.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Отримуємо текст для пошуку
            string searchTerm = textBox1.Text.ToLower();

            // Пошук фільмів за допомогою LINQ
            var searchResults = movies.Where(movie =>
                movie.Title.ToLower().Contains(searchTerm) ||
                movie.Genre.ToLower().Contains(searchTerm) ||
                movie.Director.ToLower().Contains(searchTerm) ||
                movie.Year.ToString().Contains(searchTerm) ||
                movie.DurationMinutes.ToString().Contains(searchTerm)
            ).ToList();

            // Оновлюємо таблицю результатами пошуку
            dataGridView1.DataSource = searchResults;
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(); // Відкриває форму як модальне вікно
        }
    }
}
