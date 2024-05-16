using System;

namespace UI.PowerUps
{
    public class QueueTdaCola<T> : IQueue<T>
    {
        private T[] cola;
        private int index;
    
        public void InitQueue(int size) //Inicializo la cola
        {
            cola = new T[size];
            index = 0;
        }

        public void Acolar(T obj) //Agrego un objeto a la cola
        {
            if (index >= cola.Length)
            {
                throw new Exception("La cola esta llena");
            }
            
            for (int i = index; i > 0; i--)
            {
                cola[i] = cola[i - 1];
            }

            cola[0] = obj;
            index++;
        }

        public T DesAcolar() // Saco el primer elemento en entrar
        {
            if (index == 0)
            {
                throw new Exception("La cola esta vacia");
            }
        
            T retorno = cola[index - 1];
            index--;
        
            return retorno;
        }

        public T First() // Referencio al primer elemento en entrar
        {
            if (index == 0)
            {
                throw new Exception("La cola esta vacia");
            }
        
            return cola[index - 1];
        }

        public bool IsQueueEmpty()
        {
            return index == 0;
        }
    }
}