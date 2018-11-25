using System;
using System.Collections.Generic;

namespace AutNutriYA{
    public class Platillo{

        public string tipo{get; set;}
        public string platillo{get; set;}
        public string ingredientes{get; set;}
        public double calorias{get; set;}
        public double porcion{get; set;}
        public Platillo(){}
        public Platillo(string tipo, string platillo, double calorias, string ingredientes, double porcion){
            
            this.tipo = tipo;
            this.platillo = platillo;
            this.calorias = calorias;
            this.ingredientes = ingredientes;
            this.porcion = porcion;
        }
    }
}