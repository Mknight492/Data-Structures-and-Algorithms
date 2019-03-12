using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Addition
{


    class StringHashMap
    {
        public class Contact
        {
            public long PhoneNumber { get; set; }
            public string Name { get; set; }
        }

        public class HashMap
        {
            private List<string>[] hashMap;
            private long a = -1;
            private long b = -1;
            private int prime = 1000000007;
            private ulong[] hashMemo = new ulong[50];

            public HashMap(int m)
            {
                hashMap = new List<string>[m];
            }

            private long HashLong(long numberToHash)
            {
                if (a == -1)
                {
                    var randomGen = new Random();
                    a = randomGen.Next(1, this.prime - 1);
                    b = randomGen.Next(0, prime - 1);
                }
                var Hashed = (((a * numberToHash + b) % prime) % 5000);
                return Hashed;
            }


            private ulong HashString(string stringToHash)
            {
                ulong total = 0;

                for (int i = stringToHash.Length - 1; i >= 0; i--)
                {
                    total = (total * 263 + Convert.ToUInt64(stringToHash[i])) % (ulong)prime;
                }
                return total % (ulong)hashMap.Length;

            }

            public string Find(string stringToFind)
            {
                var Hashed = HashString(stringToFind);
                if (hashMap[Hashed] == null)
                {
                    return "no";
                }
                else if (hashMap[Hashed].Find(x => x == stringToFind) != null)
                {
                    return "yes";
                }
                else
                {
                    return "no";
                }
            }

            public void Add(string stringToAdd)
            {
                var Hashed = HashString(stringToAdd);
                if (hashMap[Hashed] == null)
                {
                    hashMap[Hashed] = new List<string>();
                }
                var contact = hashMap[Hashed].SingleOrDefault(x => x == stringToAdd);
                if (contact == null)
                {
                    hashMap[Hashed].Add(stringToAdd);
                }

            }

            public void Delete(string stringToDelete)
            {
                var Hashed = HashString(stringToDelete);
                if (hashMap[Hashed] != null)
                {
                    var contact = hashMap[Hashed].SingleOrDefault(x => x == stringToDelete);
                    if (contact != null)
                    {
                        hashMap[Hashed].Remove(stringToDelete);
                    }
                }
            }

            public List<string> Check(long i)
            {
                if (hashMap[i] != null)
                {
                    return hashMap[i];
                }
                else return new List<string>();
            }
        }


        public static void HashQueries(string[] queries, int m)
        {
            var HashSetInstance = new HashMap(m);
            foreach (var query in queries)
            {
                var querieSegments = query.Split(' ');
                var qs = querieSegments.Skip(1);
                var sqr = string.Join(" ", qs);

                switch (querieSegments[0])
                {
                    case "add":
                        HashSetInstance.Add(sqr);
                        break;
                    case "del":
                        HashSetInstance.Delete(sqr);
                        break;
                    case "find":
                        var contact = HashSetInstance.Find(sqr);
                        Console.WriteLine(contact);
                        break;
                    case "check":
                        var HashBucket = HashSetInstance.Check(Convert.ToInt64(querieSegments[1]));
                        //HashBucket.Reverse();
                        if (HashBucket.Count == 0)
                        {
                            Console.WriteLine();
                        }
                        else
                        {
                            for (var i = HashBucket.Count - 1; i >= 0; i--)
                            {
                                Console.Write(HashBucket[i] + " ");
                            }
                            Console.WriteLine();
                        }
                        break;
                }
            }


        }


        //static void Main(string[] args)
        //{

        //    var m = Convert.ToInt32(Console.ReadLine());
        //    var numberOfQueries = Convert.ToInt32(Console.ReadLine());
        //    var list = new String[numberOfQueries];
        //    for (var i = 0; i < numberOfQueries; i++)
        //    {
        //        list[i] = Console.ReadLine();
        //    }
        //    HashQueries(list, m);
        //}
    }
}




