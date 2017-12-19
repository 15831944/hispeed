using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CodeCell.Bricks.AppFramework
{
    /// <summary>
    /// ���в˵�����������������߶�ʵ�ָýӿ�
    /// </summary>
    public interface IItem
    {
        string ToolTips { get;}
        string Name { get; set; }
        string Text { get;}
        Guid Id { get;}
        bool Visible { get; set; }
        bool AllowDock { get; set; }
        ToolStripItemDisplayStyle DisplayStyle { get; }
        IItem[] Children { get; }
    }
}
