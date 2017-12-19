/******************************************************************************
 * ö������:enumStackStatus                                                   *
 * ---------------------------------------------------------------------------*
 * ��������:                                                                  *
 *     ��ǲ���ջ��ǰ״̬��                                                   *
 *     1��HasUndoOperation | HasRedoOperation //��Redo��Undo����              *
 *     2��HasUndoOperation                    //��Undo����                    *
 *     3��HasRedoOperation                    //��Redo����                    *
 *     4��IsOverCapacity                      //�Ѿ�����ջ��������          *
 *     5��NoneOperation                       //��Redo\Undo����               *
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
    [Flags]
    public enum dssStackStatus
    {
        HasUndoOperation = 0x1,
        HasRedoOperation = 0x2,
        IsOverCapacity   = 0x4,
        NoneOperation    = 0x8
    }
}
