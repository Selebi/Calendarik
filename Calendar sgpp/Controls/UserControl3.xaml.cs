using System;
using System.Windows.Controls;

namespace PlanSelector.Controls
{
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();
            foreach (RadioButtonPlate c in Main.Children)
            {
                if (c.Text == DateTime.Now.Year.ToString())
                {
                    c.IsSelected = true;
                    break;
                }
            }
        }

        public event Action<string> PickedYear;

        private void Checked(RadioButtonPlate obj)
        {
            PickedYear?.Invoke(obj.Text);
        }
    }
}
