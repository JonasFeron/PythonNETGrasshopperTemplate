using MyGrasshopperPluginCore.CS_Model;
using Python.Runtime;

namespace MyGrasshopperPluginCore.PythonNET.Convertors
{
    public class CS2Py_DataEncoder : IPyObjectEncoder
    {
        public bool CanEncode(Type type)
        {
            return type == typeof(CS_Data);
        }

        public PyObject? TryEncode(object obj)
        {
            if (!CanEncode(obj.GetType()))
                return null;

            var data = (CS_Data)obj;
            using (Py.GIL())
            {
                // Import the Python module containing the py_data class
                dynamic pyModel = Py.Import("py_model.py_data");

                // Create a new instance of py_data with our C# data
                return pyModel.Py_Data(
                    data.AList,
                    data.RowNumber,
                    data.ColNumber
                );
            }
        }
    }
}