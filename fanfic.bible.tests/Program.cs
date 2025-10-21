namespace fanfic.bible.tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting test execution...");
            TestRunner.RunAllTests();
            Console.WriteLine("Test execution finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
