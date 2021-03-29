using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class Node<T>
    {
        public T Element { get; set; }
        public Node<T> Previous { get; set; }

        public Node()
        {

        }

        public Node(T element)
        {
            this.Element = element;
        }

        public Node(T element, Node<T> previousNode)
        {
            this.Element = element;
            this.Previous = previousNode;
        }

    }
}
