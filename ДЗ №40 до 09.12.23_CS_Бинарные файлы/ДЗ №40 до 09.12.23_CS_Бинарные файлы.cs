using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Payment
{

    [Serializable]
    class PaymentInvoice : ISerializable
    {
        // Fields
        public double PaymentPerDay { get; set; }
        public int NumberOfDays { get; set; }
        public double PenaltyPerDay { get; set; }
        public int DaysDelayed { get; set; }

        // Non-serialized fields
        [NonSerialized] private double totalAmountWithoutPenalty;
        [NonSerialized] private double penalty;
        [NonSerialized] private double totalAmountToPay;

        // Calculated properties
        public double TotalAmountWithoutPenalty => totalAmountWithoutPenalty;
        public double Penalty => penalty;
        public double TotalAmountToPay => totalAmountToPay;

        // Constructor
        public PaymentInvoice(double paymentPerDay, int numberOfDays, double penaltyPerDay, int daysDelayed)
        {
            PaymentPerDay = paymentPerDay;
            NumberOfDays = numberOfDays;
            PenaltyPerDay = penaltyPerDay;
            DaysDelayed = daysDelayed;

            // Calculate values
            CalculateValues();
        }

        // Serialization method
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PaymentPerDay", PaymentPerDay);
            info.AddValue("NumberOfDays", NumberOfDays);
            info.AddValue("PenaltyPerDay", PenaltyPerDay);
            info.AddValue("DaysDelayed", DaysDelayed);
        }

        // Deserialization constructor
        public PaymentInvoice(SerializationInfo info, StreamingContext context)
        {
            PaymentPerDay = info.GetDouble("PaymentPerDay");
            NumberOfDays = info.GetInt32("NumberOfDays");
            PenaltyPerDay = info.GetDouble("PenaltyPerDay");
            DaysDelayed = info.GetInt32("DaysDelayed");

            // Calculate values after deserialization
            CalculateValues();
        }

        private void CalculateValues()
        {
            totalAmountWithoutPenalty = PaymentPerDay * NumberOfDays;
            penalty = DaysDelayed > 0 ? PenaltyPerDay * DaysDelayed : 0;
            totalAmountToPay = totalAmountWithoutPenalty + penalty;
        }

        public override string ToString()
        {
            return $"Оплата за день: {PaymentPerDay}\n" +
                   $"Количество дней: {NumberOfDays}\n" +
                   $"штраф за один день задержки оплаты: {PenaltyPerDay}\n" +
                   $"количество дней задержки оплаты: {DaysDelayed}\n" +
                   $"сумма к оплате без штрафа (вычисляемое поле): {TotalAmountWithoutPenalty}\n" +
                   $"штраф (вычисляемое поле): {Penalty}\n" +
                   $"общая сумма к оплате (вычисляемое поле): {TotalAmountToPay}";
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Приложение «Счет для оплаты»");

            PaymentInvoice invoice = null;

            while (true)
            {
                Console.WriteLine("1. Создать новый счет");
                Console.WriteLine("2. Сохранить счет в файл");
                Console.WriteLine("3. Загрузить счет из файла");
                Console.WriteLine("4. Вывести информацию о счете");
                Console.WriteLine("5. Выход");

                Console.Write("Выберите действие (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        invoice = CreateNewInvoice();
                        break;

                    case "2":
                        if (invoice != null)
                        {
                            SaveToFile(invoice, "payment_invoice.dat");
                            Console.WriteLine("\nСчет сохранен в файл.");
                        }
                        else
                        {
                            Console.WriteLine("\nСначала создайте счет (выберите действие 1).");
                        }
                        break;

                    case "3":
                        invoice = LoadFromFile("payment_invoice.dat");
                        if (invoice != null)
                        {
                            Console.WriteLine("\nСчет загружен из файла.");
                        }
                        else
                        {
                            Console.WriteLine("\nНе удалось загрузить счет из файла.");
                        }
                        break;

                    case "4":
                        if (invoice != null)
                        {
                            Console.WriteLine("\nИнформация о счете:\n" + invoice);
                        }
                        else
                        {
                            Console.WriteLine("\nСначала создайте счет (выберите действие 1).");
                        }
                        break;

                    case "5":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("\nНеверный выбор. Попробуйте еще раз.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static PaymentInvoice CreateNewInvoice()
        {
            Console.Write("\nВведите оплату за день: ");
            double paymentPerDay = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите количество дней: ");
            int numberOfDays = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите штраф за один день задержки оплаты: ");
            double penaltyPerDay = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите количество дней задержки оплаты: ");
            int daysDelayed = Convert.ToInt32(Console.ReadLine());

            return new PaymentInvoice(paymentPerDay, numberOfDays, penaltyPerDay, daysDelayed);
        }

        static void SaveToFile(PaymentInvoice invoice, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, invoice);
            }
        }

        static PaymentInvoice LoadFromFile(string fileName)
        {
            PaymentInvoice invoice = null;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    invoice = (PaymentInvoice)formatter.Deserialize(fs);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\nФайл не найден.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при загрузке счета: {ex.Message}");
            }
            return invoice;
        }
    }
}
