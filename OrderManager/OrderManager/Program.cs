using System;

//TODO: Вынести валидацию строк и чисел.
//TODO: Использовать встроенные классы или методы для создания исключений.

class OrderManager
{
    static string GetProductName() 
    {
        string productName = "";
        bool isValid = false;

        while (!isValid)
        {
            Console.Write("Enter product name: ");
            productName = Console.ReadLine();
            if ( productName == "" )
                Console.WriteLine($"Please enter a valid product name. You entered \"{productName}\".\n");
            else
                isValid = true;
        }
        return productName;
    }

    static uint GetProductAmount(string productName)
    {
        uint productAmount = 0;
        bool isValid = false;

        while (!isValid)
        {
            try
            {
                Console.Write($"Enter how much \"{productName}\" you need: ");
                productAmount = Convert.ToUInt32(Console.ReadLine());
                if (productAmount > 0)
                    isValid = true;
                else
                    Console.WriteLine("Please enter an amount greater than 0.\n");
            }
            catch
            {
                Console.WriteLine("Invalid number entered. Please try again.\n");
            }
        }
        return productAmount;
    }

    static string GetUserName()
    {
        string userName = "";
        bool isValid = false;

        while (!isValid)
        {
            Console.Write("Enter user name: ");
            userName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userName))
                Console.WriteLine($"Please enter a valid user name. You entered \"{userName}\".\n");
            else
                isValid = true;
        }
        return userName;
    }

    static string GetDeliveryAddress() //TODO: Можно проводить более подробный опрос адремса у пользователя (страна, город, улица, дом, корпус, квартира)
    {
        string deliveryAddress = "";
        bool isValid = false;

        while (!isValid)
        {
            Console.Write("Enter the delivery address: ");
            deliveryAddress = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(deliveryAddress))
                Console.WriteLine($"Please enter a valid delivery address. You entered \"{deliveryAddress}\".\n");
            else
                isValid = true;
        }
        return deliveryAddress;
    }

    static bool IsCorrectOrder(string productName, uint productAmount, string userName, string deliveryAddress)
    {
        Console.WriteLine($"\nHi, {userName}! You ordered {productAmount} unit(s) of \"{productName}\" for delivery to {deliveryAddress}.");
        Console.Write("Is it correct? (Enter y/n): ");
        char flag = Console.ReadKey().KeyChar;
        Console.WriteLine();
        return flag == 'y' || flag == 'Y';
    }

    static DateTime GetDeliveryDate()
    {
        const int deliveryDays = 3;
        return DateTime.Today.AddDays(deliveryDays);
    }

    static void WriteOrder(string productName, uint productAmount, string userName, string deliveryAddress, DateTime deliveryDate)
    {
        Console.WriteLine($"\n{userName}, your order for {productAmount} unit(s) of \"{productName}\" has been placed!");
        Console.WriteLine($"Expect delivery to: {deliveryAddress} by {deliveryDate:dd.MM.yyyy}");
    }

    static void Main()
    {
        string productName, userName, deliveryAddress;
        uint productAmount;
        DateTime deliveryDate;

        do
        {
            Console.WriteLine();
            productName = GetProductName();
            productAmount = GetProductAmount(productName);
            userName = GetUserName();
            deliveryAddress = GetDeliveryAddress();
        } while (!IsCorrectOrder(productName, productAmount, userName, deliveryAddress));

        deliveryDate = GetDeliveryDate();
        WriteOrder(productName, productAmount, userName, deliveryAddress, deliveryDate);
    }
}
