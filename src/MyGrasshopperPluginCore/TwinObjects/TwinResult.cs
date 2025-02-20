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

//this file was imported from https://github.com/JonasFeron/PythonConnectedGrasshopperTemplate and is used WITHOUT modifications.
//------------------------------------------------------------------------------------------------------------

//Copyright < 2021 - 2025 > < Université catholique de Louvain (UCLouvain)>

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

//List of the contributors to the development of PythonConnectedGrasshopperTemplate: see NOTICE file.
//Description and complete License: see NOTICE file.
//------------------------------------------------------------------------------------------------------------

using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGrasshopperPluginCore.TwinObjects
{
    /// <summary>
    /// A Twin result object is used to transfer results between Python and C# using Python.NET's built-in conversion.
    /// </summary>
    public class TwinResult
    {
        public List<List<double>> Matrix { get; set; }

        public TwinResult()
        {
            Init();
        }

        private void Init()
        {
            Matrix = new List<List<double>>();
        }

        /// <summary>
        /// Converts a list of lists of doubles to a Grasshopper structure of GH_Numbers.
        /// </summary>
        /// <param name="matrix">The list of lists of doubles to convert.</param>
        /// <returns>A GH_Structure containing the converted GH_Numbers.</returns>
        public static GH_Structure<GH_Number> ListListToGH_Struct(List<List<double>> matrix)
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
    }
}
