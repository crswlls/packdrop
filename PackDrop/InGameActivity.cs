using Android.App;
using Android.OS;
using Android.Widget;
using Android.Graphics;
using Java.Net;
using System.Threading;
using ViewModels;
using Android.Views;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;
using SharedKernel;

namespace PackDrop
{
    [Activity (Label = "InGameActivity")]            
    public class InGameActivity : Activity
    {
        private Dictionary<string, Bitmap> _bitmapLookup = new Dictionary<string, Bitmap>();

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
            FindViewById<ListView>(Resource.Id.game).Adapter = Vm.Column0.GetAdapter(GetColumnView);

            Vm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Vm.Game)) {
                    image.SetY(Vm.FallingTileYPos);
                }
            };
        }

        private ImageView CreateNewImageTile()
        {
            var layout = FindViewById<FrameLayout>(Resource.Id.gameLayout);
            var image = new ImageView(ApplicationContext);
            ThreadPool.QueueUserWorkItem(a => 
            {
                DownloadBitmap("temp");
                RunOnUiThread(() => 
                    {
                        image.SetImageBitmap(_bitmapLookup["temp"]);
                        image.LayoutParameters = new ViewGroup.LayoutParams(Vm.TileSize, Vm.TileSize);
                        layout.AddView(image);
                    });
            });
            return image;
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            Vm.Initialise ();
        }

        private View GetColumnView(int position, Tile tile, View convertView)
        {
            var imageView = new ImageView(Application.Context);
            imageView.SetImageBitmap (_bitmapLookup["temp"]);
            imageView.LayoutParameters = new ViewGroup.LayoutParams(Vm.TileSize, Vm.TileSize);

            return imageView;
        }

        private void DownloadBitmap(string id)
        {
            if (_bitmapLookup.ContainsKey(id)) {
                return;
            }

            try
            {
                using (var connection = new URL("DUMMYURLHERE").OpenConnection())
                {
                    connection.Connect();
                    using (var input = connection.InputStream)
                    {
                        _bitmapLookup.Add(id, BitmapFactory.DecodeStream(input));
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

