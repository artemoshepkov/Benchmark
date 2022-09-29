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
        public uint BlockSize;
        public string ElementType;
        public uint BufferSize;
        public uint LaunchNum;
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

        //public override string ToString()
        //{
        //    return string.Format();
        //}
    }
}
