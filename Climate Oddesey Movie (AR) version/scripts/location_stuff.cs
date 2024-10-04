using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LocationStuff : MonoBehaviour
{
    // The target location you want to show the distance to
    private GPSLoc targetLocation = new GPSLoc(90.426292f, 23.815231f); // Example target location

    public TMP_Text debugTxt;
    public bool gps_ok = false;

    GPSLoc currLoc = new GPSLoc();

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Check if the user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location not enabled on device or app does not have permission to access location");
            debugTxt.text = "Location not enabled on device or app does not have permission to access location";
            yield break;
        }

        // Starts the location service
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds, this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            debugTxt.text += "\nTimed Out";
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            debugTxt.text += ("\nUnable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, retrieve the device's current location
            gps_ok = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gps_ok)
        {
            // Get current location
            currLoc.lat = Input.location.lastData.latitude;
            currLoc.lon = Input.location.lastData.longitude;

            // Reset the debug text
            debugTxt.text = $"Current Location:\nLat: {currLoc.lat}\nLon: {currLoc.lon}\n";

            // Calculate the distance to the target location
            double distanceBetween = distance(currLoc.lat, currLoc.lon, targetLocation.lat, targetLocation.lon, 'K') * 1000; // Convert to meters

            // Update the debug text with the distance to the target (with 6 decimal precision)
            debugTxt.text += $"Distance to Target: {distanceBetween:F6} meters\n";

            // Optionally, you can give feedback to the player when they are close
            if (distanceBetween < 5) // Example: If within 5 meters
            {
                debugTxt.text += "You are very close to the target!\n";
            }
        }
    }

    // Calculate the distance between two GPS coordinates
    private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
    {
        if ((lat1 == lat2) && (lon1 == lon2))
        {
            return 0;
        }
        else
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            return (dist);
        }
    }

    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }
}

// Simple class to store GPS coordinates
public class GPSLoc
{
    public float lon;
    public float lat;

    public GPSLoc()
    {
        lon = 0;
        lat = 0;
    }

    public GPSLoc(float lon, float lat)
    {
        this.lon = lon;
        this.lat = lat;
    }
}
