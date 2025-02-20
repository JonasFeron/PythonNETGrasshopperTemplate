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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel.Types;


namespace MyGrasshopperPluginCore.TwinObjects
{
    /// <summary>
    /// A Twin data object is used to transfer data between the main Grasshopper/C# thread and the Python thread.
    /// Uses Python.NET's built-in conversion methods for efficient data transfer.
    /// </summary>
    public class TwinData
    {
        public List<double> AList { get; set; }
        public int RowNumber { get; set; }
        public int ColNumber { get; set; }

        public TwinData()
        {
            Init();
        }

        public TwinData(List<GH_Number> list, int rowNumber, int colNumber)
        {
            AList = list.Select(n => n.Value).ToList();
            RowNumber = rowNumber;
            ColNumber = colNumber;
        }

        /// <summary>
        /// Initializes all properties to their default values.
        /// </summary>
        private void Init()
        {
            AList = new List<double>();
            RowNumber = 0;
            ColNumber = 0;
        }
    }
}