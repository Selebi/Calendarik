using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PlanSelector.Controls
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            Panels.Add("Месячный", MonthPlan);
            Panels.Add("Годовой", YearPlan);
            Panels.Add("Квартальный", QuartalPlan);

            MonthPlan.PickedMonth += s => 
                { SelectedMonth = s; CreateStateString(); };
            YearPlan.PickedYear += s => 
                { SelectedYear = s; CreateStateString(); };
            QuartalPlan.PickedQuartal += s => 
                { SelectedQuartal = s; CreateStateString(); };

            MonthName.TryGetValue(DateTime.Now.Month, out string val);
            SelectedMonth = val;
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

        string SelectedMonth = "";
        string SelectedQuartal = ((int)(DateTime.Now.Month / 3.1 + 1)).ToString() + " Квартал";
        string SelectedYear = DateTime.Now.Year.ToString();
        string SelectedType = "";

        public event Action<object> NewPanel;
        public event Action<string> NewVariant;

        UserControl2 MonthPlan = new UserControl2();
        UserControl3 YearPlan = new UserControl3();
        UserControl4 QuartalPlan = new UserControl4();

        private Dictionary<string, object> Panels = new Dictionary<string, object>();

        private void Checked(RadioButtonPlate obj)
        {
            if (Panels.TryGetValue(obj.Text, out object panel))
            {
                SelectedType = obj.Text;
                NewPanel?.Invoke(panel);
                CreateStateString();
            }
        }

        private void CreateStateString()
        {
            string result = "";
            switch (SelectedType)
            {
                case ("Годовой"):
                    result = $"Годовые варианты плана за {SelectedYear} год.";
                    break;
                case ("Квартальный"):
                    result = $"Квартальные варианты плана за {SelectedQuartal}, {SelectedYear} год.";
                    break;
                case ("Месячный"):
                    result = $"Месячные варианты плана за {SelectedMonth}, {SelectedYear} год.";
                    break;
            }
            NewVariant?.Invoke(result);
        }
    }
}
