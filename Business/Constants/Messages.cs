using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string Added = "Added!";
        public static string Deleted = "Deleted!";
        public static string Updated = "Updated!";
        public static string NameError2Letter = "Name must be at least 2 letters!";
        public static string Error = "An error occurred";
        public static string AuthorizationDenied = "Authorization Denied";
        public static string UserRegistered = "User Registered";
        public static string UserNotFound = "User not found";
        public static string PasswordError = "Password error";
        public static string SuccessfulLogin = "Successful login";
        public static string UserAlreadyExists = "User already exist";
        public static string AccessTokenCreated = "Access token created";
    }
}
