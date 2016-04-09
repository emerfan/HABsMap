//Initialises the map and sets the coordinates to Ireland
var map = L.map('map').setView([53.0000, -8.0000], 7);

//Tile Image is needed for the Map library
L.tileLayer('http://services.arcgisonline.com/arcgis/rest/services/Ocean/World_Ocean_Base/MapServer/tile/{z}/{y}/{x}.png', {
    //The maximum amount a user can zoom in
    maxZoom: 12,
    // The minimum amount a user can zoom out - prevents the user from zooming out too far and showing tiled maps
    minZoom: 7,
    //Copyright information on the map
    attribution: 'Map data &copy; <a href="http://www.marine.ie/">Marine Institute</a>'
}).addTo(map);



