using System;
using System.Collections.Generic;
using Android.Graphics;

namespace PackDrop
{
    public static class BitmapCache
    {
        private static Dictionary<string, Bitmap> _cache = new Dictionary<string, Bitmap>();

        public static Bitmap Get(string key)
        {
            return _cache[key];
        }

        public static bool Contains(string key)
        {
            return _cache.ContainsKey(key);
        }

        public static void Add(string key, Bitmap value)
        {
            _cache.Add(key, value);
        }
    }
}

