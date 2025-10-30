using ClassLibrary1_Prueba;
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace POKEDEX
{
    public sealed partial class Snorlax : UserControl, iPokemon
    {
        public Snorlax()
        {
            this.InitializeComponent();
            this.IsTabStop = true;
            verFondo(true);
        }
        DispatcherTimer dtTime;


        private Boolean est_herido = false;
        private Boolean est_cansado = false;
        private Boolean est_muerto = false;
        private Storyboard anima;

        private string categoria = "Dormir";
        private string tipo = "Normal";
        private double altura = 2.1;
        private double peso = 460.0;
        private string evolucion = "Ninguna";
        private string descripcion = "Snorlax es un pokemon perezoso. Come y duerme todo el día. Sus mayores habilidades son tirar las uñas y provocar terremotos, sus babas funcionan de escudo, cuando se cansa se duerme y recupera energía y vida.";


        public double Vida { get { return this.vida.Value; } set { this.vida.Value = value; } }
        public double Energia { get { return this.energia.Value; } set { this.energia.Value = value; } }
        string iPokemon.Nombre { get { return this.Nombre.Text; } set { this.Nombre.Text = value; } }
        public string Categoría { get { return this.categoria; } set { this.categoria = value; } }
        public string Tipo { get { return this.tipo; } set { this.tipo = value; } }
        public double Altura { get { return this.altura; } set { this.altura = value; } }
        public double Peso { get { return this.peso; } set { this.peso = value; } }
        public string Evolucion { get { return this.evolucion; } set { this.evolucion = value; } }
        public string Descripcion { get { return this.descripcion; } set { this.descripcion = value; } }


        public void verFondo(bool ver)
        {
            if (!ver) { this.GridPrincipal.Background = null; }
            else
            {
                //TODO Poner fondo de nuevo
            }

        }

        public void verFilaVida(bool ver)
        {
            if (!ver) { this.vida.Visibility = Visibility.Collapsed; }
            else
            {
                //TODO Poner fondo de nuevo
            }

        }

        public void verFilaEnergia(bool ver)
        {
            if (!ver) { this.energia.Visibility = Visibility.Collapsed; }
            else
            {
                //TODO Poner fondo de nuevo
            }
        }

        public void verPocionVida(bool ver)
        {
            if (!ver) { this.pocionR.Visibility = Visibility.Collapsed; }
            else
            {
                //TODO Poner fondo de nuevo
            }
        }

        public void verPocionEnergia(bool ver)
        {
            if (!ver) { this.pocionA.Visibility = Visibility.Collapsed; }
            else
            {
                //TODO Poner fondo de nuevo
            }
        }

        public void verNombre(bool ver)
        {
            if (!ver) { this.Nombre.Visibility = Visibility.Collapsed; }
            else
            {
                //TODO Poner fondo de nuevo
            }
        }

        public void activarAniIdle(bool activar)
        {
            if (!activar) { this.VSnorlax.Visibility = Visibility.Collapsed; }
            else
            {
                //TODO Poner fondo de nuevo
            }
        }

        public void animacionAtaqueFlojo()
        {

            a_uñas();
        }

        public void animacionAtaqueFuerte()
        {
            a_terremoto();
        }

        public void animacionDefensa()
        {
            a_escudo();
        }

        public void animacionDescasar()
        {

            a_bostedo_siesta_energia();

        }

        public void animacionCansado()
        {
            a_cansado();
        }

        public void animacionNoCansado()
        {
            a_oriCan();
        }

        public void animacionHerido()
        {
            a_herido();
        }

        public void animacionNoHerido()
        {
            a_oriHer();
        }

        public void animacionDerrota()
        {

            a_derrotado();

        }
        private void subirVida()
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(25);
            dtTime.Tick += increaseHealth;
            dtTime.Start();
        }

        private void usePotionRed(object sender, PointerRoutedEventArgs e)
        {
            subirVida();
            this.pocionR.Opacity = 0.5;
            verPocionVida(false);

        }

        private void increaseHealth(object sender, object e)
        {
            this.vida.Value += 0.5;
            if (vida.Value >= 100)
            {
                this.dtTime.Stop();
                this.pocionR.Opacity = 1;

            }

        }

        //INCREMENTAR ENERGÍA

        private void subirEnergia()
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(25);
            dtTime.Tick += increaseEnergy;
            dtTime.Start();
        }

        private void usePotionYellow(object sender, PointerRoutedEventArgs e)
        {
            subirEnergia();
            this.pocionA.Opacity = 0.5;
            verPocionEnergia(false);
        }

        private void increaseEnergy(object sender, object e)
        {
            this.energia.Value += 0.5;
            if (energia.Value >= 100)
            {
                this.dtTime.Stop();
                this.pocionA.Opacity = 1;
            }

        }






        //animacion1
        private async void a_uñas()
        {
            if (est_muerto == false)
            {
                MediaPlayer mpSonidos1 = new MediaPlayer();
                mpSonidos1.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets_Pokemon_iniciales/uñas.mp3"));
                mpSonidos1.Play();
                await Task.Delay(500);
                anima = (Storyboard)this.Resources["uñas"];
                anima.Begin();
            }


        }
        //animacion2
        private async void a_terremoto()
        {
            if (est_muerto == false)
            {
                anima = (Storyboard)this.Resources["terremoto"];
                anima.Begin();

                MediaPlayer mpSonidos2 = new MediaPlayer();
                mpSonidos2.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets_Pokemon_iniciales/derrumbe.mp3"));
                await Task.Delay(1100);
                mpSonidos2.Play();
                await Task.Delay(2000);
                mpSonidos2.Pause();
            }


        }
        //animacion3
        private void a_escudo()
        {
            if (est_muerto == false)
            {
                MediaPlayer mpSonidos3 = new MediaPlayer();
                mpSonidos3.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets_Pokemon_iniciales/escudo.mp3"));
                mpSonidos3.Play();
                anima = (Storyboard)this.Resources["baba_escudo"];
                anima.Begin();
            }


        }
        //animacion4
        private async void a_cansado()
        {
            if (est_muerto == false)
            {
                if (est_cansado == false)
                {
                    est_cansado = true;
                    anima = (Storyboard)this.Resources["cansado"];
                    anima.Begin();

                    MediaPlayer mpSonidos4 = new MediaPlayer();
                    mpSonidos4.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets_Pokemon_iniciales/aDormir.mp3"));
                    mpSonidos4.Play();
                    await Task.Delay(5450);
                    mpSonidos4.Pause();
                }
            }


        }
        //animacion5
        private async void a_bostedo_siesta_energia()
        {
            if (est_muerto == false)
            {
                if (est_herido == true || est_cansado == true)
                {
                    anima = (Storyboard)this.Resources["bostezo_siesta_energia"];
                    anima.Begin();
                    await Task.Delay(1500);
                    MediaPlayer mpSonidos5 = new MediaPlayer();
                    mpSonidos5.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets_Pokemon_iniciales/roncar.mp3"));
                    mpSonidos5.Play();
                    if (est_herido == true)
                    {
                        a_oriHer();

                    }
                    if (est_cansado == true)
                    {
                        a_oriCan();
                    }
                }
            }




        }
        //animacion6
        private async void a_herido()
        {
            if (est_muerto == false)
            {
                if (est_herido == false)
                {
                    est_herido = true;
                    MediaPlayer mpSonidos6 = new MediaPlayer();
                    mpSonidos6.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets_Pokemon_iniciales/vidaBaja.mp3"));
                    mpSonidos6.Play();
                    await Task.Delay(1000);
                    anima = (Storyboard)this.Resources["herido"];
                    anima.Begin();
                    await Task.Delay(5450);
                    mpSonidos6.Pause();

                }
            }


        }
        //animacion7
        private async void a_derrotado()
        {
            if (est_muerto == false)
            {

                if (est_cansado == true)
                {
                    a_oriCan();
                }
                est_muerto = true;
                MediaPlayer mpSonidos7 = new MediaPlayer();
                mpSonidos7.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets_Pokemon_iniciales/muerte.mp3"));
                mpSonidos7.Play();
                await Task.Delay(800);
                anima = (Storyboard)this.Resources["derrotado"];
                anima.Begin();
            }

        }
        //animacion vuelta a ori desde Cnsado
        private void a_oriCan()
        {
            if (est_muerto == false)
            {
                anima = (Storyboard)this.Resources["oriCan"];
                anima.Begin();
                est_cansado = false;
            }


        }
        //animacion vuelta a ori desde herido
        private void a_oriHer()
        {
            if (est_muerto == false)
            {
                anima = (Storyboard)this.Resources["oriHer"];
                anima.Begin();
                est_herido = false;
            }


        }
    }
}
