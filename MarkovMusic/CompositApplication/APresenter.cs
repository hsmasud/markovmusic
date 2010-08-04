using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CompositApplication
{
    public abstract class APresenter:DependencyObject, IPresenter
    {
        private readonly IView _view;

        public APresenter(IView view)
        {
            _view = view;
            _view.Presenter = this;
        }

        #region Implementation of IPresenter

        public IView View
        {
            get { return _view; }
        }

        #endregion
    }
}