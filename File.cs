using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Race_Read_Files
{
    class File
    {
        public void Generate(int filesCount, int fileSize)
        {
            Random random = new Random();

            for (int id = 1; id <= filesCount; ++id)
            {
                string path = Path.GetFullPath(@"..\..\Files\");

                using (StreamWriter sw = new StreamWriter(path + $@"file-{id}.txt"))
                {
                    for(int i = 0; i < fileSize; ++i)
                    {
                        sw.Write((char)random.Next(97, 123));

                        if(i % 20 == 0 &&  i != 0)
                        {
                            sw.WriteLine();
                        }
                    }
                }
            }
        }

        private int FileSize(string path)
        {
            int fileSize = 0;
            string line;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    for (int i = 0; i < line.Length; ++i)
                    {
                        fileSize++;
                    }
                }
            }

            return fileSize;
        }

        private async Task<StringBuilder> XOREncryptionAsync(string path, char key)
        {
            StringBuilder hash = new StringBuilder();
            int fileSize = FileSize(path);
            char[] str = new char[fileSize];

            using (StreamReader sr = new StreamReader(path))
            {
                await sr.ReadAsync(str, 0, fileSize);

                foreach (char i in str)
                {
                    hash.Append(((char)(i ^ key)).ToString());
                }

            }

            return hash;
        }

        public async void AlternateEncryption(int filesCount)
        {
            for(int id = 1; id <= filesCount; ++id)
            {
                Console.WriteLine($"{id} - {await Task.Run(() => XOREncryptionAsync($@"C:\prj\Race_Read_Files\Files\file-{id}.txt", 'Z'))}");
            }
        }

        public async void ParallelEncryption(int filesCount)
        {
            Task<StringBuilder>[] tasks = new Task<StringBuilder>[filesCount];

            for (int id = 0; id < filesCount; ++id)
            {
                tasks[id] = XOREncryptionAsync($@"C:\prj\Race_Read_Files\Files\file-{id+1}.txt", 'Z');
            }

            await Task.WhenAll(tasks);

            foreach(var result in tasks)
            {
                Console.WriteLine(result.Result);
            }
        }
    }
}

