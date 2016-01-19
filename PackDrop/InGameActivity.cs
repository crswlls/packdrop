using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using Java.Net;
using SharedKernel;
using ViewModels;

namespace PackDrop
{
    [Activity (Label = "InGameActivity")]            
    public class InGameActivity : Activity
    {
        private Dictionary<string, Bitmap> _bitmapLookup = new Dictionary<string, Bitmap>();
        private ImageView _fallingImage;

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
            FindViewById<Button>(Resource.Id.rightBtn).SetCommand ("Click", Vm.MoveRightCommand);
            FindViewById<Button>(Resource.Id.leftBtn).SetCommand ("Click", Vm.MoveLeftCommand);
            FindViewById<Button>(Resource.Id.downBtn).SetCommand ("Click", Vm.DropCommand);
            var gameArea = FindViewById<RelativeLayout>(Resource.Id.gameArea);

            _fallingImage = CreateNewImageTile();
            FindViewById<ListView>(Resource.Id.game1).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView>(Resource.Id.game1).RequestLayout();
            FindViewById<ListView>(Resource.Id.game1).Adapter = Vm.Column1.GetAdapter(GetColumnView);
            ////FindViewById<ListView>(Resource.Id.game1).SetBackgroundColor(Color.AliceBlue);
            FindViewById<ListView>(Resource.Id.game2).Adapter = Vm.Column2.GetAdapter(GetColumnView);
            FindViewById<ListView>(Resource.Id.game2).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView>(Resource.Id.game2).RequestLayout();
            ////FindViewById<ListView>(Resource.Id.game2).SetBackgroundColor(Color.Beige);
            FindViewById<ListView>(Resource.Id.game3).Adapter = Vm.Column3.GetAdapter(GetColumnView);
            FindViewById<ListView>(Resource.Id.game3).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView>(Resource.Id.game3).RequestLayout();
            FindViewById<ListView>(Resource.Id.game4).Adapter = Vm.Column4.GetAdapter(GetColumnView);
            FindViewById<ListView>(Resource.Id.game4).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView>(Resource.Id.game4).RequestLayout();
            FindViewById<ListView>(Resource.Id.game5).Adapter = Vm.Column5.GetAdapter(GetColumnView);
            FindViewById<ListView>(Resource.Id.game5).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView>(Resource.Id.game5).RequestLayout();
            FindViewById<ListView>(Resource.Id.game6).Adapter = Vm.Column6.GetAdapter(GetColumnView);
            FindViewById<ListView>(Resource.Id.game6).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView>(Resource.Id.game6).RequestLayout();
            //// FindViewById<ListView>(Resource.Id.game6).SetBackgroundColor(Color.Chocolate);
            FindViewById<ListView>(Resource.Id.game7).Adapter = Vm.Column7.GetAdapter(GetColumnView);
            FindViewById<ListView>(Resource.Id.game7).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView>(Resource.Id.game7).RequestLayout();
            FindViewById<ListView>(Resource.Id.game8).Adapter = Vm.Column8.GetAdapter(GetColumnView);
            FindViewById<ListView>(Resource.Id.game8).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView>(Resource.Id.game8).RequestLayout();
            FindViewById<ListView>(Resource.Id.game9).Adapter = Vm.Column9.GetAdapter(GetColumnView);
            FindViewById<ListView>(Resource.Id.game9).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView>(Resource.Id.game9).RequestLayout();

            var padding = (int)(10 * Application.Context.Resources.DisplayMetrics.Density);

            Vm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Vm.Game)) {
                    _fallingImage.SetX(Vm.FallingTileXPos + padding);
                    _fallingImage.SetY(Vm.FallingTileYPos);
                }
            };
        }

        private ImageView CreateNewImageTile()
        {
            var layout = FindViewById<RelativeLayout>(Resource.Id.gameArea);
            var image = new ImageView(ApplicationContext);
            ThreadPool.QueueUserWorkItem(a => 
            {
                DownloadBitmap("temp");
                RunOnUiThread(() => 
                    {
                        image.SetImageBitmap(_bitmapLookup["temp"]);
                        image.LayoutParameters = new RelativeLayout.LayoutParams(Vm.TileSize, Vm.TileSize);
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
            imageView.LayoutParameters = new ListView.LayoutParams(Vm.TileSize, Vm.TileSize);
            _fallingImage.SetY(-150);

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

