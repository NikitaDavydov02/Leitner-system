using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Leitner_System.ViewModel;
using System.Windows.Forms;

namespace Leitner_System.View
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        SettingsViewModel viewModel;
        public Settings()
        {
            InitializeComponent();
            viewModel = new SettingsViewModel();
            DataContext = viewModel;
        }
        public event EventHandler GoToHomePage;

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveSettings();
            EventHandler handler = GoToHomePage;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void chooseSaveFolder_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
            folderPath = folderBrowserDialog1.SelectedPath;
            viewModel.SettingsModel.AbsolutePathOfSaveDeckFolder = folderPath;
            DataContext = viewModel.SettingsModel;
        }

        private void chooseBackupFolder_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
            folderPath = folderBrowserDialog1.SelectedPath;
            viewModel.SettingsModel.AbsolutePathOfBackupFolder = folderPath;
            DataContext = viewModel.SettingsModel;
        }

        private void trainingTemplatesScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void newCardButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddTrainingTemplate();
        }

        private void deleteCardButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.DeleteSelectedTemplates(new List<int>() { trainingTemplatesListView.SelectedIndex });
        }

        private void Copy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<int> indexesOfSelectedItems = new List<int>();
            int i = 0;
            foreach (TrainingTemplateViewModel item in viewModel.TrainingTemplates)
            {
                if (trainingTemplatesListView.SelectedItems.Contains(item))
                    indexesOfSelectedItems.Add(i);
                i++;
            }
            viewModel.CopyTemplatesInBuffer(indexesOfSelectedItems);
        }
        private void Paste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.PasteTemplatesFromBuffer();
        }
        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            List<int> indexesOfSelectedItems = new List<int>();
            int i = 0;
            foreach (TrainingTemplateViewModel item in viewModel.TrainingTemplates)
            {
                if (trainingTemplatesListView.SelectedItems.Contains(item))
                    indexesOfSelectedItems.Add(i);
                i++;
            }
            viewModel.DeleteSelectedTemplates(indexesOfSelectedItems);
        }

    }
}
