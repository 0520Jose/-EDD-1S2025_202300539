using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Estructuras.Grafos
{
    public class Nodo2
    {
        public int Id { get; set; }
        public Nodo2 siguiente { get; set; }
        public List<Nodo1> hijos { get; set; }
        
        public Nodo2 (int id) {
            Id = id;
            siguiente = null;
            hijos = new List<Nodo1> {};
        }

        public void AgregarHijo(Nodo1 hijo) {
            hijos.Add(hijo);
        }

        public bool buscarHijo(int id) {
            foreach (Nodo1 hijo in hijos) {
                if (hijo.Id == id) {
                    return true;
                }
            }
            return false;
        }
    }
}