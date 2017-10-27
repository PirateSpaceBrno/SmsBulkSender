using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmsBulkSender.GSM;

namespace GSM
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneBook phoneBook = new PhoneBook();

            foreach(var line in phoneBook.ListAllGroups())
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("----");

            foreach (var line in phoneBook.ListAllGroupMembers("PKS"))
            {
                Console.WriteLine(line.Name);
            }
            Console.WriteLine("----");

            foreach (var line in phoneBook.ListAllGroupMembers("psb"))
            {
                Console.WriteLine(line.Name);
            }
            Console.WriteLine("----");
        }
    }
}
