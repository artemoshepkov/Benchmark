

using Benchmark;

namespace Tester
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[] { "-m", "SSD", "-b", "4Mb", "-l", "10" };
            }

            string memoryType = "";
            int blockSize = 1;
            int countTests = 0;
            string pathToTestFile = "";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-m" || args[i] == "--memory-type")
                {
                    memoryType = (args[i + 1]);

                    if (memoryType != "RAM" && memoryType != "SSD" && memoryType != "HDD" && memoryType != "flash")
                        throw new ArgumentException("Invalid memory type");

                    if (memoryType == "SSD")
                        pathToTestFile = @"C:\Newfolder\array.txt";

                    if (memoryType == "flash")
                        pathToTestFile = @"E:\Newfolder\array.txt";

                    i++;
                }

                if (args[i] == "-b" || args[i] == "--block-size")
                {
                    if (args[i + 1].Contains("Kb"))
                    {
                        blockSize *= 1024;
                        args[i + 1] = args[i + 1].Replace("Kb", "");
                    }
                    else if (args[i + 1].Contains("Mb"))
                    {
                        blockSize *= 1024 * 1024;
                        args[i + 1] = args[i + 1].Replace("Mb", "");
                    }

                    var tempBlockSize = 0;
                    if (!int.TryParse(args[i + 1], out tempBlockSize))
                        throw new ArgumentException();
                    blockSize *= tempBlockSize;

                    i++;
                }

                if (args[i] == "-l" || args[i] == "--launch-count")
                {
                    if (!int.TryParse(args[i + 1], out countTests))
                        throw new ArgumentException("luanch-count must be a number.");

                    if (countTests < 0)
                        throw new ArgumentException("luanch-count must be a positive number.");

                    i++;
                }
            }

            var benchmark = new BenchmarkMemory();

            var pathToTable = "TestResults.csv";

            var result = new List<MemoryTest>();

            for (int i = 1; i < countTests + 1; i++)
            {
                result.Add(benchmark.Run(memoryType, countTests, pathToTestFile, (int)(blockSize * i / sizeof(int))));

                Console.WriteLine(i + " test completed.");
            }

            //result.Add(benchmark.Run(memoryType, countTests, pathToTestFile, 64 / sizeof(int)));
            //result.RemoveAt(0);
            //result.Add(benchmark.Run(memoryType, countTests, pathToTestFile, 64  / sizeof(int)));
            //result.Add(benchmark.Run(memoryType, countTests, pathToTestFile, 256 * 1024  / sizeof(int)));
            //result.Add(benchmark.Run(memoryType, countTests, pathToTestFile, 1 * 1024 * 1024 / sizeof(int)));
            //result.Add(benchmark.Run(memoryType, countTests, pathToTestFile, 6 * 1024 * 1024 / sizeof(int)));

            using (StreamWriter writer = new StreamWriter(pathToTable, true))
            {
                writer.WriteLine("MemoryType;BlockSize;ElementType;LaunchNum;Timer;WriteTime;AverageWriteTime;WriteBandwidth;WriteAbsError;WriteRelError;ReadTime;AverageReadTime;ReadBandwidth;ReadAbsError;ReadRelError");
                writer.WriteLine();

                result.ForEach(x => writer.WriteLine(x.ToString()));

                writer.WriteLine();
            }
        }
    

        private static void TestProcessor()
        {
            var benchmark = new BenchmarkProcessor();
            benchmark.RegisterTask(new TaskData("", ""),
                () =>
                {
                    int numberOperatoins = 0;

                    //Thread.Sleep(100);
                    numberOperatoins++;

                    return numberOperatoins;
                });
            benchmark.RegisterTask(new TaskData("", ""),
                () =>
                {
                    int numberOperatoins = 0;

                    //Thread.Sleep(100);
                    numberOperatoins++;

                    return numberOperatoins;
                });
            benchmark.RegisterTask(new TaskData("", ""),
                () =>
                {
                    int numberOperatoins = 0;

                    //Thread.Sleep(100);
                    numberOperatoins++;

                    return numberOperatoins;
                });

            var testResults = benchmark.Run();

            var path = "TestResults.csv";

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var item in testResults)
                    writer.WriteLine(item.ToString());
            }

            foreach (var item in testResults)
            {
                Console.WriteLine(item);
            }
        }
    }
}
