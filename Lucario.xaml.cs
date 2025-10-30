using ClassLibrary1_Prueba;
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
    public sealed partial class Lucario : UserControl, iPokemon
    {
        DispatcherTimer dtTime;
        public Lucario()
        {
            this.InitializeComponent();
            this.IsTabStop = true;

            // Animacion por defecto
            Storyboard idleAnimation = (Storyboard)this.Resources["Idle"];
            idleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            idleAnimation.Begin();

            Storyboard Escudo = (Storyboard)this.Resources["Escudo"];
            Escudo.Completed += BattleShildCompleted;

            Storyboard AuraSphere = (Storyboard)this.Resources["AuraSphere"];
            Escudo.Completed += AuraSphereCompleted;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        private void ProgressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) { }
        private void ProgressBar_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e) { }
        private void PokemonName_SelectionChanged(object sender, RoutedEventArgs e) { }

        // EVENTO CURA
        private void Evento_Cura(object sender, RoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += increaseHealth;
            dtTime.Start();
            this.HealthPotion.Opacity = 0.5;
            if (pbHealth.Value == pbHealth.Maximum)
            {
                ((iPokemon)this).verPocionVida(false);
            }
        }

        private void increaseHealth(object sender, object e)
        {
            this.pbHealth.Value += 0.2;
            if (pbHealth.Value >= 100)
            {
                this.dtTime.Stop();
                this.pbHealth.Opacity = 1;
            }
        }

        // Variables para limitar las animaciones de vida y energia a una ejecucion
        private bool lowHealthAnimationPlayed = false;
        private bool recoveredHealthAnimationPlayed = false;
        private bool tiredAnimationPlayed = false;
        private bool notTiredAnimationPlayed = false;
        private bool defeatedAnimationPlayed = false;
        private bool recoveredFromDefeatAnimationPlayed = false;

        double iPokemon.Vida
        {
            get => pbHealth.Value;
            set
            {
                double newValue = Math.Max(0, Math.Min(value, pbHealth.Maximum));
                pbHealth.Value = newValue;
            }
        }
        double iPokemon.Energia
        {
            get => pbEnergy.Value;
            set
            {
                double newValue = Math.Max(0, Math.Min(value, pbEnergy.Maximum));
                pbEnergy.Value = newValue;
            }
        }
        string iPokemon.Nombre
        {
            get => PokemonName.Text;
            set => PokemonName.Text = value;
        }

        private string categoria;
        string iPokemon.Categoría
        {
            get => categoria;
            set => categoria = value;
        }

        private string tipo;
        string iPokemon.Tipo
        {
            get => tipo;
            set => tipo = value;
        }

        private double altura;
        double iPokemon.Altura
        {
            get => altura;
            set => altura = value;
        }

        private double peso;
        double iPokemon.Peso
        {
            get => peso;
            set => peso = value;
        }

        private string evolucion;
        string iPokemon.Evolucion
        {
            get => evolucion;
            set => evolucion = value;
        }

        private string descripcion;
        string iPokemon.Descripcion
        {
            get => descripcion;
            set => descripcion = value;
        }

        // EVENTO ENERGIA
        private void Evento_Energia(object sender, RoutedEventArgs e)
        {
            dtTime = new DispatcherTimer();
            dtTime.Interval = TimeSpan.FromMilliseconds(100);
            dtTime.Tick += increaseEnergy;
            dtTime.Start();
            this.EnergyPotion.Opacity = 0.5;
            if (pbEnergy.Value == pbEnergy.Maximum)
            {
                ((iPokemon)this).verPocionEnergia(false);
            }
        }

        private void increaseEnergy(object sender, object e)
        {
            this.pbEnergy.Value += 0.2;
            if (pbHealth.Value >= 100)
            {
                this.dtTime.Stop();
                this.pbEnergy.Opacity = 1;
            }
        }

        // AUMENTAR Y DISMINUIR ENERGIA Y SALUD 
        private void AumentarSalud(object sender, RoutedEventArgs e)
        {
            this.pbHealth.Value += this.pbHealth.Maximum * 0.05;
            if (this.pbHealth.Value > this.pbHealth.Maximum)
            {
                this.pbHealth.Value = this.pbHealth.Maximum;
            }
            CheckHealthAndPlayAnimation();
        }

        private void DisminuirSalud(object sender, RoutedEventArgs e)
        {
            this.pbHealth.Value -= this.pbHealth.Maximum * 0.05;
            if (this.pbHealth.Value > this.pbHealth.Maximum)
            {
                this.pbHealth.Value = this.pbHealth.Maximum;
            }
            CheckHealthAndPlayAnimation();
        }

        private void AumentarEnergia(object sender, RoutedEventArgs e)
        {
            this.pbEnergy.Value += this.pbEnergy.Maximum * 0.05;
            if (this.pbEnergy.Value > this.pbEnergy.Maximum)
            {
                this.pbEnergy.Value = this.pbEnergy.Maximum;
            }
            CheckEnergyAndPlayAnimation();
        }

        private void DisminuirEnergia(object sender, RoutedEventArgs e)
        {
            this.pbEnergy.Value -= this.pbEnergy.Maximum * 0.05;
            if (this.pbEnergy.Value > this.pbEnergy.Maximum)
            {
                this.pbEnergy.Value = this.pbEnergy.Maximum;
            }
            CheckEnergyAndPlayAnimation();
        }


        // Escucha eventos de la animacion de defensa hasta que termina
        private void BattleShildCompleted(object sender, object e)
        {
            Storyboard secondAnimation = (Storyboard)this.Resources["Idle"];
            secondAnimation.Begin();
            ImgBattleShield.Opacity = 0;
        }

        // Escucha eventos de la animacion de ataque fuerte hasta que termina
        private void AuraSphereCompleted(object sender, object e)
        {
            Storyboard idleAnimation = (Storyboard)this.Resources["Idle"];
            idleAnimation.Begin();
            PlaySound("AuraSphere.mp3");
        }

        // Comprueba el estado de la barra de salud
        private void CheckHealthAndPlayAnimation()
        {
            double healthPercentage = (this.pbHealth.Value / this.pbHealth.Maximum) * 100;

            if (healthPercentage < 50)
            {
                if (!lowHealthAnimationPlayed)
                {
                    ((iPokemon)this).animacionHerido();
                    lowHealthAnimationPlayed = true;
                    recoveredHealthAnimationPlayed = false;
                }
            }
            else if (healthPercentage >= 50)
            {
                if (!recoveredHealthAnimationPlayed)
                {
                    ((iPokemon)this).animacionNoHerido();
                    recoveredHealthAnimationPlayed = true;
                    lowHealthAnimationPlayed = false;
                }
            }

            if (this.pbHealth.Value == 0 && !defeatedAnimationPlayed)
            {
                ((iPokemon)this).animacionDerrota();
                defeatedAnimationPlayed = true;
                recoveredFromDefeatAnimationPlayed = false;
            }
            else if (this.pbHealth.Value > 0 && !recoveredFromDefeatAnimationPlayed && defeatedAnimationPlayed)
            {
                Storyboard recoveredFromDefeatAnimation = (Storyboard)this.Resources["Derrotado_Inverso"];
                recoveredFromDefeatAnimation.Begin();
                recoveredFromDefeatAnimationPlayed = true;
                defeatedAnimationPlayed = false;
            }
        }

        // Comprueba el estado de la barra de energia
        private void CheckEnergyAndPlayAnimation()
        {
            double energyPercentage = (this.pbEnergy.Value / this.pbEnergy.Maximum) * 100;

            if (energyPercentage < 50)
            {
                if (!tiredAnimationPlayed)
                {
                    ((iPokemon)this).animacionCansado();
                    tiredAnimationPlayed = true;
                    notTiredAnimationPlayed = false;
                }
            }
            else if (energyPercentage >= 50)
            {
                if (!notTiredAnimationPlayed)
                {
                    ((iPokemon)this).animacionNoCansado();
                    notTiredAnimationPlayed = true;
                    tiredAnimationPlayed = false;
                }
            }
        }
        private void PlaySound(string soundFileName)
        {
            mediaPlayer.Source = new Uri($"ms-appx:///Assets/{soundFileName}");
            mediaPlayer.Play();
        }

        void iPokemon.verFondo(bool ver)
        {
            Background.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        void iPokemon.verFilaVida(bool ver)
        {
            pbHealth.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        void iPokemon.verFilaEnergia(bool ver)
        {
            pbEnergy.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        void iPokemon.verPocionVida(bool ver)
        {
            HealthPotion.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        void iPokemon.verPocionEnergia(bool ver)
        {
            EnergyPotion.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        void iPokemon.verNombre(bool ver)
        {
            PokemonName.Visibility = ver ? Visibility.Visible : Visibility.Collapsed;
        }

        void iPokemon.activarAniIdle(bool activar)
        {
            if (activar)
            {
                Storyboard idleAnimation = (Storyboard)this.Resources["Idle"];
                idleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                idleAnimation.Begin();
            }
            else
            {
                Storyboard idleAnimation = (Storyboard)this.Resources["Idle"];
                idleAnimation.Stop();
            }
        }

        void iPokemon.animacionAtaqueFlojo()
        {
            Storyboard weakAttackAnimation = (Storyboard)this.Resources["Reversal"];
            weakAttackAnimation.Begin();
        }

        void iPokemon.animacionAtaqueFuerte()
        {
            Storyboard strongAttackAnimation = (Storyboard)this.Resources["AuraSphere"];
            strongAttackAnimation.Begin();
        }

        void iPokemon.animacionDefensa()
        {
            Storyboard defensa = (Storyboard)this.Resources["Escudo"];
            defensa.Begin();
        }

        void iPokemon.animacionDescasar()
        {
            Storyboard idle = (Storyboard)this.Resources["Idle"];
            idle.Begin();
        }

        void iPokemon.animacionCansado()
        {
            Storyboard cansado = (Storyboard)this.Resources["Cansado"];
            cansado.Begin();
        }

        void iPokemon.animacionNoCansado()
        {
            Storyboard noCansado = (Storyboard)this.Resources["Cansado_Inverso"];
            noCansado.Begin();
        }

        void iPokemon.animacionHerido()
        {
            Storyboard herido = (Storyboard)this.Resources["Herido"];
            herido.Begin();
        }

        void iPokemon.animacionNoHerido()
        {
            Storyboard noHerido = (Storyboard)this.Resources["Herido_Inverso"];
            noHerido.Begin();
        }

        void iPokemon.animacionDerrota()
        {
            Storyboard derrotado = (Storyboard)this.Resources["Derrotado"];
            derrotado.Begin();
        }
    }
}
