using System;
using SUD.Models;

namespace SUD.Generic
{
    public class DBHelper
    {
        static ApplicationDbContext db = new ApplicationDbContext();
        

        public static int GetState(string nombre)
        {
            var state = db.States;
            return 0;
        }
    }
}