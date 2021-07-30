using LiveChat.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveChat.Server.Services
{
    public class RandomID : IRandomID
    {
        Random rng;

        public RandomID()
        {
            rng = new Random();
        }

        public string CreateID()
        {
            return "" + rng.Next(1, 999999999);
        }
    }
}
