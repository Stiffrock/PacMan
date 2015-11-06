using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_v2 //Christofer Malmberg, 911101-1715
{

    class MyStack // Klassen som skapar och hanterar stackens metoder.
    {
       /* class ListNode // Klassen som används och lagras i MyStack
        {
            public object Data { get; private set; }    // Datan som noden kommer att hantera
            public ListNode Next { get; set; }  // Datan som noden kommer att hantera

            public ListNode(object dataValue, ListNode nextNode) //Konstruktorn för klassen listnode som då är objektet som stacken kommer att hantera, tar ett object som ska lagras och en annan nod som den blir länkad till.
            {
                Data = dataValue;
                Next = nextNode;
            }
        }*/

        public ListNode firstNode = null;
        public int eCount = 0;

        public void Push(object element) // Push metoden sätter firstNode till det pushade objektet och nextNode till den forra firstNode.
        {
            if (firstNode == null)
            {
                firstNode = new ListNode(element, null);
                eCount += 1;
            }
            else
            {
                ListNode newNode = new ListNode(element, firstNode);
                firstNode = newNode;
                eCount += 1;
            }
        }

        public object Pop() // Pop metoden tar bort en nod i stacken, sätter nextNode till firstNode och minskar antalet element.
        {
            if (eCount == 0)
            {
                return null;
            }
            else
            {
                object data = firstNode.Data;
                firstNode = firstNode.Next;
                eCount -= 1;
                return data;
            }
        }

        public object Peek() // Peek metoden returnerar den nod som ligger överst på stacken.
        {
            if (eCount == 0)
            {
                return null;
            }
            else
            {
                return firstNode.Data;
            }
        }
        public int Count() //Returnerar alla element som är inlagda i stacken. 
        {
            return eCount;
        }
    }
}
