package com.android.eindopdracht.spotify;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.io.InputStream;

/**
 * Created by rogie on 03-Apr-17.
 */

public class SearchList extends ArrayAdapter<String> {

    private final Activity context;
    private final String[] web;
    private final String[] imageId;

    public SearchList(Activity context, String[] web, String[] imageId) {
        super(context, R.layout.search_list, web);
        this.context = context;
        this.web = web;
        this.imageId = imageId;
    }

    @Override
    public View getView(int position, View view, ViewGroup parent) {
        LayoutInflater inflater = context.getLayoutInflater();
        View rowView= inflater.inflate(R.layout.search_list, null, true);

        //Set text
        TextView txtTitle = (TextView) rowView.findViewById(R.id.ArtistName);
        txtTitle.setText(web[position]);

        //Set image
        ImageView imageView = (ImageView) rowView.findViewById(R.id.ArtistImage);
        new DownloadImageTask(imageView).execute(imageId[position]);

        return rowView;
    }

    public String getArtistName(int position){
        return web[position];
    }

    private class DownloadImageTask extends AsyncTask<String, Void, Bitmap> {
        ImageView bmImage;

        public DownloadImageTask(ImageView bmImage) {
            this.bmImage = bmImage;
        }

        protected Bitmap doInBackground(String... urls) {
            String urldisplay = urls[0];
            Bitmap mIcon11 = null;
            try {
                InputStream in = new java.net.URL(urldisplay).openStream();
                mIcon11 = BitmapFactory.decodeStream(in);
            } catch (Exception e) {
                e.printStackTrace();
            }
            return mIcon11;
        }

        protected void onPostExecute(Bitmap result) {
            bmImage.setImageBitmap(result);
        }
    }
}