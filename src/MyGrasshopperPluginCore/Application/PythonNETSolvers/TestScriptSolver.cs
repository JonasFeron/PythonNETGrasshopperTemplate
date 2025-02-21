using System.Numerics;
using Grasshopper.Kernel;
using MyGrasshopperPluginCore.CS_Model;
using Python.Runtime;
using Rhino.Runtime;
using System;

namespace MyGrasshopperPluginCore.Application.PythonNETSolvers
{
    public static class TestScriptSolver
    {
        public static string? Solve(string str0, string str1)
        {
            string pythonScript = "test_script";

            string result = "";

            // following code is inspired from https://github.com/pythonnet/pythonnet/wiki/Threading
            var m_threadState = PythonEngine.BeginAllowThreads();
            using (Py.GIL())
            {
                try
                {
                    dynamic test_script = PyModule.Import(pythonScript);
                    dynamic mainFunction = test_script.main;
                    dynamic pyResult = mainFunction(str0, str1);
                    result = (string)pyResult;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            PythonEngine.EndAllowThreads(m_threadState);

            return result;
        }
    }
}
