
//Add the Popups to the Map
function displayData(response) {
    if (response != null) {
        for (var i = 0; i < response.length; i++) {
            L.marker([response[i].latitude, response[i].longitude])
                .addTo(map)
                .bindPopup
                    (
                    "<strong>Area: "
                    + response[i].location_name
                    + "</strong>"
                    + "<br/><strong>Current Status: </strong>"
                    + response[i].getCurrentStatus
                    + "<br/><a href='/sample?areaname=" + response[i].location_name + "'>View All Samples</a>"
                );
        }
    }
}

