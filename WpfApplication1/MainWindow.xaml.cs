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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

// W czasie projektowania,najpierw mamy otworzyć Solution Explorer i ustawić mediów dla odtwarzaczа wideo.

namespace VideoPlayer
{
    /// <summary>
    /// Logika interakcji dla Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        
        // Tworzenie timera, aby wyświetlić lokalizację wideo.
 
        private DispatcherTimer timerVideoTime;

        // Tworzenie zegara i rózne przygotowania do pracy Media.

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(0.1);
            timerVideoTime.Tick += new EventHandler(timer_Tick);
            player.Stop();
        }

        private void player_MediaOpened(object sender, RoutedEventArgs e)
        {
            sbarPosition.Minimum = 0;
            sbarPosition.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
            sbarPosition.Visibility = Visibility.Visible;
        }

        // Pokaż pozycję odtwarzania wideo na pasku - ScrollBar i pola tekstowego - TextBox.

        private void ShowPosition()
        {
            sbarPosition.Value = player.Position.TotalSeconds;
            txtPosition.Text = player.Position.TotalSeconds.ToString("0.0");
        }

        // Włączanie i wyłączanie odpowiednich przycisków.
        
        public void EnableButtons(bool is_playing)
        {
            if (is_playing)
            {
                btnPlay.IsEnabled = false;
                btnPause.IsEnabled = true;
                btnPlay.Opacity = 0.5;
                btnPause.Opacity = 1.0;
            }
            else
            {
                btnPlay.IsEnabled = true;
                btnPause.IsEnabled = false;
                btnPlay.Opacity = 1.0;
                btnPause.Opacity = 0.5;
            }
            timerVideoTime.IsEnabled = is_playing;
        }

        // Ladowanie przycisku Open dla otwierania wybranego przez użytkownika filmiku.

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".avi",
                Filter = "Video Files (*.avi)|*.avi|All Files (*.*)|*.*",
                CheckFileExists = true
            };

            if (dlg.ShowDialog(this) == true)
            {
                var filename = dlg.FileName;

                player.Source = new Uri(filename);
            }
         //   btnOpen.Visibility = System.Windows.Visibility.Hidden;
        }


        // Ladowanie przycisku Play.

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            EnableButtons(true);
        //    btnPlay.Visibility=System.Windows.Visibility.Hidden;
        }

        // Ladowanie przycisku Pause.

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
            EnableButtons(false);
        //    btnPause.Visibility = System.Windows.Visibility.Hidden;
        }

        // Ladowanie przycisku Stop.

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            EnableButtons(false);
            ShowPosition();
        //    btnStop.Visibility = System.Windows.Visibility.Hidden;
        }

        // Ladowanie odtwarzacia pozycji nagranigo wideo.

        private void timer_Tick(object sender, EventArgs e)
        {
            ShowPosition();
        }

        // Ladowanie przycisku Choose dla ustawienia pozycji otwarzania wideo.

        private void btnSetPosition_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan timespan = TimeSpan.FromSeconds(double.Parse(txtPosition.Text));
            player.Position = timespan;
            ShowPosition();
          //  btnSetPosition.Visibility = System.Windows.Visibility.Hidden;
        }
        /*
        private void player_MouseMove(object sender, MouseEventArgs e)
        {
            btnPlay.Visibility = System.Windows.Visibility.Visible;
        }
        */
    }
}