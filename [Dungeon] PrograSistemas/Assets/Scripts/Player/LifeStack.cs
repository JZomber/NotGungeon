using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStack
{
    private Stack<int> lifeStack;

    public LifeStack()
    {
        lifeStack = new Stack<int>();
    }

    // Agregar vida a la pila
    public void AddLife(int life)
    {
        lifeStack.Push(life);
    }

    // Quitar vida de la pila
    public void RemoveLife()
    {
        if (lifeStack.Count > 0)
        {
            lifeStack.Pop();
        }
    }

    // Obtener cantidad de vidas actual
    public int GetLifeCount()
    {
        return lifeStack.Count;
    }

    // Verificar si la pila está vacía
    public bool IsEmpty()
    {
        return lifeStack.Count == 0;
    }
}
