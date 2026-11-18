using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LR_2_LAPSHINA_BPI_24_01
{
    public partial class MainWindow : Window
    {
        private Formula_1 form1 = new Formula_1();
        private Formula_2 form2 = new Formula_2();
        private Formula_3 form3 = new Formula_3();
        private Formula_4 form4 = new Formula_4();
        private Formula_5 form5 = new Formula_5();

        public MainWindow()
        {
            InitializeComponent();

            Img1.Source = form1.WpfSourceLight;
            Img2.Source = form2.WpfSourceLight;
            Img3.Source = form3.WpfSourceLight;
            Img4.Source = form4.WpfSourceLight;
            Img5.Source = form5.WpfSourceLight;

            // BtnCalc.Click += Calc_Click;   // ← убрали, чтобы не дублировать с XAML

            Radio1.Checked += editReadOnlyForm1;
            Radio2.Checked += editReadOnlyForm2;
            Radio3.Checked += editReadOnlyForm3;
            Radio4.Checked += editReadOnlyForm4;
            Radio5.Checked += editReadOnlyForm5;

            R1TextA.TextChanged += validateInput;
            R2TextA.TextChanged += validateInput;
            R2TextB.TextChanged += validateInput;
            R3TextA.TextChanged += validateInput;
            R3TextB.TextChanged += validateInput;
            R4TextD.TextChanged += validateInput;
            R4TextA.TextChanged += validateInput;
            R5TextX.TextChanged += validateInput;
            R5TextY.TextChanged += validateInput;
            R5TextN.TextChanged += validateInput;
            R5TextK.TextChanged += validateInput;

            editReadOnlyForm1(null, null);
        }

        // ---------- Переключение темы ----------
        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            App.ToggleTheme();
            if (App.CurrentTheme == App.AppTheme.Dark)
            {
                ThemeToggleButton.Content = "☀";
                Img1.Source = form1.WpfSourceDark;
                Img2.Source = form2.WpfSourceDark;
                Img3.Source = form3.WpfSourceDark;
                Img4.Source = form4.WpfSourceDark;
                Img5.Source = form5.WpfSourceDark;
            }
            else
            {
                ThemeToggleButton.Content = "🌙";
                Img1.Source = form1.WpfSourceLight;
                Img2.Source = form2.WpfSourceLight;
                Img3.Source = form3.WpfSourceLight;
                Img4.Source = form4.WpfSourceLight;
                Img5.Source = form5.WpfSourceLight;
            }
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Formula selected = null;

                if (Radio1.IsChecked == true)
                {
                    form1.A = double.Parse(R1TextA.Text);
                    form1.F = double.Parse(R1ComboF.Text);
                    selected = form1;
                }
                else if (Radio2.IsChecked == true)
                {
                    form2.A = double.Parse(R2TextA.Text);
                    form2.B = double.Parse(R2TextB.Text);
                    form2.F = double.Parse(R2ComboF.Text);
                    selected = form2;
                }
                else if (Radio3.IsChecked == true)
                {
                    form3.A = double.Parse(R3TextA.Text);
                    form3.B = double.Parse(R3TextB.Text);
                    form3.C = double.Parse(R3ComboC.Text);
                    form3.D = double.Parse(R3ComboD.Text);
                    selected = form3;
                }
                else if (Radio4.IsChecked == true)
                {
                    form4.A = double.Parse(R4TextA.Text);
                    form4.D = double.Parse(R4TextD.Text);
                    form4.C = double.Parse(R4ComboC.Text);
                    selected = form4;
                }
                else if (Radio5.IsChecked == true)
                {
                    form5.X = double.Parse(R5TextX.Text);
                    form5.Y = double.Parse(R5TextY.Text);
                    form5.N = int.Parse(R5TextN.Text);
                    form5.K = int.Parse(R5TextK.Text);
                    selected = form5;
                }

                if (selected == null)
                {
                    MessageBox.Show("Выберите формулу!");
                    return;
                }

                double result = selected.Calculate();

                resultBox.Text = "Ответ: " + result.ToString("F4");
                this.Title = "Ответ: " + result.ToString("F4");
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте правильность ввода чисел!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ---------- Валидация (использует ресурсы темы) ----------
        private void validateInput(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.SetResourceReference(TextBox.BackgroundProperty, "NeutralBrush");
                return;
            }

            try
            {
                double.Parse(tb.Text);
                tb.SetResourceReference(TextBox.BackgroundProperty, "OkBrush");
            }
            catch
            {
                tb.SetResourceReference(TextBox.BackgroundProperty, "ErrorBrush");
            }
        }

        private void setAllReadOnly()
        {
            List<TextBox> textBoxes = new List<TextBox>
            {
                R1TextA,
                R2TextA, R2TextB,
                R3TextA, R3TextB,
                R4TextD, R4TextA,
                R5TextX, R5TextY, R5TextN, R5TextK
            };

            foreach (TextBox tb in textBoxes)
            {
                tb.Text = "";
                tb.IsReadOnly = true;
                tb.SetResourceReference(TextBox.BackgroundProperty, "NeutralBrush");
            }
        }

        private void editReadOnlyForm1(object sender, RoutedEventArgs e)
        {
            setAllReadOnly();
            R1TextA.IsReadOnly = false;
        }

        private void editReadOnlyForm2(object sender, RoutedEventArgs e)
        {
            setAllReadOnly();
            R2TextA.IsReadOnly = false;
            R2TextB.IsReadOnly = false;
        }

        private void editReadOnlyForm3(object sender, RoutedEventArgs e)
        {
            setAllReadOnly();
            R3TextA.IsReadOnly = false;
            R3TextB.IsReadOnly = false;
        }

        private void editReadOnlyForm4(object sender, RoutedEventArgs e)
        {
            setAllReadOnly();
            R4TextD.IsReadOnly = false;
            R4TextA.IsReadOnly = false;
        }

        private void editReadOnlyForm5(object sender, RoutedEventArgs e)
        {
            setAllReadOnly();
            R5TextX.IsReadOnly = false;
            R5TextY.IsReadOnly = false;
            R5TextN.IsReadOnly = false;
            R5TextK.IsReadOnly = false;
        }
    }
}
