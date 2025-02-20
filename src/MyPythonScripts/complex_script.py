# PythonNETGrasshopperTemplate

# Copyright <2025> <Jonas Feron>

# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at

#     http://www.apache.org/licenses/LICENSE-2.0

# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

# List of the contributors to the development of PythonNETGrasshopperTemplate: see NOTICE file.
# Description and complete License: see NOTICE file.
# ------------------------------------------------------------------------------------------------------------

# Copyright <2021-2025> <Université catholique de Louvain (UCLouvain)>

# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at

#     http://www.apache.org/licenses/LICENSE-2.0

# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

# List of the contributors to the development of PythonConnectedGrasshopperTemplate: see NOTICE file.
# Description and complete License: see NOTICE file.
# ------------------------------------------------------------------------------------------------------------

import sys
import numpy as np
import json

from TwinObjects.TwinData import TwinData
from TwinObjects.TwinResult import TwinResult


def main(twin_data):
    """
    Process the input data using Python.NET's built-in conversion.
    
    Args:
        twin_data (TwinData): Input data from C#
        
    Returns:
        TwinResult: Processed data to be sent back to C#
    """
    twin_result = TwinResult()
    # Convert input to numpy array
    if isinstance(twin_data, TwinData):
        twin_result.Matrix = np.array(twin_data.AList).reshape(twin_data.RowNumber, twin_data.ColNumber)
        return twin_result    
    else:
        raise TypeError("Input is not a TwinData object")
