using Android.App;
using Android.Widget;
using Android.OS;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace PackDrop
{
    [Activity (Label = "PackDrop", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : ActivityBase, Android.Views.View.IOnClickListener
    {
        private static bool _initialised;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var layout = FindViewById<LinearLayout> (Resource.Id.mainLayout);
            
            layout.SetOnClickListener (this);

            if (!_initialised)
            {
                _initialised = true;
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

                var nav = new NavigationService();
                nav.Configure(nameof(InGameActivity), typeof(InGameActivity));

                SimpleIoc.Default.Register<INavigationService>(() => nav);
            }
        }

        public void OnClick (Android.Views.View v)
        {
            var nav = ServiceLocator.Current.GetInstance<INavigationService>();
            nav.NavigateTo(nameof(InGameActivity));
        }
    }
}


