using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PlanSelector.Controls
{
    public partial class Calendar : UserControl
    {
        public Calendar()
        {
            InitializeComponent();
            Year.Text = DateTime.Now.Year.ToString();
            Day.Text = DateTime.Now.Day.ToString();
            MonthName.TryGetValue(DateTime.Now.Month, out string monthName);
            Month.Text = monthName;
            closeO.Completed += CloseO_Completed;
        }

        public event Action<string> NewState;

        DoubleAnimation closeO = new DoubleAnimation()
        {
            From = 1,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.15)
        };
        DoubleAnimation openO = new DoubleAnimation()
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.15)
        };

        private object currenPanel;

        private Dictionary<int, string> MonthName = new Dictionary<int, string>()
        {
            { 1, "Январь" },
            { 2, "Февраль" },
            { 3, "Март" },
            { 4, "Апрель" },
            { 5, "Май" },
            { 6, "Июнь" },
            { 7, "Июль" },
            { 8, "Август" },
            { 9, "Сентябрь" },
            { 10, "Октябрь" },
            { 11, "Ноябрь" },
            { 12, "Декабрь" }
        };

        private void NewPanel(object obj)
        {
            currenPanel = obj;

            if (PlanTypeGrid.Children.Count > 0)
            {
                UserControl el = (UserControl)PlanTypeGrid.Children[0];
                el.BeginAnimation(OpacityProperty, closeO);
            }
            else
            {
                CloseO_Completed(null, null);
            }

        }

        private void CloseO_Completed(object sender, EventArgs e)
        {
            PlanTypeGrid.Children.Clear();

            UserControl el = (UserControl)currenPanel;
            el.Opacity = 0;
            PlanTypeGrid.Children.Add(el);
            el.BeginAnimation(OpacityProperty, openO);
        }

        private void NewVariant(string obj)
        {
            NewState?.Invoke(obj);
        }
    }
}
