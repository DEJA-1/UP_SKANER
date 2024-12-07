using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WIA;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Skaner
{
    public partial class Form1 : Form
    {
        private DeviceManager dManager = new DeviceManager();
        private DeviceInfo dInfos;
        private Device Scanner;
        private int dpi = 300;
        private int brightness = 0;
        private int contrast = 0;
        private int width = 210; // Domy�lna szeroko�� dla A4 (300 DPI)
        private int height = 297; // Domy�lna wysoko�� dla A4 (300 DPI)
        private int color_mode = 1; // Domy�lny tryb kolorowy
        private ImageFile image;

        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        // Inicjalizacja kontrolek
        private void InitializeControls()
        {
            // Pobieranie listy skaner�w
            for (int i = 1; i <= dManager.DeviceInfos.Count; i++)
            {
                string scannerName = dManager.DeviceInfos[i].Properties["Name"].get_Value().ToString();

                // Dodanie urz�dzenia do listy 
                cbUrzadzenie.Items.Add(scannerName);
                dInfos = dManager.DeviceInfos[i];

            }
            if (cbUrzadzenie.Items.Count > 0)
            {
                cbUrzadzenie.SelectedIndex = 0; // Ustawienie pierwszego skanera jako domy�lnego
            }
            else
            {
                Console.WriteLine("Nie wykryto �adnych skaner�w.");
            }

            // ComboBox4 (Cz�� obrazu)
            cbCzescObrazu.Items.Clear();
            cbCzescObrazu.Items.Add("1/1");
            cbCzescObrazu.Items.Add("1/2");
            cbCzescObrazu.Items.Add("1/4");
            cbCzescObrazu.SelectedIndex = 0;

            // ComboBox3 (Format zapisu)
            cbFormat.Items.Clear();
            cbFormat.Items.Add("jpeg");
            cbFormat.Items.Add("png");
            cbFormat.SelectedIndex = 0;

            // ComboBox2 (Tryb skanowania)
            cbTryb.Items.Clear();
            cbTryb.Items.Add("Kolorowy");
            cbTryb.Items.Add("Czarno-bia�y");
            cbTryb.SelectedIndex = 0;
        }

        private void bPolacz_click(object sender, EventArgs e) // Po��cz
        {
            if (cbUrzadzenie.SelectedIndex >= 0)
            {
                Scanner = dInfos.Connect(); // Po��czenie ze skanerem
                MessageBox.Show($"Po��czono ze skanerem: {cbUrzadzenie.SelectedItem}");
            }
            else
            {
                MessageBox.Show("Wybierz skaner z listy przed po��czeniem.");
            }
        }

        private void bSkanuj_click(object sender, EventArgs e) // Skanuj
        {
            // Pobierz katalog g��wny projektu (2 poziomy wy�ej od katalogu `bin`)
            string projectBasePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

            // Po��cz �cie�k� projektu z nazw� testowego pliku
            string filePath = Path.Combine(projectBasePath, "testuuuje.jpg");

            // Sprawd�, czy plik istnieje
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Plik nie zosta� znaleziony: {filePath}");
                return;
            }

            // Za�aduj oryginalny obraz
            Bitmap originalImage = new Bitmap(filePath);

            // Ustal proporcje wy�wietlania na podstawie wybranej cz�ci obrazu
            Rectangle cropRect;
            string czescObrazu = cbCzescObrazu.SelectedItem.ToString(); // 1/1, 1/2, 1/4
            if (czescObrazu == "1/1")
            {
                cropRect = new Rectangle(0, 0, originalImage.Width, originalImage.Height); // Ca�y obraz
            }
            else if (czescObrazu == "1/2")
            {
                cropRect = new Rectangle(0, 0, originalImage.Width, originalImage.Height / 2); // P� obrazu
            }
            else if (czescObrazu == "1/4")
            {
                cropRect = new Rectangle(0, 0, originalImage.Width, originalImage.Height / 4); // �wier� obrazu
            }
            else
            {
                MessageBox.Show("Nieznana cz�� obrazu.");
                return;
            }

            // Przytnij obraz
            Bitmap croppedImage = originalImage.Clone(cropRect, originalImage.PixelFormat);

            // Wy�wietlenie obrazu w kontrolce PictureBox
            obraz.SizeMode = PictureBoxSizeMode.StretchImage;
            obraz.Image = croppedImage; // Wy�wietlenie przyci�tego obrazu

            // Generowanie unikalnej nazwy pliku
            string uniqueFileName = $"test{DateTime.Now.Ticks}.jpg";
            string savePath = Path.Combine(projectBasePath, uniqueFileName);

            // Zapisanie przyci�tego obrazu do nowego pliku
            croppedImage.Save(savePath);

            // Tworzenie obiektu ImageFile z zapisanego pliku
            var wiaImage = new ImageFile();
            wiaImage.LoadFile(savePath);
            image = wiaImage; // Przypisanie do zmiennej `image`

            MessageBox.Show($"Symulacja skanowania zako�czona.\nObraz za�adowano z: {filePath}.\nObraz zapisano jako: {savePath}");

            // Zwolnij zasoby
            originalImage.Dispose();
            croppedImage.Dispose();
        }




        //private void bSkanuj_click(object sender, EventArgs e) // Skanuj
        //{
        //    if (Scanner == null)
        //    {
        //        MessageBox.Show("Najpierw po��cz si� ze skanerem.");
        //        return;
        //    }

        //    var scannerItem = Scanner.Items[1];

        //    // Ustawienia DPI, jasno�ci i kontrastu
        //    SetProperty(scannerItem.Properties, "6147", dpi);
        //    SetProperty(scannerItem.Properties, "6148", dpi);
        //    SetProperty(scannerItem.Properties, "6149", 0); // Pocz�tkowy piksel (X)
        //    SetProperty(scannerItem.Properties, "6150", 0); // Pocz�tkowy piksel (Y)

        //    // Wyb�r cz�ci obrazu
        //    string czescObrazu = cbCzescObrazu.SelectedItem.ToString();
        //    if (czescObrazu == "1/1")
        //    {
        //        SetProperty(scannerItem.Properties, "6151", width);
        //        SetProperty(scannerItem.Properties, "6152", height);
        //    }
        //    else if (czescObrazu == "1/2")
        //    {
        //        SetProperty(scannerItem.Properties, "6151", width / 2);
        //        SetProperty(scannerItem.Properties, "6152", height / 2);
        //    }
        //    else if (czescObrazu == "1/4")
        //    {
        //        SetProperty(scannerItem.Properties, "6151", width / 4);
        //        SetProperty(scannerItem.Properties, "6152", height / 4);
        //    }

        //    SetProperty(scannerItem.Properties, "6154", brightness);
        //    SetProperty(scannerItem.Properties, "6155", contrast);

        //    // Ustawienie trybu koloru
        //    color_mode = cbTryb.SelectedIndex == 0 ? 1 : 4; // Kolorowy: 1, Czarno-bia�y: 4
        //    SetProperty(scannerItem.Properties, "6146", color_mode);

        //    // Wyb�r formatu obrazu
        //    if (cbFormat.SelectedIndex == 0)
        //    {
        //        image = (ImageFile)scannerItem.Transfer("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}"); // JPEG
        //    }
        //    else if (cbFormat.SelectedIndex == 1)
        //    {
        //        image = (ImageFile)scannerItem.Transfer("{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}"); // PNG
        //    }

        //    string fileName = $"skan_{DateTime.Now.Ticks}.{cbFormat.SelectedItem}";
        //    string projectPath = Path.Combine(Environment.CurrentDirectory, fileName);

        //    if (File.Exists(projectPath))
        //    {
        //        File.Delete(projectPath);
        //    }

        //    image.SaveFile(projectPath);

        //    // Wy�wietlenie obrazu w PictureBox "obraz"
        //    obraz.SizeMode = PictureBoxSizeMode.StretchImage; // Opcjonalne
        //    obraz.ImageLocation = projectPath; // Wy�wietlenie obrazu w kontrolce

        //    MessageBox.Show($"Skan zapisany w projekcie: {projectPath}");
        //}


        private void bZapisz_click(object sender, EventArgs e)
        {
            // Sprawd�, czy obraz zosta� za�adowany
            if (image != null)
            {
                // Tworzenie instancji SaveFileDialog
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    // Ustawienia dialogu
                    saveFileDialog.Title = "Zapisz obraz jako";
                    saveFileDialog.Filter = "Obrazy (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"; // Filtr plik�w
                    saveFileDialog.DefaultExt = cbFormat.SelectedItem.ToString(); // Domy�lny format
                    saveFileDialog.FileName = "skan"; // Domy�lna nazwa pliku

                    // Wy�wietlenie dialogu
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Sprawdzenie, czy plik ju� istnieje
                        if (File.Exists(saveFileDialog.FileName))
                        {
                            // Je�li plik istnieje, usu� go
                            File.Delete(saveFileDialog.FileName);
                        }

                        // Zapisanie pliku
                        image.SaveFile(saveFileDialog.FileName);
                        MessageBox.Show("Obraz zosta� zapisany jako: " + saveFileDialog.FileName);
                    }
                }
            }
            else
            {
                MessageBox.Show("Brak obrazu do zapisania.");
            }
        }

        // Ustawianie w�a�ciwo�ci skanera
        private static void SetProperty(IProperties properties, object propName, object propValue)
        {
            try
            {
                Property property = properties.get_Item(ref propName);
                property.set_Value(ref propValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting property {propName}: {ex.Message}");
            }
        }
    }
}
