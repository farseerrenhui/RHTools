using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RHTools
{
    public partial class BindingSample : Window
    {
        public BindingSample()
        {
            InitializeComponent();

            Binding bindingName1 = new Binding("Text") { Source = name1 };
            Binding bindingName2 = new Binding("Text") { Source = name2 };
            Binding bindingPwd1 = new Binding("Text") { Source = pwd1 };
            Binding bindingPwd2 = new Binding("Text") { Source = pwd2 };
            MultiBinding mb = new MultiBinding() { Mode = BindingMode.OneWay };
            mb.Bindings.Add(bindingName1);
            mb.Bindings.Add(bindingName2);
            mb.Bindings.Add(bindingPwd1);
            mb.Bindings.Add(bindingPwd2);
            mb.Converter = new LoginBindingConvert();
            this.submitButton.SetBinding(Button.IsEnabledProperty, mb);

            Binding sliderBinding = new Binding("Value") { Source = slider };
            RangeValidationRule rvr = new RangeValidationRule();
            rvr.ValidatesOnTargetUpdated = true;
            sliderBinding.ValidationRules.Add(rvr);
            sliderBinding.NotifyOnValidationError = true;
            this.sliderTextbox.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(ValidationError));
            sliderTextbox.SetBinding(TextBox.TextProperty, sliderBinding);

            Binding _sliderBinding = new Binding("Text") { Source = _sliderTextbox };
            RangeValidationRule _rvr = new RangeValidationRule();
            _sliderBinding.ValidationRules.Add(_rvr);
            _slider.SetBinding(Slider.ValueProperty, _sliderBinding);
        }

        void ValidationError(object sender, RoutedEventArgs args)
        {
            if (Validation.GetErrors(this.sliderTextbox).Count > 0)
                this.sliderTextbox.ToolTip = Validation.GetErrors(this.sliderTextbox)[0].ErrorContent.ToString();
        }
    }

    class LoginBindingConvert : IMultiValueConverter
    {
        #region IMultiValueConverter 成员

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!values.Cast<string>().Any<string>(text => string.IsNullOrEmpty(text)))
            {
                if (values[0].ToString().Equals(values[1].ToString()) && values[2].ToString().Equals(values[3].ToString()))
                    return true;
            }
            return false;
        }

        //不会被调用
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class RangeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double d = 0;
            if (double.TryParse(value.ToString(), out d))
            {
                if (d >= 0 && d <= 100)
                    return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "数据验证失败");
        }
    }
}
