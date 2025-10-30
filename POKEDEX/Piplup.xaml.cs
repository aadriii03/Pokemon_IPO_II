using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI;
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
    public sealed partial class Piplup : UserControl, iPokemon
    {
        DispatcherTimer dtTimeVida;
        DispatcherTimer dtTimeEnergia;

        public Piplup()
        {
            this.InitializeComponent();
            verNombre(true);
        }
        public double Vida { get { return this.barra_vida.Value; } set { this.barra_vida.Value = value; } }
        public double Energia { get { return this.barra_ataque.Value; } set { this.barra_ataque.Value = value; } }
        public string Nombre { get { return txtNombre.Text; } set { txtNombre.Text = value; } }
        public string Categoría { get { return categoría; } set { categoría = value; } }
        public string Tipo { get { return tipo; } set { tipo = value; } }
        public double Altura { get { return altura; } set { altura = value; } }
        public double Peso { get { return peso; } set { peso = value; } }
        public string Evolucion { get { return evolucion; } set { evolucion = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        private string categoría = "Pingüino";
        private string tipo = "Agua";
        private double altura = 0.4;
        private double peso = 5.2;
        private string evolucion = "Prinplup";
        private string descripcion = "Vive en las costas de los países nórdicos. Es un gran nadador y puede bucear más de 10 minutos";

        private bool verVida = true;
        private bool verEnergia = true;


        /******************/
        /* Metodos de la Interfaz */

        public void verFondo(bool ver)
        {

            if (!ver) { this.img_fondo.Opacity = 0; }
            else
            {
                this.img_fondo.Opacity = 100;
            }
        }

        public void verFilaVida(bool ver)
        {
            VerVida = ver;
        }

        public void verFilaEnergia(bool ver)
        {
            VerEnergia = ver;
        }

        public void verPocionVida(bool ver)
        {
            if (!ver) { this.pocion_vida.Opacity = 0; }
            else
            {
                this.pocion_vida.Opacity = 100;
            }
        }

        public void verPocionEnergia(bool ver)
        {
            if (!ver) { this.pocion_energia.Opacity = 0; }
            else
            {
                this.pocion_energia.Opacity = 100;
            }
        }

        public void verNombre(bool ver)
        {
            if (!ver) { this.txtNombre.Opacity = 0; }
            else
            {
                this.txtNombre.Opacity = 100;
            }
        }

        public void activarAniIdle(bool activar)
        {
            throw new NotImplementedException();
        }

        public void animacionAtaqueFlojo()
        {
            AnimaAtaqFlojo_AuxAsync();
        }

        public void animacionAtaqueFuerte()
        {
            AnimaAtaqFuerte_AuxAsync();
        }

        public void animacionDefensa()
        {
            AnimaDefensa_AuxAsync();
        }

        public void animacionDescasar()
        {
            AnimaDescansar_AuxAsync();
        }

        public void animacionCansado()
        {
            AnimaCansado_AuxAsync();
        }

        public void animacionNoCansado()
        {
            SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);
            circulo_izq.Fill = whiteBrush;
            circulo_der.Fill = whiteBrush;
        }

        public void animacionHerido()
        {
            AnimaHerido_AuxAsync();
        }

        public void animacionNoHerido()
        {
            imagen_lloro1.Visibility = Visibility.Collapsed;
            imagen_lloro2.Visibility = Visibility.Collapsed;
            imagen_herida1.Visibility = Visibility.Collapsed;
            imagen_herida2.Visibility = Visibility.Collapsed;
            imagen_herida3.Visibility = Visibility.Collapsed;
            SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);
            circulo_izq.Fill = whiteBrush;
            circulo_der.Fill = whiteBrush;
        }

        public void animacionDerrota()
        {
            AnimaDerrota_AuxAsync();
        }

        /******************/
        /* Metodos Auxiliares Interfaz */

        public async Task ActivarAniIdle()
        {
            throw new NotImplementedException();
        }
        public async Task AnimaAtaqFlojo_AuxAsync()
        {
            Storyboard anima;
            MediaPlayer mpSonidos2 = new MediaPlayer();
            mpSonidos2.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsPiplupMLTN/sonido_surf.mp3"));
            mpSonidos2.Play();
            await Task.Delay(200);
            anima = (Storyboard)this.Resources["animacion_conjunta"];
            anima.Begin();

            await Task.Delay(3500);
            mpSonidos2.Pause();

        }
        public async Task AnimaAtaqFuerte_AuxAsync()
        {
            Storyboard anima;
            MediaPlayer mpSonidos = new MediaPlayer();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsPiplupMLTN/sonido_frioPolar.mp3"));
            mpSonidos.Play();
            await Task.Delay(200);
            anima = (Storyboard)this.Resources["ataqueFuerte_FrioPolar"];
            anima.Begin();

            await Task.Delay(5000);
            mpSonidos.Pause();
        }
        public async Task AnimaDefensa_AuxAsync()
        {
            Storyboard anima;
            MediaPlayer mpSonidos3 = new MediaPlayer();
            mpSonidos3.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsPiplupMLTN/sonido_escudo.mp3"));
            mpSonidos3.Play();
            await Task.Delay(500);
            anima = (Storyboard)this.Resources["defensa_escudo"];
            anima.Begin();

            await Task.Delay(4000);
            mpSonidos3.Pause();
        }
        public async Task AnimaDescansar_AuxAsync()
        {
            Storyboard anima;
            anima = (Storyboard)this.Resources["Descanso"];

            anima.Begin();
            await Task.Delay(2000);

            MediaPlayer mpSonidos7 = new MediaPlayer();

            mpSonidos7.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsPiplupMLTN/sonido_descanso.mp3"));
            mpSonidos7.Play();

            SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
            circulo_izq.Fill = greenBrush;
            circulo_der.Fill = greenBrush;

            await Task.Delay(5000);

            SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);
            circulo_izq.Fill = whiteBrush;
            circulo_der.Fill = whiteBrush;
        }

        public async Task AnimaCansado_AuxAsync()
        {
            Storyboard anima;
            MediaPlayer mpSonidos4 = new MediaPlayer();
            mpSonidos4.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsPiplupMLTN/sonido_cansado.mp3"));
            mpSonidos4.Play();
            anima = (Storyboard)this.Resources["Estado_cansado"];
            anima.Begin();
            await Task.Delay(3000);
        }

        public async Task AnimaHerido_AuxAsync()
        {
            Storyboard anima;
            MediaPlayer mpSonidos4 = new MediaPlayer();
            mpSonidos4.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsPiplupMLTN/sonido_herir.m4a"));
            mpSonidos4.Play();
            anima = (Storyboard)this.Resources["Estado_Herido"];
            anima.Begin();
            await Task.Delay(4000);
        }
        public async Task AnimaDerrota_AuxAsync()
        {
            Storyboard anima;
            MediaPlayer mpSonidos4 = new MediaPlayer();
            mpSonidos4.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsPiplupMLTN/sonido_derrotado.mp3"));
            mpSonidos4.Play();

            anima = (Storyboard)this.Resources["estado_derrotado"];
            anima.Begin();
            await Task.Delay(3000);
        }

        public bool VerEnergia
        {
            get { return verEnergia; }
            set
            {
                this.verEnergia = value;
                if (!verEnergia) this.Grid_General.RowDefinitions[1].Height = new GridLength(0);
                else this.Grid_General.RowDefinitions[1].Height = new GridLength(10, GridUnitType.Star);
            }
        }
        public bool VerVida
        {
            get { return verVida; }
            set
            {
                this.verVida = value;
                if (!verVida) this.Grid_General.RowDefinitions[0].Height = new GridLength(0);
                else this.Grid_General.RowDefinitions[0].Height = new GridLength(10, GridUnitType.Star);
            }
        }
        private void pocion_vida_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            dtTimeVida = new DispatcherTimer();
            dtTimeVida.Interval = TimeSpan.FromMilliseconds(100);
            dtTimeVida.Tick += increaseHealth;
            dtTimeVida.Start();
            this.pocion_vida.Opacity = 0.5;
        }

        private void increaseHealth(object sender, object e)
        {
            this.barra_vida.Value += 1;
            if (barra_vida.Value >= 100)
            {
                this.dtTimeVida.Stop();
                this.pocion_vida.Opacity = 1;
                this.Focus(FocusState.Programmatic);
                verPocionVida(false);
                this.pocion_vida.Visibility = Visibility.Collapsed;
            }
        }

        private void pocion_energia_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            dtTimeEnergia = new DispatcherTimer();
            dtTimeEnergia.Interval = TimeSpan.FromMilliseconds(100);
            dtTimeEnergia.Tick += increaseEnergy;
            dtTimeEnergia.Start();
            this.pocion_energia.Opacity = 0.5;
        }
        private void increaseEnergy(object sender, object e)
        {
            this.barra_ataque.Value += 1;
            if (barra_ataque.Value >= 100)
            {
                this.dtTimeEnergia.Stop();
                this.pocion_energia.Opacity = 1;
                this.Focus(FocusState.Programmatic);
                verPocionEnergia(false);
                this.pocion_energia.Visibility = Visibility.Collapsed;
            }
        }
    }
}
