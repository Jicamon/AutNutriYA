using System;
using System.Collections.Generic;

namespace AutNutriYA{


    public class Nutriologo{

        public Nutriologo( string newCorreo, string newNombre,string newPacientes, string newDireccion, double newTelefono){
            Correo = newCorreo;
            Nombre = newNombre;
            Pacientes = newPacientes;
            Direccion = newDireccion;
            Telefono = newTelefono;
        }
        public Nutriologo(){
            
        }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Pacientes { get; set; }
        public string Direccion {get; set; }
        public double Telefono {get; set; }
    }

}