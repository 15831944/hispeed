using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CodeCell.Bricks.AppFramework
{
    public interface ICommand:IItem
    {
        Image Image { get;}
        bool BeginGroup { get;}
        bool Enabled { get;}
        /// <summary>
        /// ����ITool��������Ч
        /// ����ICommandֻ�н��䵱���˵��ϲ���Ч
        /// </summary>
        bool Checked { get;}
        void Click();
        void Init(IHook hook);
        IHook Hook { get;}
    }
}
