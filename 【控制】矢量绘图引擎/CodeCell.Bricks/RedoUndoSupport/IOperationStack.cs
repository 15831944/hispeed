/******************************************************************************
 * �ӿ�����:IOperationStack                                                   *
 *          �������κ�����ʵ�ָýӿڣ��������ṩһ��Ĭ��ʵ��OperationStack  *
 * ---------------------------------------------------------------------------*
 * ��������:                                                                  *
 *     ����ջ�ӿڣ�ʵ�ָýӿڵ���������¹��ܣ�                               *
 *     1��������ҪUndo��Redo�����Ĳ���(IOperation)                            *
 *     2����Stack������ʽ������ÿһ��IOperationִ��Redo��Undo                 *
 *     3��ʹ���¼���ʽ֪ͨ�ⲿ��������ջ�ı仯                                *
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
    public interface IOperationStack
    {
        bool Enabled { get; set; }
        /// <summary>
        /// ջ�����
        /// </summary>
        int Capacity { get;set;}
        /// <summary>
        /// ��ĳһ�����������ջ����ִ����Do()����
        /// </summary>
        /// <param name="mOperation">ʵ��IOperation�ӿڵ�����ʵ��</param>
        /// <retruns>�Ƿ����ɹ���������ɹ�������ջ����������</retruns>>
        bool Do(IOperation mOperation);
        /// <summary>
        /// �Ըո�ִ���˳��������Ĳ���ִ����������
        /// </summary>
        void Redo();
        /// <summary>
        /// �Ե�ǰ����(ִ����Do������Redo����)ִ�г�������
        /// </summary>
        void Undo();
        /// <summary>
        /// ���ò���ջ��ִ��������ջ�����ÿ�
        /// </summary>
        void Reset();
        /// <summary>
        /// �Ӳ���ջ���Ƴ���ĳ������
        /// </summary>
        /// <param name="mOperation">����������</param>
        void Remove(IOperation mOperation);
        /// <summary>
        /// ���ݹ�������ɾ������
        /// </summary>
        /// <param name="data"></param>
        void Remove(object data);
        void ClearUndoedOperations();
        /// <summary>
        /// ֪ͨ�ⲿ�⻷����ջ�ѷ����仯(���磺�п���ִ��Redo�����Ĳ����ȵ�)
        /// </summary>
        StackStatusChangedHandler OnStackStatusChanged{get;set;}
        /// <summary>
        /// ˢ��״̬
        /// </summary>
        void UpdateStatus();
        /// <summary>
        /// ״̬
        /// </summary>
        dssStackStatus Status { get;}
    }
}
