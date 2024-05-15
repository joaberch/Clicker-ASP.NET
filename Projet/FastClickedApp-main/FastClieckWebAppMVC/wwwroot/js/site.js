//Const declaration
const INTNbrAfterComma = 2
const INTMILLE = 1000
const INTMILLION = 1000000
const INTBILLION = 1000000000
const INTTRILLION = 1000000000000
const INTQUATUORILLION = 1000000000000000
const INTQUINTILLION = 1000000000000000000
const INTSEXTILLION = 1000000000000000000000
const INTSEPTILLION = 1000000000000000000000000
const INTOCTILLION = 1000000000000000000000000000

//var declaration
let intNbrClick = 0
let intGains = 1
let intRestart = 1

let strUsername = prompt("your username : ")
connectToDB();

///Connect to the database
function connectToDB() {
    // Call a C# function to insert the player name in the database
    $.ajax({
        type: "POST",
        url: "Home/InsertPlayerScore",
        data: {
            playerName: strUsername,
        }
    });
    document.getElementById("currentUser").textContent = strUsername
}

///When the player click the image
function clicked() {
    intNbrClick += intGains * intRestart
    displayValue(intNbrClick, "nbrClick", "")
}

///When the player click the button to get more click
function moreClick(number) {
    //If he has clicked the spend all button
    if (number == 2) {
        intGains += (intNbrClick / 15)
        intNbrClick = 0
        displayValue(intNbrClick, "nbrClick", "")
        displayValue(intGains, "gains", "gains : ")
    } else if (intNbrClick > number * 10 - 1) { //else
        intGains += number
        intNbrClick -= number * 10
        displayValue(intNbrClick, "nbrClick", "")
        displayValue(intGains, "gains", "gains : ")
    }
}

///When the player click the restart button
function moreRestart() {
    if (intNbrClick > (INTQUATUORILLION * 100) - 1) {
        intRestart += (intNbrClick / (INTQUATUORILLION * 100))
        intNbrClick = 0
        intGains = 1
        displayValue(intNbrClick, "nbrClick", "")
        displayValue(intGains, "gains", "gains : ")
        displayValue(intRestart, "restart", "restart : ")

        connectToDB();
    }
}

///display the number with ergonomy
function displayValue(intValue, id, strText) {
    let nbrDisplay;

    if (intValue > INTOCTILLION - 1) {
        nbrDisplay = (intValue / INTOCTILLION).toFixed(INTNbrAfterComma) + 'o'
    } else if (intValue > INTSEPTILLION - 1) {
        nbrDisplay = (intValue / INTSEPTILLION).toFixed(INTNbrAfterComma) + 'S';
    } else if (intValue > INTSEXTILLION - 1) {
        nbrDisplay = (intValue / INTSEXTILLION).toFixed(INTNbrAfterComma) + 's';
    } else if (intValue > INTQUINTILLION - 1) {
        nbrDisplay = (intValue / INTQUINTILLION).toFixed(INTNbrAfterComma) + 'Q';
    } else if (intValue > INTQUATUORILLION - 1) {
        nbrDisplay = (intValue / INTQUATUORILLION).toFixed(INTNbrAfterComma) + 'q';
    } else if (intValue > INTTRILLION - 1) {
        nbrDisplay = (intValue / INTTRILLION).toFixed(INTNbrAfterComma) + 'T';
    } else if (intValue > INTBILLION - 1) {
        nbrDisplay = (intValue / INTBILLION).toFixed(INTNbrAfterComma) + 'B';
    } else if (intValue > INTMILLION - 1) {
        nbrDisplay = (intValue / INTMILLION).toFixed(INTNbrAfterComma) + 'M';
    } else if (intValue > INTMILLE - 1) {
        nbrDisplay = (intValue / INTMILLE).toFixed(INTNbrAfterComma) + 'K';
    } else {
        nbrDisplay = intValue.toFixed(INTNbrAfterComma)
    }
    document.getElementById(id).textContent = strText + nbrDisplay
}