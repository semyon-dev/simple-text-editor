using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace S_Pad_Light
{
    /// <summary>
    /// Logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 40, 50};
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            BoldButton.IsSelected = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            ItalicButton.IsSelected = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineButton.IsSelected = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }

        public void btn_open_Click(object sender, RoutedEventArgs e)  //read file
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текст (*.txt)|*.txt |Rich Text Format (*.rtf)|*.rtf | All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        public void btn_save_Click(object sender, RoutedEventArgs e)  //save file
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Текст (*.txt)|*.txt |Rich Text Format (*.rtf)|*.rtf | All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }
        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e) //font
        {
            if (cmbFontFamily.SelectedItem != null)
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)   // font size
        {
            try
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);
            }
            catch { }
        }
        public void UnderlineText(object sender, RoutedEventArgs e)    // underline text
        {
            if (rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty) != TextDecorations.Underline)
                rtbEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
                rtbEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
        }

        public void UnUnderlineText(object sender, RoutedEventArgs e)    // underline text
        {
            if (rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty) != TextDecorations.Underline)
                rtbEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
                rtbEditor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
        }

        public void BoldText(object sender, RoutedEventArgs e)  // bold text
        {
            rtbEditor.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
        }

        public void UnBoldText(object sender, RoutedEventArgs e)  // unbold text
        {
            rtbEditor.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
        }

        public void ItalicText(object sender, RoutedEventArgs e)   // italics
        {
            rtbEditor.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
        }

        public void UnItalicText(object sender, RoutedEventArgs e)   // without italics 
        {
            rtbEditor.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
        }

        public void btn_info_Click(object sender, RoutedEventArgs e)  // info
        {
            MessageBox.Show("Creator : Semyon Novikov \nVersion : v1.0.0 \nDonate BTC: 16gzM2uGF8WyfamRrwNQdFCpKBe8b7zvw9", "About" , MessageBoxButton.OK , MessageBoxImage.Information);
        }
    }
}
