using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;

namespace MyGrasshopperPlugin.GH_Model
{
    public static class Convert
    {
        /// <summary>
        /// Converts a list of lists of doubles to a Grasshopper structure of GH_Numbers.
        /// </summary>
        /// <param name="matrix">The list of lists of doubles to convert.</param>
        /// <returns>A GH_Structure containing the converted GH_Numbers.</returns>
        public static GH_Structure<GH_Number> ToTree(List<List<double>> matrix)
        {
            GH_Structure<GH_Number> tree = new GH_Structure<GH_Number>();
            for (int i = 0; i < matrix.Count; i++)
            {
                GH_Path path = new GH_Path(i);
                List<GH_Number> branch = matrix[i].Select(x => new GH_Number(x)).ToList();
                tree.AppendRange(branch, path);
            }
            return tree;
        }
        public static List<double> ToDouble(List<GH_Number> aList)
        {
            return aList.Select(n => n.Value).ToList();
        }
            
    }
}
