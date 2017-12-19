using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Telerik.WinControls.Themes.ControlDefault
{
    public partial class ProgressBar : ControlDefaultThemeComponent
    {
        public ProgressBar()
        {
            InitializeComponent();
        }

        public ProgressBar(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
