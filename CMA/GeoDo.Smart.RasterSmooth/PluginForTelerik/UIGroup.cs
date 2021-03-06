﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GeoDo.RSS.Core.UI;
using Telerik.WinControls.UI;

namespace GeoDo.Smart.RasterSmooth
{
    public partial class UIGroup : UserControl, IUIGroupProvider
    {
        RadRibbonBar _bar = null;
        RadRibbonBarGroup _groupBar = new RadRibbonBarGroup();
        ISmartSession _session = null;
        RadButtonElement _btnCompute = new RadButtonElement("数据平滑");

        public UIGroup()
        {
            InitializeComponent();
            CreateRibbonBar();
        }

        private void CreateRibbonBar()
        {
            _groupBar.Text = "数据平滑";
            _bar = new RadRibbonBar();
            _bar.Dock = DockStyle.Top;

            _btnCompute.Margin = new Padding(2, 2, 2, 2);
            _btnCompute.TextAlignment = ContentAlignment.BottomCenter;
            _btnCompute.ImageAlignment = ContentAlignment.TopCenter;
            _btnCompute.Click += new EventHandler(_btnCompute_Click);
            _groupBar.Items.Add(_btnCompute);
        }

        void _btnCompute_Click(object sender, EventArgs e)
        {
            try
            {
                string exe = System.AppDomain.CurrentDomain.BaseDirectory + "GeoDo.Smart.RasterSmooth.exe";
                Process.Start(exe);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public object Content
        {
            get { return _groupBar; }
        }

        public void Init(ISmartSession session, params object[] arguments)
        {
            this._session = session;
            SetImage();
        }

        public void UpdateStatus()
        {

        }

        private void SetImage()
        {
            if (_session != null)
            {
                _btnCompute.Image = _session.UIFrameworkHelper.GetImage("cma:proDataCollect32.png");
            }
        }
    }
}
