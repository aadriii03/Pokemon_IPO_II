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

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace POKEDEX
{
    public sealed partial class Toxicroack : UserControl, iPokemon
    {
        public Toxicroack()
        {
            this.InitializeComponent();
            defaultStoryboard = (Storyboard)this.Resources["Moverse"];
            defaultStoryboard.Begin();
            this.IsTabStop = true;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            Pocima_Vida.PointerReleased += Pocima_Vida_PointerReleased;
            Pocima_Energia.PointerReleased += Pocima_Energia_PointerReleased;
        }
        Storyboard defaultStoryboard;
        DispatcherTimer dtTime;

        private Boolean cansado = false;
        private Boolean herido = false;
        private MediaPlayer mpsonidos = new MediaPlayer();
        private Storyboard sb = new Storyboard();

        private string nombre = "Toxicroack";
        private string categoria = "Veneno/Lucha";
        private double altura = 1.3;
        private double peso = 44.4;
        private string evolucion = "Croagunk";
        private string descripcion = "Toxicroack es un Pokémon de tipo veneno y lucha. Es conocido por su habilidad de envenenar a sus oponentes con un solo toque gracias a las toxinas que segrega por sus dedos. A pesar de su naturaleza peligrosa, es un Pokémon muy ágil y estratégico en combate, usando su astucia y sus movimientos de lucha para derrotar a sus enemigos.";


        public double Energia { get { return this.BarraEnergia.Value; } set { this.BarraEnergia.Value = value; } }
        public string Nombre { get { return this.nombre; } set { this.nombre = value; } }
        public string Categoría { get { return this.categoria; } set { this.categoria = value; } }
        public string Tipo { get { return this.Tipo; } set { this.Tipo = value; } }
        public double Altura { get { return this.altura; } set { this.altura = value; } }
        public double Peso { get { return this.peso; } set { this.peso = value; } }
        public string Evolucion { get { return this.evolucion; } set { this.evolucion = value; } }
        public string Descripcion { get { return this.descripcion; } set { this.descripcion = value; } }
        public double Vida { get { return this.BarraVida.Value; } set { this.BarraVida.Value = value; } }


        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            // Dirigir el evento de teclado al control de usuario ToxicroackJPG
            ToxicroackJPG_KeyDown(args.VirtualKey);
        }

        private void ToxicroackJPG_KeyDown(Windows.System.VirtualKey key)
        {
            mpsonidos.Pause();
            defaultStoryboard.Stop();

            // Determinar qué tecla se presionó
            switch (key)
            {
                case Windows.System.VirtualKey.Number2:
                    // Llamar al método correspondiente al ataque débil
                    animacionAtaqueFlojo();
                    break;
                case Windows.System.VirtualKey.Number3:
                    // Llamar al método correspondiente al ataque fuerte
                    animacionAtaqueFuerte();
                    break;
                case Windows.System.VirtualKey.Number4:
                    // Llamar al método correspondiente a la defensa
                    animacionDefensa();
                    break;
                case Windows.System.VirtualKey.Number5:
                    animacionDescasar();
                    break;
                case Windows.System.VirtualKey.Number6:
                    animacionCansado();
                    break;
                case Windows.System.VirtualKey.Number7:
                    animacionHerido();
                    break;
                case Windows.System.VirtualKey.Number8:
                    animacionDerrota();
                    break;
                default:
                    // Si no se presionó una tecla específica que manejes, no hacer nada
                    defaultStoryboard.Begin();
                    break;
            }
        }


        private void Pocima_Vida_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += subeVida;
            dtTime.Start();
            this.Pocima_Vida.Opacity = 0.5;
        }

        private void Pocima_Energia_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += subeEnergia;
            dtTime.Start();
            this.Pocima_Energia.Opacity = 0.5;
        }

        private void subeVida(object sender, object e)
        {
            this.BarraVida.Value += 0.2;
            if (BarraVida.Value >= 100)
            {
                this.dtTime.Stop();
                this.Pocima_Vida.Visibility = Visibility.Collapsed;
            }
        }

        private void subeEnergia(object sender, object e)
        {
            this.BarraEnergia.Value += 0.2;
            if (BarraEnergia.Value >= 100)
            {
                this.dtTime.Stop();
                this.Pocima_Energia.Visibility = Visibility.Collapsed;
            }

        }

        public void verFondo(bool ver)
        {
            if (ver)
            {
                // Mostrar la imagen de fondo
                FondoImagen.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la imagen de fondo
                FondoImagen.Visibility = Visibility.Collapsed;
            }
        }


        public void verFilaVida(bool ver)
        {
            if (ver)
            {
                // Mostrar la barra de vida
                BarraVida.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la barra de vida
                BarraVida.Visibility = Visibility.Collapsed;
            }
        }


        public void verFilaEnergia(bool ver)
        {
            if (ver)
            {
                // Mostrar la barra de vida
                BarraEnergia.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la barra de vida
                BarraEnergia.Visibility = Visibility.Collapsed;
            }
        }

        public void verPocionVida(bool ver)
        {
            if (ver)
            {
                // Mostrar la barra de vida
                Pocima_Vida.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la barra de vida
                Pocima_Vida.Visibility = Visibility.Collapsed;
            }
        }

        public void verPocionEnergia(bool ver)
        {
            if (ver)
            {
                // Mostrar la barra de vida
                Pocima_Energia.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar la barra de vida
                Pocima_Energia.Visibility = Visibility.Collapsed;
            }
        }

        public void verNombre(bool ver)
        {
            if (ver)
            {
                // Mostrar el nombre del Pokémon
                NombrePokemon.Visibility = Visibility.Visible;
            }
            else
            {
                // Ocultar el nombre del Pokémon
                NombrePokemon.Visibility = Visibility.Collapsed;
            }
        }

        private void ResetAnimation(bool cansado, bool herido)
        {
            sb.Stop();

            if (cansado)
            {
                sb = (Storyboard)this.Resources["EstarCansado"];
                sb.Begin();
            }
            else if (herido)
            {
                sb = (Storyboard)this.Resources["MantenerseHerido"];
                sb.Begin();
            }
            else if (!cansado && !herido)
            {
                defaultStoryboard.Begin();
            }
        }

        public void activarAniIdle(bool activar)
        {
            if (activar)
            {
                // Iniciar la animación "moverse"
                defaultStoryboard.Begin();
            }
        }

        public void animacionAtaqueFlojo()
        {
            sb.Stop();
            sb = (Storyboard)this.Resources["AtaqueDebil"];
            sb.Completed += (s, args) => ResetAnimation(cansado, herido);
            mpsonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Toxicroack+incial/Sonidos/AtaqueDebil2.mp3"));
            mpsonidos.Play();
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(1300);
            dtTime.Tick += (s, args) =>
            {
                sb.Begin();
                dtTime.Stop();
            };
            dtTime.Start();
        }

        public void animacionAtaqueFuerte()
        {
            // Obtener el storyboard de AtaqueFuerte y suscribirse al evento Completed
            Storyboard storyboardAtaqueFuerte = (Storyboard)this.Resources["AtaqueFuerte"];
            MediaPlayer mpSonidos = new MediaPlayer();
            storyboardAtaqueFuerte.Completed += (sender, e) =>
            {
                storyboardAtaqueFuerte.Stop();
                // Detener el sonido
                //mpSonidos.Pause();
                // Una vez que la animación de AtaqueFuerte se complete, activar la animación idle
                activarAniIdle(true);
            };

            // Iniciar la animación de AtaqueFuerte
            storyboardAtaqueFuerte.Begin();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Toxicroack+incial/Sonidos/AtaqueFuerte.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();
        }

        public void animacionDefensa()
        {
            // Obtener el storyboard de Escudo1 y suscribirse al evento Completed
            Storyboard storyboardEscudo1 = (Storyboard)this.Resources["Escudo1"];
            storyboardEscudo1.Completed += (sender, e) =>
            {
                storyboardEscudo1.Stop();
                // Una vez que la animación de Escudo1 se complete, activar la animación idle
                activarAniIdle(true);
            };

            // Iniciar la animación de Escudo1
            storyboardEscudo1.Begin();
        }


        public void animacionDescasar()
        {
            // Iniciar la animación "Descanso"
            Storyboard storyboardDescanso = (Storyboard)this.Resources["Descanso"];
            storyboardDescanso.Begin();
            MediaPlayer mpSonidos = new MediaPlayer();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Toxicroack+incial/Sonidos/Ronquido.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();

            // Suscribirse al evento Completed de la animación "Descanso"
            storyboardDescanso.Completed += (sender, e) =>
            {
                // Cuando la animación "Descanso" se complete, iniciar la animación "Mantenerse Dormido" y establecer un temporizador
                Storyboard storyboardMantenerseDormido = (Storyboard)this.Resources["MantenerseDormido"];
                storyboardMantenerseDormido.Begin();

                // Crear un temporizador con un intervalo de 2 segundos
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);

                // Suscribirse al evento Tick del temporizador
                timer.Tick += (s, args) =>
                {
                    // Detener la animación "Mantenerse Dormido" después de 2 segundos
                    storyboardMantenerseDormido.Stop();
                    // Detener el sonido
                    mpSonidos.Pause();

                    // Detener el temporizador
                    timer.Stop();
                };

                // Iniciar el temporizador
                timer.Start();
            };
        }


        public void animacionCansado()
        {
            // Iniciar la animación "Herido"
            Storyboard storyboardCansado = (Storyboard)this.Resources["Cansado"];
            storyboardCansado.Begin();
            MediaPlayer mpSonidos = new MediaPlayer();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Toxicroack+incial/Sonidos/Bostezo.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();

            // Suscribirse al evento Completed de la animación "Herido"
            storyboardCansado.Completed += (sender, e) =>
            {
                // Cuando la animación "Herido" se complete, iniciar la animación "MantenerHerido" y establecer un temporizador
                Storyboard storyboardEstarCansado = (Storyboard)this.Resources["EstarCansado"];
                storyboardEstarCansado.Begin();

                // Crear un temporizador con un intervalo de 2 segundos
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);

                // Suscribirse al evento Tick del temporizador
                timer.Tick += (s, args) =>
                {
                    // Detener la animación "MantenerHerido" después de 2 segundos
                    storyboardEstarCansado.Stop();
                    // Detener el sonido
                    mpSonidos.Pause();

                    // Detener el temporizador
                    timer.Stop();
                };

                // Iniciar el temporizador
                timer.Start();
            };
        }

        public void animacionNoCansado()
        {
            Moverse.Begin();
        }

        public void animacionHerido()
        {
            // Iniciar la animación "Herido"
            Storyboard storyboardHerido = (Storyboard)this.Resources["Herido"];
            storyboardHerido.Begin();
            MediaPlayer mpSonidos = new MediaPlayer();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Toxicroack+incial/Sonidos/Herido.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();

            // Suscribirse al evento Completed de la animación "Herido"
            storyboardHerido.Completed += (sender, e) =>
            {
                // Cuando la animación "Herido" se complete, iniciar la animación "MantenerHerido" y establecer un temporizador
                Storyboard storyboardMantenerHerido = (Storyboard)this.Resources["MantenerHerido"];
                storyboardMantenerHerido.Begin();

                // Crear un temporizador con un intervalo de 2 segundos
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(2);

                // Suscribirse al evento Tick del temporizador
                timer.Tick += (s, args) =>
                {
                    // Detener la animación "MantenerHerido" después de 2 segundos
                    storyboardMantenerHerido.Stop();

                    // Detener el sonido
                    mpSonidos.Pause();

                    // Detener el temporizador
                    timer.Stop();
                };

                // Iniciar el temporizador
                timer.Start();
            };
        }


        public void animacionNoHerido()
        {
            Moverse.Begin();
        }

        public void animacionDerrota()
        {
            // Obtener el storyboard de la animación "Muerte" y suscribirse al evento Completed
            Storyboard storyboardMuerte = (Storyboard)this.Resources["Muerte"];
            storyboardMuerte.Completed += (sender, e) =>
            {
                // Una vez que la animación de "Muerte" se complete, realizar las acciones necesarias
                // Por ejemplo, detener la animación, ocultar elementos, etc.
            };

            // Iniciar la animación de "Muerte"
            storyboardMuerte.Begin();
            MediaPlayer mpSonidos = new MediaPlayer();
            mpSonidos.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets+Toxicroack+incial/Sonidos/Muerte.mp3"));
            // Reproducir el sonido
            mpSonidos.Play();
        }

        public void activarAnimacionIdle(bool activar)
        {
            throw new NotImplementedException();
        }
    }
}
