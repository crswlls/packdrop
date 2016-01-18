using System;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;

namespace ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public InGameViewModel InGameVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InGameViewModel>();
            }
        }

        static ViewModelLocator()
        {
            Reset();
        }

        public static void Reset()
        {
            ServiceLocator.SetLocatorProvider (() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel> ();
            SimpleIoc.Default.Register<InGameViewModel> ();
        }
    }
}

