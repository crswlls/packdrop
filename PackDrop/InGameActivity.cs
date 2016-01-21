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
using System.Collections.ObjectModel;

namespace PackDrop
{
    [Activity (Label = "PackDrop", Icon = "@drawable/icon")]            
    public class InGameActivity : Activity
    {
        private ImageView _fallingImage;

        private InGameViewModel Vm
        {
            get
            {
                return App.Locator.InGameVm;
            }
        }

        public TextView ScoreValue { get; set;}
        public Binding ScoreBinding { get; set;}

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.InGame);
            FindViewById<ImageButton>(Resource.Id.rightBtn).SetCommand ("Click", Vm.MoveRightCommand);
            FindViewById<ImageButton>(Resource.Id.leftBtn).SetCommand ("Click", Vm.MoveLeftCommand);
            FindViewById<ImageButton>(Resource.Id.downBtn).SetCommand ("Click", Vm.DropCommand);
            ScoreValue = FindViewById<TextView>(Resource.Id.scoreValue);
            ScoreBinding = this.SetBinding(() => Vm.Score, () => ScoreValue.Text);

            SetupListView(Resource.Id.game1, Vm.Column1);
            SetupListView(Resource.Id.game2, Vm.Column2);
            SetupListView(Resource.Id.game3, Vm.Column3);
            SetupListView(Resource.Id.game4, Vm.Column4);
            SetupListView(Resource.Id.game5, Vm.Column5);
            SetupListView(Resource.Id.game6, Vm.Column6);
            SetupListView(Resource.Id.game7, Vm.Column7);
            SetupListView(Resource.Id.game8, Vm.Column8);
            SetupListView(Resource.Id.game9, Vm.Column9);

            // HACK : Duplicates 10dp padding for the list views
            var padding = (int)(10 * Application.Context.Resources.DisplayMetrics.Density);

            // HACK: Look at a nicer way to do this with MVVMLight
            Vm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Vm.FallingTileYPos))
                {
                    MoveTile(padding);
                }

                if (e.PropertyName == nameof(Vm.FallingTileImage))
                {
                    NewTile();
                }
            };
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            Vm.Initialise ();
        }

        private void SetupListView(int resourceId, ObservableCollection<Tile> column)
        {
            FindViewById<ListView> (resourceId).Adapter = column.GetAdapter (GetColumnView);
            FindViewById<ListView> (resourceId).LayoutParameters.Width = Vm.TileSize;
            FindViewById<ListView> (resourceId).RequestLayout ();
        }

        private void MoveTile(int padding)
        {
            RunOnUiThread (() =>  {
                if (_fallingImage == null) {
                    _fallingImage = CreateNewImageTile (Vm.FallingTileImage);
                }
                _fallingImage.SetX (Vm.FallingTileXPos + padding);
                _fallingImage.SetY (Vm.FallingTileYPos);
            });
        }

        private void NewTile()
        {
            RunOnUiThread (() =>  {
                _fallingImage.SetY (-150);
                _fallingImage.SetImageBitmap (BitmapCache.Get (Vm.FallingTileImage));
            });
        }

        private ImageView CreateNewImageTile(string cacheId)
        {
            var layout = FindViewById<RelativeLayout>(Resource.Id.gameArea);
            var image = new ImageView(ApplicationContext);
            image.SetImageBitmap(BitmapCache.Get(cacheId));
            image.LayoutParameters = new RelativeLayout.LayoutParams(Vm.TileSize, Vm.TileSize);
            layout.AddView(image);
            return image;
        }

        private View GetColumnView(int position, Tile tile, View convertView)
        {
            var imageView = new ImageView(Application.Context);
            imageView.SetImageBitmap (BitmapCache.Get(tile.ImageId));
            imageView.LayoutParameters = new ListView.LayoutParams(Vm.TileSize, Vm.TileSize);
            return imageView;
        }
    }
}

