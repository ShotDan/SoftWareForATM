using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftWareForATM
{
    public class ATM
    {
        private Database _database = new Database();
        private User _user = new User();

        private void ShowInfo()
        {
            Console.WriteLine("Добро пожаловать в банкомат!\nВсе пользователи могут снимать до 50000 рублей за одну транзакцию.");
            Console.WriteLine();
            Console.WriteLine("Введите номер банковской карты, расставляя пробелы:");
        }

        private void TryChangePinCode(int pinCode)
        {
            if (pinCode.ToString().Length == 4)
            {
                _database.ChangePinCode(pinCode);
                Console.WriteLine("Пин-код успешно изменён!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Пин-код не может быть меньше или больше 4 цифр!");
                Console.ReadKey();
            }
        }

        private void ShowError()
        {
            Console.WriteLine("Некорректный ввод!");
        }

        private void TryWithdrawCash()
        {
            Console.WriteLine("Какую сумму вы хотите снять с банковской карты?");
            string userInput = Console.ReadLine();
            int.TryParse(userInput, out int desiredSum);

            if(desiredSum > 0 && desiredSum <= 50000)
            {
                if (_database.checkEnoughMoney(desiredSum))
                {
                    _database.WithdrawCash(desiredSum);
                    _user.GetCash(desiredSum);
                }
                else
                {
                    Console.WriteLine("На балансе недостаточно средств!");
                }
            }
            else
            {
                ShowError();
            }
        }

        private void TryPutMoney()
        {
            Console.WriteLine("На какую сумму вы желаете пополнить банковскую карту?");
            string userInput = Console.ReadLine();
            int.TryParse(userInput, out int desiredSum);

            if(desiredSum > 0)
            {
                if (_user.Money >= desiredSum)
                {
                    _database.PutCash(desiredSum);
                    _user.PutCash(desiredSum);
                }
                else
                {
                    Console.WriteLine("У вас недостаточно наличных!");
                }
            }
            else
            {
                ShowError();
            }
        }

        private void BankCardManagment()
        {
            bool isOpen = true;
            while (isOpen)
            {

                Console.Clear();
                Console.WriteLine($"Ваши наличные: {_user.Money} руб.");
                Console.Write("Баланс карты:");
                _database.ShowCardBalance();
                Console.WriteLine("Выберите операцию:\n1 - Снять наличные\n2 - Пополнить баланс карты\n3 - Изменить пин-код карты\n4 - Выход");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        TryWithdrawCash();
                        break;

                    case "2":
                        TryPutMoney();
                        break;

                    case "3":
                        Console.WriteLine("Введите новый пинкод(пинкод должен содержать 4 цифры):");
                        userInput = Console.ReadLine();

                        if (int.TryParse(userInput, out int inputPinCode))
                        {
                            TryChangePinCode(inputPinCode);
                        }
                        else
                        {
                            ShowError();
                        }
                        break;

                    case "4":
                        isOpen = false;
                        break;

                    default:
                        ShowError();
                        break;
                }
                Console.ReadKey();
            }
        }

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                ShowInfo();
                string userInput = Console.ReadLine();

                if (_database.IsCardExists(userInput))
                {
                    string cardNumber = userInput;
                    Console.WriteLine("Введите пин-код от вашей карты:");
                    userInput = Console.ReadLine();
                    int.TryParse(userInput, out int pinCode);

                    if (_database.IsPinCodeCorrect(cardNumber, pinCode))
                    {
                        BankCardManagment();
                    }
                    else
                    {
                        Console.WriteLine("Неверный пинкод!");
                    }
                }
                else
                {
                    Console.WriteLine("Такой карты не существует!");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
