using System;
using System.Collections.Generic;
using System.Text;

class HuffmanNode : IComparable<HuffmanNode>
{

    public char Caracter;
    public int Frecuencia;
    public HuffmanNode Izquierdo;
    public HuffmanNode Derecho;

    public int CompareTo(HuffmanNode? otro)
    {
        if (otro == null) return 1;
        return this.Frecuencia.CompareTo(otro.Frecuencia);
    }


}