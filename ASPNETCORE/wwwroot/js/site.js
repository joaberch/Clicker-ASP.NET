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
	let nbrDisplay;

	if (nbrClick > 999999999999999) {
		nbrDisplay = (nbrClick / 1000000000000000).toFixed(2) + 'Q';
	} else if (nbrClick > 999999999999) {
		nbrDisplay = (nbrClick / 1000000000000).toFixed(2) + 'T';
	} else if (nbrClick > 999999999) {
		nbrDisplay = (nbrClick / 1000000000).toFixed(2) + 'B';
	} else if (nbrClick > 999999) {
		nbrDisplay = (nbrClick / 1000000).toFixed(2) + 'M';
	} else if (nbrClick > 999) {
		nbrDisplay = (nbrClick / 1000).toFixed(2) + 'K';
	} else {
		nbrDisplay = nbrClick.toString();
	}

	document.getElementById('nbrClick').innerText = nbrDisplay
}
function moreClick(number) {
	if (nbrClick > number * 10 - 1) {
		gains += number
		nbrClick -= number * 10
		document.getElementById('nbrClick').innerText = nbrClick
		document.getElementById('gains').innerText = 'gains : ' + gains
	}
}
