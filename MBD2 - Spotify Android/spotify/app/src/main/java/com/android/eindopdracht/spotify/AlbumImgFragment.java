package com.android.eindopdracht.spotify;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.android.eindopdracht.spotify.R;

/**
 * Created by rogie on 03-Apr-17.
 */

public class AlbumImgFragment  extends Fragment {

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.activity_album_img, container, false);
    }
}