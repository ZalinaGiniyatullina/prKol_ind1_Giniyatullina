using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace prKol_ind1_Giniyatullina
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = "file1.txt";

            if (!File.Exists(file))
            {
                Console.WriteLine($"Файл {file} не найден!");
                return;
            }

            //с зарплатой меньше 10000
            Queue<string> mensheTen = new Queue<string>();

            //с зарплатой 10000 и больше
            Queue<string> ostalnie = new Queue<string>();

            string[] lines = File.ReadAllLines(file);


            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(';');

                if (parts.Length >= 6 && int.TryParse(parts[5], out int zp))
                {
                    if (zp < 10000)
                    {
                        mensheTen.Enqueue(line);
                    }
                    else
                    {
                        ostalnie.Enqueue(line);
                    }
                }
            }
                Console.WriteLine("РЕЗУЛЬТАТ (Сначала с ЗП < 10000, затем остальные):");
            Console.WriteLine();

            int counter = 1;
            while (mensheTen.Count > 0)
            {
                string sot = mensheTen.Dequeue();
                string[] parts = sot.Split(';');
                Console.WriteLine($"{counter}. {parts[0]} {parts[1]} {parts[2]}, {parts[3]}, {parts[4]} лет, {parts[5]} руб.");
                counter++;
            }

            counter = 1;
            while (ostalnie.Count > 0)
            {
                string employee = ostalnie.Dequeue();
                string[] parts = employee.Split(';');
                Console.WriteLine($"{counter}. {parts[0]} {parts[1]} {parts[2]}, {parts[3]}, {parts[4]} лет, {parts[5]} руб.");
                counter++;
            }

            Console.ReadKey();
        }
    }
}
