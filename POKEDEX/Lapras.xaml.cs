using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
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
    public sealed partial class Lapras : UserControl, iPokemon
    {
        public Lapras()
        {
            this.InitializeComponent();
            IniciarStoryboards();
            debilStoryboard.Completed += DebilStoryboard_Completed;
            fuerteStoryboard.Completed += FuerteStoryboard_Completed;
            descansoStoryboard.Completed += DescansoStoryboard_Completed;
            escudoStoryboard.Completed += EscudoStoryboard_Completed;

            // Iniciar el Storyboard "iddle" por defecto
            iddleStoryboard.Begin();
        }
        bool estadoHerido = false;
        bool estadoCansado = false;
        string Lapras_Categoría = "Transporte";
        string Lapras_Tipo = "Agua/Hielo";
        double Lapras_Altura = 2.5;
        double Lapras_Peso = 220.0;
        string Lapras_Evolucion = "Ninguna";
        string Lapras_Descripcion = "Lapras es un Pokémon que vive en los mares. Le encanta surcar los mares con gente y Pokémon en su lomo, el cual resulta muy cómodo.";

        Storyboard iddleStoryboard;
        Storyboard debilStoryboard;
        Storyboard fuerteStoryboard;
        Storyboard heridoEstado;
        Storyboard finalHeridoEstado;
        Storyboard finalCansadoEstado;
        Storyboard cansadoEstado;
        Storyboard descansoStoryboard;
        Storyboard escudoStoryboard;
        Storyboard muerteEstado;
        Storyboard caraCansadoHerido;
        Storyboard finalCaraCansadoHerido;

        //Sonidos
        MediaPlayerElement sonidos = new MediaPlayerElement();
        private void IniciarStoryboards()
        {
            iddleStoryboard = (Storyboard)this.Resources["iddle"];
            debilStoryboard = (Storyboard)this.Resources["ataque_debil"];
            fuerteStoryboard = (Storyboard)this.Resources["ataque_fuerte"];
            heridoEstado = (Storyboard)this.Resources["estado_herido"];
            finalHeridoEstado = (Storyboard)this.Resources["final_estado_herido"];
            finalCansadoEstado = (Storyboard)this.Resources["final_estado_cansado"];
            cansadoEstado = (Storyboard)this.Resources["estado_cansado"];
            descansoStoryboard = (Storyboard)this.Resources["descanso"];
            escudoStoryboard = (Storyboard)this.Resources["escudo"];
            muerteEstado = (Storyboard)this.Resources["muerte"];
            caraCansadoHerido = (Storyboard)this.Resources["cara_cansado_herido"]; ;
            finalCaraCansadoHerido = (Storyboard)this.Resources["final_cara_cansado_herido"]; ;
        }

        //Getters y setters

        public double Vida
        {
            get { return this.pb_heart.Value; }
            set { this.pb_heart.Value = value; }
        }

        public double Energia
        {
            get { return this.pb_energy.Value; }
            set { this.pb_energy.Value = value; }
        }

        public string Nombre
        {
            get { return this.name.Text; }
            set { this.name.Text = value; }
        }

        public string Categoría
        {
            get { return this.Lapras_Categoría; }
            set { this.Lapras_Categoría = value; }
        }

        public string Tipo
        {
            get { return this.Lapras_Tipo; }
            set { this.Lapras_Tipo = value; }
        }

        public double Altura
        {
            get { return this.Lapras_Altura; }
            set { this.Lapras_Altura = value; }
        }

        public double Peso
        {
            get { return this.Lapras_Peso; }
            set { this.Lapras_Peso = value; }
        }

        public string Evolucion
        {
            get { return this.Lapras_Evolucion; }
            set { this.Lapras_Evolucion = value; }
        }
        public string Descripcion
        {
            get { return this.Lapras_Descripcion; }
            set { this.Lapras_Descripcion = value; }
        }

        // Funciones para mostrar/ocultar elementos

        public void verFondo(bool ver)
        {
            if (ver)
                this.Fondo.Opacity = 1.0;
            else
                this.Fondo.Opacity = 0.0;

        }

        public void verFilaVida(bool ver)
        {
            if (ver)
            {
                this.pb_heart.Opacity = 1.0;
                this.heartIcon.Opacity = 1.0;
                this.heartPotion.Opacity = 1.0;
            }

            else
            {
                this.pb_heart.Opacity = 0.0;
                this.heartIcon.Opacity = 0.0;
                this.heartPotion.Opacity = 0.0;
            }

        }

        public void verFilaEnergia(bool ver)
        {
            if (ver)
            {
                this.pb_energy.Opacity = 1.0;
                this.energyIcon.Opacity = 1.0;
                this.energyPotion.Opacity = 1.0;
            }

            else
            {
                this.pb_energy.Opacity = 0.0;
                this.energyIcon.Opacity = 0.0;
                this.energyPotion.Opacity = 0.0;
            }

        }

        public void verPocionVida(bool ver)
        {
            if (ver)
            {
                this.heartPotion.Opacity = 1.0;
            }

            else
            {
                this.heartPotion.Opacity = 0.0;
            }

        }

        public void verPocionEnergia(bool ver)
        {
            if (ver)
            {
                this.energyPotion.Opacity = 1.0;
            }

            else
            {
                this.energyPotion.Opacity = 0.0;
            }

        }

        public void verNombre(bool ver)
        {
            if (ver)
                this.name.Opacity = 1.0;
            else
                this.name.Opacity = 0.0;
        }

        //Funciones para animar pokemon
        public void activarAniIdle(bool activar)
        {
            if (activar)
            {
                iddleStoryboard.Begin();
            }

            else
            {
                iddleStoryboard.Stop();
            }
        }

        public void animacionAtaqueFlojo()
        {
            debilStoryboard.Begin();
            this.sonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsLaprasACE/Sounds/AtaqueDebil.mp3"));
            sonidos.MediaPlayer.Play();
        }

        public void animacionAtaqueFuerte()
        {
            fuerteStoryboard.Begin();
            this.sonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsLaprasACE/Sounds/AtaqueFuerte.mp3"));
            sonidos.MediaPlayer.Play();
        }

        public void animacionDefensa()
        {
            escudoStoryboard.Begin();
            this.sonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsLaprasACE/Sounds/Escudo.mp3"));
            sonidos.MediaPlayer.Play();
        }

        public void animacionDescasar()
        {
            descansoStoryboard.Begin();
            this.sonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsLaprasACE/Sounds/Descanso.mp3"));
            sonidos.MediaPlayer.Play();
        }

        public void animacionCansado()
        {
            estadoCansado = true;
            cansadoEstado.Begin();
            caraCansadoHerido.Begin();
            this.sonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsLaprasACE/Sounds/Cansado.mp3"));
            sonidos.MediaPlayer.Play();

        }

        public void animacionNoCansado()
        {
            estadoCansado = false;

            cansadoEstado.Stop();
            finalCansadoEstado.Begin();
            if (!estadoCansado && !estadoHerido)
            {
                caraCansadoHerido.Stop();
                finalCaraCansadoHerido.Begin();
            }

            this.sonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsLaprasACE/Sounds/QuitarCansado.mp3"));
            sonidos.MediaPlayer.Play();

        }

        public void animacionHerido()
        {
            estadoHerido = true;
            heridoEstado.Begin();
            caraCansadoHerido.Begin();
            this.sonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsLaprasACE/Sounds/PowerDown.mp3"));
            sonidos.MediaPlayer.Play();
        }

        public void animacionNoHerido()
        {
            estadoHerido = false;

            finalHeridoEstado.Begin();
            if (!estadoCansado && !estadoHerido)
            {
                caraCansadoHerido.Stop();
                finalCaraCansadoHerido.Begin();
            }

            this.sonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsLaprasACE/Sounds/PowerUp.mp3"));
            sonidos.MediaPlayer.Play();
        }

        public void animacionDerrota()
        {
            muerteEstado.Begin();
            this.sonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsLaprasACE/Sounds/Muerte.mp3"));
            sonidos.MediaPlayer.Play();
        }

        //Animaciones completadas
        private void DebilStoryboard_Completed(object sender, object e)
        {
            debilStoryboard.Stop();
            activarAniIdle(true);
        }

        private void FuerteStoryboard_Completed(object sender, object e)
        {
            fuerteStoryboard.Stop();
            activarAniIdle(true);
        }

        private void DescansoStoryboard_Completed(object sender, object e)
        {
            descansoStoryboard.Stop();
            if (estadoHerido || estadoCansado)
                caraCansadoHerido.Begin();
            activarAniIdle(true);

        }

        private void EscudoStoryboard_Completed(object sender, object e)
        {
            escudoStoryboard.Stop();
            if (estadoHerido || estadoCansado)
                caraCansadoHerido.Begin();
            activarAniIdle(true);
        }
    }
}
