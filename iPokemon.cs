using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1_Prueba
{
    interface iPokemon
    {  
        public double Vida { get; set; }
        public double Energia { get; set; }
        public string Nombre { get; set; } //Nombre del Pokemon
        public string Categoría { get; set; } //Gas, Murciélago, Ratón..
        public string Tipo { get; set; } //Eléctrico, Veneno, Volador...
        public double Altura { get; set; } // En metros
        public double Peso { get; set; } // En kilos
        public string Evolucion { get; set; } // Nombre de la evolución o evoluciones
        public string Descripcion { get; set; } // Entre 200 y 500 caracteres
        
        public void verFondo(bool ver);
        public void verFilaVida(bool ver);
        public void verFilaEnergia(bool ver);
        public void verPocionVida(bool ver);
        public void verPocionEnergia(bool ver);
        public void verNombre(bool ver);

        public void activarAniIdle(bool activar);
        public void animacionAtaqueFlojo();
        public void animacionAtaqueFuerte();
        public void animacionDefensa();
        public void animacionDescasar();

        public void animacionCansado();
        public void animacionNoCansado();
        public void animacionHerido();
        public void animacionNoHerido();
        public void animacionDerrota();

    }
}
