using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWareForATM
{
    public class BankCard
    {
        private int _pinCode;

        public string CardNumber { get; private set; }
        public int Money { get; private set; }


        public BankCard(string cardNumber, int pinCode, int money)
        {
            CardNumber = cardNumber;
            _pinCode = pinCode;
            Money = money;
        }

        public bool CheckPinCode(int userInput)
        {
            return (userInput == _pinCode);
        }

        public void ChangePinCode(int pinCode)
        {
            _pinCode = pinCode;
        }

        public void WithdrawCash(int desiredSum)
        {
            Money-=desiredSum;
        }

        public void PutCash(int desiredSum)
        {
            Money += desiredSum;
        }
    }
}
