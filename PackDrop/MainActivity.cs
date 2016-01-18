using Android.App;
using Android.Widget;
using Android.OS;
using GalaSoft.MvvmLight.Views;
using ViewModels;
using GalaSoft.MvvmLight.Helpers;

namespace PackDrop
{
    [Activity (Label = "PackDrop", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : ActivityBase
    {
        private MainViewModel Vm
        {
            get {
                return App.Locator.MainVm;

            }
        }

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var layout = FindViewById<LinearLayout> (Resource.Id.mainLayout);
            layout.SetCommand ("Click", Vm.LaunchCommand);
        }
    }
}


