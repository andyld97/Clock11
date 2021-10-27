using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Clock11.Controls
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : ComboBox
    {
        public delegate void colorChanged(Color c);
        public event colorChanged ColorChanged;

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Black));

        public Color SelectedColor
        {
            get => (Color)this.GetValue(SelectedColorProperty);
            set
            {
                this.SetValue(SelectedColorProperty, value);

                var r = (cmb.GetTemplateChild("contentFill1") as Rectangle);
                r.Fill = new SolidColorBrush(value);
            }
        }

        public ColorPicker()
        {
            InitializeComponent();

            cmb.ApplyTemplate();

            var pal = cmb.GetTemplateChild("colorPalette") as ColorPalette;
            pal.OnColorChanged += Pal_OnColorChanged;
        }

        private void Pal_OnColorChanged(Color color)
        {
            // Color ellipse
            cmb.ApplyTemplate();

            var r = (cmb.GetTemplateChild("contentFill1") as Rectangle);
            r.Fill = new SolidColorBrush(color);

            this.IsDropDownOpen = false;
            this.SelectedColor = color;
            this.ColorChanged?.Invoke(color);
        }

        public Button GenerateTemplate()
        {
            return new Button
            {
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(0.6),
                Style = Application.Current.Resources["buttonWithoutSelection"] as Style
            };
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.cmb.IsDropDownOpen = !this.cmb.IsDropDownOpen;
        }
    }
}
