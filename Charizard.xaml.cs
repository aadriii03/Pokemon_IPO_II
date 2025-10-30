using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using ClassLibrary1_Prueba;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace POKEDEX
{
    public sealed partial class Charizard : UserControl, iPokemon
    {
        public double Vida { get { return this.pb_heal.Value; } set { this.pb_heal.Value = value; } }
        public double Energia { get { return this.pb_energy.Value; } set { this.pb_energy.Value = value; } }
        public string Nombre { get { return Nombre; } set { Nombre = value; } }
        public string Categoría { get { return Categoría; } set { Categoría = value; } }
        public string Tipo { get { return Tipo; } set { Tipo = value; } }
        public double Altura { get { return Altura; } set { Altura = value; } }
        public double Peso { get { return Peso; } set { Peso = value; } }
        public string Evolucion { get { return Evolucion; } set { Evolucion = value; } }
        public string Descripcion { get { return Descripcion; } set { Descripcion = value; } }


        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool evolution = false;
        private bool derrotado = false;
        private Storyboard sb_normal;
        private DispatcherTimer dtTime;

        public Charizard()
        {
            this.InitializeComponent();
            this.IsTabStop = true;
            this.sb_normal = (Storyboard)this.Resources["sb_normal"];
            this.sb_normal.RepeatBehavior = RepeatBehavior.Forever;
            this.sb_normal.Begin();
        }
        public void verFondo(bool ver)
        {
            Image background = this.FindName("background") as Image;
            if (ver)
            {
                background.Visibility = Visibility.Visible;
            }
            else
            {
                background.Visibility = Visibility.Collapsed;
            }
        }

        public void verFilaVida(bool ver)
        {
            ProgressBar pb_heal = this.FindName("pb_heal") as ProgressBar;
            if (ver)
            {
                pb_heal.Visibility = Visibility.Visible;
            }
            else
            {
                pb_heal.Visibility = Visibility.Collapsed;
            }
        }

        public void verFilaEnergia(bool ver)
        {
            ProgressBar pb_energy = this.FindName("pb_energy") as ProgressBar;
            if (ver)
            {
                pb_energy.Visibility = Visibility.Visible;
            }
            else
            {
                pb_energy.Visibility = Visibility.Collapsed;
            }
        }

        public void verPocionVida(bool ver)
        {
            Image green_potion = this.FindName("green_potion") as Image;
            if (ver)
            {
                green_potion.Visibility = Visibility.Visible;
            }
            else
            {
                green_potion.Visibility = Visibility.Collapsed;
            }
        }

        public void verPocionEnergia(bool ver)
        {
            Image blue_potion = this.FindName("blue_potion") as Image;
            if (ver)
            {
                blue_potion.Visibility = Visibility.Visible;
            }
            else
            {
                blue_potion.Visibility = Visibility.Collapsed;
            }
        }

        public void verNombre(bool ver)
        {
            TextBlock name = this.FindName("name") as TextBlock;
            if (ver)
            {
                name.Visibility = Visibility.Visible;
            }
            else
            {
                name.Visibility = Visibility.Collapsed;
            }
        }

        public void activarAniIdle(bool activar)
        {
            if (activar)
            {
                this.sb_normal.Begin();
            }
            else
            {
                this.sb_normal.Stop();
            }
        }

        public void animacionAtaqueFlojo()
        {
            this.StartAnimation("sb_garra_dragon");
            this.PlayAudio("ms-appx:///Assets/CharizardASM/audio/garra.mp3");
        }

        public void animacionAtaqueFuerte()
        {
            this.checkEvolution();
            this.StartAnimation("sb_lanza_llamas");
            this.PlayAudio("ms-appx:///Assets/CharizardASM/audio/lanzallamas.mp3");
        }

        public void animacionDefensa()
        {
            if (!evolution)
            {
                this.PlayAudio("ms-appx:///Assets/CharizardASM/audio/evolucion.mp3");
                this.StartAnimation("sb_mega_evolucion");
                this.evolution = true;
            }
        }

        public void animacionDescasar()
        {
            this.checkEvolution();
            this.PlayAudio("ms-appx:///Assets/CharizardASM/audio/mewing.mp3");
            this.StartAnimation("sb_mewing");
        }

        public void animacionCansado()
        {
            this.sb_normal.Stop();
        }

        public void animacionNoCansado()
        {
            this.sb_normal.Begin();
        }

        public void animacionHerido()
        {
            this.sb_normal.Stop();
        }

        public void animacionNoHerido()
        {
            this.PlayAudio("ms-appx:///Assets/CharizardASM/audio/recuperado.mp3");
            this.StartAnimation("sb_levantar");
            derrotado = false;
            green_potion_PointerReleased(null, null);
        }

        public void animacionDerrota()
        {
            this.derrotado = true;
            this.matar(null, null);
            this.StartAnimation("sb_derrotado");
        }
        private void StartAnimation(string storyboardName)
        {
            Storyboard storyboard = (Storyboard)this.Resources[storyboardName];
            if (storyboard != null)
            {
                storyboard.Begin();
            }
        }


        private void PlayAudio(string audioFilePath)
        {
            if (mediaPlayer == null)
            {
                mediaPlayer = new MediaPlayer();
            }

            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri(audioFilePath));
            mediaPlayer.Play();
        }

        async void ShowAlert(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
        private void checkEvolution()
        {

            Image mano_mewing = this.FindName("mano_mewing") as Image;
            Image lanzallamas = this.FindName("lanzallamas") as Image;
            Image cara_mewing = this.FindName("cara_mewing") as Image;

            if (evolution)
            {
                mano_mewing.Source = new BitmapImage(new Uri("ms-appx:///Assets/CharizardASM/mano_mewing_black.png"));
                lanzallamas.Source = new BitmapImage(new Uri("ms-appx:///Assets/CharizardASM/lanzallamas_azul.png"));
                cara_mewing.Source = new BitmapImage(new Uri("ms-appx:///Assets/CharizardASM/mewing_black.png"));
            }
            else
            {
                mano_mewing.Source = new BitmapImage(new Uri("ms-appx:///Assets/CharizardASM/mano_mewing.png"));
                lanzallamas.Source = new BitmapImage(new Uri("ms-appx:///Assets/CharizardASM/lanzallamas.png"));
                cara_mewing.Source = new BitmapImage(new Uri("ms-appx:///Assets/CharizardASM/mewing.png"));
            }
        }

        private void green_potion_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (!this.derrotado)
            {
                dtTime = new DispatcherTimer();
                dtTime.Interval = TimeSpan.FromMilliseconds(100);
                dtTime.Tick += increaseHealth;
                dtTime.Start();
                Image green_potion = this.FindName("green_potion") as Image;
                green_potion.Opacity = 0.5;
            }
        }

        private void increaseHealth(object sender, object e)
        {
            ProgressBar pb_heal = this.FindName("pb_heal") as ProgressBar;
            pb_heal.Value += 5;
            if (pb_heal.Value >= 100)
            {
                this.dtTime.Stop();
                pb_heal.Opacity = 1;
            }
        }

        private void matar(object sender, PointerRoutedEventArgs e)
        {
            if (this.derrotado)
            {
                dtTime = new DispatcherTimer();
                dtTime.Interval = TimeSpan.FromMilliseconds(100);
                dtTime.Tick += decreaseHealth;
                dtTime.Start();
                Image green_potion = this.FindName("green_potion") as Image;
                green_potion.Opacity = 0.5;
            }
        }

        private void decreaseHealth(object sender, object e)
        {
            ProgressBar pb_heal = this.FindName("pb_heal") as ProgressBar;
            pb_heal.Value -= 5;
            if (pb_heal.Value <= 0)
            {
                this.dtTime.Stop();
                pb_heal.Opacity = 1;
            }
        }
    }
}
