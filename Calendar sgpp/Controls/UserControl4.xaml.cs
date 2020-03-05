using System;
using System.Windows.Controls;

namespace PlanSelector.Controls
{
    public partial class UserControl4 : UserControl
    {
        public UserControl4()
        {
            InitializeComponent();
            foreach (RadioButtonPlate c in Main.Children)
            {
                if (c.Text == ((int)(DateTime.Now.Month / 3.1 + 1)).ToString() + " Квартал")
                {
                    c.IsSelected = true;
                    break;
                }
            }
        }

        public event Action<string> PickedQuartal;

        private void Checked(RadioButtonPlate obj)
        {
            PickedQuartal?.Invoke(obj.Text);
        }
    }
}
