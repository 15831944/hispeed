using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CodeCell.Bricks.AppFramework
{
    public interface IToolbar:IItem
    {
        /// <summary>
        /// IItemֻ����ICommand,ITool,IControlItem
        /// </summary>
        IItem[] Items { get;}
        /// <summary>
        /// ��ǰ����
        /// </summary>
        ICommand CurrentTool { get;set;}
        /// <summary>
        /// ���ҹ���
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICommand FindCommand(Guid id);
        /// <summary>
        /// ���ҹ���
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ICommand FindCommand(string name);
        /// <summary>
        /// ���ҹ���
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        ICommand FindCommand(IItem item);
        /// <summary>
        /// ͣ��λ��
        /// </summary>
        DockStyle DockStyle { get; }
    }
}
