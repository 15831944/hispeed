﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoDo.RSS.MIF.Core
{
    public interface IConfigEdit
    {
        bool TrySaveConfig(out string message);
    }
}
