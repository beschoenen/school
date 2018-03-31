package org.avans.VTSOa.deslimstemens.Tests;

import org.avans.VTSOa.deslimstemens.*;

import static org.junit.Assert.*;

/**
 * Created by tyil on 12/10/15.
 */
public class MainTest extends Main {
    @org.junit.Test
    public void testExceptions() throws Exception {
        int response = Main.postException(new Exception("Test exception"));

        assertEquals(response, 200);
    }
}
