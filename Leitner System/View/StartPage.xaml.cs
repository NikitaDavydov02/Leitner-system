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
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.ObjectModel;
using Leitner_System.ViewModel;

namespace Leitner_System.View
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private LeitnerSystemViewModel viewModel;
        private bool cardsInDeckIsActive = false;
        private bool deckListIsActive = false;
        public StartPage()
        {
            InitializeComponent();
            viewModel = new LeitnerSystemViewModel();
            DataContext = viewModel;
            viewModel.PropertyChanged += CurrentCardChanged;
            cardScrollViewer.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Disabled;
        }
        private void CurrentCardChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentCard")
                cardPresentationAndEditingElement.SetViewModel(viewModel.CurrentCard);
        }
        private void startTraining_Click(object sender, RoutedEventArgs e)
        {
            TrainingViewModel trainingViewModel = viewModel.StartNewTraining();
            if (trainingViewModel == null)
                return;
            OnTrainingStart(new TrainingStartEventArgs(trainingViewModel));
        }
        private void cardsInDeck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.UpdateCurrentSelectedCard(cardsInDeck.SelectedIndex);
        }
        private void decksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.UpdateCurrentDeckIndex(decksList.SelectedIndex);
        }
        private void deckSaveButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentDeck.SaveRenameThisDeck();
        }
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SelectAllDecks();
        }
        private void ReverseAll_Click(object sender, RoutedEventArgs e)
        {
            //viewModel.ReverseAllDecks();
        }
        private void Copy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cardsInDeckIsActive)
            {
                List<int> indexesOfSelectedItems = new List<int>();
                int i = 0;
                foreach (CardViewModel item in cardsInDeck.Items)
                {
                    if (cardsInDeck.SelectedItems.Contains(item))
                        indexesOfSelectedItems.Add(i);
                    i++;
                }
                viewModel.CopyCardsInBuffer(indexesOfSelectedItems);
            }
            else if (deckListIsActive)
            {
                List<int> indexesOfSelectedDecks = new List<int>();
                int i = 0;
                foreach (DeckViewModel deck in decksList.Items)
                {
                    if (decksList.SelectedItems.Contains(deck))
                        indexesOfSelectedDecks.Add(i);
                    i++;
                }
                viewModel.CopyDecksInBuffer(indexesOfSelectedDecks);
            }
        }
        private void cardsInDeck_GotFocus(object sender, RoutedEventArgs e)
        {
            cardsInDeckIsActive = true;
            e.Handled = true;
        }
        private void cardsInDeck_LostFocus(object sender, RoutedEventArgs e)
        {
            cardsInDeckIsActive = false;
            e.Handled = true;
        }
        private void decksList_GotFocus(object sender, RoutedEventArgs e)
        {
            deckListIsActive = true;
            e.Handled = true;
        }
        private void decksList_LostFocus(object sender, RoutedEventArgs e)
        {
            deckListIsActive = false;
            e.Handled = true;
        }
        private void Paste_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Paste_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cardsInDeckIsActive)
            {
                viewModel.PasteCardsFromBuffer();
            }
            else if (deckListIsActive)
            {
                viewModel.PasteDecksFromBuffer();
            }
        }
        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void DeleteSelectedCards()
        {
            List<int> indexesOfSelectedItems = new List<int>();
            int i = 0;
            foreach (CardViewModel item in cardsInDeck.Items)
            {
                if (cardsInDeck.SelectedItems.Contains(item))
                    indexesOfSelectedItems.Add(i);
                i++;
            }
            viewModel.DeleteSelectedCards(indexesOfSelectedItems);
        }
        private void DeleteSelectedDecks()
        {
            List<int> indexesOfSelectedDecks = new List<int>();
            int i = 0;
            foreach (DeckViewModel deck in decksList.Items)
            {
                if (decksList.SelectedItems.Contains(deck))
                    indexesOfSelectedDecks.Add(i);
                i++;
            }
            viewModel.DeleteSelectedDecks(indexesOfSelectedDecks);
        }
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cardsInDeckIsActive)
                DeleteSelectedCards();
            else if (deckListIsActive)
                DeleteSelectedDecks();
        }
        private void ScrollViewer_GotFocus(object sender, RoutedEventArgs e)
        {
            cardsInDeckIsActive = true;
            e.Handled = true;
        }
        private void ScrollViewer_LostFocus(object sender, RoutedEventArgs e)
        {
            cardsInDeckIsActive = false;
            e.Handled = true;
        }
        private void deckScrollViewer_LostFocus(object sender, RoutedEventArgs e)
        {
            deckListIsActive = false;
            e.Handled = true;
        }
        private void deckScrollViewer_GotFocus(object sender, RoutedEventArgs e)
        {
            deckListIsActive = true;
            e.Handled = true;
        }
        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cardsInDeckIsActive)
                viewModel.AddCardToCurrentDeck();
            else if (deckListIsActive)
                viewModel.AddDeck();
        }
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.ChooseFolder();
        }
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (cardsInDeckIsActive)
                SaveSelectedCards();
            else if (deckListIsActive || sender == deckSaveButton || sender == deckName)
            {
                viewModel.CurrentDeck.SaveRenameThisDeck();
            }
        }
        private void SaveSelectedCards()
        {
            List<int> indexesOfSelectedItems = new List<int>();
            int i = 0;
            foreach (CardViewModel item in cardsInDeck.Items)
            {
                if (cardsInDeck.SelectedItems.Contains(item))
                    indexesOfSelectedItems.Add(i);
                i++;
            }
            viewModel.SaveSelectedCards(indexesOfSelectedItems);
        }
        private void deleteDeckButton_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteSelectedDecks();
        }
        private void deleteCardButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedCards();
        }

        private void newDeckButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddDeck();
        }

        private void deleteDeckButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedDecks();
        }

        private void newCardButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.AddCardToCurrentDeck();
        }

        private void deleteCardButton_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteSelectedCards();
        }

        private void deckScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
        public void TrainigIsFinished()
        {
            viewModel.TrainingIsFinished();
        }
        public event EventHandler TrainingStart;
        public event EventHandler GoToSettings;
        public void OnTrainingStart(TrainingStartEventArgs args)
        {
            EventHandler handler = TrainingStart;
            if (handler != null)
                handler(this, args);
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            EventHandler handler = GoToSettings;
            if (handler != null)
                handler(this, new EventArgs());
        }

        private void ReverseSetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(viewModel!=null)
                viewModel.GeneralReverseSettingsChanged(ReverseSetting.SelectedIndex);
        }
        public bool CheckForUnsavedChanges()
        {
            return viewModel.CheckForUnsavedCards();
        }

        private void findCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            //viewModel.UpdateCardListByCardFindRequest(findCard.Text);
        }

        private void startPage_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        private void ChangePageFocusable(bool value)
        {

        }
        private void startPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardPresentationAndEditingElement.ChangeFocusableOfElements(false);
            this.Focus();            
            cardPresentationAndEditingElement.ChangeFocusableOfElements(true);
        }

        private void importExcel_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel == null)
                return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            viewModel.ImportExcelFileToCurrentDeck(openFileDialog.FileName);
        }

        private void exportEacel_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel == null)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.ShowDialog();
            viewModel.ExportCurrentDeckToExcelFile(saveFileDialog.FileName);
        }

        private void chooseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ChooseFolder();
        }
    }
    public class TrainingStartEventArgs:EventArgs
    {
        public TrainingViewModel trainingViewModel { get; private set; }
        public TrainingStartEventArgs(TrainingViewModel trainingViewModel)
        {
            this.trainingViewModel = trainingViewModel;
        }
    }
}
