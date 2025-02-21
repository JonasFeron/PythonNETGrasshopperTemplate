using System;
using System.Collections.Generic;
using MyGrasshopperPluginCore.CS_Model;
using Python.Runtime;
using System.Linq;

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
                    dynamic npArray = py.matrix;
                    
                    var matrix = ToListList(npArray);

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

        private static List<List<double>> ToListList(dynamic npArray)
        {
            var matrix = new List<List<double>>();

            // Get array dimensions
            var shape = ((PyObject)npArray.shape).As<int[]>();
            if (shape.Length != 2)
            {
                throw new ArgumentException("Expected 2D numpy array");
            }

            // Get flat array of data
            var flatData = ((PyObject)npArray.ravel()).As<double[]>();

            // Convert flat array to List<List<double>> given the shape of npArray
            for (int i = 0; i < shape[0]; i++)
            {
                matrix.Add(flatData.Skip(i * shape[1]).Take(shape[1]).ToList());
            }

            return matrix;
        }
    }
}