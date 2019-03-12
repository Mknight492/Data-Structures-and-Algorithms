using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addition.DataStructures
{
    class HashMapClass
    {
            public class Contact
            {
                public long PhoneNumber { get; set; }
                public string Name { get; set; }
            }

            public class HashMap2
            {
                private List<Contact>[] hashMap = new List<Contact>[5000];
                private long a = -1;
                private long b = -1;
                private int prime2 = 10000019;

                private long Hash(long numberToHash)
                {
                    if (a == -1)
                    {
                        var randomGen = new Random();
                        a = randomGen.Next(1, prime2 - 1);
                        b = randomGen.Next(0, prime2 - 1);
                    }
                    var Hashed = (((a * numberToHash + b) % prime2) % 5000);
                    return Hashed;
                }

                public bool HasContact(long number)
                {
                    var Hashed = Hash(number);
                    if (hashMap[Hashed] == null)
                    {
                        return false;
                    }
                    else if (hashMap[Hashed].Find(x => x.PhoneNumber == number) != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                public string GetContact(long number)
                {
                    var Hashed = Hash(number);
                    if (hashMap[Hashed] == null)
                    {
                        return "not found";
                    }
                    var contact = hashMap[Hashed].Find(x => x.PhoneNumber == number);
                    if (contact != null && contact.Name != null)
                    {
                        return contact.Name;
                    }
                    else
                    {
                        return "not found";
                    }

                }
                public void AddContact(long number, string name)
                {
                    var Hashed = Hash(number);
                    if (hashMap[Hashed] == null)
                    {
                        hashMap[Hashed] = new List<Contact>();
                    }
                    var contact = hashMap[Hashed].Find(x => x.PhoneNumber == number);
                    if (contact != null && contact.Name != null)
                    {
                        contact.Name = name;
                    }
                    else
                    {
                        hashMap[Hashed].Add(new Contact
                        {
                            Name = name,
                            PhoneNumber = number
                        });
                    }
                }

                public void DeleteContact(long number)
                {
                    var Hashed = Hash(number);
                    if (hashMap[Hashed] != null)
                    {
                        var contact = hashMap[Hashed].Find(x => x.PhoneNumber == number);
                        if (contact != null)
                        {
                            hashMap[Hashed].Remove(contact);
                        }
                    }
                }
            }


            public static void HashQueries(string[] queries)
            {
                var HashSetInstance = new HashMap2();
                foreach (var query in queries)
                {
                    var querieSegments = query.Split(' ');
                    switch (querieSegments[0])
                    {
                        case "add":
                            HashSetInstance.AddContact(Convert.ToInt64(querieSegments[1]), querieSegments[2]);
                            break;
                        case "del":
                            HashSetInstance.DeleteContact(Convert.ToInt64(querieSegments[1]));
                            break;
                        case "find":
                            var contact = HashSetInstance.GetContact(Convert.ToInt64(querieSegments[1]));
                            Console.WriteLine(contact);
                            break;
                    }
                }


            }

            //static void Main(string[] args)
            //{
            //    //generate the array with the size of the heap at index 0
            //    var numberOfQueries = Convert.ToInt64(Console.ReadLine());
            //    var list = new String[numberOfQueries];
            //    for (var i = 0; i < numberOfQueries; i++)
            //    {
            //        list[i] = Console.ReadLine();
            //    }

            //    HashQueries(list);
            //}
        }
    
}

