using System;
using System.Collections.Generic;
using DSW03A1.DataAccess;

namespace DSW03A1.BusinessLogic
{
    public class AuthService
    {
        private readonly FileHandler fileHandler;
        private string current_email;
        private string current_name;
        private string current_role;

        public AuthService()
        {
            fileHandler = new FileHandler();
            current_email = string.Empty;
            current_name = string.Empty;
            current_role = string.Empty;
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(current_email);
        }

        public string GetCurrentEmail()
        {
            return current_email;
        }

        public string GetCurrentName()
        {
            return current_name;
        }

        public string GetCurrentRole()
        {
            return current_role;
        }

        public bool Login(string email, string password)
        {
            List<string[]> users = fileHandler.ReadUsers();
            return TryMatchCredentials(users, email, password);
        }

        private bool TryMatchCredentials(List<string[]> users, string email, string password)
        {
            foreach (string[] user in users)
            {
                if (CredentialsMatch(user, email, password))
                {
                    EstablishSession(user);
                    return true;
                }
            }
            return false;
        }

        private bool CredentialsMatch(string[] user, string email, string password)
        {
            return user[0].Trim().Equals(email, StringComparison.OrdinalIgnoreCase)
                && user[1].Trim().Equals(password);
        }

        private void EstablishSession(string[] user)
        {
            current_email = user[0].Trim();
            current_name = user[2].Trim();
            current_role = user[3].Trim();
        }

        public void Logout()
        {
            current_email = string.Empty;
            current_name = string.Empty;
            current_role = string.Empty;
        }
    }
}
