using System;
using GameplayContext;
using Android.App;
using System.Runtime.Remoting.Contexts;
using Java.Interop;
using Android.Views;

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
            _height = 600;
            _width = 300;
        }

        public int GameHeight { get { return _height; } }

        public int GameWidth { get { return _width; } }
    }
}

