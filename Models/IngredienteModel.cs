using System;
using System.Collections.Generic;

namespace AutNutriYA{

    public class Ingrediente{
        public Ingrediente(){
            
        }

    public Ingrediente(string PK, string RK){
        Tipo = PK;
        Nombre = RK;
    }

    public string Tipo {get;  set;}
    public string Nombre {get; set;}

    }

}