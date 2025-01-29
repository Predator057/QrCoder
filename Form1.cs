using System;
using System.Drawing;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;

namespace QrGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(this.Form1_KeyDown);
            KeyPreview = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V) 
            {
                //textBox1.Text = "gggg";
                pictureBox1.Image = Clipboard.GetImage();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog(); //  load будет запрашивать у пользователя место, из которого он хочет загрузить файл.
            if (load.ShowDialog() == DialogResult.OK) // //если пользователь нажимает в обозревателе кнопку "Открыть".
            {
                pictureBox1.ImageLocation = load.FileName; // в pictureBox'e открывается файл, который был по пути, запрошенном пользователем.
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string qrtext = richTextBox1.Text; //считываем текст из TextBox'a
            QRCodeEncoder encoder = new QRCodeEncoder(); //создаем объект класса QRCodeEncoder
            Bitmap qrcode = encoder.Encode(qrtext); // кодируем слово, полученное из TextBox'a (qrtext) в переменную qrcode. класса Bitmap(класс, который используется для работы с изображениями)
            pictureBox1.Image = qrcode as Image; // pictureBox выводит qrcode как изображение.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog(); // save будет запрашивать у пользователя, место, в которое он захочет сохранить файл. 
            save.Filter = "PNG|*.png|JPEG|*.jpg|GIF|*.gif|BMP|*.bmp"; //создаём фильтр, который определяет, в каких форматах мы сможем сохранить нашу информацию. В данном случае выбираем форматы изображений. Записывается так: "название_формата_в обозревателе|*.расширение_формата")
            if (save.ShowDialog() == DialogResult.OK) //если пользователь нажимает в обозревателе кнопку "Сохранить".
            {
                pictureBox1.Image.Save(save.FileName); //изображение из pictureBox'a сохраняется под именем, которое введёт пользователь
            }
        }

        private void button3_Click(object sender, EventArgs e)//Кнопка сканирования
        {
            if(pictureBox1.Image != null)
            {
                try
                {
                    QRCodeDecoder decoder = new QRCodeDecoder(); // создаём "раскодирование изображения"
                    richTextBox1.Text = decoder.Decode(new QRCodeBitmapImage(pictureBox1.Image as Bitmap));
                }
                catch
                {
                    MessageBox.Show("Не удалось прочитать QR", "Ошибка чтения!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }else MessageBox.Show("ЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫ", "ЪЫЬ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image != null)
                Clipboard.SetImage(pictureBox1.Image);
            else MessageBox.Show("Нельзя! Запрещено! \n Должен быть QR", "Ужас!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
