using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ConsoleApp1
{
    class Node
    {
        public int data { get; set; }
        public Node next { get; set; }
        public Node(int d)
        {
            data = d;
            next = null;
        }
    }

    class List : IEnumerable<int>
    {
        public Node head;
        public List()
        {
            head = null;
        }

        public void AddData(int data)
        {
            Node node = new Node(data);
            if (head == null)
            {
                head = node;
            } else if(head.next == null)
            {
                head.next = node;
            }
            else
            {
                node.next = head.next;
                head.next = node;
            }
        }

        public int FindFirstGreater(int data)
        {
            /*Node node = head;
            while (node != null)
            {
                if (node.data > data)
                {
                    return node.data;
                }
                node = node.next;
            }*/
            foreach (int value in this)
            {
                if(value > data)
                {
                    return value;
                }
            }
            return -1;
        }

        public int SumOfLess(int data)
        {
            int res = 0;

            /*Node node = head;
            while (node != null)
            {
                if (node.data < data)
                {
                    res += node.data;
                }
                node = node.next;
            }*/
            foreach (int value in this)
            {
                if (value < data)
                {
                    res += value;
                }
            }
            return res;
        }

        public List GetNewListWithGreater(int data)
        {
            List newList = new List();
            /*Node node = head;
            while (node != null)
            {
                if (node.data > data)
                {
                    newList.AddData(node.data);
                }
                node = node.next;
            }*/
            foreach (int value in this)
            {
                if (value > data)
                {
                    newList.AddData(value);
                }
                
            }
            return newList;
        }

        public void RemoveAfterMax()
        {
            if (head == null)
            { return; }

            Node node = head;
            Node maxNode = head;
            int max = head.data;
            while (node != null)
            {
                if (node.data > max)
                {
                    max = node.data;
                    maxNode = node;
                }
                node = node.next;
            }

            Node temp = maxNode.next;
            maxNode.next = null;
            while (temp != null)
            {
                Node nextNode = temp.next;
                temp = null;
                temp = nextNode;
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.data;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void PrintList()
        {
            Console.WriteLine("\nPrinting list:");
            foreach (int value in this)
            {
                Console.Write(value + " ");
            }
        }

        

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            List list = new List();
            Random random = new Random();
            Console.Write("Manual input or random? (M/R): ");
            if (Console.ReadKey().Key == ConsoleKey.M)
            {
                Console.WriteLine("\nTo stop inputting enter ESCAPE.");
                int i = 1;
                do
                {
                    Console.Write("Element " + i + " : ");
                    int el = Convert.ToInt32(Console.ReadLine());
                    list.AddData(el);
                    i++;

                } while (Console.ReadKey().Key != ConsoleKey.Escape);


                Console.WriteLine("Your list: ");
                Node l = list.head;
                while (l != null)
                {
                    Console.Write(l.data + " ");
                    l = l.next;
                }
            }
            else
            {
                Console.Write("\nHow many elements to generate?: ");
                int num = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Generated list:");
                for (int i = 0; i < num; i++)
                {
                    int el = random.Next(0, num + 1);
                    list.AddData(el);
                }
                Console.WriteLine("Your list: ");
                Node l = list.head;
                while (l != null)
                {
                    Console.Write(l.data + " ");
                    l = l.next;
                }
            }

            Console.WriteLine("\n1. Find first element greater than given");
            Console.WriteLine("2. Sum of elements less than given");
            Console.WriteLine("3. Get new list with elements greater than given");
            Console.WriteLine("4. Remove elements after max");
            Console.WriteLine("ESCAPE. Exit");

            do
            {
                Console.Write("\nChoose function: ");
                ConsoleKey choise = Console.ReadKey().Key;
                switch (choise)
                {
                    case ConsoleKey.D1:
                        int res1 = list.FindFirstGreater(GivenElement());
                        Console.WriteLine("First element greater than given is " + res1);
                        break;
                    case ConsoleKey.D2:
                        int res2 = list.SumOfLess(GivenElement());
                        Console.WriteLine("Sum of elements less than given is " + res2);
                        break;
                    case ConsoleKey.D3:
                        List res3 = list.GetNewListWithGreater(GivenElement());
                        Console.Write("\nNew list is: ");
                        Node node1 = res3.head;
                        while (node1 != null)
                        {
                            Console.Write(node1.data + " ");
                            node1 = node1.next;
                        }
                        break;
                    case ConsoleKey.D4:
                        Console.Write("\nNew list is: ");
                        list.RemoveAfterMax();
                        Node node2 = list.head;
                        while (node2 != null)
                        {
                            Console.Write(node2.data + " ");
                            node2 = node2.next;
                        }
                        break;
                   /* case ConsoleKey.D5:
                        int index1 = GivenElement();
                        Console.WriteLine("\nThis element is: " + list[index1]);
                        break;
                    case ConsoleKey.D6:
                        Console.WriteLine("\nInput the Index then the Value:");
                        int index2 = GivenElement();
                        int element = GivenElement();
                        list[index2] = element;
                        Console.Write("\nNew list is: ");
                        Node node3 = list.head;
                        while (node3 != null)
                        {
                            Console.Write(node3.data + " ");
                            node3 = node3.next;
                        }
                        break;
                    case ConsoleKey.D7:
                        Console.WriteLine("\nPrinting list: ");
                        list.PrintList();
                        break;*/
                    default:
                        break;
                }
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        static int GivenElement()
        {
            Console.Write("\nInput your element: ");
            int given = Convert.ToInt32(Console.ReadLine());
            return given;
        }
    }
}