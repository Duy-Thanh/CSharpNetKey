// Copyright (c) 2020 DuyThanhSoftwares. All right reserved
// Copyright (c) 2010-2012 Published by Bùi Đức Tiến. All right reserved

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace CSharpNetKey
{
    internal class SingleInstance : WindowsFormsApplicationBase
    {
        private SingleInstance()
        {
            base.IsSingleInstance = true;
        }
        public static void Run(Form f, StartupNextInstanceEventHandler startupHandler)
        {
            SingleInstance app = new SingleInstance();
            app.MainForm = f;
            app.StartupNextInstance += startupHandler;
            app.Run(Environment.GetCommandLineArgs());
        }

    }
}
