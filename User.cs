using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWareForATM
{
    public class User
    {
        public int Money { get; private set; }

        public User() 
        { 
            Random random= new Random();
            Money= random.Next(1000, 30000);
        }

        public void GetCash(int CashWithDraw)
        {
            Money += CashWithDraw;
        }

        public void PutCash(int desiredSum)
        {
            Money-= desiredSum;
        }
    }
}
