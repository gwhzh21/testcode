using System;
using System.Threading.Tasks;

namespace ConsoleAppNext
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await Proudcter.ProduceConsumeConcurrently();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}
