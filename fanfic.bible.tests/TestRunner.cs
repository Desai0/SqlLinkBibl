using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System;
using System.Linq;

namespace fanfic.bible.tests
{
    public static class TestRunner
    {
        public static void RunAllTests()
        {
            Assembly testAssembly = Assembly.GetExecutingAssembly();
            var testTypes = testAssembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(TestClassAttribute), true).Length > 0)
                .OrderBy(t => t.Name);

            int totalTests = 0;
            int passedTests = 0;
            int failedTests = 0;

            foreach (var type in testTypes)
            {
                Console.WriteLine($"Running tests for: {type.Name}");
                var testMethods = type.GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(TestMethodAttribute), true).Length > 0)
                    .OrderBy(m => m.Name);
                var testInstance = Activator.CreateInstance(type);

                var initializeMethods = type.GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(TestInitializeAttribute), true).Length > 0);

                foreach (var testMethod in testMethods)
                {
                    totalTests++;
                    try
                    {
                        foreach (var initMethod in initializeMethods)
                        {
                            initMethod.Invoke(testInstance, null);
                        }
                        testMethod.Invoke(testInstance, null);
                        Console.WriteLine($"  [PASS] {testMethod.Name}");
                        passedTests++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  [FAIL] {testMethod.Name}");
                        Console.WriteLine($"    Error: {ex.InnerException?.Message}");
                        failedTests++;
                    }
                }
            }

            // Итоговая статистика
            Console.WriteLine();
            Console.WriteLine("TEST SUMMARY:");
            Console.WriteLine($"Total tests: {totalTests}");
            Console.WriteLine($"Passed: {passedTests}");
            Console.WriteLine($"Failed: {failedTests}");
            Console.WriteLine($"Success rate: {(totalTests > 0 ? (passedTests * 100.0 / totalTests).ToString("F1") : "0")}%");

            if (failedTests > 0)
            {
                Environment.Exit(1);
            }
        }
    }
}