using System;
using System.Collections.Generic;

namespace AutNutriYA{


    public class Paciente{
        public Paciente(){
            
        }
        public Paciente( string p1, string p11,string p2, int p3, int p4, int p5, double p6, string p7, string p8){
            NombreNut = p1;
            Correo = p11;
            NombrePac = p2;
            Edad = p3;
            Altura = p4;
            Peso = p5;
            IMC = p6;
            Alergias = p7; 
            CorreoNut = p8;
        }

        public string NombreNut { get; set; }
        public string NombrePac { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }
        public int Altura {get; set; }
        public int Peso {get; set; }
        public double IMC {get; set; }
        public string Alergias {get; set;}
        public string CorreoNut {get; set;}
    }

}