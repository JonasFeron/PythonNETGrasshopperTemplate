using Python.Runtime;

namespace MyGrasshopperPluginCore.Converters
{
    public static class Main
    {
        public static void RegisterConverters()
        {
            PyObjectConversions.RegisterEncoder(new CS2Py_DataEncoder());
            PyObjectConversions.RegisterDecoder(new Py2CS_ResultDecoder());
        }
    }
}
