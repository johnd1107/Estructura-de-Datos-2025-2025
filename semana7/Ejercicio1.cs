using System;
using System.Collections.Generic;

namespace PilaEjercicios
{
    public class Ejercicio1
    {
        public static void VerificarParentesis()
        {
            Console.Clear();
            Console.Write("Ingrese una expresión matemática: ");
            string expresion = Console.ReadLine();

            Stack<char> pila = new Stack<char>();
            bool balanceado = true;

            foreach (char c in expresion)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    pila.Push(c);
                }
                else if (c == ')' || c == '}' || c == ']')
                {
                    if (pila.Count == 0)
                    {
                        balanceado = false;
                        break;
                    }

                    char tope = pila.Pop();

                    if ((c == ')' && tope != '(') ||
                        (c == '}' && tope != '{') ||
                        (c == ']' && tope != '['))
                    {
                        balanceado = false;
                        break;
                    }
                }
            }

            if (balanceado && pila.Count == 0)
                Console.WriteLine("\n Fórmula balanceada.");
            else
                Console.WriteLine("\n Fórmula NO balanceada.");
        }
    }
}
