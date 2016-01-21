using Android.App;
using Android.OS;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using ViewModels;

namespace PackDrop
{
    [Activity (Label = "Game Over", Icon = "@drawable/icon")]            
    public class GameOverActivity : Activity
    {
        public TextView ScoreValue { get; set;}
        public Binding ScoreBinding { get; set;}

        public GameOverViewModel Vm
        {
            get
            {
                return App.Locator.GameOverVm;
            }
        }

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.GameOver);

            ScoreValue = FindViewById<TextView>(Resource.Id.scoreValue);
            ScoreBinding = this.SetBinding(() => Vm.Score, () => ScoreValue.Text);
        }
    }
}

