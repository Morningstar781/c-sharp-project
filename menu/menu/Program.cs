using System;
using System.Collections.Generic;
using System.IO;

class GroceryItem
{
    public int ItemID { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Total => Quantity * Price;
}

class Program
{
    static List<GroceryItem> groceryList = new List<GroceryItem>();
    const double VAT_RATE = 0.16;

    static void Main()
    {
        Console.WriteLine("Enter the input file name (with extension): ");
        string fileName = Console.ReadLine();
        ReadFromFile(fileName);
        PrintReceipt();
        WriteToFile("output_receipt.txt");
    }

    static void ReadFromFile(string fileName)
    {
        try
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 4)
                {
                    groceryList.Add(new GroceryItem
                    {
                        ItemID = int.Parse(parts[0]),
                        Name = parts[1],
                        Quantity = int.Parse(parts[2]),
                        Price = double.Parse(parts[3])
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
        }
    }

    static void PrintReceipt()
    {
        double subTotal = 0;
        Console.WriteLine("\nShopping Receipt");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("ID  Name       Qty  Price   Total");
        Console.WriteLine("-------------------------------------------");

        foreach (var item in groceryList)
        {
            Console.WriteLine($"{item.ItemID,-5} {item.Name,-10} {item.Quantity,-4} {item.Price,-7} {item.Total,-7}");
            subTotal += item.Total;
        }

        double tax = subTotal * VAT_RATE;
        double grandTotal = subTotal + tax;

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"Sub-Total: {subTotal:F2}");
        Console.WriteLine($"VAT (16%): {tax:F2}");
        Console.WriteLine($"Grand Total: {grandTotal:F2}");
    }

    static void WriteToFile(string outputFileName)
    {
        using (StreamWriter writer = new StreamWriter(outputFileName))
        {
            writer.WriteLine("Shopping Receipt");
            writer.WriteLine("-------------------------------------------");
            writer.WriteLine("ID  Name       Qty  Price   Total");
            writer.WriteLine("-------------------------------------------");
            double subTotal = 0;

            foreach (var item in groceryList)
            {
                writer.WriteLine($"{item.ItemID,-5} {item.Name,-10} {item.Quantity,-4} {item.Price,-7} {item.Total,-7}");
                subTotal += item.Total;
            }

            double tax = subTotal * VAT_RATE;
            double grandTotal = subTotal + tax;

            writer.WriteLine("-------------------------------------------");
            writer.WriteLine($"Sub-Total: {subTotal:F2}");
            writer.WriteLine($"VAT (16%): {tax:F2}");
            writer.WriteLine($"Grand Total: {grandTotal:F2}");
        }
        Console.WriteLine($"Receipt written to {outputFileName}");
    }
}
