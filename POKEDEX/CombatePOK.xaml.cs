using ClassLibrary1_Prueba;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace POKEDEX
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CombatePOK : Page
    {
        private bool isPlayer1Turn = true;
        private string player1PokemonName;
        private string player2PokemonName;
        private UserControl pokemon1;
        private UserControl pokemon2;
        private bool EscudoJUG1 = false;
        private bool EscudoJUG2 = false;
        private int player1PotionVida = 2;
        private int player1PotionEnergia = 2;
        private int player2PotionVida = 2;
        private int player2PotionEnergia = 2;

        public CombatePOK()
        {
            this.InitializeComponent();
            if (isPlayer1Turn == true)
            {
                txtturno.Text = "Turno del Jugador 1";
                BtnAtaqueDebilDJUG1.IsEnabled = true;
                BtnAtaqueFuerteDJUG1.IsEnabled = true;
                BtnDescansoDJUG1.IsEnabled = true;
                BtnAtaqueDebilDJUG2.IsEnabled = false;
                BtnAtaqueFuerteDJUG2.IsEnabled = false;
                BtnDescansoDJUG2.IsEnabled = false;
                BtnEscudoDJUG1.IsEnabled = true;
                BtnEscudoDJUG2.IsEnabled = false;
                Pocion_vida_JUG1.IsEnabled = true;
                Pocion_energia_JUG1.IsEnabled = true;
                Pocion_vida_JUG2.IsEnabled = false;
                Pocion_energia_JUG2.IsEnabled = false;
            }
            else
            {
                txtturno.Text = "Turno del Jugador 2";
                BtnAtaqueDebilDJUG1.IsEnabled = false;
                BtnAtaqueFuerteDJUG1.IsEnabled = false;
                BtnDescansoDJUG1.IsEnabled = false;
                BtnAtaqueDebilDJUG2.IsEnabled = true;
                BtnAtaqueFuerteDJUG2.IsEnabled = true;
                BtnDescansoDJUG2.IsEnabled = true;
                BtnEscudoDJUG1.IsEnabled = false;
                BtnEscudoDJUG2.IsEnabled = true;
                Pocion_vida_JUG1.IsEnabled = false;
                Pocion_energia_JUG1.IsEnabled = false;
                Pocion_vida_JUG2.IsEnabled = true;
                Pocion_energia_JUG2.IsEnabled = true;
            }

            }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Tuple<string, string> pokemonNames)
            {
                player1PokemonName = pokemonNames.Item1;
                player2PokemonName = pokemonNames.Item2;

                // Cargar los controles de usuario correspondientes a los nombres de los Pokémon
                pokemon1 = LoadPokemonControl(player1PokemonName);
                this.Jugador1Control.Children.Add(this.pokemon1);
                pokemon2 = LoadPokemonControl(player2PokemonName);
                this.Jugador2Control.Children.Add(this.pokemon2);
                if (pokemon1 is iPokemon p1)
                {
                    p1.Vida = 100;
                    p1.Energia = 100;
                }
                if (pokemon2 is iPokemon p2)
                {
                    p2.Vida = 100;
                    p2.Energia = 100;
                }
            }
        }

        private UserControl LoadPokemonControl(string pokemonName)
        {
            // Aquí debes implementar la lógica para cargar el control de usuario
            // correspondiente al nombre del Pokémon. Esto puede variar dependiendo
            // de cómo tengas implementados tus controles de usuario.

            switch (pokemonName)
            {
                case "Dragonite":
                    return new Dragonite();
                case "Toxicroack":
                    return new Toxicroack();
                case "Butterfree":
                   return new ButterFreeACC();
                case "Charizard":
                    return new Charizard();
                case "Snorlax":
                    return new Snorlax();
                case "Garchomp":
                    return new Garchomp();
                case "Piplup":
                    return new Piplup();
                case "Articuno":
                    return new Articuno();
                case "Lapras":
                    return new Lapras();
                case "Gengar":
                    return new Gengar();
                case "Grookey":
                    return new Grookey();
                case "Lucario":
                    return new Lucario();
                default:
                    return new Dragonite();
            }
        }

        private async void BtnAtaqueDebilDJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1)
            {
                if (pokemon2 is iPokemon p2)
                {
                  if (p1.Energia >= 10) 
                    {
                    if (EscudoJUG2 == false)
                    {
                        p1.animacionAtaqueFlojo();
                        p2.Vida -= 15;
                        p1.Energia -= 10;
                        if (p2.Vida <= 0)
                        {
                            p2.animacionDerrota();
                            await Task.Delay(TimeSpan.FromSeconds(4));
                            MessageDialog dialog = new MessageDialog("Jugador 1 ha ganado");
                            IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                            Frame.Navigate(typeof(Combate));
                        }
                        Endturn();
                    } else
                    {
                        MessageDialog dialog = new MessageDialog("El ataque es inofensivo ya que el Jugador 2 utilizo escudo en su turno anterior");
                        IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                        EscudoJUG2 = false;
                        Endturn();
                    }
                    } else
                    {
                        MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar ataque débil");
                        IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                    }
                }      
            }
        }

        private void Endturn()
        {
            isPlayer1Turn = !isPlayer1Turn;
            if (isPlayer1Turn == true)
            {
                txtturno.Text = "Turno del Jugador 1";
                BtnAtaqueDebilDJUG1.IsEnabled = true;
                BtnAtaqueFuerteDJUG1.IsEnabled = true;
                BtnDescansoDJUG1.IsEnabled = true;
                BtnAtaqueDebilDJUG2.IsEnabled = false;
                BtnAtaqueFuerteDJUG2.IsEnabled = false;
                BtnDescansoDJUG2.IsEnabled = false;
                BtnEscudoDJUG1.IsEnabled = true;
                BtnEscudoDJUG2.IsEnabled = false;
                Pocion_vida_JUG1.IsEnabled = true;
                Pocion_energia_JUG1.IsEnabled = true;
                Pocion_vida_JUG2.IsEnabled = false;
                Pocion_energia_JUG2.IsEnabled = false;
            }
            else
            {
                txtturno.Text = "Turno del Jugador 2";
                BtnAtaqueDebilDJUG1.IsEnabled = false;
                BtnAtaqueFuerteDJUG1.IsEnabled = false;
                BtnDescansoDJUG1.IsEnabled = false;
                BtnAtaqueDebilDJUG2.IsEnabled = true;
                BtnAtaqueFuerteDJUG2.IsEnabled = true;
                BtnDescansoDJUG2.IsEnabled = true;
                BtnEscudoDJUG1.IsEnabled = false;
                BtnEscudoDJUG2.IsEnabled = true;
                Pocion_vida_JUG1.IsEnabled = false;
                Pocion_energia_JUG1.IsEnabled = false;
                Pocion_vida_JUG2.IsEnabled = true;
                Pocion_energia_JUG2.IsEnabled = true;
            }
        }

        private async void BtnAtaqueFuerteDJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1)
            {
                if (pokemon2 is iPokemon p2)
                {
                    if (p1.Energia >= 20) {
                    if (EscudoJUG2 == false)
                    {
                        p1.animacionAtaqueFuerte();
                        await Task.Delay(TimeSpan.FromSeconds(4));
                        p2.Vida -= 30;
                        p1.Energia -= 20;
                        if (p2.Vida <= 0)
                        {
                            p2.animacionDerrota();
                            await Task.Delay(TimeSpan.FromSeconds(4));
                            MessageDialog dialog = new MessageDialog("Jugador 1 ha ganado");
                            IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                            Frame.Navigate(typeof(Combate));
                        }
                        Endturn();
                    } else
                    {
                        MessageDialog dialog = new MessageDialog("El ataque es inofensivo ya que el Jugador 2 utilizo escudo en su turno anterior");
                        IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                        EscudoJUG2 = false;
                        Endturn();
                    }
                    } else
                    {
                        MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar ataque fuerte");
                        IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                    }
                }
            }
        }

        private void BtnDescansoDJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1)
            {
                p1.animacionDescasar();
                p1.Vida += 10;
                p1.Energia += 25;
                Endturn();
            }

        }

        private void BtnEscudoDJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1)
            {
                if (p1.Energia >= 51)
                {
                    p1.animacionDefensa();
                    p1.Energia -= 51;
                    EscudoJUG1 = true;
                } else
                {
                    MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar escudo");
                    IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                }
            }
            Endturn();

        }

        private async void BtnAtaqueDebilDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1)
            {
                if (pokemon2 is iPokemon p2)
                {
                    if (p2.Energia >= 10)
                    {
                        if (EscudoJUG1 == false)
                        {
                            p2.animacionAtaqueFlojo();
                            await Task.Delay(TimeSpan.FromSeconds(4));
                            p1.Vida -= 15;
                            p2.Energia -= 10;
                            if (p1.Vida <= 0)
                            {
                                p1.animacionDerrota();
                                await Task.Delay(TimeSpan.FromSeconds(4));
                                MessageDialog dialog = new MessageDialog("Jugador 2 ha ganado");
                                IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                                Frame.Navigate(typeof(Combate));
                            }
                            Endturn();
                        }
                        else
                        {
                            MessageDialog dialog = new MessageDialog("El ataque es inofensivo ya que el Jugador 1 utilizo escudo en su turno anterior");
                            IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                            EscudoJUG1 = false;
                            Endturn();
                        }
                    } else
                    {
                        MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar ataque débil");
                        IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                    }
                }
            }
        }

        private void BtnAtaqueFuerteDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1)
            {
                if (pokemon2 is iPokemon p2)
                {
                    if (p2.Energia >= 20) {
                        if (EscudoJUG1 == false)
                        {
                            p2.Energia -= 20;
                            p2.animacionAtaqueFuerte();
                            p1.Vida -= 30;
                            if (p1.Vida <= 0)
                            {
                                p1.animacionDerrota();
                                MessageDialog dialog = new MessageDialog("Jugador 2 ha ganado");
                                IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                                Frame.Navigate(typeof(Combate));
                            }
                            Endturn();
                        }
                        else
                        {
                            MessageDialog dialog = new MessageDialog("El ataque es inofensivo ya que el Jugador 1 utilizo escudo en su turno anterior");
                            IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                            EscudoJUG1 = false;
                            Endturn();
                        }
                    } else
                    {
                        MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar ataque fuerte");
                        IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                    }
                }
            }

        }

        private void BtnDescansoDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon2 is iPokemon p2)
            {
                p2.animacionDescasar();
                p2.Vida += 10;
                p2.Energia += 25;
                Endturn();
            }

        }

        private void BtnEscudoDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon2 is iPokemon p2)
            {
                if (p2.Energia >= 51)
                {
                    p2.animacionDefensa();
                    p2.Energia -= 51;
                    EscudoJUG2 = true;
                } else
                {
                    MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar escudo");
                    IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                }
            }
            Endturn();
        }

        private async void BtnPocionVidaDJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (player1PotionVida > 0)
            {
                if (pokemon1 is iPokemon p1)
                {
                    if (p1.Vida <= 50)
                    {
                        p1.Vida += 50;
                    }
                    else
                    {
                        p1.Vida = 100;
                    }

                    player1PotionVida--;
                    MessageDialog dialog = new MessageDialog("Jugador 1 ha usado una Poción de Vida");
                    await dialog.ShowAsync();
                    Endturn();
                }
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Jugador 1 no tiene más Pociones de Vida");
                await dialog.ShowAsync();
            }
        }

        private async void BtnPocionEnergiaDJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (player1PotionEnergia > 0)
            {
                if (pokemon1 is iPokemon p1)
                {
                    if (p1.Energia <= 50)
                    {
                        p1.Energia += 50;
                    }
                    else
                    {
                        p1.Energia = 100;
                    }

                    player1PotionEnergia--;
                    MessageDialog dialog = new MessageDialog("Jugador 1 ha usado una Poción de Energía");
                    await dialog.ShowAsync();
                    Endturn();
                }
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Jugador 1 no tiene más Pociones de Energía");
                await dialog.ShowAsync();
            }
        }

        private async void BtnPocionVidaDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (player2PotionVida > 0)
            {
                if (pokemon2 is iPokemon p2)
                {
                    if (p2.Vida <= 50)
                    {
                        p2.Vida += 50;
                    }
                    else
                    {
                        p2.Vida = 100;
                    }

                    player2PotionVida--;
                    MessageDialog dialog = new MessageDialog("Jugador 2 ha usado una Poción de Vida");
                    await dialog.ShowAsync();
                    Endturn();
                }
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Jugador 2 no tiene más Pociones de Vida");
                await dialog.ShowAsync();
            }
        }

        private async void BtnPocionEnergiaDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (player2PotionEnergia > 0)
            {
                if (pokemon2 is iPokemon p2)
                {
                    if (p2.Energia <= 50)
                    {
                        p2.Energia += 50;
                    }
                    else
                    {
                        p2.Energia = 100;
                    }

                    player2PotionEnergia--;
                    MessageDialog dialog = new MessageDialog("Jugador 2 ha usado una Poción de Energía");
                    await dialog.ShowAsync();
                    Endturn();
                }
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Jugador 2 no tiene más Pociones de Energía");
                await dialog.ShowAsync();
            }
        }


    }
}
