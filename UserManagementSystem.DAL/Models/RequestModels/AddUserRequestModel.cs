﻿namespace UserManagementSystem.DAL.Models.RequestModels
{
    public class AddUserRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
    }
}
