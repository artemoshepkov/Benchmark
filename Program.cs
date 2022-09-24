

namespace ProcessorTester
{
    public class Program
    {
        private static void Main()
        {
            var benchmark = new Benchmark();
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
