using Tester;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Tester
{
    public class BenchmarkProcessor
    {
        private Stopwatch _stopwatch;
        private Dictionary<TaskData, Func<int>> _tasks;
        public BenchmarkProcessor()
        {
            _stopwatch = new Stopwatch();
            _tasks = new Dictionary<TaskData, Func<int>>();
        }
        public BenchmarkProcessor(Dictionary<TaskData, Func<int>> tasks)
        {
            _stopwatch = new Stopwatch();
            _tasks = tasks;
        }

        public void RegisterTask(TaskData task, Func<int> taskFunc)
        {
            _tasks[task] = taskFunc;
        }

        public List<ProcessorTest> Run(int countTests = 10)
        {
             var processorTests = InitializeProcessorTestsList();

            int testNumber = 1;
            foreach (var test in processorTests)
            {
                _stopwatch.Restart();

                test.InstructionCount = _tasks[test.Task]();

                _stopwatch.Stop();

                test.Time = _stopwatch.ElapsedTicks;

                test.LaunchNumer = testNumber++;

                test.AverageTime = Math.Round(CalculateAverageTimeForTask(test, countTests), 5);

                test.AbsoluteError = Math.Round(test.Time - 1 / test.AverageTime, 5);

                test.RelativeError = Math.Round(test.AbsoluteError / test.Time / 100, 5);

                test.TaskPerformance = Math.Round(test.InstructionCount / test.Time, 5);
            }

            return processorTests;
        }

        private float CalculateAverageTimeForTask(ProcessorTest test, int numberTests)
        {
            float averageTime = 0;

            for (int i = 0; i < numberTests; i++)
            {
                _stopwatch.Restart();
                _tasks[test.Task]();
                _stopwatch.Stop();

                averageTime += _stopwatch.ElapsedTicks;
            }

            return averageTime / numberTests;
        }
        private List<ProcessorTest> InitializeProcessorTestsList()
        {
            List<ProcessorTest> processorTests = new List<ProcessorTest>();

            var timerName = _stopwatch.GetType().ToString();

            foreach (var processorTask in _tasks.Keys)
            {
                processorTests.Add(new ProcessorTest
                    (
                        processorTask,
                        timerName
                    ));
            }

            return processorTests;
        }
    }
}
