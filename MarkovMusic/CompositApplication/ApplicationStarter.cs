using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CompositApplication
{
    public class ApplicationStarter:DependencyObject
    {
        public ApplicationStarter Start()
        {
            _bootstrapper = CreateBootstrapper();
            _bootstrapper.Run();
            ShellPresenter = _bootstrapper.Container.Resolve<IShellPresenter>();
            return this;
        }

        protected virtual Bootstrapper CreateBootstrapper()
        {
            return new Bootstrapper();
        }

        public static readonly DependencyProperty ShellPresenterProperty = DependencyProperty.Register(
            "ShellPresenter",
            typeof(IShellPresenter),
            typeof(ApplicationStarter));

        private Bootstrapper _bootstrapper;

        public IShellPresenter ShellPresenter
        {
            get
            {
                return (IShellPresenter)GetValue(ShellPresenterProperty);
            }
            set
            {
                SetValue(ShellPresenterProperty, value);
            }
        }
    }
}