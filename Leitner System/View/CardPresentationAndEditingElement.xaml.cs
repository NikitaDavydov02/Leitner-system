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
using Leitner_System.ViewModel;

namespace Leitner_System.View
{
    /// <summary>
    /// Логика взаимодействия для CardPresentationAndEditingElement.xaml
    /// </summary>
    public partial class CardPresentationAndEditingElement : System.Windows.Controls.UserControl
    {
        public CardViewModel viewModel;
        public CardPresentationAndEditingElement()
        {
            InitializeComponent();
            SetViewModel(null);
        }
        public void ChangeOrientationOfTextBoxes()
        {
            Grid.SetColumn(questionTextBox, 0);
            Grid.SetColumn(answerTextBox, 1);

            Grid.SetRow(questionTextBox, 3);
            Grid.SetRow(answerTextBox, 3);

            Grid.SetColumnSpan(questionTextBox, 1);
            Grid.SetColumnSpan(answerTextBox, 1);

            Grid.SetRowSpan(questionTextBox, 2);
            Grid.SetRowSpan(answerTextBox, 2);
        }
        public void SetViewModel(CardViewModel vm)
        {
            viewModel = vm;
            DataContext = viewModel;
        }
        private void chooseQuestionImage_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel == null)
                return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            viewModel.UpdateQuestionImage(openFileDialog.FileName);
        }

        private void chooseAnswerImage_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel == null)
                return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            viewModel.UpdateAnswerImage(openFileDialog.FileName);
        }

        private void saveCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel == null)
                return;
            viewModel.Question = questionTextBox.Text;
            viewModel.Answer = answerTextBox.Text;
            viewModel.SaveThisCard();
            //System.Windows.MessageBox.Show("Карта успешно сохранена");
        }
        public void ChangeTextBoxesFocusable(bool focusable)
        {
        //    questionTextBox.Focusable = focusable;
        //    answerTextBox.Focusable = focusable;
        }

        private void questionTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
        //    e.Handled = true;
        //    questionTextBox.Focusable = true;
        //    this.Focusable = false;
        //    answerTextBox.Focusable = false;
        //    questionTextBox.Focus();
        }

        private void answerTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
        //    e.Handled = true;
        //    answerTextBox.Focusable = true;
        //    this.Focusable = false;
        //    questionTextBox.Focusable = false;
        //    answerTextBox.Focus();
        }

        private void donSaveCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel == null)
                return;
            viewModel.DontSaveCurrentCard();
        }

        private void questionTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //e.Handled = true;
            //questionTextBox.Focusable = true;
            //this.Focusable = false;
            //answerTextBox.Focusable = false;
            //questionTextBox.Focus();
        }

        private void answerTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //e.Handled = true;
            //answerTextBox.Focusable = true;
            //this.Focusable = false;
            //questionTextBox.Focusable = false;
            //answerTextBox.Focus();
        }

        private void mainGrid_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        public void ChangeFocusableOfElements(bool value)
        {
            saveCardButton.Focusable = value;
            donSaveCardButton.Focusable = value;
            chooseAnswerImage.Focusable = value;
            chooseQuestionImage.Focusable = value;
            answerTextBox.Focusable = value;
            questionTextBox.Focusable = value;
        }
        private void mainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeFocusableOfElements(false);
            this.Focus();
            ChangeFocusableOfElements(true);
        }
    }
    class ChangePageBlockingEventArgs : EventArgs
    {
        public bool Enabled { get; private set; }
        public ChangePageBlockingEventArgs(bool enabled)
        {
            Enabled = enabled;
        }
    }
}
