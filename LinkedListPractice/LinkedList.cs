using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListPractice
{
    abstract public class LinkedList<T>
    {
        public int Count { get; protected set; }
        public Node<T> First { get; protected set; }
        public Node<T> Last { get; protected set; }
        abstract public void AddFirst(T value);
        abstract public void AddLast(T value);
        abstract public void AddBefore(Node<T> node, T value);
        abstract public void AddAfter(Node<T> node, T value);
        abstract public void RemoveFirst();
        abstract public void RemoveLast();
        abstract public void Remove(Node<T> node);
    }

    public class Node<T>
    {
        public Node<T> Next { get; internal set; }
        public Node<T> Previous { get; internal set; }
        public T value { get; internal set; }

        public Node(T value)
        {
            this.value = value;
        }
    }

    public class SinglyLinkedList<T> : LinkedList<T>
    {
        public override void AddFirst(T value)
        {
            Node<T> node = new Node<T>(value);
            if (Count <= 0)
                Last = node;
            else
                node.Next = First;
            First = node;
            Count++;
        }

        public override void AddLast(T value)
        {
            Node<T> node = new Node<T>(value);
            if (Count <= 0)
                First = node;
            else
                Last.Next = node;
            Last = node;
            Count++;
        }


        /* 將某值加入在某特定節點之前
         * 做法：
         * 先看是不是加在第一個
         * 如果加在第一個，請使用AddFirst();
         * 如果不是加在第一個，要先找到該節點的前一個preNode
         * 將preNode的Next指向newNode，再將newNode的Next指向該節點
         * 
         * */
        public override void AddBefore(Node<T> node, T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (node == First)
                AddFirst(value);
            else
            {
                Node<T> preNode = FindPreviousNode(node);
                preNode.Next = newNode;
                newNode.Next = node;
            }
            Count++;
        }

        /* 將某值加在某特定節點之後
         * 先看是不是加在最後一個
         * 如果不是則要將NewNode.Next指到node.Next
         * 再將node.Next指到newNode
         */
        public override void AddAfter(Node<T> node, T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (node == Last)
                AddLast(value);
            else
            {
                newNode.Next = node.Next;
                node.Next = newNode;
            }
            Count++;
        }


        /*
         * 如果已空，發生例外；
         * 如果只有一個節點(First == Last)，全指為Null
         * 其他的話則把第二個節點設為第一個(第一個的Next變成null即離開此LinkedList)
         * 
         */
        public override void RemoveFirst()
        {
            if (Count <= 0)
                throw new IndexOutOfRangeException();
            else if (Count == 1)
            {
                First = null;
                Last = null;
            }
            else
            {
                Node<T> node = First.Next;
                First.Next = null;
                First = node;
            }
            Count--;
        }

        public override void RemoveLast()
        {
            if (Count <= 0)
                throw new IndexOutOfRangeException();
            else if (Count == 1)
            {
                First = null;
                Last = null;
            }
            else
            {
                Node<T> node = FindPreviousNode(Last);
                node.Next = null;
                Last = node;
            }
            Count--;
        }

        public override void Remove(Node<T> node)
        {
            if (Count > 0)
            {
                if (node == First)
                    RemoveFirst();
                else if (node == Last)
                    RemoveLast();
                else
                {
                    Node<T> prenode = FindPreviousNode(node);
                    prenode.Next = node.Next;
                    node.Next = null;
                }
                Count--;
                return;
            }
            throw new IndexOutOfRangeException();

        }

        private Node<T> FindPreviousNode(Node<T> node)
        {
            Node<T> preNode = First;
            while (preNode != null && node != preNode.Next)
            {
                preNode = preNode.Next;
            }
            return preNode;
        }
    }

    public class DoubleLinkedList<T> : LinkedList<T>
    {
        public override void AddFirst(T value)
        {
            Node<T> node = new Node<T>(value);
            if (Count <= 0)
                Last = node;
            else
            {
                node.Next = First;
                First.Previous = node;
            }
            First = node;
            Count++;
        }

        public override void AddLast(T value)
        {
            Node<T> node = new Node<T>(value);
            if (Count <= 0)
                First = node;
            else
            {
                node.Previous = Last;
                Last.Next = node;
            }
            Last = node;
            Count++;
        }

        public override void AddBefore(Node<T> node, T value)
        {
            if (node == First)
            {
                AddFirst(value);
                return;
            }
            Node<T> newNode = new Node<T>(value);
            newNode.Previous = node.Previous;
            node.Previous.Next = newNode;
            node.Previous = newNode;
            newNode.Next = node;
            Count++;
        }

        public override void AddAfter(Node<T> node, T value)
        {
            if(node == Last)
            {
                AddLast(value);
                return;
            }
            Node<T> newNode = new Node<T>(value);
            newNode.Next = node.Next;
            node.Next.Previous = newNode;
            node.Next = newNode;
            newNode.Previous = node;
            Count++;
        }

        public override void RemoveFirst()
        {
            if (Count <= 0)
                throw new IndexOutOfRangeException();
            else if (Count == 1)
            {
                First = null;
                Last = null;
            }
            else
            {
                Node<T> node = First.Next;
                First.Next = null;
                node.Previous = null;
                First = node;
            }
            Count--;
        }

        public override void RemoveLast()
        {
            if (Count <= 0)
                throw new IndexOutOfRangeException();
            else if (Count == 1)
            {
                First = null;
                Last = null;
            }
            else
            {
                Node<T> node = Last.Previous;
                Last.Previous = null;
                node.Next = null;
                Last = node;
            }
            Count--;
        }

        public override void Remove(Node<T> node)
        {
            if (node == First)
                RemoveFirst();
            else if (node == Last)
                RemoveLast();
            else
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
                node.Next = null;
                node.Previous = null;
                Count--;
            }
        }
    }
}
