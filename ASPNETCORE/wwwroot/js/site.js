// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Code
bool boolConnectedToDB = false

let xhttp = new XMLHttpRequest();
let url = 'https://mongodb://localhost:27017/NomDeVotreBaseDeDonnees';

xhttp.onreadystatechange = function () {
    if (this.readyState == 4 && this.status == 200) {
        console.log(this.responseText);
    }
}

let nbrClick = 0
let gains = 1
function clicked() {
    nbrClick += gains;
    document.getElementById('nbrClick').innerText = nbrClick
}
function moreClick(number) {
    if (nbrClick > number * 10 - 1) {
        gains += number
        nbrClick -= number * 10
        document.getElementById('nbrClick').innerText = nbrClick
        document.getElementById('gains').innerText = 'gains : ' + gains
    }
}
