using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CodeCell.Bricks.AppFramework
{
    public interface IControlItem:ICommand
    {
        /// <summary>
        /// �ؼ��ߴ�
        /// </summary>
        Size Size { get;}
        /// <summary>
        /// �ؼ�����
        /// ������Toolbar������Խ��յ����(������ToolStripItem���������)
        /// </summary>
        object Control { get;}
        /// <summary>
        /// �����ToolStripDrioDown���ͣ����ֵ��Ч
        /// </summary>
        IItem[] Items { get;}
    }
}
