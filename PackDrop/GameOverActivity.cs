using Android.App;
using Android.OS;

namespace PackDrop
{
    [Activity (Label = "GameOverActivity")]            
    public class GameOverActivity : Activity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.GameOver);
        }
    }
}

