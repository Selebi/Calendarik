using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PlanSelector.Controls
{
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
            MonthName.TryGetValue(DateTime.Now.Month, out string val);
            foreach (RadioButtonPlate c in Main.Children)
            {
                if (c.Text == val)
                {
                    c.IsSelected = true;
                    break;
                }
            }
        }

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

        public event Action<string> PickedMonth;

        private void MonthChecked(RadioButtonPlate obj)
        {
            PickedMonth?.Invoke(obj.Text);
        }
    }
}
