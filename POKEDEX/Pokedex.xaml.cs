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


namespace POKEDEX
{

    public sealed partial class Pokedex : Page
    {
        public Pokedex()
        {
            this.InitializeComponent();
        }

        private void ApplyScaleAnimation(Grid grid, double scaleX, double scaleY)
        {
            // Define una animación para cambiar el tamaño del Grid
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = scaleX;
            scaleTransform.ScaleY = scaleY;

            // Ajusta el punto de transformación para que sea el centro del Grid
            grid.RenderTransformOrigin = new Point(0.5, 0.5);

            // Aplica la animación al Grid
            grid.RenderTransform = scaleTransform;
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ApplyScaleAnimation(sender as Grid, 1.1, 1.1);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ApplyScaleAnimation(sender as Grid, 1.0, 1.0);
        }

        private void Grid_PointerPressed_Butterfree(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ButterFreePokedex));
        }

        private void Grid_PointerPressed_Toxycroac(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ToxycroacPokedex));
        }

        private void Grid_PointerPressed_Dragonite(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(DragonitePokedex));
        }

        private void Grid_PointerPressed_Articuno(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ArticunoPokedex));
        }

        private void Grid_PointerPressed_Lucario(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LucarioPokedex));
        }

        private void Grid_PointerPressed_Charizard(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(CharizardPokedex));
        }

        private void Grid_PointerPressed_Grookey(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(GrookeyPokedex));
        }

        private void Grid_PointerPressed_Garchomp(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(GarchompPokedex));
        }

        private void Grid_PointerPressed_Piplup(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PipplupPokedex));
        }

        private void Grid_PointerPressed_Lapras(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LaprasPokedex));
        }

        private void Grid_PointerPressed_Gengar(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(GengarPokedex));
        }

        private void Grid_PointerPressed_Snorlax(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SnorlaxPokedex));
        }

    }
}
