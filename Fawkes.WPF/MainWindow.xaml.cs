using Fawkes.BLL;
using Fawkes.BLL.FileSearchers;
using Fawkes.WPF.Model;
using System;
using System.Windows;

namespace Fawkes.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string VALID_ERROR = "Błąd walidacji danych.";
        private const string START = "-- START --";
        private const string END = "\n-- KONIEC --";

        private SearchInitializer _searchInitializer;

        public MainWindow()
        {
            InitializeComponent();
            _searchInitializer = new SearchInitializer();

            FillSearchByComboBox();
        }

        private void FillSearchByComboBox()
        {
            foreach (var enumValue in (FileSearcherTypes[])Enum.GetValues(typeof(FileSearcherTypes)))
            {
                if (enumValue < FileSearcherTypes.Extensions) continue;

                searchByCombo.Items.Add(enumValue);
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            resultText.Text = START;
            spinnerIcon.Visibility = Visibility.Visible;
            searchButton.IsEnabled = false;

            try
            {
                SetInitializer();

                var process = new SearchTextProcess(_searchInitializer, new Displayer(resultText));
                await process.GetSearchedFilePaths();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                resultText.Text += $"\n{ex.Message}";
            }

            resultText.Text += END;
            spinnerIcon.Visibility = Visibility.Hidden;
            searchButton.IsEnabled = true;
        }

        private void SetInitializer()
        {
            bool isValid = _searchInitializer
                .SetValues(searchByCombo.Text, fileInfo.Text, searchedText.Text, path.Text)
                .Valid();

            if (!isValid)
                throw new Exception(VALID_ERROR);
        }

        private void ChoiceDirectory_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                path.Text = dialog.SelectedPath;
            }
        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(resultText.Text);
        }
    }
}
