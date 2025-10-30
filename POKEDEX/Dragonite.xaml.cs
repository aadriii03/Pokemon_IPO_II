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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace POKEDEX
{
    public sealed partial class Dragonite : UserControl, iPokemon
    {
        public Dragonite()
        {
            this.InitializeComponent();
            this.IsTabStop = true;
            this.KeyDown += ControlTeclas;
            defaultStoryboard = (Storyboard)this.Resources["Default"];
            defaultStoryboard.Begin();
        }
        Storyboard defaultStoryboard;
        DispatcherTimer dtTime;

        private Boolean cansado = false;
        private Boolean herido = false;
        private MediaPlayer mpsonidos = new MediaPlayer();
        private Storyboard sb = new Storyboard();

        public double Vida
        {
            get { return this.pbVida.Value; }
            set { this.pbVida.Value = value; }

        }

        public double Energia
        {
            get { return this.pbStamina.Value; }
            set { this.pbStamina.Value = value; }
        }
        public string Nombre
        {
            get { return this.txtNombre.Text; }
            set { this.txtNombre.Text = value; }
        }
        public string Categoría { get { return "Dragon"; } set { } }
        public string Tipo { get { return "Volador/Dragon"; } set { } }
        public double Altura { get { return 210; } set { } }
        public double Peso { get { return 120; } set { } }
        public string Evolucion { get { return "Dragonair"; } set { } }
        public string Descripcion
        {
            get
            {
                return
                "Dragonite es un Pokémon legendario que posee la habilidad de volar a velocidades impresionantes, alcanzando hasta 1,500 kilómetros por hora. Su imponente tamaño y su envergadura lo hacen una criatura majestuosa en los cielos. A pesar de su apariencia intimidante, Dragonite es conocido por su naturaleza amigable y protectora hacia aquellos que considera sus amigos. Sin embargo, cuando se enfrenta a una amenaza, puede desatar un poderío increíble en la batalla, utilizando su fuerza física y sus poderosos ataques de tipo dragón y volador. Su evolución a partir de Dragonair es un símbolo de la madurez y la fortaleza que alcanza este Pokémon, convirtiéndose en un aliado valioso para cualquier Entrenador que lo tenga a su lado.";
            }
            set { }
        }
        private void ControlTeclas(object sender, KeyRoutedEventArgs e)
        {
            mpsonidos.Pause();
            defaultStoryboard.Stop();

            switch (e.Key)
            {
                case Windows.System.VirtualKey.Number1:
                    animacionAtaqueFlojo();
                    break;

                case Windows.System.VirtualKey.Number2:
                    animacionAtaqueFuerte();
                    break;

                case Windows.System.VirtualKey.Number3:
                    animacionDefensa();
                    break;

                case Windows.System.VirtualKey.Number4:
                    animacionDescasar();
                    break;

                case Windows.System.VirtualKey.Number5:
                    animacionHerido();
                    break;

                case Windows.System.VirtualKey.Number6:
                    animacionDerrota();
                    break;

                case Windows.System.VirtualKey.Number7:
                    img_escudoesfera.Visibility = Visibility.Collapsed;
                    ResetAnimation(cansado, herido);
                    break;

                case Windows.System.VirtualKey.Number8:
                    animacionCansado();
                    break;

                default:
                    // Si no se presiona ninguna tecla válida, vuelve a iniciar la animación predeterminada
                    defaultStoryboard.Begin();
                    break;
            }

        }

        private void ReproducirAtaqueDespuesEnfado(MediaPlayer sender, object args)
        {
            mpsonidos.MediaEnded -= ReproducirAtaqueDespuesEnfado;
            mpsonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDragoniteCSD/Ataque.mp3"));
            mpsonidos.Play();
        }

        private void ResetAnimation(bool cansado, bool herido)
        {
            sb.Stop();

            if (cansado)
            {
                sb = (Storyboard)this.Resources["Cansado"];
                sb.Begin();
            }
            else if (herido)
            {
                sb = (Storyboard)this.Resources["Herido"];
                sb.Begin();
            }
            else if (!cansado && !herido)
            {
                defaultStoryboard.Begin();
            }
        }

        private void usePotionRed(object sender, PointerRoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += increaseHealth;
            dtTime.Start();
            this.img_pocionvida.Opacity = 0.5;
        }

        private void increaseHealth(object sender, object e)
        {
            this.pbVida.Value += 0.2;
            if (pbVida.Value >= 100)
            {
                this.dtTime.Stop();
                this.img_pocionvida.Opacity = 1;
            }
        }

        private void img_pocionvida_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            usePotionRed(sender, e);
        }



        public void verFondo(bool ver)
        {
            if (!ver) { this.FondoPK.Background = null; }
            else
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///AssetsDragoniteCSD/escenariopokemon.jpg"));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.FondoPK.Background = imageBrush;

            }
        }

        public void verFilaVida(bool ver)
        {
            if (!ver)
            {
                this.pbVida.Visibility = Visibility.Collapsed; // Oculta la ProgressBar de vida
            }
            else
            {
                this.pbVida.Visibility = Visibility.Visible; // Muestra la ProgressBar de vida
            }
        }

        public void verFilaEnergia(bool ver)
        {
            if (!ver)
            {
                this.pbStamina.Visibility = Visibility.Collapsed; // Oculta la ProgressBar de energía
            }
            else
            {
                this.pbStamina.Visibility = Visibility.Visible; // Muestra la ProgressBar de energía
            }
        }



        public void verPocionVida(bool ver)
        {
            if (!ver)
            {
                this.img_pocionvida.Source = null;
            }
            else
            {
                this.img_pocionvida.Source = new BitmapImage(new Uri("ms-appx:///AssetsDragoniteCSD/pocionvida.png"));
            }
        }



        public void verPocionEnergia(bool ver)
        {
            if (!ver)
            {
                this.img_pocionstamina.Source = null; // Elimina la imagen de la poción de energía
            }
            else
            {
                // Asigna la imagen de la poción de energía nuevamente
                this.img_pocionstamina.Source = new BitmapImage(new Uri("ms-appx:///AssetsDragoniteCSD/pocionstamina.png"));
            }
        }


        public void verNombre(bool ver)
        {
            if (!ver)
            {
                this.txtNombre.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.txtNombre.Visibility = Visibility.Visible;
            }
        }


        public void activarAnimacionIdle(bool activar)
        {
            if (activar)
            {
                // Iniciar la animación por defecto de inactividad
                defaultStoryboard.Begin();
            }
            else
            {
                // Detener la animación por defecto de inactividad
                defaultStoryboard.Stop();
            }
        }

        public void animacionAtaqueFuerte()
        {
            sb.Stop();
            sb = (Storyboard)this.Resources["Cabezazo"];
            sb.Completed += (s, args) => ResetAnimation(cansado, herido);
            sb.Begin();
            mpsonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDragoniteCSD/explosion.wav"));
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(1000);
            dtTime.Tick += (s, args) =>
            {
                mpsonidos.Play();
                dtTime.Stop();
            };
            dtTime.Start();
        }

        public void animacionAtaqueFlojo()
        {
            sb.Stop();
            sb = (Storyboard)this.Resources["ColaDragon"];
            sb.Completed += (s, args) => ResetAnimation(cansado, herido);
            mpsonidos.MediaEnded += ReproducirAtaqueDespuesEnfado;
            mpsonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDragoniteCSD/Enfado.mp3"));
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

        public void animacionDefensa()
        {
            sb.Stop();
            sb = (Storyboard)this.Resources["Escudo"];
            sb.Completed += (s, args) => ResetAnimation(cansado, herido);
            sb.Begin();
            mpsonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDragoniteCSD/escudo.mp3"));
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(1300);
            dtTime.Tick += (s, args) =>
            {
                mpsonidos.Play();
                dtTime.Stop();
            };
            dtTime.Start();
        }

        public void animacionDescasar()
        {
            sb.Stop();
            sb = (Storyboard)this.Resources["CuraVida"];
            sb.Completed += (s, args) => ResetAnimation(cansado, herido);
            mpsonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDragoniteCSD/CuraVida.mp3"));
            mpsonidos.Play();
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(800);
            dtTime.Tick += (s, args) =>
            {
                sb.Begin();
                dtTime.Stop();
            };
            dtTime.Start();
            cansado = false;
            herido = false;
        }

        public void animacionCansado()
        {
            sb.Stop();
            sb = (Storyboard)this.Resources["Cansado"];
            sb.Completed += (s, args) => ResetAnimation(cansado, herido);
            mpsonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDragoniteCSD/cansado.mp3"));
            mpsonidos.Play();
            sb.Begin();
            cansado = true;
        }

        public void animacionNoCansado()
        {
            ResetAnimation(cansado, herido);
        }

        public void animacionHerido()
        {
            sb.Stop();
            sb = (Storyboard)this.Resources["Herido"];
            sb.Completed += (s, args) => ResetAnimation(cansado, herido);
            mpsonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDragoniteCSD/herido.mp3"));
            mpsonidos.Play();
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(1300);
            dtTime.Tick += (s, args) =>
            {
                sb.Begin();
                dtTime.Stop();
            };
            dtTime.Start();
            herido = true;
        }

        public void animacionNoHerido()
        {
            ResetAnimation(cansado, herido);
        }

        public void animacionDerrota()
        {
            sb.Stop();
            sb = (Storyboard)this.Resources["Muerte"];
            mpsonidos.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///AssetsDragoniteCSD/muertedef.mp3"));
            mpsonidos.Play();
            sb.Begin();
            defaultStoryboard.Stop();
        }

        public void activarAniIdle(bool activar)
        {
            throw new NotImplementedException();
        }
    }
}
