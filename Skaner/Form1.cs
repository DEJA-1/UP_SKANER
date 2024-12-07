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
        private int width = 210; // Domyœlna szerokoœæ dla A4 (300 DPI)
        private int height = 297; // Domyœlna wysokoœæ dla A4 (300 DPI)
        private int color_mode = 1; // Domyœlny tryb kolorowy
        private ImageFile image;

        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        // Inicjalizacja kontrolek
        private void InitializeControls()
        {
            // Pobieranie listy skanerów
            for (int i = 1; i <= dManager.DeviceInfos.Count; i++)
            {
                string scannerName = dManager.DeviceInfos[i].Properties["Name"].get_Value().ToString();

                // Dodanie urz¹dzenia do listy 
                cbUrzadzenie.Items.Add(scannerName);
                dInfos = dManager.DeviceInfos[i];

            }
            if (cbUrzadzenie.Items.Count > 0)
            {
                cbUrzadzenie.SelectedIndex = 0; // Ustawienie pierwszego skanera jako domyœlnego
            }
            else
            {
                Console.WriteLine("Nie wykryto ¿adnych skanerów.");
            }

            // ComboBox4 (Czêœæ obrazu)
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
            cbTryb.Items.Add("Czarno-bia³y");
            cbTryb.SelectedIndex = 0;
        }

        private void bPolacz_click(object sender, EventArgs e) // Po³¹cz
        {
            if (cbUrzadzenie.SelectedIndex >= 0)
            {
                Scanner = dInfos.Connect(); // Po³¹czenie ze skanerem
                MessageBox.Show($"Po³¹czono ze skanerem: {cbUrzadzenie.SelectedItem}");
            }
            else
            {
                MessageBox.Show("Wybierz skaner z listy przed po³¹czeniem.");
            }
        }

        private void bSkanuj_click(object sender, EventArgs e) // Skanuj
        {
            // Pobierz katalog g³ówny projektu (2 poziomy wy¿ej od katalogu `bin`)
            string projectBasePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

            // Po³¹cz œcie¿kê projektu z nazw¹ testowego pliku
            string filePath = Path.Combine(projectBasePath, "testuuuje.jpg");

            // SprawdŸ, czy plik istnieje
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Plik nie zosta³ znaleziony: {filePath}");
                return;
            }

            // Za³aduj oryginalny obraz
            Bitmap originalImage = new Bitmap(filePath);

            // Ustal proporcje wyœwietlania na podstawie wybranej czêœci obrazu
            Rectangle cropRect;
            string czescObrazu = cbCzescObrazu.SelectedItem.ToString(); // 1/1, 1/2, 1/4
            if (czescObrazu == "1/1")
            {
                cropRect = new Rectangle(0, 0, originalImage.Width, originalImage.Height); // Ca³y obraz
            }
            else if (czescObrazu == "1/2")
            {
                cropRect = new Rectangle(0, 0, originalImage.Width, originalImage.Height / 2); // Pó³ obrazu
            }
            else if (czescObrazu == "1/4")
            {
                cropRect = new Rectangle(0, 0, originalImage.Width, originalImage.Height / 4); // Æwieræ obrazu
            }
            else
            {
                MessageBox.Show("Nieznana czêœæ obrazu.");
                return;
            }

            // Przytnij obraz
            Bitmap croppedImage = originalImage.Clone(cropRect, originalImage.PixelFormat);

            // Wyœwietlenie obrazu w kontrolce PictureBox
            obraz.SizeMode = PictureBoxSizeMode.StretchImage;
            obraz.Image = croppedImage; // Wyœwietlenie przyciêtego obrazu

            // Generowanie unikalnej nazwy pliku
            string uniqueFileName = $"test{DateTime.Now.Ticks}.jpg";
            string savePath = Path.Combine(projectBasePath, uniqueFileName);

            // Zapisanie przyciêtego obrazu do nowego pliku
            croppedImage.Save(savePath);

            // Tworzenie obiektu ImageFile z zapisanego pliku
            var wiaImage = new ImageFile();
            wiaImage.LoadFile(savePath);
            image = wiaImage; // Przypisanie do zmiennej `image`

            MessageBox.Show($"Symulacja skanowania zakoñczona.\nObraz za³adowano z: {filePath}.\nObraz zapisano jako: {savePath}");

            // Zwolnij zasoby
            originalImage.Dispose();
            croppedImage.Dispose();
        }




        //private void bSkanuj_click(object sender, EventArgs e) // Skanuj
        //{
        //    if (Scanner == null)
        //    {
        //        MessageBox.Show("Najpierw po³¹cz siê ze skanerem.");
        //        return;
        //    }

        //    var scannerItem = Scanner.Items[1];

        //    // Ustawienia DPI, jasnoœci i kontrastu
        //    SetProperty(scannerItem.Properties, "6147", dpi);
        //    SetProperty(scannerItem.Properties, "6148", dpi);
        //    SetProperty(scannerItem.Properties, "6149", 0); // Pocz¹tkowy piksel (X)
        //    SetProperty(scannerItem.Properties, "6150", 0); // Pocz¹tkowy piksel (Y)

        //    // Wybór czêœci obrazu
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
        //    color_mode = cbTryb.SelectedIndex == 0 ? 1 : 4; // Kolorowy: 1, Czarno-bia³y: 4
        //    SetProperty(scannerItem.Properties, "6146", color_mode);

        //    // Wybór formatu obrazu
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

        //    // Wyœwietlenie obrazu w PictureBox "obraz"
        //    obraz.SizeMode = PictureBoxSizeMode.StretchImage; // Opcjonalne
        //    obraz.ImageLocation = projectPath; // Wyœwietlenie obrazu w kontrolce

        //    MessageBox.Show($"Skan zapisany w projekcie: {projectPath}");
        //}


        private void bZapisz_click(object sender, EventArgs e)
        {
            // SprawdŸ, czy obraz zosta³ za³adowany
            if (image != null)
            {
                // Tworzenie instancji SaveFileDialog
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    // Ustawienia dialogu
                    saveFileDialog.Title = "Zapisz obraz jako";
                    saveFileDialog.Filter = "Obrazy (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"; // Filtr plików
                    saveFileDialog.DefaultExt = cbFormat.SelectedItem.ToString(); // Domyœlny format
                    saveFileDialog.FileName = "skan"; // Domyœlna nazwa pliku

                    // Wyœwietlenie dialogu
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Sprawdzenie, czy plik ju¿ istnieje
                        if (File.Exists(saveFileDialog.FileName))
                        {
                            // Jeœli plik istnieje, usuñ go
                            File.Delete(saveFileDialog.FileName);
                        }

                        // Zapisanie pliku
                        image.SaveFile(saveFileDialog.FileName);
                        MessageBox.Show("Obraz zosta³ zapisany jako: " + saveFileDialog.FileName);
                    }
                }
            }
            else
            {
                MessageBox.Show("Brak obrazu do zapisania.");
            }
        }

        // Ustawianie w³aœciwoœci skanera
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
