using System;
using GameplayContext;
using Android.App;
using System.Runtime.Remoting.Contexts;
using Java.Interop;
using Android.Views;
using Android.Content.Res;

namespace PackDrop
{
    public class GameDimensions : IGameDimensions
    {
        private int _height;
        private int _width;

        public void Update(int h, int w)
        {
            _height = h;
            _width = w;
        }

        public GameDimensions()
        {
            _height = (Application.Context.Resources.DisplayMetrics.HeightPixels * 2) / 3;
            _width = Application.Context.Resources.DisplayMetrics.WidthPixels - (int)(20 * Application.Context.Resources.DisplayMetrics.Density);
        }

        public int GameHeight { get { return _height; } }

        public int GameWidth { get { return _width; } }
    }
}

