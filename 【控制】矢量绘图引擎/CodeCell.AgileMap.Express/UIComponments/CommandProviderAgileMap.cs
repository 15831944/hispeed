using System;
using System.Collections.Generic;
using System.Text;
using CodeCell.Bricks.AppFramework;

namespace CodeCell.AgileMap.Express
{
    public class CommandProviderAgileMap:ICommandProvider
    {
        protected IToolbar[] _toolbars = null;
        protected IMenu[] _menus = null;

        public CommandProviderAgileMap()
        {
            _toolbars = new IToolbar[] 
                             {
                                 new MainToolBar()
                             };
            _menus = new IMenu[] 
                             {
                                 new FileMenus(),
                                 new EditMenus(),
                                 new ViewMenus(),
                                 new ToolMenus()
                             };
        }

        #region ICommandProvider ��Ա

        public IToolbar[] Toolbars
        {
            get 
            {
                return _toolbars;
            }
        }

        public IMenu[] Menus
        {
            get 
            {
                return _menus;
            }
        }

        #endregion
    }
}
