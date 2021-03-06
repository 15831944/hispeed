﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel;
using GeoDo.RSS.Layout.GDIPlus;

namespace GeoDo.RSS.Layout.Elements
{
    [Export(typeof(IElement)), Category("文本")]
    public class TextElementCurrentDateTime5 : TextElement
    {
        public TextElementCurrentDateTime5()
            : base()
        {
            _name = "五号日期";
            _text = DateTime.Now.ToString("yyyy-MM-dd");
            _font = new System.Drawing.Font("微软雅黑", 10.5f);
        }
    }
}
