using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class Combate : Page
    {
        public Combate()
        {
            this.InitializeComponent();
        }

        private void Button_Click_Single(object sender, RoutedEventArgs e)
        {
            Frame aux = (Frame)this.Parent;
            aux.Navigate(typeof(Seleccion), "Single");
        }

        private void Button_Click_Multi(object sender, RoutedEventArgs e)
        {
            Frame aux = (Frame)this.Parent;
            aux.Navigate(typeof(Seleccion), "Multi");
        }

        /* Animaciones para los botones al presionar */

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                ScaleTransform scaleTransform = new ScaleTransform();
                scaleTransform.ScaleX = 1.1;
                scaleTransform.ScaleY = 1.1;

                button.RenderTransformOrigin = new Point(0.5, 0.5);

                button.RenderTransform = scaleTransform;
            }
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                ScaleTransform scaleTransform = new ScaleTransform();
                scaleTransform.ScaleX = 1.0;
                scaleTransform.ScaleY = 1.0;

                button.RenderTransformOrigin = new Point(0.5, 0.5);

                button.RenderTransform = scaleTransform;
            }
        }




    }
}
