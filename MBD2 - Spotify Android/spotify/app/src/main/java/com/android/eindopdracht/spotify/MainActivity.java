package com.android.eindopdracht.spotify;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Toast;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class MainActivity extends AppCompatActivity {

    private static final String SPOTIFYPREFERENCES = "SpotifyPreferences";
    String favoArtistSetName = "favoArtists";
    SharedPreferences sharedpreferences;
    private SwipeRefreshLayout swipeContainer;
    ListView favoArtistList;
    ArrayAdapter<String> arrayAdapterFavoList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Toolbar myToolbar = (Toolbar) findViewById(R.id.TheToolbar);
        setSupportActionBar(myToolbar);
        getSupportActionBar().setTitle("Favorites");

        favoArtistList = (ListView) findViewById(R.id.FavoArtistList);
        AdapterView.OnItemClickListener openArtistClickedHandler = new AdapterView.OnItemClickListener() {
            public void onItemClick(AdapterView parent, View v, int position, long id) {
                String artistName = arrayAdapterFavoList.getItem(position);
                Intent ArtistIntent = new Intent(MainActivity.this, ArtistActivity.class);
                ArtistIntent.putExtra("ArtistName", artistName);
                ArtistIntent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                startActivity(ArtistIntent);
            }
        };
        AdapterView.OnItemLongClickListener deleteFavoArtistClickHandler = new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> parent, View view, int position, long id) {
                String artistName = arrayAdapterFavoList.getItem(position);
                createDialog(view, artistName);
                return true;
            }
        };

        favoArtistList.setOnItemClickListener(openArtistClickedHandler);
        favoArtistList.setOnItemLongClickListener(deleteFavoArtistClickHandler);

        swipeContainer = (SwipeRefreshLayout) findViewById(R.id.swipeContainer);
        swipeContainer.setOnRefreshListener(new SwipeRefreshLayout.OnRefreshListener() {
            @Override
            public void onRefresh() {
                reloadFavoList();
            }
        });
        swipeContainer.setColorSchemeResources(android.R.color.holo_blue_bright);

        sharedpreferences = getSharedPreferences(SPOTIFYPREFERENCES, Context.MODE_PRIVATE);
    }

    @Override
    public void onResume()
    {
        super.onResume();
        reloadFavoList();
    }

    public void createDialog(View view, final String name){

        AlertDialog.Builder adb = new AlertDialog.Builder(this);
        adb.setTitle("Are you sure to delete '" + name + "' from your favo list?");
        adb.setIcon(android.R.drawable.ic_dialog_alert);
        adb.setPositiveButton("OK", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                Set<String> favoArtists = new HashSet<>(sharedpreferences.getStringSet(favoArtistSetName, new HashSet<String>()));
                SharedPreferences.Editor editor = sharedpreferences.edit();
                favoArtists.remove(name);

                editor.putStringSet(favoArtistSetName, favoArtists);
                editor.apply();
                editor.commit();

                reloadFavoList();
            } });

        adb.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                Toast.makeText(getApplicationContext(), "cancel", Toast.LENGTH_LONG).show();
            } });

        AlertDialog alertDialog = adb.create();
        alertDialog.show();
    }

    @Override public boolean onCreateOptionsMenu(Menu menu)
    {
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch(item.getItemId()) {
            case R.id.favo_toolbar_search:
                Intent intent = new Intent(this, SearchActivity.class);
                this.startActivity(intent);
                break;
            default:
                return super.onOptionsItemSelected(item);
        }

        return true;
    }

    private void getAndSetArtistFavoList(){
        //Get favolist
        Set favoArtists = sharedpreferences.getStringSet(favoArtistSetName, null);

        if(favoArtists != null && favoArtists.size() > 0) {
            List<String> list = new ArrayList<String>(favoArtists);
            arrayAdapterFavoList = new ArrayAdapter<String>(MainActivity.this, android.R.layout.simple_list_item_1, list);
            favoArtistList.setAdapter(arrayAdapterFavoList);
        }
    }

    private void reloadFavoList() {
        if(arrayAdapterFavoList != null) {
            arrayAdapterFavoList.clear();
        }
            getAndSetArtistFavoList();
            swipeContainer.setRefreshing(false);
    }
}
