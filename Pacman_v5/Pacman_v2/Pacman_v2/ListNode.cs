using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_v2 //Christofer Malmberg, 911101-1715
{
    class ListNode // Klassen som används och lagras i MyStack
    {
        public object Data { get; private set; }    // Datan som noden kommer att hantera
        public ListNode Next { get; set; }  // Datan som noden kommer att hantera

        public ListNode(object dataValue, ListNode nextNode) //Konstruktorn för klassen listnode som då är objektet som stacken kommer att hantera, tar ett object som ska lagras och en annan nod som den blir länkad till.
        {
            Data = dataValue;
            Next = nextNode;
        }
    }
}
