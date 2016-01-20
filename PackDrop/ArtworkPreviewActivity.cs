
using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using ViewModels;
using Android.Graphics;
using GalaSoft.MvvmLight.Helpers;
using Java.Net;
using System.Threading;

namespace PackDrop
{
    [Activity (Label = "ArtworkPreviewActivity")]            
    public class ArtworkPreviewActivity : Activity
    {
        private Dictionary<string, Bitmap> _bitmapLookup = new Dictionary<string, Bitmap>();

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
        }

        private View GetArtworkView(int position, Uri uri, View convertView)
        {
            var imageView = new ImageView(Application.Context);

            imageView.LayoutParameters = new Gallery.LayoutParams (150, 100);
            imageView.SetScaleType (ImageView.ScaleType.FitXy);

            ThreadPool.QueueUserWorkItem(a => 
                { 
                    DownloadBitmap(uri.AbsoluteUri);
                    RunOnUiThread(() => imageView.SetImageBitmap (_bitmapLookup[uri.AbsoluteUri]));
                });

            return imageView;
        }

        private void DownloadBitmap(string url)
        {
            if (_bitmapLookup.ContainsKey(url)) {
                return;
            }

            try
            {
                using (var connection = new URL(url).OpenConnection())
                {
                    connection.Connect();
                    using (var input = connection.InputStream)
                    {
                        _bitmapLookup.Add(url, BitmapFactory.DecodeStream(input));
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

