using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace QR_Code_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            productVersionLabel.Text = "Версия" + Application.ProductVersion.ToString();
        }

        public void button1_Click(object sender, EventArgs e)
        {


            QRCodeWriter qrEncode = new QRCodeWriter(); //создание QR кода

            string strRUS = textBox1.Text;  //строка на русском языке

            Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();    //для колекции поведений
            hints.Add(EncodeHintType.CHARACTER_SET, "utf-8");   //добавление в коллекцию кодировки utf-8
            BitMatrix qrMatrix = qrEncode.encode(   //создание матрицы QR
                strRUS,                 //кодируемая строка
                BarcodeFormat.QR_CODE,  //формат кода, т.к. используется QRCodeWriter применяется QR_CODE
                300,                    //ширина
                300,                    //высота
                hints);                 //применение колекции поведений

            BarcodeWriter qrWrite = new BarcodeWriter();                        //класс для кодирования QR в растровом файле
            Bitmap qrImage = qrWrite.Write(qrMatrix);                           //создание изображения
            qrImage.Save("1.bmp", System.Drawing.Imaging.ImageFormat.Bmp);      //сохранение изображения
            BarcodeReader qrDecode = new BarcodeReader();                       //чтение QR кода
            Result text = qrDecode.Decode((Bitmap)Bitmap.FromFile("1.bmp"));    //декодирование растрового изображения
            pictureBox1.Image = Image.FromFile("1.bmp");                        //вывод результата



        }

        public void button2_Click(object sender, EventArgs e)
        {
            //вместо pictureBox1 укажите свой pictureBox

            if (pictureBox1.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                //отображать ли предупреждение, если пользователь указывает имя уже существующего файла
                savedialog.OverwritePrompt = true;
                //отображать ли предупреждение, если пользователь указывает несуществующий путь
                savedialog.CheckPathExists = true;
                //список форматов файла, отображаемый в поле "Тип файла"
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                //отображается ли кнопка "Справка" в диалоговом окне
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        pictureBox1.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
