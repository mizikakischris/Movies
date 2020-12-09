using Movie.Interfaces;
using Movie.Repository.Data;
using Movie.Types.Models;
using System;
using System.Text;

namespace Movie.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public User Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsUniqueUser(string username)
        {
            throw new NotImplementedException();
        }

        public User Register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
