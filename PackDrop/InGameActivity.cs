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

            var layout = FindViewById<LinearLayout> (Resource.Id.gameLayout);
            var image = new ImageView (ApplicationContext);
            ThreadPool.QueueUserWorkItem (a => 
                {
                    var imgBitmap = DownloadBitmap();
                    RunOnUiThread(() => 
                        {
                            image.SetImageBitmap (imgBitmap);
                            layout.AddView (image);
                        });
                });

            Vm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Vm.YPosition)) {
                    image.SetY (Vm.YPosition);
                }
            };
        }

        protected override void OnResume ()
        {
            base.OnResume ();
            Vm.Initialise ();
        }

        private Bitmap DownloadBitmap()
        {
            Bitmap bitmap = null;
            try
            {
                using (var connection = new URL("PUTDUMMYURLHERE").OpenConnection())
                {
                    connection.Connect();
                    using (var input = connection.InputStream)
                    {
                        bitmap = BitmapFactory.DecodeStream(input);
                    }
                }
            }
            catch
            {
                /// Do nothing for now
            }

            return bitmap;
        } 
    }
}

