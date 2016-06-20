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

/// W czasie projektowania,najpierw mamy otworzyć Solution Explorer i ustawić mediów dla odtwarzaczа wideo.

namespace VideoPlayer
{
    /**
     @class Window1
        <b>Class Windows1</b> zawiera w sobie jeden konstruktor oraz metody do realizacji dzialania aplikacji.
    */
    public partial class Window1 : Window
    {

        /// Standartowy konstruktor classy

        public Window1()
        {
            InitializeComponent();
        }

        /// <b>Tworzenie timera, aby wyświetlić lokalizację wideo.</b>

        public DispatcherTimer timerVideoTime;

        /**
         <b>Tworzenie zegara i rózne przygotowania do pracy Media.</b>
         Ten kod tworzy nowy DispatcherTimer monitorowania postępu filmu.
         Ta metoda kończy swoje dzialanie po wywolaniu metody Stop.
         @note Jeśli pominąć tę metodą, kontrola jest niewidoczna, a pasek nie pokazuje żadnego stanowiska.
            @param sender
            @param e
             */
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(0.1);
            timerVideoTime.Tick += new EventHandler(timer_Tick);
            player.Stop();
        }
        /**
         <i>Obsługa zarzenia jakie jest wykonany, gdy plik multimedialny kończy ładowanie.</i>
         Metoda określa minimalne i maksymalne właściwości formantu pasek przewijania tak, może to stanowić pełny czas trwania filmu.
            @param sender
            @param e
             */
        public void player_MediaOpened(object sender, RoutedEventArgs e)
        {
            sbarPosition.Minimum = 0;
            sbarPosition.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
            sbarPosition.Visibility = Visibility.Visible;
        }
        /**
         <b>Pokazuje pozycję odtwarzania wideo na pasku - ScrollBar i pola tekstowego - TextBox.</b>
         Program korzysta z dwóch metod pomocniczych, ShowPosition i EnableButtons.
         <i>Poniższy kod przedstawia metodę ShowPosition.</i>
         Metoda ta wyświetla wartość MediaElement steruje właściwość Position w ScrollBar i TextBox.
           */

        public void ShowPosition()
        {
            sbarPosition.Value = player.Position.TotalSeconds;
            txtPosition.Text = player.Position.TotalSeconds.ToString("0.0");
        }
        /**
         <b>Włączanie i wyłączanie odpowiednich przycisków.</b>
         <i>Poniższy kod przedstawia metodę EnableButtons (Włącz przyciski).</i>
         Metoda ta umożliwia lub wyłącza przyciski odtwarzania i pauzy w zależności od tego, czy film jest aktualnie odtwarzany. 
         Na przykład, jeśli film jest odtwarzany następnie przycisk Play jest wyłączony, a przycisk pauzy jest włączona.
         Kod ustawia krycie przyciski do 1,0 dla włączonych przycisków (a więc pojawiają się one zwykle) i 0,5 dla przycisków niepełnosprawnych (więc są wyszarzone). 
            @param is_playing
             */
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
        /**
         <b>Poniższy kod pokazuje jak przyciski sterowanie w MediaElement.</b>
         Kod przycisków jest dość oczywisty. Kontrole MediaElement Play metoda rozpoczyna film, jego metody Pause zatrzymuje go, a jego metoda Stop zatrzymuje wideo i przewija się do początku.
         Metoda do ladowania przycisku Open dla otwierania wybranego przez użytkownika filmiku.
            @param sender
            @param e
            @param dlg
            @param filename
             */
        public void btnOpen_Click(object sender, RoutedEventArgs e)
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
        }
        /**
        Metoda do ladowania przycisku Play.
            @param sender
            @param e
       */
        public void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            EnableButtons(true);
        }
        /**
         Metoda do ladowania przycisku Pause.
            @param sender
            @param e
        */
        public void btnPause_Click(object sender, RoutedEventArgs e)
        {
            player.Pause();
            EnableButtons(false);
        }
        /**
         Metoda do ladowania przycisku Stop.
             @param sender
             @param e
            */
        public void btnStop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            EnableButtons(false);
            ShowPosition();
        }
        /**
         Metoda do ladowania przycisku Okay dla ustawienia pozycji otwarzania wideo.
         Jeśli chcesz patszyć film nie od początku lub film nie jest odtwarzany, można wprowadzić czas w polu tekstowym, a następnie kliknij przycisk Ustaw pozycji. Gdy to zrobisz, poniższy kod jest wykonywany.
         Ten kod konwertuje czas wprowadzony do TimeSpan i ustawia właściwość Pozycja gracza do niego. Następnie wywołuje ShowPosition zaktualizować wyświetlanie pozycji.
            @param sender
            @param e
            @param timespan
             */
        public void btnSetPosition_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan timespan = TimeSpan.FromSeconds(double.Parse(txtPosition.Text));
            player.Position = timespan;
            ShowPosition();
        }
        /**
         <b>Ladowanie odtwarzacia pozycji nagranigo wideo.</b>
         Timer Tick obsługi zdarzenia, pokazane w poniższym kodzie, po prostu wywołuje ShowPosition zaktualizować wyświetlanie pozycji.
           @param sender
           @param e
             */
        public void timer_Tick(object sender, EventArgs e)
        {
            ShowPosition();
        }
    }
}