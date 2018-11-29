using System;
using System.Collections.Generic;

namespace AutNutriYA{
    public class Dieta{
        
        public string Nombre{get; set;}
        public string Dia{get; set;}
        public string Desayuno{get; set;}
        public string ColacionM{get; set;}
        public string Comida{get; set;}
        public string ColacionT{get; set;}
        public string Cena{get; set;}
        public string Bebida1 { get; set; }
        public string Bebida2 { get; set; }
        public string Bebida3 { get; set; }

        public Dieta(){}
        public Dieta(string Nombre, string Dia, string Desayuno, string ColacionM, string Comida, string ColacionT,string Cena, string Bebida1, string Bebida2, string Bebida3){
            
            this.Nombre = Nombre;
            this.Dia = Dia;
            this.Desayuno = Desayuno;
            this.ColacionM = ColacionM;
            this.Comida = Comida;
            this.ColacionT = ColacionT;
            this.Cena = Cena;
            this.Bebida1=Bebida1;
            this.Bebida2=Bebida2;
            this.Bebida3=Bebida3;
            
        }
    }
}