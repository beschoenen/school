package com.android.eindopdracht.spotify;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.InputStream;
import java.util.ArrayList;

public class AlbumActivity extends AppCompatActivity {

    ImageView albumImage;
    ListView songList;
    String albumName;
    ArrayAdapter<String> arrayAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_album);

        albumName = getIntent().getExtras().getString("AlbumName");

        Toolbar myToolbar = (Toolbar) findViewById(R.id.TheToolbar);
        setSupportActionBar(myToolbar);
        getSupportActionBar().setTitle(albumName);

        albumImage = (ImageView) findViewById(R.id.AlbumImg);
        songList = (ListView) findViewById(R.id.SongList);

        //Get Artist data
        String spotifyUrlAlbum = "https://api.spotify.com/v1/search?query="+ albumName +"&type=album&offset=0&limit=20";
        new AlbumActivity.ProcessJSON().execute(spotifyUrlAlbum);
    }

    private class ProcessJSON extends AsyncTask<String, Void, String> {
        protected String doInBackground(String... strings){
            String stream = null;
            String urlString = strings[0];

            HTTPDataHandler hh = new HTTPDataHandler();
            stream = hh.GetHTTPData(urlString);

            return stream;
        }

        protected void onPostExecute(String stream){
            //Process JSON DATA
            if(stream != null){
                try{
                    // Get the full HTTP Data as JSONObject
                    JSONObject reader = new JSONObject(stream);
                    JSONObject artists = reader.getJSONObject("albums");

                    JSONArray items = artists.getJSONArray("items");
                    JSONObject obj = items.getJSONObject(0);

                    //Get album img
                    JSONArray images = obj.getJSONArray("images");
                    JSONObject image = images.getJSONObject(0);
                    String albumImg = image.getString("url");

                    //Get songs url
                    String songurl = obj.getString("href");

                    //Get album img
                    new AlbumActivity.DownloadImageTask(albumImage).execute(albumImg);

                    //Get album songs
                    new AlbumActivity.DownloadNumbersTask(songList).execute(songurl);
                }catch(JSONException e){
                    e.printStackTrace();
                    Toast.makeText(getApplicationContext(), "Could't process data", Toast.LENGTH_SHORT).show();
                }
            }
            else{
                Toast.makeText(getApplicationContext(), "No data", Toast.LENGTH_SHORT).show();
            }
        }
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

    private class DownloadNumbersTask extends AsyncTask<String, Void, String> {
        ListView lList;

        public DownloadNumbersTask(ListView lList) {
            this.lList = lList;
        }

        protected String doInBackground(String... strings){
            String stream = null;
            String urlString = strings[0];

            HTTPDataHandler hh = new HTTPDataHandler();
            stream = hh.GetHTTPData(urlString);

            return stream;
        }

        protected void onPostExecute(String stream) {
            //Process JSON DATA
            if(stream != null){
                try{
                    // Get the full HTTP Data as JSONObject
                    JSONObject reader= new JSONObject(stream);
                    JSONObject artists = reader.getJSONObject("tracks");

                    JSONArray items = artists.getJSONArray("items");
                    ArrayList<String> songNames = new ArrayList<String>();

                    for (int i = 0; i < items.length(); i++){
                        JSONObject artist = items.getJSONObject(i);
                        String name = artist.getString("name");
                        songNames.add(name);
                    }

                    arrayAdapter = new ArrayAdapter<String>(AlbumActivity.this, android.R.layout.simple_list_item_1, songNames);
                    songList.setAdapter(arrayAdapter);

                }catch(JSONException e){
                    e.printStackTrace();
                    Toast.makeText(getApplicationContext(), "Could't process data", Toast.LENGTH_SHORT).show();
                }

            }
            else{
                Toast.makeText(getApplicationContext(), "No data", Toast.LENGTH_SHORT).show();
            }
        }
    }
}
