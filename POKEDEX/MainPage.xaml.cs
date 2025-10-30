using Pokedex;
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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace POKEDEX
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (nvSample.SelectedItem == null)
            {   
                nvSample.SelectedItem = nvSample.MenuItems[0];
                contentFrame.Navigate(typeof(Principal));
            }
        }


        private void nvSample_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(Ajustes));
            }
            else
            {
                Microsoft.UI.Xaml.Controls.NavigationViewItem item = args.SelectedItem as Microsoft.UI.Xaml.Controls.NavigationViewItem;
                switch (item.Tag)
                {
                    case "Inicio":
                        contentFrame.Navigate(typeof(Principal));
                        break;
                    case "Info":
                        contentFrame.Navigate(typeof(Info));
                        break;
                    case "Mis Pokemons":
                        contentFrame.Navigate(typeof(Prueba));
                        break;
                    case "Combate":
                        contentFrame.Navigate(typeof(Combate));
                        break;
                    case "Pokedex":
                        contentFrame.Navigate(typeof(Pokedex));
                        break;
                }

            }
        }


    }
}

