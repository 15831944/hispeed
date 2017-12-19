/******************************************************************************
 * ����������:OperationStack                                                  *
 * ʵ�ֽӿ�  :IOperationStack                                                 *
 * ---------------------------------------------------------------------------*
 * ��������:����ջ�ӿ�IOperationStack��ʵ���࣬���幦�������μ�IOperationStack*                                 *
 * ---------------------------------------------------------------------------*
 * �����¼��                                                                 *
 * 1��ʱ��    :2007-11-29                                                     * 
 *    ����Ա  :��²�                                                         *
 *    ����    :����                                                           *
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using CodeCell.Bricks.Runtime;

namespace CodeCell.Bricks.RedoUndo
{
    public class OperationStack:IOperationStack
    {
        /// <summary>
        /// ������ջ��ִ����Do()��Redo()�����Ĳ�����ѹ���ջ
        /// </summary>
        private Stack<IOperation> _OprStack = new Stack<IOperation>();
        /// <summary>
        /// ������ջ��ִ����Undo()�����Ĳ�����ѹ���ջ
        /// һ��ִ����Do()��������ջ���ᱻ���
        /// </summary>
        private Stack<IOperation> _UndoedStack = new Stack<IOperation>();
        /// <summary>
        /// ջ�����
        /// </summary>
        private int _capacity = 1000;
        private StackStatusChangedHandler _OnStackStatusChanged = null;
        //
        private bool _enabled = true;

        public OperationStack()
        { 
            //
        }

        #region IOperationStack ��Ա

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public dssStackStatus Status 
        {
            get
            {
                return status;
            }
        }

        public int Capacity 
        {
            get 
            {
                return _capacity;
            }
            set 
            {
                _capacity = value;
            }
        }

        public bool Do(IOperation mOperation)
        {
            //���ջ���������ȣ���֪ͨ�ⲿ����
            if (_OprStack.Count > _capacity)
            {
                if (_OnStackStatusChanged != null)
                    _OnStackStatusChanged(dssStackStatus.IsOverCapacity);
                Log.WriterWarning("OperationStack", "Do", "����ջ����������" + _capacity.ToString() + ",��ȫ����ա�");
                _OprStack.Clear();
                _UndoedStack.Clear();
            }

            _OprStack.Push(mOperation);
            mOperation.Do();
            _UndoedStack.Clear();

            UpdateStatus();

            return true;
        }

        public void Redo()
        {
            if (_UndoedStack.Count > 0)
            {
                IOperation opr = _UndoedStack.Pop();
                opr.Redo();
                _OprStack.Push(opr);

            }

            UpdateStatus();
        }

        public void Undo()
        {
            if (_OprStack.Count > 0)
            {
                IOperation opr = _OprStack.Pop();
                opr.Undo();
                _UndoedStack.Push(opr);
            }

            UpdateStatus();
        }

        public void Reset()
        {
            _OprStack.Clear();
            _UndoedStack.Clear();

            UpdateStatus();
        }

        public void Remove(IOperation mOperation)
        {
            if (mOperation == null)
                return;
            bool IsRemoved = RemoveOprFromStack(_OprStack, mOperation);
            if (!IsRemoved)
                IsRemoved = RemoveOprFromStack(_UndoedStack, mOperation);

            UpdateStatus();
        }

        public void Remove(object data)
        {
            if (data == null)
                return;
            bool IsRemoved = RemoveOprFromStack(_OprStack, data);
            if (!IsRemoved)
                IsRemoved = RemoveOprFromStack(_UndoedStack, data);

            UpdateStatus();
        }

        public void ClearUndoedOperations()
        {
            _UndoedStack.Clear();
        }

        private bool RemoveOprFromStack(Stack<IOperation> mStack, object data)
        {
            bool IsRemoved = false;
            Stack<IOperation> ops = new Stack<IOperation>();

            IOperation opr = null;
            while (mStack.Count > 0)
            {
                opr = mStack.Pop();
                if (!opr.Data.Equals(data))
                    ops.Push(opr);
                else
                    IsRemoved = true;
            }

            while (ops.Count > 0)
            {
                opr = ops.Pop();
                mStack.Push(opr);
            }

            return IsRemoved;
        }

        private bool RemoveOprFromStack(Stack<IOperation> mStack,IOperation mOpr)
        {
            bool IsRemoved = false;
            Stack<IOperation> ops = new Stack<IOperation>();

            IOperation opr = null;
            while (mStack.Count>0)
            {
                opr = mStack.Pop();
                if (!opr.Equals(mOpr))
                    ops.Push(opr);
                else
                    IsRemoved = true;
            }

            while (ops.Count>0)
            {
                opr = ops.Pop();
                mStack.Push(opr);
            }

            return IsRemoved;
        }

        dssStackStatus status = 0;

        public void UpdateStatus()
        {
            status = 0;
            if (_OprStack.Count > 0)
                status = status | dssStackStatus.HasUndoOperation;
            if (_UndoedStack.Count > 0)
                status = status | dssStackStatus.HasRedoOperation;

            if (_OnStackStatusChanged != null)
                _OnStackStatusChanged(status);
        }

        public StackStatusChangedHandler OnStackStatusChanged
        {
            get 
            {
                return _OnStackStatusChanged; 
            }
            set 
            {
                _OnStackStatusChanged = value; 
            }
        }

        #endregion
    }
}
