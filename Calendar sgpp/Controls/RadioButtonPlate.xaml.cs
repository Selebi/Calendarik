using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PlanSelector.Controls
{
    public partial class RadioButtonPlate : UserControl, INotifyPropertyChanged
    {
        public RadioButtonPlate()
        {
            InitializeComponent();
            DataContext = this;
            SelectColor = Colors.Aqua;
        }

        public enum Type
        {
            RadioButton,
            Button,
            CheckBox
        }

        #region Свойства

        public Type TypeControl { get; set; } = Type.RadioButton;

        string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        double _height;
        public new double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        double _width;
        public new double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        double _fontSize;
        public new double FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                OnPropertyChanged();
            }
        }

        SolidColorBrush _selectBrush;
        public SolidColorBrush SelectBrush
        {
            get => _selectBrush;
            set
            {
                _selectBrush = value;
                OnPropertyChanged();
            }
        }

        SolidColorBrush _mouseOverBrush;
        public SolidColorBrush MouseOverBrush
        {
            get => _mouseOverBrush;
            set
            {
                _mouseOverBrush = value;
                OnPropertyChanged();
            }
        }

        public Color SelectColor
        {
            set
            {
                SelectBrush = new SolidColorBrush(value);
                MouseOverBrush = new SolidColorBrush(new Color() { A = 32, R = value.R, G = value.G, B = value.B });
            }
        }

        bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    if (!value)
                        StartUnSelectAnimation();
                }
            }
        }

        #endregion

        #region События мышиные

        private new void MouseEnterEvent(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsSelected)
            {
                StartMouseEnterAnimation();
            }
        }

        private new void MouseLeaveEvent(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsSelected)
            {
                StartMouseLeaveAnimation();
            }
        }

        private new void MouseDownEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!IsSelected)
            {
                if (TypeControl == Type.RadioButton)
                    UnselectNeighbors();
                IsSelected = true;
                StartMouseLeaveAnimation();
                StartSelectAnimation();
            }
        }

        #endregion

        #region Анимация

        private void StartMouseEnterAnimation()
        {
            DoubleAnimation HeightAnim = new DoubleAnimation()
            {
                From = 0,
                To = this.ActualHeight,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            DoubleAnimation WidthAnim = new DoubleAnimation()
            {
                From = 0,
                To = this.ActualWidth,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            DoubleAnimation OpacityAnim = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            MouseOverBorder.BeginAnimation(HeightProperty, HeightAnim);
            MouseOverBorder.BeginAnimation(WidthProperty, WidthAnim);
            MouseOverBorder.BeginAnimation(OpacityProperty, OpacityAnim);
        }

        private void StartMouseLeaveAnimation()
        {
            DoubleAnimation HeightAnim = new DoubleAnimation()
            {
                From = this.ActualHeight,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            DoubleAnimation WidthAnim = new DoubleAnimation()
            {
                From = this.ActualWidth,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            DoubleAnimation OpacityAnim = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            MouseOverBorder.BeginAnimation(HeightProperty, HeightAnim);
            MouseOverBorder.BeginAnimation(WidthProperty, WidthAnim);
            MouseOverBorder.BeginAnimation(OpacityProperty, OpacityAnim);
        }

        private void StartSelectAnimation()
        {
            DoubleAnimation HeightAnim = new DoubleAnimation()
            {
                From = 0,
                To = this.ActualHeight,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            DoubleAnimation WidthAnim = new DoubleAnimation()
            {
                From = 0,
                To = this.ActualWidth,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            DoubleAnimation OpacityAnim = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            ColorAnimation TextAnim = new ColorAnimation()
            {
                To = Colors.White,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            SelectBorder.BeginAnimation(HeightProperty, HeightAnim);
            SelectBorder.BeginAnimation(WidthProperty, WidthAnim);
            SelectBorder.BeginAnimation(OpacityProperty, OpacityAnim);
            Textblock.BeginAnimation(TextBlock.ForegroundProperty, TextAnim);
        }

        private void StartUnSelectAnimation()
        {
            DoubleAnimation HeightAnim = new DoubleAnimation()
            {
                From = this.ActualHeight,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            DoubleAnimation WidthAnim = new DoubleAnimation()
            {
                From = this.ActualWidth,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            DoubleAnimation OpacityAnim = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            ColorAnimation TextAnim = new ColorAnimation()
            {
                To = Colors.Black,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            SelectBorder.BeginAnimation(HeightProperty, HeightAnim);
            SelectBorder.BeginAnimation(WidthProperty, WidthAnim);
            SelectBorder.BeginAnimation(OpacityProperty, OpacityAnim);
            Textblock.BeginAnimation(ForegroundProperty, TextAnim);
        }

        #endregion

        private void UnselectNeighbors()
        {
            if (this.Parent is Grid g)
            {
                foreach(var c in g.Children)
                {
                    if (c is RadioButtonPlate rb)
                    {
                        if (rb.TypeControl == Type.RadioButton)
                            rb.IsSelected = false;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
