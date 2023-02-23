using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace CatIstagram.Server.Data.Entites
{
    public class user : IdentityUser
    {
        public IEnumerable<Cat> Cats { get; } = new HashSet<Cat>();
    }
}
