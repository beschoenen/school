package com.android.eindopdracht.spotify;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.graphics.PorterDuff;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.InputStream;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Set;

public class ArtistActivity extends AppCompatActivity {

    private static final String SPOTIFYPREFERENCES = "SpotifyPreferences";

    String favoArtistSetName = "favoArtists";
    SharedPreferences sharedpreferences;

    ImageView artistImage;
    ListView albumList;
    String artistName;
    AlbumList adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_artist);

        artistName = getIntent().getExtras().getString("ArtistName");

        Toolbar myToolbar = (Toolbar) findViewById(R.id.TheToolbar);
        setSupportActionBar(myToolbar);
        getSupportActionBar().setTitle(artistName);

        artistImage = (ImageView) findViewById(R.id.ArtistImg);
        albumList = (ListView) findViewById(R.id.AlbumList);
        AdapterView.OnItemClickListener openArtistClickedHandler = new AdapterView.OnItemClickListener() {
            public void onItemClick(AdapterView parent, View v, int position, long id) {
                String clickedAlbum = adapter.getAlbumName(position);
                Intent ArtistIntent = new Intent(ArtistActivity.this, AlbumActivity.class);
                ArtistIntent.putExtra("AlbumName", clickedAlbum);
                ArtistIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                startActivity(ArtistIntent);
            }
        };

        albumList.setOnItemClickListener(openArtistClickedHandler);
        sharedpreferences = getSharedPreferences(SPOTIFYPREFERENCES, Context.MODE_PRIVATE);

        //Get Artist data
        String spotifyUrl = "https://api.spotify.com/v1/search?query=" + artistName + "&type=artist&offset=0&limit=20";
        new ProcessJSON().execute(spotifyUrl);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_artist, menu);
        MenuItem item = menu.findItem(R.id.toolbar_favorite);

        // Get favolist
        Set<String> favoArtists = sharedpreferences.getStringSet(favoArtistSetName, null);
        item.getIcon().setColorFilter(
                favoArtists != null && favoArtists.contains(artistName) ? Color.YELLOW : Color.WHITE,
                PorterDuff.Mode.MULTIPLY
        );

        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.toolbar_favorite:
                if (!ToggleArtistFromFavoList(artistName, item)) {
                    Toast.makeText(this, "Could not save/delete", Toast.LENGTH_SHORT).show();
                }
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    private boolean ToggleArtistFromFavoList(String artistName, MenuItem menuItem) {
        // Get favolist
        Set<String> favoArtists = new HashSet<>(sharedpreferences.getStringSet(favoArtistSetName, new HashSet<String>()));

        SharedPreferences.Editor editor = sharedpreferences.edit();

        if (!favoArtists.contains(artistName)) { // Artist is no favorite yet
            menuItem.getIcon().setColorFilter(Color.YELLOW, PorterDuff.Mode.MULTIPLY);
            Toast.makeText(this, artistName + " is added to your favorite list", Toast.LENGTH_SHORT).show();
            favoArtists.add(artistName);
        } else { // Artist was a favorite
            menuItem.getIcon().setColorFilter(Color.WHITE, PorterDuff.Mode.MULTIPLY);
            Toast.makeText(this, artistName + " is deleted from your favorite list", Toast.LENGTH_SHORT).show();
            favoArtists.remove(artistName);
        }

        editor.putStringSet(favoArtistSetName, favoArtists);

        editor.apply();
        return editor.commit();
    }

    private class ProcessJSON extends AsyncTask<String, Void, String> {
        protected String doInBackground(String... strings) {
            String stream = null;
            String urlString = strings[0];

            HTTPDataHandler hh = new HTTPDataHandler();
            stream = hh.GetHTTPData(urlString);

            return stream;
        }

        protected void onPostExecute(String stream) {
            //Process JSON DATA
            if (stream != null) {
                try {
                    // Get the full HTTP Data as JSONObject
                    JSONObject reader = new JSONObject(stream);
                    JSONObject artists = reader.getJSONObject("artists");

                    JSONArray items = artists.getJSONArray("items");
                    JSONObject obj = items.getJSONObject(0);

                    //Get image
                    JSONArray images = obj.getJSONArray("images");
                    JSONObject image = images.getJSONObject(0);
                    String img = image.getString("url");
                    new DownloadImageTask(artistImage).execute(img);

                    //Get albums
                    String spotifyUrlAlbum = "https://api.spotify.com/v1/search?query=" + artistName + "&type=album&offset=0&limit=20";
                    new DownloadAlbumsTask(albumList).execute(spotifyUrlAlbum);

                } catch (JSONException e) {
                    e.printStackTrace();
                    Toast.makeText(getApplicationContext(), "Could't process data", Toast.LENGTH_SHORT).show();
                }
            } else {
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

    private class DownloadAlbumsTask extends AsyncTask<String, Void, String> {
        ListView lList;

        public DownloadAlbumsTask(ListView lList) {
            this.lList = lList;
        }

        protected String doInBackground(String... strings) {
            String stream = null;
            String urlString = strings[0];

            HTTPDataHandler hh = new HTTPDataHandler();
            stream = hh.GetHTTPData(urlString);

            return stream;
        }

        protected void onPostExecute(String stream) {
            //Process JSON DATA
            if (stream != null) {
                try {
                    ArrayList<String> albumNames = new ArrayList<String>();
                    ArrayList<String> albumImage = new ArrayList<String>();

                    // Get the full HTTP Data as JSONObject
                    JSONObject reader = new JSONObject(stream);
                    JSONObject artists = reader.getJSONObject("albums");
                    JSONArray items = artists.getJSONArray("items");

                    for (int i = 0; i < items.length(); i++) {
                        JSONObject artist = items.getJSONObject(i);
                        String name = artist.getString("name");
                        albumNames.add(name);

                        //Image
                        JSONObject imagesobj = items.getJSONObject(i);
                        JSONArray images = imagesobj.getJSONArray("images");
                        String img = null;
                        if (images.length() != 0) {
                            JSONObject image = images.getJSONObject(0);
                            img = image.getString("url");
                        }
                        else{
                            img = "";
                        }

                        albumImage.add(img);
                    }

                    String[] artistsArray = new String[albumNames.size()];
                    artistsArray = albumNames.toArray(artistsArray);

                    String[] imagesArray = new String[albumImage.size()];
                    imagesArray = albumImage.toArray(imagesArray);

                    adapter = new AlbumList(ArtistActivity.this, artistsArray, imagesArray);
                    albumList.setAdapter(adapter);

                } catch (JSONException e) {
                    e.printStackTrace();
                    Toast.makeText(getApplicationContext(), "Could't process data", Toast.LENGTH_SHORT).show();
                }

            } else {
                Toast.makeText(getApplicationContext(), "No data", Toast.LENGTH_SHORT).show();
            }
        }
    }
}