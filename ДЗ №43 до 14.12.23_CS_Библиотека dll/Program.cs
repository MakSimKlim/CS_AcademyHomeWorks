using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ComputerDll;

namespace ДЗ__43_CS_Сборка_с_библиотекой_dll
{
    public class Serializator
    {
        public static void SerializeComputerList(List<Computer> computerList, string fileName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, computerList);
                }

                Console.WriteLine($"Serialization successful. File saved as {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serialization error: {ex.Message}");
            }
        }
    }

    public class Deserializator
    {
        public static List<Computer> DeserializeComputerList(string fileName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    return (List<Computer>)binaryFormatter.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Пример использования сериализации
            List<Computer> computersToSerialize = new List<Computer>
            {
            new Computer { Brand = "Brand1", SerialNumber = "SN1", Generation = "Gen1" },
            new Computer { Brand = "Brand2", SerialNumber = "SN2", Generation = "Gen2" }
        };

            Serializator.SerializeComputerList(computersToSerialize, "ComputerList.dat");

            // Пример использования десериализации
            List<Computer> deserializedComputers = Deserializator.DeserializeComputerList("ComputerList.dat");

            if (deserializedComputers != null)
            {
                foreach (var computer in deserializedComputers)
                {
                    Console.WriteLine($"Brand: {computer.Brand}, Serial Number: {computer.SerialNumber}, Generation: {computer.Generation}");
                }
            }
            Console.ReadKey();
        }
    }
}
