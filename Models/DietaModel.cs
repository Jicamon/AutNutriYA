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
        public string Desayuno_A{get; set;}
        public string ColacionM_A{get; set;}
        public string Comida_A{get; set;}
        public string ColacionT_A{get; set;}
        public string Cena_A{get; set;}
        public string Bebida1_A { get; set; }
        public string Bebida2_A { get; set; }
        public string Bebida3_A { get; set; }

        public Dieta(){}
        public Dieta(string Nombre, string Dia, string Desayuno, string Desayuno_A, string ColacionM, string ColacionM_A, 
                     string Comida, string Comida_A, string ColacionT, string ColacionT_A, string Cena, string Cena_A, 
                     string Bebida1, string Bebida1_A, string Bebida2, string Bebida2_A, string Bebida3, string Bebida3_A){
            
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
            this.Desayuno_A = Desayuno_A;
            this.ColacionM_A = ColacionM_A;
            this.Comida_A = Comida_A;
            this.ColacionT_A = ColacionT_A;
            this.Cena_A = Cena_A;
            this.Bebida1_A=Bebida1_A;
            this.Bebida2_A=Bebida2_A;
            this.Bebida3_A=Bebida3_A;
            
        }
    }
}