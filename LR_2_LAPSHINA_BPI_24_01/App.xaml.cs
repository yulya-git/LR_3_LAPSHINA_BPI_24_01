using System;
using System.Windows;

namespace LR_2_LAPSHINA_BPI_24_01
{
    public partial class App : Application
    {
        public enum AppTheme { Light, Dark }

        public static AppTheme CurrentTheme { get; private set; } = AppTheme.Light;

        public static void ApplyTheme(AppTheme theme)
        {
            var dict = new ResourceDictionary();
            switch (theme)
            {
                case AppTheme.Dark:
                    dict.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
                    break;
            }

            var merged = Current.Resources.MergedDictionaries;
            for (int i = merged.Count - 1; i >= 0; i--)
            {
                var src = merged[i].Source?.OriginalString ?? "";
                if (src.Contains("LightTheme.xaml") || src.Contains("DarkTheme.xaml"))
                    merged.RemoveAt(i);
            }

            merged.Add(dict);
            CurrentTheme = theme;
        }

        public static void ToggleTheme()
        {
            ApplyTheme(CurrentTheme == AppTheme.Light ? AppTheme.Dark : AppTheme.Light);
        }
    }
}