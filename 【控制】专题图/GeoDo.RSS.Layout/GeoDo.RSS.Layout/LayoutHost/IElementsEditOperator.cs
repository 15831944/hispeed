﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoDo.RSS.Layout
{   
    public interface IElementsEditOperator
    {
        void Group();
        void Ungroup();
        void Aligment(enumElementAligment style);
    }
}
