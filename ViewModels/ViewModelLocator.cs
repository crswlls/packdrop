﻿using Microsoft.Practices.ServiceLocation;
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

        public ArtworkPreviewViewModel ArtworkPreviewVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ArtworkPreviewViewModel>();
            }
        }

        public GameOverViewModel GameOverVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GameOverViewModel>();
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
            SimpleIoc.Default.Register<ArtworkPreviewViewModel> ();
            SimpleIoc.Default.Register<GameOverViewModel> ();
        }
    }
}

