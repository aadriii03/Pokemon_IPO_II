using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace POKEDEX
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Ajustes : Page
    {
        public Ajustes()
        {
            this.InitializeComponent();

        }

        private async void MostrarMensajeNoImplementado()
        {
            ContentDialog mensajeDialogo = new ContentDialog
            {
                Title = "Opción no implementada",
                Content = "Esta opción aún no está implementada debido a limitaciones de tiempo. Por favor, espere a la siguiente versión del software.",
                CloseButtonText = "Aceptar"
            };

            ContentDialogResult resultado = await mensajeDialogo.ShowAsync();
        }


    }
}
