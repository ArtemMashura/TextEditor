using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml;
using WpfApp4.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        TextRange selectedText;
        Settings _setting = new Settings();
        BrushConverter converter = new BrushConverter();
        string working_with_path;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SaveFile()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "text|*.txt";
            if (dialog.ShowDialog() == true)
            {
                TextRange doc = new TextRange(RichTB.Document.ContentStart, RichTB.Document.ContentEnd);
                using (FileStream fs = new FileStream(dialog.FileName, FileMode.OpenOrCreate))
                {
                    doc.Save(fs, DataFormats.Text);
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "text|*.txt";
            if (dialog.ShowDialog() == true)
            {
                TextRange doc = new TextRange(RichTB.Document.ContentStart, RichTB.Document.ContentEnd);
                working_with_path = dialog.FileName;
                using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open))
                {
                    doc.Load(fs, DataFormats.Text);
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("By ITStep", "Autor", MessageBoxButton.OK, MessageBoxImage.Information);
        }  

        private void StyleWhite_Click(object sender, RoutedEventArgs e)
        {
            myWindow.Background = Brushes.White;
            _setting.background1 = myWindow.Background;
            }

        private void StyleGray_Click(object sender, RoutedEventArgs e)
        {
            myWindow.Background = Brushes.Gray;
            _setting.background1 = myWindow.Background;
        }

        private void StyleBlue_Click(object sender, RoutedEventArgs e)
        {
            myWindow.Background = Brushes.Blue;
            _setting.background1 = myWindow.Background;
        }

        private void StyleItalic_Click(object sender, RoutedEventArgs e)
        {
            if (!this.RichTB.Selection.IsEmpty)
                if (!FontStyles.Italic.Equals(this.RichTB.Selection.GetPropertyValue(TextElement.FontWeightProperty)))
                    this.RichTB.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontStyles.Italic);
                else
                    this.RichTB.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);

        }

        private void StyleBold_Click(object sender, RoutedEventArgs e)
        {
            if (!selectedText.IsEmpty)
                if (!FontWeights.Bold.Equals(selectedText.GetPropertyValue(TextElement.FontWeightProperty)))
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                else
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
        }

       

        private void RichTB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!this.RichTB.Selection.GetPropertyValue(TextElement.FontSizeProperty).Equals(DependencyProperty.UnsetValue))
                Size_TB.Text = Convert.ToString(this.RichTB.Selection.GetPropertyValue(TextElement.FontSizeProperty));
            else
                Size_TB.Text = null;
            if (!FontWeights.Bold.Equals(this.RichTB.Selection.GetPropertyValue(TextElement.FontWeightProperty)))
                Bold_CB.IsChecked = false;
            else
                Bold_CB.IsChecked = true;
            if (!FontStyles.Italic.Equals(this.RichTB.Selection.GetPropertyValue(TextElement.FontWeightProperty)))
                Italic_CB.IsChecked = false;
            else
                Italic_CB.IsChecked = true;
        }

        

        private void FontSizeBTN_Click(object sender, RoutedEventArgs e)
        {
            var font_size = Convert.ToDouble(Size_TB.Text);
            RichTB.Focus();
            if (!selectedText.IsEmpty)
                if (Size_TB.Text != null)
                {
                    selectedText.ApplyPropertyValue(TextElement.FontSizeProperty, font_size);
                }
        }

        private void RichTB_LostFocus(object sender, RoutedEventArgs e)
        {
            selectedText = new TextRange(RichTB.Selection.Start, RichTB.Selection.End);
        }

        private void Bold_CB_Click(object sender, RoutedEventArgs e)
        {
            RichTB.Focus();
            if (!selectedText.IsEmpty)
                if (Bold_CB.IsChecked == false)
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                else
                    selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private void Italic_CB_Click(object sender, RoutedEventArgs e)
        {
            RichTB.Focus();
            if (!selectedText.IsEmpty)
                if (Bold_CB.IsChecked == false)
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                else
                    selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
        }

        private void mainWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.WriteAllText("settings.json", JsonConvert.SerializeObject(_setting, Newtonsoft.Json.Formatting.Indented));
        }

        private void myWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("settings.json"))
            {
                _setting = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("settings.json"));
                myWindow.Background = _setting.background1;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            TextRange doc = new TextRange(RichTB.Document.ContentStart, RichTB.Document.ContentEnd);
            if (working_with_path == null)
            {
                SaveFile();
            }
            else
            {
                using (FileStream fs = new FileStream(working_with_path, FileMode.Open))
                {
                    doc.Save(fs, DataFormats.Text);
                }
            }
            
        }
    }

}
