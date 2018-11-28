using System;
using System.Collections.Generic;

namespace AutNutriYA{
    public class Recordatorio{

        public string Nombre{get; set;}
        public string Dia{get; set;}
        public string platilloDesayuno{get; set;}
        public double porcionDes{get; set;}
        public string platilloComida{get; set;}
        public double porcionCom{get; set;}
        public string platilloCena{get; set;}
        public double porcionCena{get; set;}
        public string comentario{get; set;}
        public Recordatorio(){}
        public Recordatorio(string Nombre, string Dia, string platilloDesayuno,double porcionDes, string platilloComida,double porcionCom, string platilloCena,double porcionCena, string comentario)
        {
            this.Nombre = Nombre;
            this.Dia = Dia;
            this.platilloDesayuno = platilloDesayuno;
            this.porcionDes = porcionDes;
            this.platilloComida = platilloComida;
            this.porcionCom = porcionCom;
            this.platilloCena = platilloCena;
            this.porcionCena = porcionCena;
            this.comentario =  comentario;
        }
    }
}