/******************************************************************************
 * ί������:StatckStatusChangedHandler                                        *
 * ---------------------------------------------------------------------------*
 * ��������:������ջ״̬�����仯ʱ֪ͨ�ⲿ����                                *
 * ---------------------------------------------------------------------------*
 * �����¼��                                                                 *
 * 1��ʱ��    :2007-11-29                                                     * 
 *    ����Ա  :��²�                                                         *
 *    ����    :����                                                           *
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeCell.Bricks.RedoUndo
{
    /// <summary>
    /// ����ջ״̬ö��(��ö���б�����ͣ�����ʹ��λ�����)
    /// </summary>
    /// <param name="mStackStatus"></param>
    public delegate void StackStatusChangedHandler(dssStackStatus mStackStatus);
}
