﻿//Const declaration
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
    }
}

function displayValue(intValue, id, strText) {
    let nbrDisplay;

    if (intValue > INTOCTILLION - 1) {
        nbrDisplay = (intValue / INTOCTILLION).toFixed(2) + 'o'
    } else if (intValue > INTSEPTILLION - 1) {
        nbrDisplay = (intValue / INTSEPTILLION).toFixed(2) + 'S';
    } else if (intValue > INTSEXTILLION - 1) {
        nbrDisplay = (intValue / INTSEXTILLION).toFixed(2) + 's';
    } else if (intValue > INTQUINTILLION - 1) {
        nbrDisplay = (intValue / INTQUINTILLION).toFixed(2) + 'Q';
    } else if (intValue > INTQUATUORILLION - 1) {
        nbrDisplay = (intValue / INTQUATUORILLION).toFixed(2) + 'q';
    } else if (intValue > INTTRILLION - 1) {
        nbrDisplay = (intValue / INTTRILLION).toFixed(2) + 'T';
    } else if (intValue > INTBILLION - 1) {
        nbrDisplay = (intValue / INTBILLION).toFixed(2) + 'B';
    } else if (intValue > INTMILLION - 1) {
        nbrDisplay = (intValue / INTMILLION).toFixed(2) + 'M';
    } else if (intValue > INTMILLE - 1) {
        nbrDisplay = (intValue / INTMILLE).toFixed(2) + 'K';
    } else {
        nbrDisplay = intValue.toFixed(2)
    }
    document.getElementById(id).innerText = strText + nbrDisplay
}