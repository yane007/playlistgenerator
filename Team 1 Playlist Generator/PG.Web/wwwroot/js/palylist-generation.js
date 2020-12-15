﻿

$('input[type=range]').wrap("<div class='range'></div>");
var i = 1;

$('.range').each(function () {
    var n = this.getElementsByTagName('input')[0].value;
    var x = (n / 100) * (this.getElementsByTagName('input')[0].offsetWidth - 8) - 12;
    this.id = 'range' + i;
    if (this.getElementsByTagName('input')[0].value == 0) {
        this.className = "range"
    } else {
        this.className = "range rangeM"
    }
    this.innerHTML += "<style>#" + this.id + " input[type=range]::-webkit-slider-runnable-track {background:linear-gradient(to right, #b1872d 0%, #b1872d " + n + "%, #515151 " + n + "%)} #" + this.id + ":hover input[type=range]:before{content:'" + n + "'!important;left: " + x + "px;} #" + this.id + ":hover input[type=range]:after{left: " + x + "px}</style>";
    i++
});

$('input[type=range]').on("input", function () {
    var a = this.value;
    var p = (a / 100) * (this.offsetWidth - 8) - 12;
    if (a == 0) {
        this.parentNode.className = "range"
    } else {
        this.parentNode.className = "range rangeM"
    }
    this.parentNode.getElementsByTagName('style')[0].innerHTML += "#" + this.parentNode.id + " input[type=range]::-webkit-slider-runnable-track {background:linear-gradient(to right, #b1872d 0%, #b1872d " + a + "%, #515151 " + a + "%)} #" + this.parentNode.id + ":hover input[type=range]:before{content:'" + a + "'!important;left: " + p + "px;} #" + this.parentNode.id + ":hover input[type=range]:after{left: " + p + "px}";
})



var searchInput = 'search_input';
var searchInput2 = 'search_input2';

$(document).ready(function () {
    var autocomplete;
    autocomplete = new google.maps.places.Autocomplete((document.getElementById(searchInput)), {
        types: ['geocode'],
    });

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        var near_place = autocomplete.getPlace();
    });
});


$(document).ready(function () {
    var autocomplete;
    autocomplete = new google.maps.places.Autocomplete((document.getElementById(searchInput2)), {
        types: ['geocode'],
    });

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        var near_place = autocomplete.getPlace();
    });
});