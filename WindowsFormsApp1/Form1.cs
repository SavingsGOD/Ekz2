using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public int m1 = 0;
        public string captchaText;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private bool IsValidLoginCharacter(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                   (c >= '0' && c <= '9');
        }

        private bool IsValidPassCharacter(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                   "!@#$%^&*()_=-+,./{}<>?".Contains(c) ||
                   (c >= '0' && c <= '9');
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (m1 == 0)
            {
                textBox2.PasswordChar = default;
                pictureBox2.Image = WindowsFormsApp1.Properties.Resources.free_icon_hide_2767146;
                m1 = 1;
            }
            else if (m1 == 1)
            {
                textBox2.PasswordChar = '*';
                pictureBox2.Image = WindowsFormsApp1.Properties.Resources.free_icon_eye_158746;
                m1 = 0;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IsValidPassCharacter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!IsValidLoginCharacter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (login == "user" && password == "user")
            {
                MessageBox.Show("Авторизация успешна!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WelcomeForm welcome = new WelcomeForm();
                this.Visible = false;
                welcome.ShowDialog();
                this.Close();
            }
            else
                MessageBox.Show("Неверный пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            textBox2.Text = "";
            Captha();
        }
        private void Captha()
        {
            CaptchaToImage();
            pictureBox3.Enabled = false;
            textBox1.Text = null;
            textBox2.Text = null;
            pictureBox3.Visible = true;
            this.Height = 539;
        }
        private void CaptchaToImage()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            captchaText = "";

            for (int i = 0; i < 4; i++)
            {
                captchaText += chars[random.Next(chars.Length)];
            }

            Bitmap bmp = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);
            Font font = new Font("Comic Sans MS", 30, FontStyle.Bold);

            for (int i = 0; i < 4; i++)
            {
                PointF point = new PointF(i * 50, 0);
                graphics.TranslateTransform(10, 10);
                graphics.RotateTransform(random.Next(-10, 10));
                graphics.DrawString(captchaText[i].ToString(), font, Brushes.Black, point);
                graphics.ResetTransform();
            }

            for (int i = 0; i < 10; i++)
            {
                Pen pen = new Pen(Color.Red, random.Next(2, 5));
                int x1 = random.Next(pictureBox3.Width);
                int y1 = random.Next(pictureBox3.Height);
                int x2 = random.Next(pictureBox3.Width);
                int y2 = random.Next(pictureBox3.Height);
                graphics.DrawLine(pen, x1, y1, x2, y2);
            }

            pictureBox3.Image = bmp;
        }
    }
}
