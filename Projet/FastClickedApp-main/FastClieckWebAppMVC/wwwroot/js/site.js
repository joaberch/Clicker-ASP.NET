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

let intNbrClick = 0
let intGains = 1
let intRestart = 1

let strUsername = prompt("your username : ")
connectToDB();

function connectToDB() {

    // Appel d'une fonction c# pour insérer le score du joueur 
    $.ajax({
        type: "POST",
        url: "Home/InsertPlayerScore",
        data: {
            playerName: strUsername,
        }
    });
    document.getElementById("currentUser").textContent = strUsername
}

function clicked() {
    intNbrClick += intGains * intRestart
    displayValue(intNbrClick, "nbrClick", "")
}

function moreClick(number) {
    if (number == 2) {
        intGains += (intNbrClick / 15)
        intNbrClick = 0
        displayValue(intNbrClick, "nbrClick", "")
        displayValue(intGains, "gains", "gains : ")
    } else if (intNbrClick > number * 10 - 1) {
        intGains += number
        intNbrClick -= number * 10
        displayValue(intNbrClick, "nbrClick", "")
        displayValue(intGains, "gains", "gains : ")
    }
}

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

//TODO - get the nbrRestart in the database so the player can get his value
//TODO - increment the nbr of restart in the database