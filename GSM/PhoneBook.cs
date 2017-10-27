using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmsBulkSender.GSM
{
    public class PhoneBook
    {
        private List<Record> AllRecords = new List<Record>();
        private List<string> AllGroups = new List<string>();

        public struct Record
        {
            public string Name;
            public string PhoneNumber;
            public List<string> Groups;
        };

        /// <summary>
        /// Creates new instance of PhoneBook based on data stored in PhoneBook.csv file.
        /// </summary>
        public PhoneBook()
        {
            Reload();
        }

        /// <summary>
        /// Reloads whole content of the PhoneBook.
        /// </summary>
        public void Reload()
        {
            List<string> phoneBookFileContent = File.ReadAllLines($@"{AppDomain.CurrentDomain.BaseDirectory}/PhoneBook.csv").ToList();
            List<string> output = new List<string>();

            AllRecords.Clear();
            AllGroups.Clear();

            foreach (string line in phoneBookFileContent)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    Record parsedRecord = ParseRecord(line);
                    AllRecords.Add(parsedRecord);

                    foreach (var group in parsedRecord.Groups)
                    {
                        if (!output.Contains(group, StringComparer.OrdinalIgnoreCase) && !string.IsNullOrEmpty(group))
                        {
                            output.Add(group);
                        }
                    }
                }
            }

            AllGroups = output;
        }

        private Record ParseRecord(string phoneBookLine)
        {
            List<string> explodedLine = phoneBookLine.Split(';').ToList();

            return new Record() {
                Name = explodedLine[0],
                PhoneNumber = explodedLine[1],
                Groups = explodedLine.Skip(2).ToList()
            };
        }

        /// <summary>
        /// Returns list of all groups in PhoneBook.
        /// </summary>
        /// <returns></returns>
        public List<string> ListAllGroups()
        {
            return AllGroups;
        }

        /// <summary>
        /// Returns list of all members in PhoneBook.
        /// </summary>
        /// <returns></returns>
        public List<Record> ListAllMembers()
        {
            return AllRecords;
        }

        /// <summary>
        /// Returns list of all group members in PhoneBook.
        /// </summary>
        /// <param name="group">Group name (e.g. from List of all groups)</param>
        /// <returns></returns>
        public List<Record> ListAllGroupMembers(string group)
        {
            return AllRecords.FindAll(x => x.Groups.Contains(group, StringComparer.OrdinalIgnoreCase)).ToList();
        }
    }
}
