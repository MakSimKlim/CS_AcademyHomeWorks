using System;
using System.Collections.Generic;
using System.Xml;

class Order
{
    public string OrderName { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
}

class Item
{
    public string ItemName { get; set; }
    public int Quantity { get; set; }
}

class OrderXmlManager
{
    private string filename = "orders.xml";
    private List<Order> orders = new List<Order>();

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Создать и сохранить информацию о заказах в XML-файл");
            Console.WriteLine("2. Считать информацию из XML-файла и вывести на экран");
            Console.WriteLine("3. Выход");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        GetOrderInformation();
                        SaveOrdersToXml();
                        Console.WriteLine($"Информация о заказах сохранена в файл '{filename}'.");
                        break;
                    case 2:
                        ReadOrdersFromXml();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод. Попробуйте снова.");
            }

            Console.WriteLine();
        }
    }

    private void GetOrderInformation()
    {
        orders.Clear();

        Console.WriteLine("Введите информацию о заказах. Для завершения введите '0' в поле 'Название заказа'.");
        while (true)
        {
            Console.Write("Название заказа: ");
            string orderName = Console.ReadLine();
            if (orderName.ToLower() == "0")
            {
                break;
            }

            Order order = new Order { OrderName = orderName };
            GetItemInformation(order);
            orders.Add(order);
        }
    }

    private void GetItemInformation(Order order)
    {
        Console.WriteLine("Введите информацию о товарах. Для завершения введите '0' в поле 'Наименование товара'.");
        while (true)
        {
            Console.Write("Наименование товара: ");
            string itemName = Console.ReadLine();
            if (itemName.ToLower() == "0")
            {
                break;
            }

            Console.Write("Количество: ");
            if (int.TryParse(Console.ReadLine(), out int quantity))
            {
                Item item = new Item { ItemName = itemName, Quantity = quantity };
                order.Items.Add(item);
            }
            else
            {
                Console.WriteLine("Неверный формат количества. Попробуйте снова.");
            }
        }
    }

    private void SaveOrdersToXml()
    {
        using (XmlTextWriter writer = new XmlTextWriter(filename, null))
        {
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Заказы");

            foreach (Order order in orders)
            {
                WriteOrder(writer, order);
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
    }

    private void WriteOrder(XmlTextWriter writer, Order order)
    {
        writer.WriteStartElement("Заказ");
        writer.WriteElementString("Название", order.OrderName);

        foreach (Item item in order.Items)
        {
            writer.WriteStartElement("Товар");
            writer.WriteElementString("Наименование", item.ItemName);
            writer.WriteElementString("Количество", item.Quantity.ToString());
            writer.WriteEndElement();
        }

        writer.WriteEndElement();
    }

    private void ReadOrdersFromXml()
    {
        using (XmlTextReader reader = new XmlTextReader(filename))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Заказ")
                {
                    ReadOrder(reader);
                }
            }
        }
    }

    private void ReadOrder(XmlTextReader reader)
    {
        Order order = new Order();
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Заказ")
            {
                break;
            }

            if (reader.NodeType == XmlNodeType.Element)
            {
                if (reader.Name == "Название")
                {
                    order.OrderName = reader.ReadString();
                }
                else if (reader.Name == "Товар")
                {
                    ReadItem(reader, order);
                }
            }
        }

        DisplayOrderInformation(order);
    }

    private void ReadItem(XmlTextReader reader, Order order)
    {
        Item item = new Item();
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Товар")
            {
                break;
            }

            if (reader.NodeType == XmlNodeType.Element)
            {
                if (reader.Name == "Наименование")
                {
                    item.ItemName = reader.ReadString();
                }
                else if (reader.Name == "Количество")
                {
                    if (int.TryParse(reader.ReadString(), out int quantity))
                    {
                        item.Quantity = quantity;
                    }
                }
            }
        }

        order.Items.Add(item);
    }

    private void DisplayOrderInformation(Order order)
    {
        Console.WriteLine($"Название заказа: {order.OrderName}");
        Console.WriteLine("Товары:");
        foreach (Item item in order.Items)
        {
            Console.WriteLine($"  Наименование: {item.ItemName}, Количество: {item.Quantity}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        OrderXmlManager manager = new OrderXmlManager();
        manager.Run();
    }
}
