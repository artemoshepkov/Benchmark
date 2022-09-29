using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{

    public class BenchmarkMemory<T>
    {
        private Stopwatch _stopWatch;
        private Dictionary<string, Action<T[]>> _tasks; 

        public BenchmarkMemory()
        {
            _stopWatch = new Stopwatch(); 
            _tasks = new Dictionary<string, Action<T[]>>();
            InitializeTasks();
        }

        public LinkedList<MemoryTest> Run(string memoryType, int countTests = 10)
        {
            var memoryTests = new LinkedList<MemoryTest>();

            var timerName = _stopWatch.GetType().ToString();

            foreach (var task in _tasks)
            {
                var 

                _stopWatch.Restart();
                _tasks[memoryType]();
                _stopWatch.Stop();

                memoryTests.AddLast(new MemoryTest { 
                    MemoryType = memoryType,
                    Timer = timerName,
                });
            }

            return memoryTests;
        }
        public void RegisterTask(Func<T> task)
        {
            _tasks.Add(task);
        }

        private void InitializeTasks()
        {
            _tasks["HDD"] = (T[] array) =>
            {

            };
        }
    }
}
