using Android.App;
using Android.OS;

namespace PackDrop
{
    [Activity (Label = "Game Over", Icon = "@drawable/icon")]            
    public class GameOverActivity : Activity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.GameOver);
        }
    }
}

