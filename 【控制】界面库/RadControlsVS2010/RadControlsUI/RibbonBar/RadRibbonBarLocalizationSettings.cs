using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Telerik.WinControls.UI
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public class RibbonBarLocalizationSettings
    {
        private RadRibbonBarElement ribbonBarElement;

        private string showQuickAccessMenuBelowItemText = "��ʾ�ڹ������·�";
        private string showQuickAccessMenuAboveItemText = "��ʾ�ڹ������Ϸ�";
        private string minimizeRibbonItemText = "���𹤾���";
        private string maximizeRibbonItemText = "չ��������";

        public RibbonBarLocalizationSettings(RadRibbonBarElement ribbonBarElement)
        {
            this.ribbonBarElement = ribbonBarElement;
        }

        [DefaultValue("��ʾ�ڹ������·�"), Localizable(true)]
        public string ShowQuickAccessMenuBelowItemText
        {
            get{ return showQuickAccessMenuBelowItemText; }
            set{ showQuickAccessMenuBelowItemText = value; }
        }

        [DefaultValue("��ʾ�ڹ������Ϸ�"), Localizable(true)]
        public string ShowQuickAccessMenuAboveItemText
        {
            get { return showQuickAccessMenuAboveItemText; }
            set { showQuickAccessMenuAboveItemText = value; }
        }

        [DefaultValue("���𹤾���"), Localizable(true)]
        public string MinimizeRibbonItemText
        {
            get { return minimizeRibbonItemText; }
            set { minimizeRibbonItemText = value; }
        }

        [DefaultValue("չ��������"), Localizable(true)]
        public string MaximizeRibbonItemText
        {
            get { return maximizeRibbonItemText; }
            set { maximizeRibbonItemText = value; }
        }
    }
}
