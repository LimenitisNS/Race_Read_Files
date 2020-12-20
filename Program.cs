using System;

namespace Race_Read_Files
{
    class Program
    {
        static void Main(string[] args)
        {
            File file = new File();
            file.Generate(2, 30);
            file.AlternateEncryption(2);
            file.ParallelEncryption(2);
            Console.ReadKey();
        }
    }
}
