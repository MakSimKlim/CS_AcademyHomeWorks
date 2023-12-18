using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerDll
{

    public class Computer
    {
        // Поля с автоматическими свойствами
        public string Brand { get; set; }
        public string SerialNumber { get; set; }
        public string Generation { get; set; }

        // Приватное поле для хранения состояния включения/выключения
        private bool isTurnedOn;

        // Конструкторы
        public Computer()
        {
            // Конструктор по умолчанию
            Brand = null;
            SerialNumber = null;
            Generation = null;
            isTurnedOn = false;
        }

        public Computer(string brand, string serialNumber, string generation)
        {
            // Конструктор с параметрами
            Brand = brand;
            SerialNumber = serialNumber;
            Generation = generation;
            isTurnedOn = false;
        }

        // Методы
        public void TurnOn()
        {
            if (!isTurnedOn)
            {
                Console.WriteLine($"{Brand} Computer with Serial Number {SerialNumber} is turning on.");
                isTurnedOn = true;
            }
            else
            {
                Console.WriteLine($"{Brand} Computer is already turned on.");
            }
        }

        public void TurnOff()
        {
            if (isTurnedOn)
            {
                Console.WriteLine($"{Brand} Computer with Serial Number {SerialNumber} is turning off.");
                isTurnedOn = false;
            }
            else
            {
                Console.WriteLine($"{Brand} Computer is already turned off.");
            }
        }

        public void Reboot()
        {
            if (isTurnedOn)
            {
                Console.WriteLine($"{Brand} Computer with Serial Number {SerialNumber} is rebooting.");
                TurnOff();
                TurnOn();
            }
            else
            {
                Console.WriteLine($"{Brand} Computer cannot be rebooted because it's turned off.");
            }
        }
    }
}
