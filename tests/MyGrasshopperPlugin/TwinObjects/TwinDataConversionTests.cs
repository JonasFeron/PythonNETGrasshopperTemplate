using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGrasshopperPluginCore.PythonNET;
using MyGrasshopperPluginCore.TwinObjects;

using Python.Runtime;

namespace MyGrasshopperPluginTests.TwinObjects
{
    [TestClass]
    public class TwinDataConversionTests
    {
        private static string condaEnvPath; 
        private static string pythonDllName;
        private static string scriptDir;

        [TestInitialize]
        public void Initialize()
        {
            condaEnvPath = Config.condaEnvPath;
            pythonDllName = Config.pythonDllName;
            scriptDir = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "..", "..", "..", "..", "..", "src", "MyPythonScripts"));

            PythonNET.Initialize(condaEnvPath, pythonDllName, scriptDir);
        }

        [TestCleanup]
        public void Cleanup()
        {
            PythonNET.ShutDown();
        }

        [TestMethod]
        public void TestTwinDataConversion()
        {
            // Create test data
            var inputList = new List<double> { 1.0, 2.0, 3.0, 4.0 };
            var twinData = new TwinData
            {
                AList = inputList,
                RowNumber = 2,
                ColNumber = 2
            };

            TwinResult result = null;

            // Add script directory to Python path
            using (Py.GIL())
            {
                try
                {
                    // Convert C# object to Python
                    PyObject pyTwinData = twinData.ToPython();

                    // Import the Python script and call the main function
                    dynamic complexScript = Py.Import("complex_script");
                    dynamic pyResult = complexScript.main(pyTwinData);

                    // Convert Python result back to C#
                    result = pyResult.As<TwinResult>();
                }
                catch (PythonException ex)
                {
                    Assert.Fail($"Python error: {ex.Message}");
                }
            }

            // Verify the result
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Matrix);
            Assert.AreEqual(2, result.Matrix.Count); // Number of rows
            Assert.AreEqual(2, result.Matrix[0].Count); // Number of columns

            Assert.AreEqual(inputList[0], result.Matrix[0][0]); // 1.0 
            Assert.AreEqual(inputList[1], result.Matrix[0][1]); // 2.0 
            Assert.AreEqual(inputList[2], result.Matrix[1][0]); // 3.0 
            Assert.AreEqual(inputList[3], result.Matrix[1][1]); // 4.0 
        }
    }
}
