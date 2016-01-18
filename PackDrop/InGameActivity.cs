using Android.App;
using Android.OS;
using Android.Widget;
using Android.Graphics;
using Java.Net;
using Java.IO;
using System.Threading;
using ViewModels;

namespace PackDrop
{
    [Activity (Label = "InGameActivity")]            
    public class InGameActivity : Activity
    {
        private Bitmap _bitmap;

        private InGameViewModel Vm
        {
            get
            {
                return App.Locator.InGameVm;

            }
        }

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.InGame);

            var image = CreateNewImageTile();

            Vm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Vm.Game)) {
                    image.SetY(Vm.FallingTile.YPos);
                }
            };

            Vm.Game.NewTile += (sender, args) => {
                var newImage = CreateNewImageTile ();
                newImage.SetY (Vm.Game.GetColumn (0) [0].YPos);
            };
        }

        private ImageView CreateNewImageTile()
        {
            var layout = FindViewById<LinearLayout>(Resource.Id.gameLayout);
            var image = new ImageView(ApplicationContext);
            ThreadPool.QueueUserWorkItem(a => 
            {
                DownloadBitmap();
                RunOnUiThread(() => 
                    {
                        image.SetImageBitmap (_bitmap);
                        layout.AddView (image);
                    });
            });
            return image;
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            Vm.Initialise ();
        }

        private void DownloadBitmap()
        {
            if (_bitmap != null) {
                return;
            }

            try
            {
                using (var connection = new URL("DUMMYURLHERE").OpenConnection())
                {
                    connection.Connect();
                    using (var input = connection.InputStream)
                    {
                        _bitmap = BitmapFactory.DecodeStream(input);
                    }
                }
            }
            catch
            {
                /// Do nothing for now
            }
        } 
    }
}

