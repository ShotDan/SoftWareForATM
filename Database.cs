using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SoftWareForATM
{
    public class Database
    {
        private List<BankCard> _bankCards = new List<BankCard>();
        private BankCard _currentBankCard;

        public Database()
        {
            Init();
        }

        private void Init()
        {
            AddBankCard("1555 2500 3500 8514", 1505, 10000);
            AddBankCard("7855 7777 4501 8378", 2150, 50000);
            AddBankCard("4185 5151 5154 8981", 1485, 2000);
            AddBankCard("7777 7777 7777 7777", 1234, 9000);
            AddBankCard("1111 2551 1058 7845", 9876, 5000);
            AddBankCard("2116 5662 2155 5651", 0000, 1000);
            AddBankCard("5596 2235 8415 5125", 1111, 11000);
            AddBankCard("5517 9841 2147 8544", 5467, 21000);
            AddBankCard("8541 8545 1247 2154", 3578, 1000);
        }

        private void AddBankCard(string cardNumber, int pinCode, int money)
        {
            BankCard newBankCard = new BankCard(cardNumber, pinCode, money);
            _bankCards.Add(newBankCard);
        }

        public bool IsCardExists(string userInput)
        {
            bool isCardExists = _bankCards.Any(card => card.CardNumber == userInput);

            return isCardExists;
        }

        public bool IsPinCodeCorrect(string cardNumber, int userInput)
        {
            _currentBankCard = _bankCards.First(c => c.CardNumber == cardNumber);
            return _currentBankCard.CheckPinCode(userInput);
        }

        public void ChangePinCode(int pinCode)
        {
            _currentBankCard.ChangePinCode(pinCode);
        }

        public bool checkEnoughMoney(int desiredSum)
        {
            bool isEnoughMoney = false;

            if (_currentBankCard.Money >= desiredSum)
            {
                isEnoughMoney = true;
            }
            
            return isEnoughMoney;
        }

        public void WithdrawCash(int desiredSum)
        {
            _currentBankCard.WithdrawCash(desiredSum);
        }

        public void ShowCardBalance()
        {
            Console.WriteLine($" {_currentBankCard.Money} руб.");
        }

        public void PutCash(int desiredSum)
        {
            _currentBankCard.PutCash(desiredSum);
        }
    }
}
