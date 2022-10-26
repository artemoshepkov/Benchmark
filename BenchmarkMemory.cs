using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    public class BenchmarkMemory
    {
        private Stopwatch _stopWatch;
        private string _path;
        private Dictionary<string, Dictionary<string, Action<int[]>>> _tasks;

        private Dictionary<int, int> test = new Dictionary<int, int>();

        public BenchmarkMemory()
        {
            _stopWatch = new Stopwatch();
            _tasks = new Dictionary<string, Dictionary<string, Action<int[]>>>();
            InitializeTasks();
        }

        public MemoryTest Run(string memoryType, int countTests, string path, int countElements = 50, int countDecimalPlaces = 2)
        {
            _path = path;

            if (!_tasks.ContainsKey(memoryType))
                throw new ArgumentException("Don`t exist memory type.");

            var memoryTest = new MemoryTest()
            {
                MemoryType = memoryType.ToString(),
                BlockSize = countElements * 4,
                ElementType = typeof(int).ToString(),
                LaunchNum = new Random().Next(0, countTests),
                Timer = _stopWatch.GetType().ToString(),
                WriteTime = -1,
                AverageWriteTime = -1, 
                WriteBandwidth = -1,
                WriteAbsError = -1,
                WriteRelError = -1,
                ReadTime = -1,
                AverageReadTime = -1, 
                ReadBandwidth = -1,
                ReadAbsError = -1,
                ReadRelError = -1
            };

            var array = new int[countElements];

            FillRandomArray(array, 0, 100);

            for (int i = 0; i < countTests; i++)
            {
                _stopWatch.Restart();
                _tasks[memoryType]["write"](array);
                _stopWatch.Stop();

                if (i == countTests - 1)
                {
                    memoryTest.WriteTime = _stopWatch.ElapsedTicks;
                }

                memoryTest.AverageWriteTime += _stopWatch.ElapsedTicks;
            }

            for (int i = 0; i < countTests; i++)
            {
                _stopWatch.Restart();
                _tasks[memoryType]["read"](array);
                _stopWatch.Stop();

                if (i == countTests - 1)
                {
                    memoryTest.ReadTime = _stopWatch.ElapsedTicks;
                }

                memoryTest.AverageReadTime += _stopWatch.ElapsedTicks;
            }

            memoryTest.AverageWriteTime /= countTests;
            memoryTest.AverageReadTime /= countTests;

            memoryTest.WriteTime = (new TimeSpan((long)memoryTest.WriteTime)).TotalSeconds / 15; // translate tick to seconds
            memoryTest.AverageWriteTime = (new TimeSpan((long)memoryTest.AverageWriteTime)).TotalSeconds / 15; // translate tick to seconds
            memoryTest.ReadTime = (new TimeSpan((long)memoryTest.ReadTime)).TotalSeconds / 15; // translate tick to seconds
            memoryTest.AverageReadTime = (new TimeSpan((long)memoryTest.AverageReadTime)).TotalSeconds / 15; // translate tick to seconds

            memoryTest.WriteBandwidth = memoryTest.BlockSize / 1024 / 1024 / memoryTest.AverageWriteTime;
            memoryTest.ReadBandwidth = memoryTest.BlockSize / 1024 / 1024 / memoryTest.AverageReadTime;

            memoryTest.WriteAbsError = Math.Abs(memoryTest.WriteTime - memoryTest.AverageWriteTime);
            memoryTest.ReadAbsError = Math.Abs(memoryTest.ReadTime - memoryTest.AverageReadTime);

            memoryTest.WriteRelError = memoryTest.WriteAbsError / memoryTest.AverageWriteTime * 100;
            memoryTest.ReadRelError = memoryTest.ReadAbsError / memoryTest.AverageReadTime * 100;

            return memoryTest;
        }

        private void FillRandomArray(int[] array, int minValue, int maxValue)
        {
            var rand = new Random();

            array.Select(x => rand.Next(minValue, maxValue));
        }

        private void InitializeTasks()
        {
            _tasks["RAM"] = new Dictionary<string, Action<int[]>>()
            {
                { 
                    "write", 
                    (int[] array) =>
                        {
                            int temp = 0;
                            int n = array.Length;
                            for (int i = 0; i < n; i++)
                                array[i] = temp;
                        } },
                { 
                    "read", 
                    (int[] array) => 
                        {
                            int temp;
                            int n = array.Length;
                            for (int i = 0; i < n; i++)
                                temp = array[i];
                        } }
            };
            _tasks["SSD"] = new Dictionary<string, Action<int[]>>()
            {
                { 
                    "write", 
                    (int[] array) =>
                    {
                        using (StreamWriter writer = new StreamWriter(_path, true))
                        {
                            foreach (var item in array)
                                writer.WriteLine(item);
                        }
                    } },
                { 
                    "read", 
                    (int[] array) => 
                    {
                        using (StreamReader reader = new StreamReader(_path))
                        {
                            string line;
                            try
                            {
                                for (int i = 0;  (line = reader.ReadLine()) != null; i++)
                                    array[i] = int.Parse(line);
                            }
                            catch {}
                        }
                    } }
            };
            _tasks["HDD"] = new Dictionary<string, Action<int[]>>()
            {
                { "write", (int[] array) => { Thread.Sleep(100); } },
                { "read", (int[] array) => { Thread.Sleep(100); } }
            };
            _tasks["flash"] = new Dictionary<string, Action<int[]>>()
            {
                { 
                    "write", 
                    (int[] array) =>
                    {                        
                        using (StreamWriter writer = new StreamWriter(_path, true))
                        {
                            foreach (var item in array)
                                writer.WriteLine(item);
                        }
                    } },
                { 
                    "read", 
                    (int[] array) => 
                    {
                        using (StreamReader reader = new StreamReader(_path))
                        {
                            string line;
                            try
                            {
                            for (int i = 0;  (line = reader.ReadLine()) != null; i++)
                                array[i] = int.Parse(line);
                            }
                            catch{ }
                        }
                    } }
            };
        }
    }
}
