﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoDo.RSS.Core.UI
{
    public interface IStartProgress
    {
        void PrintStartInfo(string sInfo);
        void Stop();
    }
}
