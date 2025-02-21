using System.Numerics;
using Grasshopper.Kernel;
using MyGrasshopperPluginCore.CS_Model;
using Python.Runtime;
using Rhino.Runtime;

namespace MyGrasshopperPluginCore.Application.PythonNETSolvers
{
    public static class ComplexScriptSolver
    {
        public static CS_Result? Solve(CS_Data csData)
        {
            string pythonScript = "complex_script";
            var m_threadState = PythonEngine.BeginAllowThreads();
            CS_Result? result = null;

            using (Py.GIL())
            {
                try
                {
                    PyObject pyData = csData.ToPython();
                    dynamic script = PyModule.Import(pythonScript);
                    dynamic mainFunction = script.main;
                    dynamic pyResult = mainFunction(pyData);
                    result = pyResult.As<CS_Result>();
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
