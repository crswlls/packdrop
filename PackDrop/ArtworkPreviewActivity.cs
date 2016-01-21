
using System;
using System.Collections.Generic;

using System.Threading;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using Java.Net;
using ViewModels;

namespace PackDrop
{
    [Activity (Label = "PackDrop", Icon = "@drawable/icon")]            
    public class ArtworkPreviewActivity : Activity
    {
        private ArtworkPreviewViewModel Vm
        {
            get
            {
                return App.Locator.ArtworkPreviewVm;
            }
        }

        protected override async void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ArtworkPreview);
            await Vm.InitAsync();

            FindViewById<Gallery>(Resource.Id.gallery).Adapter = Vm.ImageUris.GetAdapter(GetArtworkView);
            var btn = FindViewById<Button>(Resource.Id.Continue);
            btn.SetCommand ("Click", Vm.GoToGameCommand);
            //// btn.SetBinding(() => btn.Enabled, () => Vm.GoToGameCommand.CanExecute);
        }

        private View GetArtworkView(int position, Uri uri, View convertView)
        {
            var imageView = new ImageView(Application.Context);

            imageView.LayoutParameters = new Gallery.LayoutParams (150, 100);
            imageView.SetScaleType (ImageView.ScaleType.FitXy);

            ThreadPool.QueueUserWorkItem(a => 
                { 
                    DownloadBitmap(uri.AbsoluteUri);
                    RunOnUiThread(() => imageView.SetImageBitmap (BitmapCache.Get(uri.AbsoluteUri)));
                });

            return imageView;
        }

        private void DownloadBitmap(string url)
        {
            if (BitmapCache.Contains(url)) {
                return;
            }

            try
            {
                using (var connection = new URL(url).OpenConnection())
                {
                    connection.Connect();
                    using (var input = connection.InputStream)
                    {
                        BitmapCache.Add(url, BitmapFactory.DecodeStream(input));
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

