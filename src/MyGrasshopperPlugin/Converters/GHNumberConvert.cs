using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;

namespace MyGrasshopperPlugin.Converters
{
    public static class GHNumberConvert
    {
        public static GH_Structure<GH_Number> ToTree(List<List<double>> matrix)
        {
            GH_Structure<GH_Number> tree = new GH_Structure<GH_Number>();
            for (int i = 0; i < matrix.Count; i++)
            {
                GH_Path path = new GH_Path(i);
                List<GH_Number> branch = ToBranch(matrix[i]);
                tree.AppendRange(branch, path);
            }
            return tree;
        }
        public static List<GH_Number> ToBranch(List<double> aList)
        {
            return aList.Select(n => new GH_Number(n)).ToList();
        }
        public static List<double> ToList(List<GH_Number> aList)
        {
            return aList.Select(n => n.Value).ToList();
        }
    }
}
