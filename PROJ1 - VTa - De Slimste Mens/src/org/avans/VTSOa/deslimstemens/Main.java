package org.avans.VTSOa.deslimstemens;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.stage.Stage;
import org.avans.VTSOa.deslimstemens.Controllers.LoginController;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.MissingException;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Navigator;
import org.avans.VTSOa.deslimstemens.Helpers.UIWrapper.Stager;

import java.io.DataOutputStream;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

public class Main extends Application{
    @Override
    public void start(Stage primaryStage) {
	    try {
		    primaryStage.setOnCloseRequest(event -> {
			    Platform.exit();
			    System.exit(0);
		    });
		    LoginController loginController = new LoginController();
		    RunTimeSettings.mainStager = new Stager(primaryStage, "MainStage");
		    RunTimeSettings.mainStager.addSceneController(loginController);
	        Navigator.registerStage(RunTimeSettings.mainStager);
		    Navigator.show("MainStage", loginController.getName());
	    } catch (MissingException e) {
		    e.printStackTrace();
	    }
    }

    public static void main(String[] args) {
        // run the application
        try {
            launch(args);
        } catch (Exception e) {
            // This one is in it's own try{} because the IDE kept complaining
	        e.printStackTrace();
            int returnStatus = Main.postException(e);

            if (returnStatus != 200) {
                System.out.println("Got response code " + returnStatus + " while trying to log the exception");
            }
	        System.exit(1);
        }
    }

    protected static int postException(Exception e) {
        try {
            // convert the stacktrace to a string
            StringWriter sw = new StringWriter();
            PrintWriter  pw = new PrintWriter(sw);
            e.printStackTrace(pw);

            // create the connection
            URL url = new URL("http://dsm.projects.tyil.work/post");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();

            // enable writing to the connection
            conn.setDoOutput(true);

            // set headers
            conn.setRequestMethod("POST");

            // send request
            DataOutputStream connWriter = new DataOutputStream(conn.getOutputStream());

            connWriter.writeBytes("post="+ e.getMessage() + "\n\n" + sw.toString());
            connWriter.flush();
            connWriter.close();

            return conn.getResponseCode();
        } catch (MalformedURLException mue) {
            System.out.println("Malformed URL");

            return -1;
        } catch (IOException ioe) {
            System.out.println(ioe.getMessage());

            return -1;
        }
    }
}
