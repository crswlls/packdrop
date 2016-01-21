
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
        public EditText SearchText { get;set; }
        public Binding TextBinding { get;set; }

        private ArtworkPreviewViewModel Vm
        {
            get
            {
                return App.Locator.ArtworkPreviewVm;
            }
        }

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ArtworkPreview);
            //// await Vm.InitAsync();

            FindViewById<GridView>(Resource.Id.gridview).Adapter = Vm.ImageUris.GetAdapter(GetArtworkView);
            var btn = FindViewById<Button>(Resource.Id.Continue);
            btn.SetCommand ("Click", Vm.GoToGameCommand);
            SearchText = FindViewById<EditText>(Resource.Id.editText);
            var searchButton = FindViewById<Button>(Resource.Id.searchBtn);
            TextBinding = this.SetBinding(() => SearchText.Text, BindingMode.TwoWay);
            searchButton.SetCommand("Click", Vm.SearchCommand, TextBinding);
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

