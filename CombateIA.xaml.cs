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
    public sealed partial class CombateIA : Page
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
        private Random random = new Random();
        private bool combateTerminado = false;


        public CombateIA()
        {
            this.InitializeComponent();
            SetTurno();
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
            if (pokemon1 is iPokemon p1 && !combateTerminado)
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
                                combateTerminado = true;
                            }
                            Endturn();
                        }
                        else
                        {
                            MessageDialog dialog = new MessageDialog("El ataque es inofensivo ya que el Jugador 2 utilizó escudo en su turno anterior");
                            IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                            EscudoJUG2 = false;
                            Endturn();
                        }
                    }
                    else
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
            SetTurno();

            if (!isPlayer1Turn)
            {
                ExecuteAITurn();
            }
        }

        private void SetTurno()
        {
            if (isPlayer1Turn)
            {
                txtturno.Text = "Turno del Jugador 1";
                BtnAtaqueDebilDJUG1.IsEnabled = true;
                BtnAtaqueFuerteDJUG1.IsEnabled = true;
                BtnDescansoDJUG1.IsEnabled = true;
                BtnEscudoDJUG1.IsEnabled = true;
                BtnAtaqueDebilDJUG2.IsEnabled = false;
                BtnAtaqueFuerteDJUG2.IsEnabled = false;
                BtnDescansoDJUG2.IsEnabled = false;
                BtnEscudoDJUG2.IsEnabled = false;
                Pocion_vida_JUG1.IsEnabled=true;
                Pocion_energia_JUG1.IsEnabled = true;
                Pocion_vida_JUG2.IsEnabled=false;
                Pocion_energia_JUG2.IsEnabled = false;
            }
            else
            {
                txtturno.Text = "Turno de la IA";
                BtnAtaqueDebilDJUG1.IsEnabled = false;
                BtnAtaqueFuerteDJUG1.IsEnabled = false;
                BtnDescansoDJUG1.IsEnabled = false;
                BtnEscudoDJUG1.IsEnabled = false;
                BtnAtaqueDebilDJUG2.IsEnabled = false;
                BtnAtaqueFuerteDJUG2.IsEnabled = false;
                BtnDescansoDJUG2.IsEnabled = false;
                BtnEscudoDJUG2.IsEnabled = false;
                Pocion_vida_JUG1.IsEnabled = false;
                Pocion_energia_JUG1.IsEnabled = false;
                Pocion_vida_JUG2.IsEnabled = true;
                Pocion_energia_JUG2.IsEnabled = true;

            }
        }

       private async void ExecuteAITurn()
        {
            await Task.Delay(5000);

            // Verificar si la IA necesita usar pociones
            if (pokemon2 is iPokemon p2 && pokemon1 is iPokemon p1 && !combateTerminado)
            {
                // Calcular la mejor acción
                int action = CalculateBestAction(p2, p1);

                // Si es necesario, usar pociones
                if (action == 2 && player2PotionVida > 0 && p2.Vida < 40 && !combateTerminado)
                {
                    // Usar poción de vida
                    p2.Vida += 50; // Suponiendo que una poción recupera 50 de vida
                    player2PotionVida--;
                    MessageDialog dialog = new MessageDialog("La IA ha usado una poción de vida");
                    await dialog.ShowAsync();
                    Endturn();
                    return;
                }
                else if (action == 3 && player2PotionEnergia > 0 && p2.Energia < 40 && !combateTerminado)
                {
                    // Usar poción de energía
                    p2.Energia += 50; // Suponiendo que una poción recupera 50 de energía
                    player2PotionEnergia--;
                    MessageDialog dialog = new MessageDialog("La IA ha usado una poción de energía");
                    await dialog.ShowAsync();
                    Endturn();
                    return;
                }

                // Ejecutar la acción determinada por la IA
                switch (action)
                {
                    case 0:
                        BtnAtaqueDebilDJUG2_Click(null, null);
                        break;
                    case 1:
                        BtnAtaqueFuerteDJUG2_Click(null, null);
                        break;
                    case 2:
                        BtnDescansoDJUG2_Click(null, null);
                        break;
                    case 3:
                        BtnEscudoDJUG2_Click(null, null);
                        break;
                }

                // Verificar si la IA ha quedado sin acciones
                if (p2.Energia < 20 || p2.Vida < 20)
                {
                    Endturn();
                }
            }
        }

        private int CalculateBestAction(iPokemon pokemonIA, iPokemon opponent)
        {
            // Calcular la diferencia de vida y energía entre la IA y el jugador
            int vidaDiferencia = (int)(opponent.Vida - pokemonIA.Vida);
            int energiaDiferencia = (int)(opponent.Energia - pokemonIA.Energia);

            // Calcular el daño potencial que puede hacer el jugador en el siguiente turno
            int action = CalculateOpponentAction(opponent);
            int damagePotencial = CalculatePotentialDamage(action, pokemonIA, opponent);


            // Si la vida de la IA es baja y tiene pociones de vida, priorizar la recuperación de vida
            if (pokemonIA.Vida < 40 && player2PotionVida > 0)
            {
                return 2; // Descanso
            }

            // Si la energía de la IA es baja y tiene pociones de energía, priorizar la recuperación de energía
            if (pokemonIA.Energia < 40 && player2PotionEnergia > 0)
            {
                return 3; // Escudo
            }

            // Si el jugador puede infligir mucho daño en el siguiente turno y la vida de la IA es baja, priorizar la recuperación de vida
            if (damagePotencial >= 30 && pokemonIA.Vida < 60 && player2PotionVida > 0)
            {
                return 2; // Descanso
            }

            // Si el jugador puede infligir mucho daño en el siguiente turno y la energía de la IA es baja, priorizar la recuperación de energía
            if (damagePotencial >= 30 && pokemonIA.Energia < 60 && player2PotionEnergia > 0)
            {
                return 3; // Escudo
            }

            // Si la vida de la IA es significativamente menor que la del jugador, priorizar el ataque fuerte
            if (vidaDiferencia > 30)
            {
                return 1; // AtaqueFuerte
            }

            // Si la vida de la IA es igual o mayor que la del jugador y la energía es suficiente, atacar
            if (pokemonIA.Energia >= 20)
            {
                return 0; // AtaqueDébil
            }

            // Si el jugador puede infligir mucho daño en el siguiente turno y la vida de la IA es alta, priorizar el ataque fuerte
            if (damagePotencial >= 30 && pokemonIA.Vida >= 60)
            {
                return 1; // AtaqueFuerte
            }

            // Si no se cumple ninguna de las condiciones anteriores, descansar
            return 2; // Descanso
        }

        private int CalculatePotentialDamage(int action, iPokemon attacker, iPokemon defender)
        {
            switch (action)
            {
                case 0: // Ataque Débil
                        // Calcular el daño potencial para el Ataque Débil
                    int damageDebil = 15; // Daño fijo para Ataque Débil
                    if (attacker.Energia < 10) // Si no hay suficiente energía para el ataque débil
                    {
                        damageDebil = 0; // El daño potencial es cero
                    }
                    return damageDebil;
                case 1: // Ataque Fuerte
                        // Calcular el daño potencial para el Ataque Fuerte
                    int damageFuerte = 30; // Daño fijo para Ataque Fuerte
                    if (attacker.Energia < 20) // Si no hay suficiente energía para el ataque fuerte
                    {
                        damageFuerte = 0; // El daño potencial es cero
                    }
                    return damageFuerte;
                default:
                    return 0; // Para acciones como Descanso o Escudo que no infligen daño
            }
        }

        int CalculateOpponentAction(iPokemon opponent)
        {
            // Supongamos que la acción predeterminada es Ataque Débil
            int action = 0;

            // Si la vida del oponente es baja, intenta usar un Ataque Fuerte
            if (opponent.Vida < 30 && opponent.Energia >= 20)
            {
                action = 1; // Cambia la acción a Ataque Fuerte
            }
            // Si la vida del oponente es muy baja, usa el Ataque Débil para asegurar
            else if (opponent.Vida < 15 && opponent.Energia >= 10)
            {
                action = 0; // Mantén la acción como Ataque Débil
            }
            // Si el oponente tiene poca energía, defiéndete
            else if (opponent.Energia < 10)
            {
                action = 3; // Cambia la acción a Defensa
            }

            return action;
        }


        private async void BtnAtaqueFuerteDJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1 && !combateTerminado)
            {
                if (pokemon2 is iPokemon p2)
                {
                    if (p1.Energia >= 20)
                    {
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
                        }
                        else
                        {
                            MessageDialog dialog = new MessageDialog("El ataque es inofensivo ya que el Jugador 2 utilizó escudo en su turno anterior");
                            IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                            EscudoJUG2 = false;
                            Endturn();
                        }
                    }
                    else
                    {
                        MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar ataque fuerte");
                        IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                    }
                }
            }
        }

        private void BtnDescansoDJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1 && !combateTerminado)
            {
                p1.animacionDescasar();
                p1.Vida += 10;
                p1.Energia += 25;
                Endturn();
            }
        }

        private void BtnEscudoDJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1 && !combateTerminado)
            {
                if (p1.Energia >= 51)
                {
                    p1.animacionDefensa();
                    p1.Energia -= 51;
                    EscudoJUG1 = true;
                }
                else
                {
                    MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar escudo");
                    IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                }
            }
            Endturn();
        }

        private async void BtnAtaqueDebilDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1 && !combateTerminado)
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
                            MessageDialog dialog = new MessageDialog("El ataque es inofensivo ya que el Jugador 1 utilizó escudo en su turno anterior");
                            IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                            EscudoJUG1 = false;
                            Endturn();
                        }
                    }
                    else
                    {
                        MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar ataque débil");
                        IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                    }
                }
            }
        }

        private void BtnAtaqueFuerteDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1 && !combateTerminado)
            {
                if (pokemon2 is iPokemon p2)
                {
                    if (p2.Energia >= 20)
                    {
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
                            MessageDialog dialog = new MessageDialog("El ataque es inofensivo ya que el Jugador 1 utilizó escudo en su turno anterior");
                            IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                            EscudoJUG1 = false;
                            Endturn();
                        }
                    }
                    else
                    {
                        MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar ataque fuerte");
                        IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                    }
                }
            }
        }

        private void BtnDescansoDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon2 is iPokemon p2 && !combateTerminado)
            {
                p2.animacionDescasar();
                p2.Vida += 10;
                p2.Energia += 25;
                Endturn();
            }
        }

        private void BtnEscudoDJUG2_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon2 is iPokemon p2 && !combateTerminado)
            {
                if (p2.Energia >= 51)
                {
                    p2.animacionDefensa();
                    p2.Energia -= 51;
                    EscudoJUG2 = true;
                }
                else
                {
                    MessageDialog dialog = new MessageDialog("No tienes suficiente energía para utilizar escudo");
                    IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                }
            }
            Endturn();
        }

        // Métodos de pociones para el Jugador 1
        private void PotionJUG1_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1)
            {
                if (player1PotionVida > 0)
                {
                    p1.Vida += 50; // Suponiendo que una poción recupera 50 de vida
                    player1PotionVida--;
                    Endturn();
                }
                else
                {
                    MessageDialog dialog = new MessageDialog("No tienes más pociones de vida");
                    IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                }
            }
        }

        private void Potion_energia_JUG1_Click(object sender, RoutedEventArgs e)
        {
            if (pokemon1 is iPokemon p1)
            {
                if (player1PotionEnergia > 0)
                {
                    p1.Energia += 50; // Suponiendo que una poción recupera 50 de energía
                    player1PotionEnergia--;
                    Endturn();
                }
                else
                {
                    MessageDialog dialog = new MessageDialog("No tienes más pociones de energía");
                    IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                }
            }
        }
    }
}
