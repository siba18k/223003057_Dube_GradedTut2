using System;
using System.Collections.Generic;
using DSW03A1.DataAccess;

namespace DSW03A1.BusinessLogic
{
    public class CaseService
    {
        private readonly FileHandler fileHandler;
        private List<string[]> cases;

        public CaseService()
        {
            fileHandler = new FileHandler();
            cases = fileHandler.ReadCases();
        }

        public List<string[]> GetCasesForStudent(string studentEmail)
        {
            List<string[]> studentCases = new List<string[]>();
            foreach (string[] c in cases)
            {
                if (c[1].Trim().Equals(studentEmail, StringComparison.OrdinalIgnoreCase))
                    studentCases.Add(c);
            }
            return studentCases;
        }

        public List<string[]> GetAllCases()
        {
            return new List<string[]>(cases);
        }

        public string CreateCase(string studentEmail, string issueType)
        {
            if (string.IsNullOrWhiteSpace(issueType)) return "Issue type cannot be empty.";
            string newId = GenerateNewCaseId();
            string[] newCase = { newId, studentEmail, issueType.Trim(), "Open" };
            cases.Add(newCase);
            fileHandler.WriteCases(cases);
            return $"Case {newId} created successfully.";
        }

        public string CloseCase(string caseId)
        {
            string[] target = FindCaseById(caseId);
            if (target == null) return $"Case {caseId} does not exist.";
            if (target[3].Trim().Equals("Closed", StringComparison.OrdinalIgnoreCase))
                return $"Case {caseId} is already closed.";
            target[3] = "Closed";
            fileHandler.WriteCases(cases);
            return $"Case {caseId} has been closed.";
        }

        public string DeleteCase(string caseId)
        {
            string[] target = FindCaseById(caseId);
            if (target == null) return $"Case {caseId} does not exist.";
            if (target[3].Trim().Equals("Closed", StringComparison.OrdinalIgnoreCase))
                return $"Case {caseId} is closed and cannot be deleted.";
            cases.Remove(target);
            fileHandler.WriteCases(cases);
            return $"Case {caseId} has been deleted.";
        }

        private string[] FindCaseById(string caseId)
        {
            foreach (string[] c in cases)
            {
                if (c[0].Trim().Equals(caseId.Trim(), StringComparison.OrdinalIgnoreCase))
                    return c;
            }
            return null;
        }

        private string GenerateNewCaseId()
        {
            int maxNum = 0;
            foreach (string[] c in cases)
            {
                string id = c[0].Trim();
                if (id.StartsWith("C", StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(id.Substring(1), out int num))
                    {
                        if (num > maxNum) maxNum = num;
                    }
                }
            }
            return "C" + (maxNum + 1).ToString("D3");
        }
    }
}
