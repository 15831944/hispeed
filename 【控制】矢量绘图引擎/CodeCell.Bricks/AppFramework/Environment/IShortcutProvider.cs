using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CodeCell.Bricks.AppFramework
{
    /// <summary>
    /// ��ݼ��ṩ��
    /// ���еĿ�ݼ�������ӿڸ����ṩ
    /// ����ͨ������ӿ�ʵ�����п�ݼ�������
    /// </summary>
    public interface IShortcutProvider
    {
        Shortcut GetShortcut(string commandName);
    }
}
