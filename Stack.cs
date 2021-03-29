using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2;

namespace Assignment2
{
    class Stack<T>
    {
        public Node<T> Head { get; set; }
        public int Size { get; set; }
        public Stack()
        {
            Clear();
        }

        public void Clear()
        {
            Head = default(Node<T>);
            Size = 0;
        }

        public void Push(T element)
        {
            Node<T> newNode = new Node<T>(element);
            Node<T> current = Head;
            Head = newNode;
            Head.Previous = current;
            Size++;
        }

        public T Top()
        {
            if(IsEmpty()) throw new ApplicationException();
            return Head.Element;
        }

        public T Pop()
        {
            if (IsEmpty()) throw new ApplicationException();
            T element = Head.Element;
            Head = Head.Previous;
            Size--;
            return element;
        }

        public bool IsEmpty()
        {
            return Size == 0;
        }

        internal Stack<T> Clone()
        {
            return (Stack<T>)this.MemberwiseClone();
        }
    }
}
