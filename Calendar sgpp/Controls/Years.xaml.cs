using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PlanSelector.Controls
{
    /// <summary>
    /// Костылестроение наше всё. Хотя это жэ демка, это временно, наверное...
    /// </summary>
    public partial class Years : UserControl
    {
        public Years()
        {
            InitializeComponent();
            CurrentYear = DateTime.Now.Year;
            currentYearBlock = new TextBlock()
            {
                Text = CurrentYear.ToString(),
                FontSize = 20,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextWrapping = TextWrapping.NoWrap
            };
            YearGrid.Children.Add(currentYearBlock);
            YearGrid.UpdateLayout();
        }

        public int CurrentYear { get; set; }
        double duration = 0.2;
        TextBlock currentYearBlock;

        private void NextYear(object sender, RoutedEventArgs e)
        {
            var year = new TextBlock()
            {
                Text = (CurrentYear + 1).ToString(),
                FontSize = 20,
                TextWrapping = TextWrapping.NoWrap,
                Margin = new Thickness(360, 8, -15, 8)
            };

            currentYearBlock.Margin = new Thickness((YearGrid.ActualWidth / 2) - (currentYearBlock.ActualWidth / 2) + 6, 8, 0, 8);
            currentYearBlock.HorizontalAlignment = HorizontalAlignment.Left;
            YearGrid.Children.Add(year);
            YearGrid.UpdateLayout();

            ThicknessAnimation NextYearAnimation = new ThicknessAnimation();
            NextYearAnimation.To = new Thickness((YearGrid.ActualWidth / 2) - (year.ActualWidth / 2) + 6, 8, 0, 8);
            NextYearAnimation.Duration = TimeSpan.FromSeconds(duration);

            NextYearAnimation.Completed += (a, b) =>
            {
                year.Margin = new Thickness(0);
                year.HorizontalAlignment = HorizontalAlignment.Left;
                year.VerticalAlignment = VerticalAlignment.Center;
                YearGrid.Children.Remove(currentYearBlock);
                currentYearBlock = year;
                CurrentYear++;
            };

            DoubleAnimation NextYearAnimationOpacity = new DoubleAnimation();
            NextYearAnimationOpacity.From = 0;
            NextYearAnimationOpacity.To = 1;
            NextYearAnimationOpacity.Duration = TimeSpan.FromSeconds(duration);

            ThicknessAnimation CurrentYearAnimation = new ThicknessAnimation();
            CurrentYearAnimation.To = new Thickness(0, 8, 0, 8);
            CurrentYearAnimation.Duration = TimeSpan.FromSeconds(duration);

            DoubleAnimation CurrentYearAnimationOpacity = new DoubleAnimation();
            CurrentYearAnimationOpacity.From = 1;
            CurrentYearAnimationOpacity.To = 0;
            CurrentYearAnimationOpacity.Duration = TimeSpan.FromSeconds(duration);

            currentYearBlock.BeginAnimation(MarginProperty, CurrentYearAnimation);
            currentYearBlock.BeginAnimation(OpacityProperty, CurrentYearAnimationOpacity);
            year.BeginAnimation(MarginProperty, NextYearAnimation);
            year.BeginAnimation(OpacityProperty, NextYearAnimationOpacity);
        }

        private void PreviousYear(object sender, RoutedEventArgs e)
        {
            var year = new TextBlock()
            {
                Text = (CurrentYear - 1).ToString(),
                FontSize = 20,
                TextWrapping = TextWrapping.NoWrap,
                Margin = new Thickness(-15, 8, 360, 8)
            };

            currentYearBlock.Margin = new Thickness((YearGrid.ActualWidth / 2) - (currentYearBlock.ActualWidth / 2) + 6, 8, 0, 8);
            currentYearBlock.HorizontalAlignment = HorizontalAlignment.Left;
            YearGrid.Children.Add(year);
            YearGrid.UpdateLayout();

            ThicknessAnimation PreviousYearAnimation = new ThicknessAnimation();
            PreviousYearAnimation.To = new Thickness((YearGrid.ActualWidth / 2) - (year.ActualWidth / 2) + 6, 8, 0, 8);
            PreviousYearAnimation.Duration = TimeSpan.FromSeconds(duration);

            PreviousYearAnimation.Completed += (a, b) =>
            {
                year.Margin = new Thickness(0);
                year.HorizontalAlignment = HorizontalAlignment.Left;
                year.VerticalAlignment = VerticalAlignment.Center;
                YearGrid.Children.Remove(currentYearBlock);
                currentYearBlock = year;
                CurrentYear--;
            };

            DoubleAnimation PreviousYearAnimationOpacity = new DoubleAnimation();
            PreviousYearAnimationOpacity.From = 0;
            PreviousYearAnimationOpacity.To = 1;
            PreviousYearAnimationOpacity.Duration = TimeSpan.FromSeconds(duration);

            ThicknessAnimation CurrentYearAnimation = new ThicknessAnimation();
            CurrentYearAnimation.To = new Thickness(400, 8, -15, 8);
            CurrentYearAnimation.Duration = TimeSpan.FromSeconds(duration);

            DoubleAnimation CurrentYearAnimationOpacity = new DoubleAnimation();
            CurrentYearAnimationOpacity.From = 1;
            CurrentYearAnimationOpacity.To = 0;
            CurrentYearAnimationOpacity.Duration = TimeSpan.FromSeconds(duration);

            currentYearBlock.BeginAnimation(MarginProperty, CurrentYearAnimation);
            currentYearBlock.BeginAnimation(OpacityProperty, CurrentYearAnimationOpacity);
            year.BeginAnimation(MarginProperty, PreviousYearAnimation);
            year.BeginAnimation(OpacityProperty, PreviousYearAnimationOpacity);
        }
    }
}
