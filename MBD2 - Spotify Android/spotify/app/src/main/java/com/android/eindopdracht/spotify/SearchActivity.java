package com.android.eindopdracht.spotify;

import android.app.SearchManager;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v4.view.MenuItemCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.SearchView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import static android.provider.ContactsContract.CommonDataKinds.Website.URL;

public class SearchActivity extends AppCompatActivity implements SearchView.OnQueryTextListener {

    ListView searchResultList;
    SearchList adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);

        Toolbar myToolbar = (Toolbar) findViewById(R.id.TheToolbar);
        setSupportActionBar(myToolbar);
        getSupportActionBar().setTitle("Search artist");

        searchResultList = (ListView) findViewById(R.id.SearchResultList);
        AdapterView.OnItemClickListener openArtistClickedHandler = new AdapterView.OnItemClickListener() {
            public void onItemClick(AdapterView parent, View v, int position, long id) {
                String clickedArtist = adapter.getArtistName(position);
                Intent ArtistIntent = new Intent(SearchActivity.this, ArtistActivity.class);
                ArtistIntent.putExtra("ArtistName", clickedArtist);
                ArtistIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                startActivity(ArtistIntent);
            }
        };
        searchResultList.setOnItemClickListener(openArtistClickedHandler);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu)
    {
        getMenuInflater().inflate(R.menu.search_menu, menu);

        SearchManager searchManager = (SearchManager) getSystemService(Context.SEARCH_SERVICE);
        MenuItem searchMenuItem = menu.findItem(R.id.toolbar_search_artist);
        SearchView searchView = (SearchView) searchMenuItem.getActionView();

        searchView.setSearchableInfo(searchManager.getSearchableInfo(getComponentName()));
        searchView.setSubmitButtonEnabled(true);
        searchView.setOnQueryTextListener(this);
        searchView.setFocusable(true);
        searchView.setIconifiedByDefault(false);
        searchView.requestFocusFromTouch();
        searchView.requestFocus();

        return true;
    }

    @Override
    public boolean onQueryTextSubmit(String query) {
        Toast.makeText(this, "Searching for "+query, Toast.LENGTH_SHORT).show();
        String spotifyUrl = "https://api.spotify.com/v1/search?query="+ query +"&type=artist&offset=0&limit=20";
        new ProcessJSON().execute(spotifyUrl);

        return false;
    }

    @Override
    public boolean onQueryTextChange(String newText) {
        return false;
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
                    ArrayList<String> artistNames = new ArrayList<String>();
                    ArrayList<String> artistImage = new ArrayList<String>();

                    // Get the full HTTP Data as JSONObject
                    JSONObject reader = new JSONObject(stream);
                    JSONObject artists = reader.getJSONObject("artists");
                    JSONArray items = artists.getJSONArray("items");

                    for (int i = 0; i < items.length(); i++){
                        //Name
                        JSONObject artist = items.getJSONObject(i);
                        String name = artist.getString("name");
                        artistNames.add(name);

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

                        artistImage.add(img);
                    }

                    String[] artistsArray = new String[artistNames.size()];
                    artistsArray = artistNames.toArray(artistsArray);

                    String[] imagesArray = new String[artistImage.size()];
                    imagesArray = artistImage.toArray(imagesArray);

                    adapter = new SearchList(SearchActivity.this, artistsArray, imagesArray);
                    searchResultList.setAdapter(adapter);

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
