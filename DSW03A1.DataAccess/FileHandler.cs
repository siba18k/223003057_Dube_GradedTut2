using System;
using System.Collections.Generic;
using System.IO;

namespace DSW03A1.DataAccess
{
    public class FileHandler
    {
        public static string usersFilePath = "./users.txt";
        public static string casesFilePath = "./cases.txt";

        private static readonly string[] validStatuses = { "Open", "Closed", "In Progress" };

        public List<string[]> ReadUsers()
        {
            List<string[]> users = new List<string[]>();

            if (File.Exists(usersFilePath) == false)
            {
                return users;
            }

            using (StreamReader reader = new StreamReader(usersFilePath))
            {
                bool isFirstLine = true;

                while (reader.EndOfStream == false)
                {
                    string line = reader.ReadLine();

                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    if (line.Trim() == "")
                    {
                        continue;
                    }

                    string[] parts = line.Split(',');

                    if (parts.Length == 4)
                    {
                        users.Add(parts);
                    }
                }
            }

            return users;
        }

        public List<string[]> ReadCases()
        {
            List<string[]> cases = new List<string[]>();

            if (File.Exists(casesFilePath) == false)
            {
                return cases;
            }

            using (StreamReader reader = new StreamReader(casesFilePath))
            {
                bool isFirstLine = true;

                while (reader.EndOfStream == false)
                {
                    string line = reader.ReadLine();

                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue;
                    }

                    if (line.Trim() == "")
                    {
                        continue;
                    }

                    if (line.Contains(',') == false)
                    {
                        continue;
                    }

                    string[] parts = line.Split(',');

                    if (IsValidCaseRecord(parts) == false)
                    {
                        continue;
                    }

                    cases.Add(parts);
                }
            }

            return cases;
        }

        private bool IsValidCaseRecord(string[] parts)
        {
            if (parts.Length != 4)
            {
                return false;
            }

            if (parts[0].Trim() == "")
            {
                return false;
            }

            if (parts[1].Trim() == "")
            {
                return false;
            }

            if (parts[2].Trim() == "")
            {
                return false;
            }

            string status = parts[3].Trim();

            if (status == "")
            {
                return false;
            }

            return IsValidStatus(status);
        }

        private bool IsValidStatus(string status)
        {
            for (int i = 0; i < validStatuses.Length; i++)
            {
                if (validStatuses[i].Equals(status, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public void WriteCases(List<string[]> cases)
        {
            using (StreamWriter writer = new StreamWriter(casesFilePath, false))
            {
                writer.WriteLine("caseId,studentEmail,issueType,status");

                for (int i = 0; i < cases.Count; i++)
                {
                    string line = cases[i][0] + "," +
                                  cases[i][1] + "," +
                                  cases[i][2] + "," +
                                  cases[i][3];

                    writer.WriteLine(line);
                }
            }
        }
    }
}
