using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    public class MemoryTest
    {
        public string MemoryType;
        public double BlockSize;
        public string ElementType;
        public int BufferSize;
        public int LaunchNum;
        public string Timer;
        public double WriteTime;
        public double AverageWriteTime;
        public double WriteBandwidth;
        public double WriteAbsError;
        public double WriteRelError;
        public double ReadTime;
        public double AverageReadTime;
        public double ReadBandwidth;
        public double ReadAbsError;
        public double ReadRelError;

        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14}",
                MemoryType, BlockSize / 1024 / 1024, ElementType, LaunchNum, Timer, 
                WriteTime, AverageWriteTime, WriteBandwidth, WriteAbsError, WriteRelError,
                ReadTime, AverageReadTime, ReadBandwidth, ReadAbsError, ReadRelError);
        }
    }
}
