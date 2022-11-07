var map;
function initMap() {
    var markers = [
        ['185', 41.333819, 69.245275],

    ];
    var infoWindowContent = [
        ['<div id=\"infowindows\"><div id=\"wraps\"><span class=\"titles\">Chilonzor tumani | ATS-276</span><ul><li><strong>Manzil: </strong><span>Chilonzor tumani, m-v Chilonzor-C, Kichik Xalqa yo\'li ko\'chasi, 8-a uy.</span></li><li><strong>Ish tartibi: </strong><span>Dushanba-Shanba,  09:00 dan 18:00 gacha</span></li></ul>'],
    ];

    map = L.map('map', {
        center: [41.333819, 69.245275],
        zoom: 14,
        scrollWheelZoom: false
    });
    var myIcon = L.icon({
        iconUrl: 'https://uztelecom.uz/images/marker-pin.png',
        iconSize: [40, 57],
        popupAnchor: [0, -30]
    });
    for (i = 0; i < markers.length; i++) {
        var position = L.latLng(markers[i][1], markers[i][2]);
        L.marker(position, { icon: myIcon }).addTo(map)/* .bindPopup(infoWindowContent[i][0]) */;
    }
    var tiles = new L.tileLayer('https://map.uztelecom.uz/hot/{z}/{x}/{y}.png', {
        attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>',
    }).addTo(map);
    // var tiles = new L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);
}

initMap();
