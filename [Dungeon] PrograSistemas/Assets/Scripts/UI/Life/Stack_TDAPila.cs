using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Stack_TDAPila<T>
{
    private T[] arr_estatico;
    private bool inicializado = false;
    private int tope;

    public void InicializarPila(int capacidad)
    {
        arr_estatico = new T[capacidad];
        inicializado = true;
        tope = -1;
    }

    public bool PilaVacia()
    {
        if (inicializado)
        {
            if (tope == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            throw new Exception("La pila no ha sido inicializada.");
        }
    }

    public void Apilar(T elemento)
    {
        if (inicializado)
        {
            tope++;
            arr_estatico[tope] = elemento;
            return;
        }
        else
        {
            throw new Exception("La pila no ha sido inicializada.");
        }
    }

    public T Desapilar()
    {
        if (inicializado)
        {
            if (!PilaVacia())
            {
                T elemento = arr_estatico[tope];
                arr_estatico[tope] = default(T);
                tope--;
                return elemento;
            }
            else
            {
                throw new Exception("La pila está vacía.");
            }
        }
        else
        {
            throw new Exception("La pila no ha sido inicializada.");
        }
    }

    public T Tope()
    {
        if (inicializado)
        {
            if (!PilaVacia())
            {
                return arr_estatico[tope];
            }
            else
            {
                throw new Exception("La pila está vacía.");
            }
        }
        else
        {
            throw new Exception("La pila no ha sido inicializada.");
        }
    }

    public int ObtenerTamano()
    {
        if (inicializado)
        {
            return tope + 1;
        }
        else
        {
            throw new Exception("La pila no ha sido inicializada.");
        }
    }

    public void LimpiarPila()
    {
        if (inicializado)
        {
            for (int i = 0; i <= tope; i++)
            {
                arr_estatico[i] = default(T);
            }
            tope = -1;
            return;
        }
        else
        {
            throw new Exception("La pila no ha sido inicializada.");
        }
    }

    public int Count()
    {
        if (inicializado)
        {
            return tope + 1;
        }
        else
        {
            throw new Exception("La pila no ha sido inicializada.");
        }
    }

}