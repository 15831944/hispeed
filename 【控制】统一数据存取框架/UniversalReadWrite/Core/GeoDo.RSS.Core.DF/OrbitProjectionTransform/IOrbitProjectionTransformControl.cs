﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoDo.RSS.Core.DF
{
    public interface IOrbitProjectionTransformControl
    {
        void BuildAsync();
        void Build();
        IOrbitProjectionTransform OrbitProjectionTransform { get; }
        void Free();
    }
}
