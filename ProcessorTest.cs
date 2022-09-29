using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class TaskData
    {
        public string Name;
        public string OperandType;

        public TaskData(string name, string operandType)
        {
            Name = name;
            OperandType = operandType;
        }
    }
    public class ProcessorTest
    {
        public readonly string ProcessorModel; //
        public TaskData Task; //
        // public string Optimisations;
        public int InstructionCount; //
        public string Timer; //
        public double Time; //
        public int LaunchNumer; // 
        public double AverageTime; //
        public double AbsoluteError; //
        public double RelativeError;
        public double TaskPerformance; // 

        public ProcessorTest(TaskData task, string timerName)
        {
            ProcessorModel = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
            Task = task;
            Timer = timerName;
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                ProcessorModel, Task.Name, Task.OperandType, /*Optimisations,*/ InstructionCount,
                Timer, Time, LaunchNumer, AverageTime, AbsoluteError,
                RelativeError, TaskPerformance);
        }
    }
}
