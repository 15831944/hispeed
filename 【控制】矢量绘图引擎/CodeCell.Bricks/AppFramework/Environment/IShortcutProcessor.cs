using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CodeCell.Bricks.AppFramework
{
    /// <summary>
    /// ��ݼ�ִ����
    /// ���еİ����������͵���ִ����
    /// </summary>
    public interface IShortcutProcessor
    {
        void AddShortcutFilter(IShortcutFilter filter);
        void AddShortcutFilters(IShortcutFilterCollection filters);
        void PreviewKeyDown(PreviewKeyDownEventArgs previewKeyDownEventArgs);
    }
}
