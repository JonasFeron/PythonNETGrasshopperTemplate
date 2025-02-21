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
//------------------------------------------------------------------------------------------------------------

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
