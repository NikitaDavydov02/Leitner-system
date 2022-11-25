﻿using System;
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
        private BitmapImage plusImage;
        private BitmapImage homeImage;
        private BitmapImage deleteImage;

        private BitmapImage deleteImageDark;
        private BitmapImage homeImageDark;
        private BitmapImage plusImageDark;
        public Settings()
        {
            InitializeComponent();
            viewModel = new SettingsViewModel();
            DataContext = viewModel;
            plusImage = new BitmapImage(new Uri("..\\Assets\\PlusSprite.png", UriKind.Relative));
            homeImage = new BitmapImage(new Uri("..\\Assets\\HomeSprite.png", UriKind.Relative));
            deleteImage = new BitmapImage(new Uri("..\\Assets\\DeleteSprite.png", UriKind.Relative));
            ////Dark images inicialization
            deleteImageDark = new BitmapImage(new Uri("..\\Assets\\DeleteSpriteDark.png", UriKind.Relative));
            plusImageDark = new BitmapImage(new Uri("..\\Assets\\PlusSpriteDark.png", UriKind.Relative));
            homeImageDark = new BitmapImage(new Uri("..\\Assets\\HomeSpriteDark.png", UriKind.Relative));

            homeButtonImage.Source = homeImageDark;
            newButtonImage.Source = plusImageDark;
            deleteButtonImage.Source = deleteImageDark;
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

        private void StackPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void StackPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void homeButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            homeButtonImage.Source = homeImage;
        }

        private void homeButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            homeButtonImage.Source = homeImageDark;
        }

        private void newCardButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            newButtonImage.Source = plusImage;
        }

        private void newCardButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            newButtonImage.Source = plusImageDark;
        }

        private void deleteCardButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteButtonImage.Source = deleteImage;
        }

        private void deleteCardButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            deleteButtonImage.Source = deleteImageDark;
        }
    }
}
