using System.Windows;
using System.Windows.Controls;

namespace GraphEditor
{
    public class InputBox : Window
    {
        public string Answer { get; private set; }

        public InputBox(string question, string caption = "", string defaultAnswer = "")
        {
            InitializeComponent();
            this.Title = caption;
            this.Question.Text = question;
            this.AnswerBox.Text = defaultAnswer;
        }

        private void InitializeComponent()
        {
            this.Width = 300;
            this.Height = 150;

            var grid = new Grid();
            this.Content = grid;

            // Row definitions
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            // Column definitions
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Question label
            var questionLabel = new TextBlock { Text = "Question:", Margin = new Thickness(10) };
            grid.Children.Add(questionLabel);
            Grid.SetRow(questionLabel, 0);
            Grid.SetColumn(questionLabel, 0);

            // Question text block
            var questionTextBlock = new TextBlock { Text = "", Margin = new Thickness(10) };
            grid.Children.Add(questionTextBlock);
            Grid.SetRow(questionTextBlock, 0);
            Grid.SetColumn(questionTextBlock, 1);
            this.Question = questionTextBlock;

            // Answer label
            var answerLabel = new TextBlock { Text = "Answer:", Margin = new Thickness(10) };
            grid.Children.Add(answerLabel);
            Grid.SetRow(answerLabel, 1);
            Grid.SetColumn(answerLabel, 0);

            // Answer text box
            var answerBox = new TextBox { Margin = new Thickness(10) };
            grid.Children.Add(answerBox);
            Grid.SetRow(answerBox, 1);
            Grid.SetColumn(answerBox, 1);
            this.AnswerBox = answerBox;

            // OK button
            var okButton = new Button { Content = "OK", Margin = new Thickness(10) };
            grid.Children.Add(okButton);
            Grid.SetRow(okButton, 2);
            Grid.SetColumn(okButton, 0);
            okButton.Click += (sender, e) => { this.Answer = this.AnswerBox.Text; this.DialogResult = true; };

            // Cancel button
            var cancelButton = new Button { Content = "Cancel", Margin = new Thickness(10) };
            grid.Children.Add(cancelButton);
            Grid.SetRow(cancelButton, 2);
            Grid.SetColumn(cancelButton, 1);
            cancelButton.Click += (sender, e) => { this.DialogResult = false; };
        }

        private TextBlock Question;
        private TextBox AnswerBox;
    }
}
