using System;
using System.Diagnostics;

class NodoAVL
{
    public int valor;
    public NodoAVL izquierdo;
    public NodoAVL derecho;
    public int altura;

    public NodoAVL(int valor)
    {
        this.valor = valor;
        this.izquierdo = null;
        this.derecho = null;
        this.altura = 1;
    }
}

class ArbolAVL
{
    public NodoAVL raiz;
    
    public int Altura(NodoAVL nodo)
    {
        return nodo == null ? 0 : nodo.altura;
    }

    public int FactorEquilibrio(NodoAVL nodo)
    {
        return nodo == null ? 0 : Altura(nodo.izquierdo) - Altura(nodo.derecho);
    }

    private NodoAVL RotacionDerecha(NodoAVL y)
    {
        NodoAVL x = y.izquierdo;
        NodoAVL T2 = x.derecho;

        x.derecho = y;
        y.izquierdo = T2;

        y.altura = Math.Max(Altura(y.izquierdo), Altura(y.derecho)) + 1;
        x.altura = Math.Max(Altura(x.izquierdo), Altura(x.derecho)) + 1;

        return x;
    }

    private NodoAVL RotacionIzquierda(NodoAVL x)
    {
        NodoAVL y = x.derecho;
        NodoAVL T2 = y.izquierdo;

        y.izquierdo = x;
        x.derecho = T2;

        x.altura = Math.Max(Altura(x.izquierdo), Altura(x.derecho)) + 1;
        y.altura = Math.Max(Altura(y.izquierdo), Altura(y.derecho)) + 1;

        return y;
    }

    public NodoAVL Insertar(NodoAVL nodo, int valor)
    {
        if (nodo == null)
            return new NodoAVL(valor);

        if (valor < nodo.valor)
            nodo.izquierdo = Insertar(nodo.izquierdo, valor);
        else if (valor > nodo.valor)
            nodo.derecho = Insertar(nodo.derecho, valor);
        else
            return nodo;

        nodo.altura = 1 + Math.Max(Altura(nodo.izquierdo), Altura(nodo.derecho));

        int balance = FactorEquilibrio(nodo);

        if (balance > 1 && valor < nodo.izquierdo.valor)
            return RotacionDerecha(nodo);
        if (balance < -1 && valor > nodo.derecho.valor)
            return RotacionIzquierda(nodo);
        if (balance > 1 && valor > nodo.izquierdo.valor)
        {
            nodo.izquierdo = RotacionIzquierda(nodo.izquierdo);
            return RotacionDerecha(nodo);
        }
        if (balance < -1 && valor < nodo.derecho.valor)
        {
            nodo.derecho = RotacionDerecha(nodo.derecho);
            return RotacionIzquierda(nodo);
        }

        return nodo;
    }

    public void Insertar(int valor)
    {
        raiz = Insertar(raiz, valor);
    }
    
    public NodoAVL Buscar(int valor)
    {
        return Buscar(raiz, valor);
    }

    private NodoAVL Buscar(NodoAVL nodo, int valor)
    {
        if (nodo == null || nodo.valor == valor)
            return nodo;
        
        if (valor < nodo.valor)
            return Buscar(nodo.izquierdo, valor);
        else
            return Buscar(nodo.derecho, valor);
    }

    public void Eliminar(int valor)
    {
        raiz = Eliminar(raiz, valor);
    }

    private NodoAVL Eliminar(NodoAVL nodo, int valor)
    {
        // Caso base
        if (nodo == null)
            return nodo;

        
        if (valor < nodo.valor)
            nodo.izquierdo = Eliminar(nodo.izquierdo, valor);
        else if (valor > nodo.valor)
            nodo.derecho = Eliminar(nodo.derecho, valor);
        else
        {
            
            if (nodo.izquierdo == null)
                return nodo.derecho;
            else if (nodo.derecho == null)
                return nodo.izquierdo;

            
            nodo.valor = ValorMinimo(nodo.derecho);

            
            nodo.derecho = Eliminar(nodo.derecho, nodo.valor);
        }

        
        nodo.altura = Math.Max(Altura(nodo.izquierdo), Altura(nodo.derecho)) + 1;

        
        int balance = FactorEquilibrio(nodo);

        
        if (balance > 1 && FactorEquilibrio(nodo.izquierdo) >= 0)
            return RotacionDerecha(nodo);

        
        if (balance > 1 && FactorEquilibrio(nodo.izquierdo) < 0)
        {
            nodo.izquierdo = RotacionIzquierda(nodo.izquierdo);
            return RotacionDerecha(nodo);
        }

        
        if (balance < -1 && FactorEquilibrio(nodo.derecho) <= 0)
            return RotacionIzquierda(nodo);

        
        if (balance < -1 && FactorEquilibrio(nodo.derecho) > 0)
        {
            nodo.derecho = RotacionDerecha(nodo.derecho);
            return RotacionIzquierda(nodo);
        }

        return nodo;
    }

    private int ValorMinimo(NodoAVL nodo)
    {
        int valorMinimo = nodo.valor;
        while (nodo.izquierdo != null)
        {
            valorMinimo = nodo.izquierdo.valor;
            nodo = nodo.izquierdo;
        }
        return valorMinimo;
    }
}


class NodoBST
{
    public int valor;
    public NodoBST izquierdo;
    public NodoBST derecho;

    public NodoBST(int valor)
    {
        this.valor = valor;
        this.izquierdo = null;
        this.derecho = null;
    }
}

class ArbolBST
{
    public NodoBST raiz;

    public void Insertar(int valor)
    {
        raiz = Insertar(raiz, valor);
    }

    private NodoBST Insertar(NodoBST nodo, int valor)
    {
        if (nodo == null)
            return new NodoBST(valor);

        if (valor < nodo.valor)
            nodo.izquierdo = Insertar(nodo.izquierdo, valor);
        else if (valor > nodo.valor)
            nodo.derecho = Insertar(nodo.derecho, valor);

        return nodo;
    }

    public NodoBST Buscar(int valor)
    {
        return Buscar(raiz, valor);
    }

    private NodoBST Buscar(NodoBST nodo, int valor)
    {
        if (nodo == null || nodo.valor == valor)
            return nodo;

        if (valor < nodo.valor)
            return Buscar(nodo.izquierdo, valor);
        else
            return Buscar(nodo.derecho, valor);
    }

    public void Eliminar(int valor)
    {
        raiz = Eliminar(raiz, valor);
    }

    private NodoBST Eliminar(NodoBST nodo, int valor)
    {
        if (nodo == null)
            return nodo;

        if (valor < nodo.valor)
            nodo.izquierdo = Eliminar(nodo.izquierdo, valor);
        else if (valor > nodo.valor)
            nodo.derecho = Eliminar(nodo.derecho, valor);
        else
        {
            
            if (nodo.izquierdo == null)
                return nodo.derecho;
            else if (nodo.derecho == null)
                return nodo.izquierdo;

            
            nodo.valor = ValorMinimo(nodo.derecho);

            
            nodo.derecho = Eliminar(nodo.derecho, nodo.valor);
        }
        return nodo;
    }

    private int ValorMinimo(NodoBST nodo)
    {
        int valorMinimo = nodo.valor;
        while (nodo.izquierdo != null)
        {
            valorMinimo = nodo.izquierdo.valor;
            nodo = nodo.izquierdo;
        }
        return valorMinimo;
    }
}

class Program
{
    static void Main()
    {
        // Datos
        int cantidad = 10000;
        int[] valores = GenerarValoresAleatorios(cantidad);
        
        // Arbol AVL
        Console.WriteLine(" ARBOL AVL ");
        MedirRendimientoAVL(valores);
        
        Console.WriteLine();
        
        // Arbol BST
        Console.WriteLine(" ARBOL BST ");
        MedirRendimientoBST(valores);
        
        Console.WriteLine("\nNota: Los tiempos pueden variar entre ejecuciones. Para resultados más precisos,");
        Console.WriteLine("se recomienda ejecutar varias veces y calcular promedios.");
    }
    
    static int[] GenerarValoresAleatorios(int cantidad)
    {
        Random rnd = new Random();
        int[] valores = new int[cantidad];
        
        for (int i = 0; i < cantidad; i++)
            valores[i] = rnd.Next(1, 100000);
            
        Array.Sort(valores);  
        return valores;
    }
    
    static void MedirRendimientoAVL(int[] valores)
    {
        ArbolAVL arbol = new ArbolAVL();
        Stopwatch stopwatch = new Stopwatch();
        
        
        stopwatch.Start();
        foreach (int valor in valores)
            arbol.Insertar(valor);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de inserción: {stopwatch.ElapsedMilliseconds} ms");
        
        
        int valorInicio = valores[0];
        int valorMedio = valores[valores.Length / 2];
        int valorFinal = valores[valores.Length - 1];
        
        stopwatch.Restart();
        NodoAVL nodoInicio = arbol.Buscar(valorInicio);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de búsqueda (inicio): {stopwatch.ElapsedMilliseconds} ms");
        
        stopwatch.Restart();
        NodoAVL nodoMedio = arbol.Buscar(valorMedio);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de búsqueda (medio): {stopwatch.ElapsedMilliseconds} ms");
        
        stopwatch.Restart();
        NodoAVL nodoFinal = arbol.Buscar(valorFinal);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de búsqueda (final): {stopwatch.ElapsedMilliseconds} ms");
        
        // Tiempo de eliminación
        int valorEliminar = valores[valores.Length / 4];
        stopwatch.Restart();
        arbol.Eliminar(valorEliminar);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de eliminación: {stopwatch.ElapsedMilliseconds} ms");
    }
    
    static void MedirRendimientoBST(int[] valores)
    {
        ArbolBST arbol = new ArbolBST();
        Stopwatch stopwatch = new Stopwatch();
        
        
        stopwatch.Start();
        foreach (int valor in valores)
            arbol.Insertar(valor);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de inserción: {stopwatch.ElapsedMilliseconds} ms");
        
        //  inicio, medio y final
        int valorInicio = valores[0];
        int valorMedio = valores[valores.Length / 2];
        int valorFinal = valores[valores.Length - 1];
        
        stopwatch.Restart();
        NodoBST nodoInicio = arbol.Buscar(valorInicio);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de búsqueda (inicio): {stopwatch.ElapsedMilliseconds} ms");
        
        stopwatch.Restart();
        NodoBST nodoMedio = arbol.Buscar(valorMedio);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de búsqueda (medio): {stopwatch.ElapsedMilliseconds} ms");
        
        stopwatch.Restart();
        NodoBST nodoFinal = arbol.Buscar(valorFinal);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de búsqueda (final): {stopwatch.ElapsedMilliseconds} ms");
        
        // se mide el tiempo y la eliminacion
        int valorEliminar = valores[valores.Length / 4];
        stopwatch.Restart();
        arbol.Eliminar(valorEliminar);
        stopwatch.Stop();
        Console.WriteLine($"Tiempo de eliminación: {stopwatch.ElapsedMilliseconds} ms");
    }
}