using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Gengar : UserControl, iPokemon
    {
        DispatcherTimer dtTime;

        public double Vida
        {
            get => this.pbVida.Value;
            set => this.pbVida.Value = value;
        }
        public double Energia
        {
            get => this.pbEnergia.Value;
            set => this.pbEnergia.Value = value;
        }
        public string Nombre { get => "Gengar"; set => throw new NotImplementedException(); }
        public string Categoría { get => "Sombra"; set => throw new NotImplementedException(); }
        public string Tipo { get => "Veneno"; set => throw new NotImplementedException(); }
        public double Altura { get => 1.50; set => throw new NotImplementedException(); }
        public double Peso { get => 40.5; set => throw new NotImplementedException(); }
        public string Evolucion { get => "No evoluciona"; set => throw new NotImplementedException(); }
        public string Descripcion { get => "Gengar es un Pokemon de tipo veneno, es malvado, tiene muchas púas en su espalda, además tiene unos imponentes ojos rojos y una sonrisa larga y burlona."; set => throw new NotImplementedException(); }
        public Gengar()
        {
            this.InitializeComponent();
            Storyboard sb = (Storyboard)this.Resources["InicialMoverExtremidades"];
            sb.Begin();

            //Para reírse
            DoubleAnimation da = new DoubleAnimation();
            Storyboard sb_B = new Storyboard();
            sb_B.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            sb_B.Children.Add(da);
            sb_B.BeginTime = TimeSpan.FromSeconds(0);
            cvBoca.RenderTransform = (Transform)new ScaleTransform();
            Storyboard.SetTarget(da, cvBoca.RenderTransform);
            Storyboard.SetTargetProperty(da, "ScaleY");
            da.From = 1;
            da.To = 1.3;
            sb_B.AutoReverse = true;
            sb_B.RepeatBehavior = new RepeatBehavior(300);
            sb_B.Begin();
        }
        private void usarPocionVida(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(50);
            dtTime.Tick += subirVida;
            dtTime.Start();
            this.imgPocVida.Opacity = 0.5;

        }

        private void subirVida(object sender, object e)
        {
            this.pbVida.Value += 0.2;
            if (pbVida.Value >= 100)
            {
                this.dtTime.Stop();
                this.imgPocVida.Opacity = 1;
            }
        }


        private void usarPocionEnergia(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(50);
            dtTime.Tick += subirEnergia;
            dtTime.Start();
            this.imgPocEnergia.Opacity = 0.5;
        }

        private void subirEnergia(object sender, object e)
        {
            this.pbEnergia.Value += 0.2;
            if (pbEnergia.Value >= 100)
            {
                this.dtTime.Stop();
                this.imgPocEnergia.Opacity = 1;
            }
        }

        private void enfadarOjoI(object sender, PointerRoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.ojo1.Resources["ojoIzqRojoKey"];
            sb.Begin();
        }
        private void enfadarOjoII(object sender, PointerRoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.ojo2.Resources["ojo2RojoKey"];
            sb.Begin();
        }
        private void hacerCosquillasPataI(object sender, PointerRoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            Storyboard sb = new Storyboard();
            sb.Duration = new Duration(TimeSpan.FromMilliseconds(300));
            sb.Children.Add(da);
            sb.BeginTime = TimeSpan.FromSeconds(0);
            cvBoca.RenderTransform = (Transform)new ScaleTransform();
            Storyboard.SetTarget(da, cvBoca.RenderTransform);
            Storyboard.SetTargetProperty(da, "ScaleY");
            da.From = 1;
            da.To = 1.3;
            sb.AutoReverse = true;
            sb.RepeatBehavior = new RepeatBehavior(3);
            sb.Begin();
        }

        public void verFondo(bool ver)
        {
            if (ver) gGengar.Background.Opacity = 100;
            else gGengar.Background.Opacity = 0;
        }

        public void verFilaVida(bool ver)
        {
            if (!ver) this.gGengar.RowDefinitions[1].Height = new GridLength(0);
            else this.gGengar.RowDefinitions[1].Height = new GridLength(10,
           GridUnitType.Star);
        }

        public void verFilaEnergia(bool ver)
        {
            if (!ver) this.gGengar.RowDefinitions[1].Height = new GridLength(0);
            else this.gGengar.RowDefinitions[1].Height = new GridLength(10,
           GridUnitType.Star);
        }

        public void verPocionVida(bool ver)
        {
            if (!ver) this.imgPocVida.Opacity = 0.5;
            else this.imgPocVida.Opacity = 1;
        }

        public void verPocionEnergia(bool ver)
        {
            if (!ver) this.imgPocEnergia.Opacity = 0.5;
            else this.imgPocEnergia.Opacity = 1;
        }

        public void verNombre(bool ver)
        {
            if (ver) this.nombre.Visibility = Visibility.Visible;
            else this.nombre.Visibility = Visibility.Collapsed;
        }

        public void activarAniIdle(bool activar)
        {
            Storyboard animacion = this.Resources["InicialMoverExtremidades"] as Storyboard;
            if (activar)
                animacion.Begin();
            else
                animacion.Stop();
        }

        public void animacionAtaqueFlojo()
        {
            Storyboard animacion = this.Resources["AnimacionAtaqueDebil"] as Storyboard;
            if (animacion != null)
            {
                animacion.Begin();
            }
        }

        public void animacionAtaqueFuerte()
        {
            Storyboard animacion = this.Resources["AnimacionAtaqueFuerte"] as Storyboard;
            if (animacion != null)
            {
                animacion.Begin();
            }
        }

        public void animacionDefensa()
        {
            Storyboard animacion = this.Resources["AnimacionAccionDefensiva_Escudo"] as Storyboard;
            if (animacion != null)
            {
                animacion.Begin();
            };
        }

        public void animacionDescasar()
        {
            Storyboard animacion = this.Resources["AnimacionAccionDescanso"] as Storyboard;
            if (animacion != null)
            {
                animacion.Begin();
            }
        }

        public void animacionCansado()
        {
            Storyboard animacion = this.Resources["EstadoCansado"] as Storyboard;
            if (animacion != null)
            {
                animacion.Begin();
            }
        }

        public void animacionNoCansado()
        {
            Storyboard animacion = this.Resources["InicialMoverExtremidades"] as Storyboard;
            if (animacion != null)
            {
                animacion.Begin();
            }
        }

        public void animacionHerido()
        {
            Storyboard animacion = this.Resources["EstadoHerido"] as Storyboard;
            if (animacion != null)
            {
                animacion.Begin();
            }
        }

        public void animacionNoHerido()
        {
            Storyboard animacion = this.Resources["InicialMoverExtremidades"] as Storyboard;
            if (animacion != null)
            {
                animacion.Begin();
            }
        }

        public void animacionDerrota()
        {
            Storyboard animacion = this.Resources["EstadoDerrotado"] as Storyboard;
            if (animacion != null)
            {
                animacion.Begin();
            }
        }

    }
}
