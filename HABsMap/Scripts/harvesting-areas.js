
//Add the Popups to the Map
function displayData(response) {
    //If JSON is passed
    if (response != null) {
        //Loop Through the JSON reponse
        for (var i = 0; i < response.length; i++) {
            //Get the Latitude and Longitude from the JSON response and add to the Leaflet Map
            L.marker([response[i].latitude, response[i].longitude])
                .addTo(map)
                //Add a pop up to the map marker which will contain the sample and status information
                .bindPopup
                    (
                    // Sample Location Name
                    "<strong style='text-align:center; font-size: 15px;'>Sample Area: "
                    + response[i].location_name
                    + "</strong>"
                    //Current Area Status
                    + "<br/><strong>Current Status: </strong>"
                    + response[i].getCurrentStatus
                    //Species Sampled in the most recent sample
                    + "<br/><strong>Species Sampled: </strong>"
                    //The name of the species which was sampled
                    + response[i].species_name
                    //A link which leads to all samples for the area. The area name is passed as a string query to the method in the sample controller
                    + "<br/><a href='/sample?areaname=" + response[i].location_name + "'><btn class='btn btn-xs btn-info' style='margin-top: 8px;'>View All Area Samples</btn></a>"
                );
        }
    }
    //If there is no JSON passed
    if(response == null)
    {
        //Alert the user to check their internet connection and update the text in the warning box on the map page
        var warning = "Could not retrive samples, no network connection detected. Please ensure that you have an internet connection. ";
        $('.alert-no-network').html(warning).css('display', 'block');
    }
}

