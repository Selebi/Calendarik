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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlanSelector.Controls
{
    /// <summary>
    /// Логика взаимодействия для Plans.xaml
    /// </summary>
    public partial class Plans : UserControl
    {
        public Plans()
        {
            InitializeComponent();
            pao.IsSelected = true;
            CalGrid.Children.Add(calendar);
            calendar.NewState += s => { DescriptionPlan.Text = s; };
        } 

        Calendar calendar = new Calendar();

        private void OpenCalendar(object sender, RoutedEventArgs e)
        {
            if (CalGrid.Visibility != Visibility.Visible)
            {
                ThicknessAnimation OpenFull = new ThicknessAnimation();
                OpenFull.To = new Thickness(0);
                OpenFull.Duration = TimeSpan.FromSeconds(0.5);

                DoubleAnimation OpenHeaderWidth = new DoubleAnimation();
                OpenHeaderWidth.To = 415;
                OpenHeaderWidth.Duration = TimeSpan.FromSeconds(0.5);

                ThicknessAnimation OpenHeader = new ThicknessAnimation();
                OpenHeader.To = new Thickness(0, 0, 0, 400);
                OpenHeader.Duration = TimeSpan.FromSeconds(0.5);
                OpenHeader.Completed += (a, b) =>
                {
                    CalGrid.BeginAnimation(MarginProperty, OpenFull);
                };

                CalGrid.Visibility = Visibility.Visible;
                CalGrid.BeginAnimation(WidthProperty, OpenHeaderWidth);
                CalGrid.BeginAnimation(MarginProperty, OpenHeader);
            }
            else
            {
                DoubleAnimation CloseHeaderWidth = new DoubleAnimation();
                CloseHeaderWidth.To = 5;
                CloseHeaderWidth.Duration = TimeSpan.FromSeconds(0.5);

                ThicknessAnimation CloseHeader = new ThicknessAnimation();
                CloseHeader.To = new Thickness(410, 0, 0, 400);
                CloseHeader.Duration = TimeSpan.FromSeconds(0.5);
                CloseHeader.Completed += (a, b) => { CalGrid.Visibility = Visibility.Hidden; };

                ThicknessAnimation CloseFull = new ThicknessAnimation();
                CloseFull.To = new Thickness(0, 0, 0, 400);
                CloseFull.Duration = TimeSpan.FromSeconds(0.5);
                CloseFull.Completed += (a, b) =>
                {
                    CalGrid.BeginAnimation(MarginProperty, CloseHeader);
                    CalGrid.BeginAnimation(WidthProperty, CloseHeaderWidth);
                };

                CalGrid.BeginAnimation(MarginProperty, CloseFull);
            }
        }
    }
}
