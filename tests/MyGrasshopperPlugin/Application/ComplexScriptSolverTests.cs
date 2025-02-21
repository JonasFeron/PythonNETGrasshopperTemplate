//PythonNETGrasshopperTemplate

//Copyright <2025> <Jonas Feron>

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

//List of the contributors to the development of PythonNETGrasshopperTemplate: see NOTICE file.
//Description and complete License: see NOTICE file.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Python.Runtime;
using MyGrasshopperPluginCore.CS_Model;
using MyGrasshopperPluginCore.Application.PythonNETInit;
using MyGrasshopperPluginCore.Application.PythonNETSolvers;

namespace MyGrasshopperPluginTests.Application
{
    [TestClass]
    public class ComplexScriptSolverTests
    {
        private static string condaEnvPath; 
        private static string pythonDllName;
        private static string scriptDir;

        [TestInitialize]
        public void Initialize()
        {
            condaEnvPath = PythonNETConfig.condaEnvPath;
            pythonDllName = PythonNETConfig.pythonDllName;
            scriptDir = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "..", "..", "..", "..", "..", "src", "MyPythonScripts"));

            PythonNETManager.Initialize(condaEnvPath, pythonDllName, scriptDir);
        }

        [TestCleanup]
        public void Cleanup()
        {
            PythonNETManager.ShutDown();
        }

        [TestMethod]
        public void TestCS_PyConversion()
        {
            // Create test data
            var inputList = new List<double> { 1.0, 2.0, 3.0, 4.0 };
            var csData = new CS_Data
            {
                AList = inputList,
                RowNumber = 2,
                ColNumber = 2
            };

            CS_Result result = new CS_Result();
            try
            {
                result = ComplexScriptSolver.Solve(csData);
            }
            catch (PythonException ex)
            {
                Assert.Fail($"Python error: {ex.Message}");
            }
            // Add script directory to Python path

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
