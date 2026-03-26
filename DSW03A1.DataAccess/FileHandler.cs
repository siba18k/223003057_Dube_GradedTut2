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
            string[] lines = File.ReadAllLines(usersFilePath);
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(',');
                if (parts.Length == 4)
                    users.Add(parts);
            }
            return users;
        }

        public List<string[]> ReadCases()
        {
            List<string[]> cases = new List<string[]>();
            if (!File.Exists(casesFilePath)) return cases;
            string[] lines = File.ReadAllLines(casesFilePath);
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.Contains(',')) continue;
                string[] parts = line.Split(',');
                if (!IsValidCaseRecord(parts)) continue;
                cases.Add(parts);
            }
            return cases;
        }

        private bool IsValidCaseRecord(string[] parts)
        {
            if (parts.Length != 4) return false;
            if (string.IsNullOrWhiteSpace(parts[0])) return false;
            if (string.IsNullOrWhiteSpace(parts[1])) return false;
            if (string.IsNullOrWhiteSpace(parts[2])) return false;
            string status = parts[3].Trim();
            if (string.IsNullOrWhiteSpace(status)) return false;
            return IsValidStatus(status);
        }

        private bool IsValidStatus(string status)
        {
            foreach (string valid in validStatuses)
            {
                if (valid.Equals(status, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        public void WriteCases(List<string[]> cases)
        {
            List<string> lines = new List<string>();
            lines.Add("caseId,studentEmail,issueType,status");
            foreach (string[] c in cases)
                lines.Add(string.Join(",", c));
            File.WriteAllLines(casesFilePath, lines);
        }
    }
}
