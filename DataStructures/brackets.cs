using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{
    class Brackets
    {
        public class Node
        {
            public char value { get; set; }
            public int position { get; set; }
            public Node Next { get; set; }
        }

        public class MyStack
        {
            public Node Head { get; set; }

            public MyStack()
            {
                Head = new Node();
            }

            public void Push(char c, int position)
            {
                var newNode = new Node
                {
                    value = c,
                    position = position

                };
                if (Head.Next == null)
                {
                    Head.Next = newNode;
                }
                else
                {
                    var temp = new Node();
                    temp = Head.Next;
                    Head.Next = newNode;
                    newNode.Next = temp;
                }

            }

            public Node Pop()
            {
                if (Head.Next == null)
                {
                    throw new Exception();

                }
                else
                {
                    var returnNode = Head.Next;

                    var temp = new Node
                    {
                        Next = Head.Next.Next
                    };
                    Head = temp;

                    return returnNode;
                }
            }

            public bool Empty()
            {
                if (Head.Next == null) return true;
                return false;
            }
        }





        public static string CheckBrackets(char[] brackets)
        {
            var myStack = new MyStack();

            var lastBracketPositon = -1;

            for (var i = 0; i < brackets.Length; i++)
            {
                if (brackets[i] == '{' || brackets[i] == '[' || brackets[i] == '(')
                {
                    myStack.Push(brackets[i], i);
                    lastBracketPositon = i;

                }
                else if (brackets[i] == '}' || brackets[i] == ']' || brackets[i] == ')')
                {
                    if (myStack.Empty())
                    {
                        return (i + 1).ToString();
                    }

                    var stackNode = myStack.Pop();


                    switch (brackets[i])
                    {
                        case '}':
                            if (stackNode.value != '{') return (i + 1).ToString();
                            break;
                        case ']':
                            if (stackNode.value != '[') return (i + 1).ToString();
                            break;
                        case ')':
                            if (stackNode.value != '(') return (i + 1).ToString();
                            break;
                    }

                }
            }
            if (myStack.Empty() || lastBracketPositon == -1)
            {
                return "Success";
            }
            return ((myStack.Pop().position + 1).ToString());
        }



        //static void Main(string[] args)
        //{
        //    var brackets = Console.ReadLine().ToCharArray();
        //    var res = CheckBrackets(brackets);
        //    Console.WriteLine(res);
        //}
    }
}
