using Android.App;
using Android.Widget;
using Android.OS;
using GalaSoft.MvvmLight.Views;
using ViewModels;
using GalaSoft.MvvmLight.Helpers;
using Android.Views.Animations;
using Android.Graphics;

namespace PackDrop
{
    [Activity (Label = "PackDrop", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ActivityBase, Android.Views.Animations.Animation.IAnimationListener
    {
        private MainViewModel Vm
        {
            get
            {
                return App.Locator.MainVm;

            }
        }

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            ActionBar.Hide(); 

            // Get our button from the layout resource,
            // and attach an event to it
            var layout = FindViewById<RelativeLayout> (Resource.Id.mainLayout);
            layout.SetBackgroundColor(Color.Black);
            var txt = FindViewById<TextView> (Resource.Id.introText);
            var logo = FindViewById<ImageView> (Resource.Id.logoImage);
            layout.SetCommand ("Click", Vm.LaunchCommand);

            TranslateAnimation transAnim = new TranslateAnimation(0, 0, -500, Resources.DisplayMetrics.HeightPixels / 5);
            transAnim.SetAnimationListener(this);
            transAnim.Interpolator = new BounceInterpolator();
            transAnim.StartOffset = 500;
            transAnim.Duration = 1500;
            transAnim.FillAfter = true;
            logo.StartAnimation(transAnim);

            AlphaAnimation fadeTextIn = new AlphaAnimation(0.0f, 1.0f); 
            txt.StartAnimation(fadeTextIn);
            fadeTextIn.StartOffset = transAnim.StartOffset + transAnim.Duration;
            fadeTextIn.Duration = 1000;
            fadeTextIn.FillAfter = true;
        }

        public void OnAnimationEnd (Animation animation)
        {
        }

        public void OnAnimationRepeat (Animation animation)
        {
        }

        public void OnAnimationStart (Animation animation)
        {
        }
    }
}


