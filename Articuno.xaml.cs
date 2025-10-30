using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using ClassLibrary1_Prueba;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace POKEDEX
{
    public sealed partial class Articuno : UserControl, iPokemon
    {
        DispatcherTimer dtRelojVida;
        DispatcherTimer dtRelojEnergia;
        MediaPlayer mpSonidos;
        Storyboard sbIdle;
        Storyboard auxiliar;

        public double Vida { get => this.pbVida.Value; set => this.pbVida.Value = value; }
        public double Energia { get => this.pbEnergia.Value; set => this.pbEnergia.Value = value; }
        public string Nombre { get => "Articuno"; set => throw new NotImplementedException(); }
        public string Categoría { get => "Hielo"; set => throw new NotImplementedException(); }
        public string Tipo { get => "Hielo/Volador"; set => throw new NotImplementedException(); }
        public double Altura { get => Convert.ToDouble(1.7); set => throw new NotImplementedException(); }
        public double Peso { get => Convert.ToDouble(55.4); set => throw new NotImplementedException(); }
        public string Evolucion { get => "No tiene evolución"; set => throw new NotImplementedException(); }
        public string Descripcion
        {
            get => "Articuno es un gran Pokémon legendario aviar con plumaje predominantemente azul y alas que se dice que están hechas de hielo. En su frente hay una cresta que consta de tres plumas azules más oscuras en forma de rombo";
            set => throw new NotImplementedException();
        }
        public Articuno()
        {
            this.InitializeComponent();
            this.IsTabStop = true;
            mpSonidos = new MediaPlayer();

            sbIdle = (Storyboard)this.Resources["moverAlas"];
            sbIdle.RepeatBehavior = RepeatBehavior.Forever;
            sbIdle.Begin();
        }
        private void subirVida_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            dtRelojVida = new DispatcherTimer();
            dtRelojVida.Interval = TimeSpan.FromMilliseconds(100);
            dtRelojVida.Tick += incrementarBarraVida;
            dtRelojVida.Start();
            subirVida.Opacity = 0.5;
        }

        private void incrementarBarraVida(object sender, object e)
        {
            pbVida.Value += 0.2; //Es +=
            if (pbVida.Value >= 100)
            {
                dtRelojVida.Stop();
                subirVida.Opacity = 1;
                this.Focus(FocusState.Programmatic);
            }

        }

        private void subirEnergia_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            dtRelojEnergia = new DispatcherTimer();
            dtRelojEnergia.Interval = TimeSpan.FromMilliseconds(100);
            dtRelojEnergia.Tick += incrementarBarraEnergia;
            dtRelojEnergia.Start();
            subirEnergia.Opacity = 0.5;
        }

        private void incrementarBarraEnergia(object sender, object e)
        {
            pbEnergia.Value += 0.2;
            if (pbEnergia.Value >= 100)
            {
                dtRelojEnergia.Stop();
                subirEnergia.Opacity = 1;
                this.Focus(FocusState.Programmatic);
            }
        }


        public void verFondo(bool ver)
        {
            if (ver) this.gridArticuno.Background.Opacity = 100;
            else this.gridArticuno.Background.Opacity = 0;
        }

        public void verFilaVida(bool ver)
        {
            if (!ver) this.gridArticuno.RowDefinitions[0].Height = new GridLength(0);
            else this.gridArticuno.RowDefinitions[0].Height = new GridLength(10, GridUnitType.Star);
        }

        public void verFilaEnergia(bool ver)
        {
            if (!ver) this.gridArticuno.RowDefinitions[1].Height = new GridLength(0);
            else this.gridArticuno.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
        }

        public void verPocionVida(bool ver)
        {
            if (!ver) this.subirVida.Visibility = Visibility.Collapsed;
            else this.subirVida.Visibility = Visibility.Visible;
        }

        public void verPocionEnergia(bool ver)
        {
            if (!ver) this.subirEnergia.Visibility = Visibility.Collapsed;
            else this.subirEnergia.Visibility = Visibility.Visible;
        }

        public void verNombre(bool ver)
        {
            if (!ver) this.gridArticuno.RowDefinitions[3].Height = new GridLength(0);
            else this.gridArticuno.RowDefinitions[3].Height = new GridLength(10, GridUnitType.Star);
        }

        public void activarAniIdle(bool activar)
        {
            if (activar) sbIdle.Begin();
            else sbIdle.Stop();
        }

        public void animacionAtaqueFlojo()
        {
            auxiliar = (Storyboard)this.Resources["ataqueDebil"];
            auxiliar.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsArticunoACG/CabezazoRalentizado.mp3"));
            mpSonidos.Play();
        }

        public void animacionAtaqueFuerte()
        {
            auxiliar = (Storyboard)this.Resources["ataqueFuerte"];
            auxiliar.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsArticunoACG/RayoHielo.mp3"));
            mpSonidos.Play();
        }

        public void animacionDefensa()
        {
            auxiliar = (Storyboard)this.Resources["defensaAtaque"];
            auxiliar.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsArticunoACG/Escudo.mp3"));
            mpSonidos.Play();
        }

        public void animacionDescasar()
        {
            auxiliar = (Storyboard)this.Resources["recuperandoVidaEnergia"];
            auxiliar.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsArticunoACG/RecuperarVida.mp3"));
            mpSonidos.Play();
        }

        public void animacionCansado()
        {
            auxiliar = (Storyboard)this.Resources["cansado"];
            auxiliar.Begin();
        }

        public void animacionNoCansado()
        {
            auxiliar = (Storyboard)this.Resources["CansadoInversa"];
            auxiliar.Begin();
        }

        public void animacionHerido()
        {
            auxiliar = (Storyboard)this.Resources["herido"];
            auxiliar.Begin();
        }

        public void animacionNoHerido()
        {
            auxiliar = (Storyboard)this.Resources["HeridoInversa"];
            auxiliar.Begin();
        }

        public void animacionDerrota()
        {
            auxiliar = (Storyboard)this.Resources["derrotado"];
            auxiliar.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsArticunoACG/DerrotadoBien.mp3"));
            mpSonidos.Play();
        }
    }
}
