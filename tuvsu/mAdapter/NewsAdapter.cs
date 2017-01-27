using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using tuvsu.mModel;
using Android.Graphics;
using System.Net;
using System.Threading.Tasks;

namespace tuvsu.ListAdapter
{
    class NewsAdapter : BaseAdapter<pNews>
    {
        mModel.ImageDownload imageDownload = new mModel.ImageDownload();

        private List<pNews> newsList;
        private Context mContext;

        public NewsAdapter(Context context, List<pNews> list)
        {
            newsList = list;
            mContext = context;
        }

        public override pNews this[int position]
        {
            get
            {
                return newsList[position];
            }
        }

        public override int Count
        {
            get
            {
                return newsList.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            /*
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.NewsRow, null, false);
            }
            //Загрузка изображение по URL
            ImageView image = row.FindViewById<ImageView>(Resource.Id.imageView1);
            Android.Util.Log.Info("1111111111 111111111111111", newsList.Count().ToString());
            this.imageAsync(image, "http://tuvsu.ru/files/news/" + newsList[position].new_photo);

            TextView title = row.FindViewById<TextView>(Resource.Id.titleNews);
            title.Text = newsList[position].new_title;

            return row;
            */

            View view = convertView;

            if (view == null)
                view = LayoutInflater.From(mContext).Inflate(Resource.Layout.NewsRow, null, false);

            pNews item = this[position];

            TextView title = view.FindViewById<TextView>(Resource.Id.titleNews);
            title.Text = item.new_title;

            ImageView image = view.FindViewById<ImageView>(Resource.Id.imageView1);
            this.imageAsync(image, "http://tuvsu.ru/files/news/" + newsList[position].new_photo);

            return view;
        }

        async void imageAsync(ImageView img, string url)
        {
            var imageBitmap = await imageDownload.GetImageBitmapFromUrl(url);
            img.SetImageBitmap (imageBitmap);
        }


    }
}