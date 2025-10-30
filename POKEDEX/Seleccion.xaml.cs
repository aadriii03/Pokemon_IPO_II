using System;
using System.Windows;
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
using Windows.UI.Xaml.Navigation;
using System.ServiceModel.Channels;
using Windows.UI.Popups;

namespace POKEDEX
{
    public sealed partial class Seleccion : Page
    {
        private string combateTipo;
        private Random _random = new Random();
        private int currentPlayer = 1;
        private string player1PokemonName;
        private string player2PokemonName;

        public Seleccion()
        {
            this.InitializeComponent();
            MessageDialog dialog = new MessageDialog("Selecciona el pokemon para el Jugador 1");
            IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            combateTipo = e.Parameter as string;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int randomIndex = _random.Next(1, 13);
            Image selectedControl = null;

            switch (randomIndex)
            {
                case 1:
                    selectedControl = Dragonite;
                    break;
                case 2:
                    selectedControl = Toxicroack;
                    break;
                case 3:
                    selectedControl = Butterfree;
                    break;
                case 4:
                    selectedControl = Charizard;
                    break;
                case 5:
                    selectedControl = Lucario;
                    break;
                case 6:
                    selectedControl = Snorlax;
                    break;
                case 7:
                    selectedControl = Garchomp;
                    break;
                case 8:
                    selectedControl = Piplup;
                    break;
                case 9:
                    selectedControl = Articuno;
                    break;
                case 10:
                    selectedControl = Lapras;
                    break;
                case 11:
                    selectedControl = Gengar;
                    break;
                case 12:
                    selectedControl = Grookey;
                    break;
                    
            }

            if (selectedControl != null)
            {
                GridViewCombate.SelectedItem = selectedControl;
            }
        }

        private void ApplyScaleAnimation(Image image, double scaleX, double scaleY)
        {
            // Define una animación para cambiar el tamaño de la imagen
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = scaleX;
            scaleTransform.ScaleY = scaleY;

            // Ajusta el punto de transformación para que sea el centro de la imagen
            image.RenderTransformOrigin = new Point(0.5, 0.5);

            // Aplica la animación a la imagen
            image.RenderTransform = scaleTransform;
        }

        private void Image_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ApplyScaleAnimation(sender as Image, 1.1, 1.1);
        }

        private void Image_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ApplyScaleAnimation(sender as Image, 1.0, 1.0);
        }


        private void GridViewCombate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridViewCombate.SelectedItem is Image selectedControl)
            {
                string selectedControlName = selectedControl.Name;
                MessageDialog dialog;

                if (currentPlayer == 1)
                {
                    dialog = new MessageDialog($"¡Jugador 1 ha seleccionado {selectedControlName}!");
                    IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                    imgJUG1.Source = selectedControl.Source;
                    player1PokemonName = selectedControlName;
                    currentPlayer = 2;

                    if (combateTipo == "Single")
                    {
                        player2PokemonName = GetRandomPokemon();
                        Frame.Navigate(typeof(CombateIA), new Tuple<string, string>(player1PokemonName, player2PokemonName));
                    }
                    else
                    {
                        dialog = new MessageDialog("Selecciona un pokemon para el Jugador 2");
                        asyncOperation = dialog.ShowAsync();
                    }
                }
                else if (currentPlayer == 2)
                {
                    dialog = new MessageDialog($"¡Jugador 2 ha seleccionado {selectedControlName}!");
                    IAsyncOperation<IUICommand> asyncOperation = dialog.ShowAsync();
                    imgJUG2.Source = selectedControl.Source;
                    player2PokemonName = selectedControlName;
                    Frame.Navigate(typeof(CombatePOK), new Tuple<string, string>(player1PokemonName, player2PokemonName));
                }
            }
        }

        private string GetRandomPokemon()
        {
            int randomIndex = _random.Next(1, 6);
            switch (randomIndex)
            {
                case 1:
                    return "Dragonite";
                case 2:
                    return "Toxicroack";
                case 3:
                    return "Butterfree";
                case 4:
                    return "Charizard";
                case 5:
                    return "Piplup";
                default:
                    return "Dragonite";
            }
        }
    }
}
