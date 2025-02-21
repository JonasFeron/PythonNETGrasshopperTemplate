using System;
using System.Collections.Generic;
using MyGrasshopperPluginCore.CS_Model;
using Python.Runtime;

namespace MyGrasshopperPluginCore.Converters
{
    public class Py2CS_ResultDecoder : IPyObjectDecoder
    {
        public bool CanDecode(PyType objectType, Type targetType)
        {
            if (targetType != typeof(CS_Result))
                return false;

            using (Py.GIL())
            {
                try
                {
                    return objectType.Name == "Py_Result";
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool TryDecode<T>(PyObject pyObj, out T? value)
        {
            value = default;
            if (typeof(T) != typeof(CS_Result))
                return false;

            using (Py.GIL())
            {
                try
                {
                    dynamic py = pyObj.As<dynamic>();

                    // Get the numpy array and convert it to a nested list
                    dynamic npArray = py.matrix;

                    // First convert to Python list
                    dynamic pyList = npArray.tolist();

                    // Then convert to C# list
                    var matrix = new List<List<double>>();
                    PyList pyListObj = new PyList(pyList);
                    foreach (PyObject rowObj in pyListObj)
                    {
                        var rowList = new List<double>();
                        PyList rowPyList = new PyList(rowObj);
                        foreach (PyObject val in rowPyList)
                        {
                            rowList.Add(val.As<double>());
                        }
                        matrix.Add(rowList);
                    }

                    value = (T)(object)new CS_Result { Matrix = matrix };
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in TryDecode: {ex.Message}");
                    return false;
                }
            }
        }
    }
}