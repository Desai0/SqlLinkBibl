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
            var testTypes = testAssembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(TestClassAttribute), true).Length > 0);

            foreach (var type in testTypes)
            {
                Console.WriteLine($"Running tests for: {type.Name}");
                var testMethods = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(TestMethodAttribute), true).Length > 0);
                var testInstance = Activator.CreateInstance(type);

                var initializeMethods = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(TestInitializeAttribute), true).Length > 0);

                foreach (var testMethod in testMethods)
                {
                    try
                    {
                        foreach(var initMethod in initializeMethods)
                        {
                            initMethod.Invoke(testInstance, null);
                        }
                        testMethod.Invoke(testInstance, null);
                        Console.WriteLine($"  [PASS] {testMethod.Name}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  [FAIL] {testMethod.Name}");
                        Console.WriteLine($"    Error: {ex.InnerException.Message}");
                    }
                }
            }
        }
    }
}
