using ClassLibrary1_Prueba;
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

namespace POKEDEX
{
    public sealed partial class ButterFreeACC : UserControl, iPokemon
    {
        DispatcherTimer dtTime;
        private Storyboard sbaux;
        Boolean activar = true;

        public ButterFreeACC()
        {
            this.InitializeComponent();
            this.IsTabStop = true; // hacer interactivo el pokemon
            activarAniIdle(activar);
            
        }

        private void ButterFreeACC_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private string nombre = "Butterfree";
        private string categoria = "bicho/volador";
        private double altura = 1.30;
        private double peso = 8.0;
        private string evolucion = "Metapod";
        private string descripcion = "Butterfree es un Pokémon que se asemeja a una mariposa. Su cuerpo está dividido en dos partes: " +
        "cabeza y tronco. Posee grandes ojos rojos, un cuerpo de color púrpura oscuro, dos pequeñas manos y dos pies de un color azul cielo. " +
        "Sus alas blancas están cubiertas por un polvo tóxico y son impermeables al agua. Gracias a ellas, puede volar incluso en días de lluvia. " +
        "En cuanto a sus capacidades de lucha, Butterfree puede defenderse de sus enemigos liberando polvillos tóxicos de sus alas que se dispersan por el aire. " +
        "Además, en combate, aletea a gran velocidad para lanzar al aire estos tóxicos polvillos. Es un Pokémon de tipo Bicho/Volador y destaca por sus 90 puntos de Ataque especial. " +
        "Butterfree es conocido por su habilidad para volar hasta 10 kilómetros en busca de miel, polen y néctar de las flores. A pesar de su apariencia delicada, es un Pokémon resistente que puede soportar condiciones climáticas adversas. " +
        "Su polvo tóxico es una defensa efectiva contra los enemigos, y también puede ser utilizado para debilitar a sus oponentes en batalla. Con su alto Ataque especial, Butterfree puede causar un daño significativo a sus oponentes.";

        public double Energia { get { return this.barra_energia.Value; } set { this.barra_energia.Value = value; } }
        public string Nombre { get { return this.nombre; } set { this.nombre = value; } }
        public string Categoría { get { return this.categoria; } set { this.categoria = value; } }
        public string Tipo { get { return this.Tipo; } set { this.Tipo = value; } }
        public double Altura { get { return this.altura; } set { this.altura = value; } }
        public double Peso { get { return this.peso; } set { this.peso = value; } }
        public string Evolucion { get { return this.evolucion; } set { this.evolucion = value; } }
        public string Descripcion { get { return this.descripcion; } set { this.descripcion = value; } }
        double iPokemon.Vida { get { return barra_vida.Value; } set { barra_vida.Value = value; }  }

        // Apartado de pocimas -------------------------------------------------------------------------------

        private void pocima_vida_PointerReleased(object sender, PointerRoutedEventArgs e) // para aumentar la vida
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += subeVida;
            dtTime.Start();
            this.pocima_vida.Opacity = 0.5;
        }

        private void subeVida(object sender, object e)
        {
            this.barra_vida.Value += 0.2;
            if (barra_vida.Value >= 100)
            {
                this.dtTime.Stop();
                this.pocima_vida.Visibility = Visibility.Collapsed;
            }
        }
        private void pocima_energia_PointerReleased(object sender, PointerRoutedEventArgs e) // para aumentar energia
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += subeEnergia;
            dtTime.Start();
            this.pocima_energia.Opacity = 0.5;
        }

        private void subeEnergia(object sender, object e)
        {
            this.barra_energia.Value += 0.2;
            if (barra_energia.Value >= 100)
            {
                this.dtTime.Stop();
                this.pocima_energia.Visibility = Visibility.Collapsed;
            }

        }

        // Lanzar animaciones -----------------------------------------------------------------------------------------------
        
        public void activarAniIdle(bool activar)
        {
            sbaux = (Storyboard)this.Resources["AniAleteo"];
            sbaux.Begin();
        }

        public void animacionAtaqueFlojo()
        {
            sbaux = (Storyboard)this.Resources["Ataque_debil"];
            sbaux.Begin();
        }
        public void animacionAtaqueFuerte()
        {
            sbaux = (Storyboard)this.Resources["Ataque_fuerte"];
            sbaux.Begin();
            MediaPlayer mpTornado = new MediaPlayer();
            mpTornado.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsButterFreeACC/tornado.mp3"));
            mpTornado.Play();
        }
        public void animacionDefensa()
        {
            sbaux = (Storyboard)this.Resources["AniEscudo"];
            sbaux.Begin();
            MediaPlayer mpEscudo = new MediaPlayer();
            mpEscudo.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsButterFreeACC/escudo.mp3"));
            mpEscudo.Play();
        }
        public void animacionDescasar()
        {

            sbaux = (Storyboard)this.Resources["AniDescanso"];
            sbaux.Begin();
        }

        public void animacionCansado()
        {
            sbaux = (Storyboard)this.Resources["AniAleteo"]; // para el alaeteo normal para iniciar el aleteo suave
            sbaux.Stop();

            sbaux = (Storyboard)this.Resources["AniCansado"];
            sbaux.Begin();
        }
        public void animacionNoCansado()
        {
            sbaux = (Storyboard)this.Resources["AniCansado"]; // para el aleteo suave e inicia el normal
            sbaux.Stop();

            sbaux = (Storyboard)this.Resources["AniAleteo"];
            sbaux.Begin();
        }
        public void animacionHerido()
        {
            sbaux = (Storyboard)this.Resources["Herido"];
            sbaux.Begin();
        }
        public void animacionNoHerido()
        {
            sbaux = (Storyboard)this.Resources["NoHerido"];
            sbaux.Begin();
        }
        public void animacionDerrota()
        {
            sbaux = (Storyboard)this.Resources["AniMuerte"];
            sbaux.Begin();
            MediaPlayer mpMuerte = new MediaPlayer();
            mpMuerte.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsButterFreeACC/muerte.mp3"));
            mpMuerte.Play();
        }

        public void verFondo(bool ver, Grid grid)
        {
            if (ver == true)
            {
                grid.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Visibility = Visibility.Collapsed;
            }
        }


        public void VerFilaVida(bool ver)
        {
            if (ver == true)
            {
                barra_vida.Visibility = Visibility.Visible;
            }
            else
            {
                barra_vida.Visibility=Visibility.Collapsed;
            }
        }

        public void verFilaEnergia(bool ver)
        {
            if(ver == true)
            {
                barra_energia.Visibility = Visibility.Visible;
            }
            else
            {
                barra_energia.Visibility= Visibility.Collapsed;
            }
        }

        public void verPocionVida(bool ver)
        {
            if(ver== true)
            {
                pocima_vida.Visibility = Visibility.Visible;
            }
            else
            {
                pocima_vida.Visibility=Visibility.Collapsed;
            }
        }

        public void verPocionEnergia(bool ver)
        {
            if( ver == true)
            {
                pocima_energia.Visibility = Visibility.Visible;
            }
            else
            {
                pocima_energia.Visibility=Visibility.Collapsed;
            }
        }


        public void verNombre(bool ver)
        {
            if (ver == true)
            {
                NombrePokemon.Visibility = Visibility.Visible;
            }
            else
            {
                NombrePokemon.Visibility=Visibility.Collapsed;
            }
        }

        public void verFondo(bool ver)
        {
            throw new NotImplementedException();
        }

        public void verFilaVida(bool ver)
        {
            throw new NotImplementedException();
        }
    }
}
