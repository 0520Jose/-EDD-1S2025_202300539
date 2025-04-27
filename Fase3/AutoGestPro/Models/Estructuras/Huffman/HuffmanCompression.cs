using System;
using System.Collections.Generic;
using System.Text;

class HuffmanCompression
{

    public static (string comprssed, HuffmanNode raiz) CompressWithTree(string input)
    {
        Dictionary<char, int> frecuencies = new Dictionary<char, int>();

        foreach(char c in input)
        {
            if(frecuencies.ContainsKey(c))
            {
                frecuencies[c]++;
            } else {
                frecuencies[c] = 1;
            }
        }

        PriorityQueue<HuffmanNode> priorityQueue = new PriorityQueue<HuffmanNode>();
        foreach(var kp in frecuencies)
        {
            priorityQueue.Enqueue(new HuffmanNode{Caracter = kp.Key, Frecuencia = kp.Value});
        }

        while(priorityQueue.Count > 1)
        {
            HuffmanNode izquierdo = priorityQueue.Dequeue();
            HuffmanNode derecho = priorityQueue.Dequeue();

            HuffmanNode parent = new HuffmanNode
            {
                Frecuencia = izquierdo.Frecuencia + derecho.Frecuencia,
                Izquierdo = izquierdo,
                Derecho = derecho

            };

            priorityQueue.Enqueue(parent);
        }
        HuffmanNode raiz = priorityQueue.Dequeue();
        Dictionary<char, string> codes = GenerarCodigosHuffman(raiz);

        StringBuilder compressed = new StringBuilder();
        
        foreach (char c in input)
        {
            compressed.Append(codes[c]);
        }

        return (compressed.ToString(), raiz);
    }

    public static string Descomprimir(string compressed, HuffmanNode raiz)
    {
        StringBuilder decompressed = new StringBuilder();
        HuffmanNode actual = raiz;
        foreach (char bit in compressed)
        {
            if (bit == '0')
                actual = actual.Izquierdo;
            else if (bit == '1')
                actual = actual.Derecho;

            if (actual.Caracter != '\0')
            {
                decompressed.Append(actual.Caracter);
                actual = raiz;
            }
        }

        return decompressed.ToString();
    }


    private static Dictionary<char, string> GenerarCodigosHuffman(HuffmanNode raiz)
    {
        var codes = new Dictionary<char, string>();
        GenerarCodigosHuffman(raiz, "", codes);
        return codes;
    }

    private static void GenerarCodigosHuffman(HuffmanNode node, string code, Dictionary<char, string> codes)
    {
        if (node == null)
            return;

        if (node.Caracter != '\0')
            codes[node.Caracter] = code;

        GenerarCodigosHuffman(node.Izquierdo, code + "0", codes);
        GenerarCodigosHuffman(node.Derecho, code + "1", codes);
    }
    
}

class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> list = new List<T>();

    public int Count => list.Count;

    public void Enqueue(T item)
    {
        list.Add(item);
        int i = list.Count - 1;

        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (list[i].CompareTo(list[parent]) >= 0)
                break;
            Swap(i, parent);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (list.Count == 0)
            throw new InvalidOperationException("Queue is empty");
        T front = list[0];
        list[0] = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        int actual = 0;

        while (true)
        {
            int izquierdo = 2 * actual + 1;
            int derecho = 2 * actual + 2;
            int masPequenio = actual;
            if (izquierdo < list.Count && list[izquierdo].CompareTo(list[masPequenio]) < 0)
                masPequenio = izquierdo;
            if (derecho < list.Count && list[derecho].CompareTo(list[masPequenio]) < 0)
                masPequenio = derecho;
            if (masPequenio == actual)
                break;
            Swap(actual, masPequenio);
            actual = masPequenio;
        }
        return front;
    }

    private void Swap(int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

}
