
//*****FLASH CONSTRUCTOR******//
var flashConstructor = function (serverName, gameId) {
    var flashvars = {};
    flashvars.gameInstanceID = gameId;
    flashvars.initialParametersAddress = "http://" + serverName + ":2570/GetInitialParameters.aspx";
    flashvars.history = 'False';
    flashvars.historyNo = 0;
    flashvars.currencyCode = 'EUR';
    var params = {};
    params.allowFullScreen = "true";
    params.wmode = "direct";
    params.allowScriptAccess = "always";
    var attributes = {};
    swfobject.embedSWF(
        "http://" + serverName + ":2570/FileLoad.aspx?fileName=wrapper-default.swf", "flashGameContent",
        "800", "600", "8.0.0", "", flashvars, params, attributes);
};

//***********NOTIFY STATE**********//
var QueueState = [];

var NotifyState = function (state) {
    QueueState.push(state);
    console.log('%cState: ' + state, 'background: #222; color: #bada55');
};

var ShiftQueueState = function() {
    if (QueueState.length > 0) {
        var state = QueueState.shift().toString();
        if (QueueState.length == 0) {
            QueueState.push(state);
        }
        return state;
    }
    return "NoState";
};

var GetState = function () {
    if (QueueState.length > 0) {
        return QueueState[QueueState.length-1].toString();
    }
    return "NoState";
};

//**********PRESS BUTTON***********//
var PressButton = function(buttonName) {
    document.getElementById("flashGameContent").PressButton(buttonName);
};

//******DEBUG SET BET REQUEST******//
var RequestDebugServerResponse = function (request) {
    document.getElementById("flashGameContent").RequestDebugServerResponse(request);
};

//***********GET LINE****************//
var line;

var GetLine = function () {
    document.getElementById("flashGameContent").GetLine();
    return line.toString();
};

var GetLineResult = function (lineResult) {
    line = lineResult;
    console.log(lineResult);
};

//***********GET LINE LIST************//
var lineList;

var GetLineList = function () {
    document.getElementById("flashGameContent").GetLineList();
    return lineList.toString();
};

var GetLineListResult = function (lineListResult) {
    lineList = lineListResult;
    console.log(lineListResult);
};

//*********GET BET*****************//
var bet;

var GetBet = function () {
    document.getElementById("flashGameContent").GetBet();
    return bet.toString();
};

var GetBetResult = function (betResult) {
    bet = betResult;
    console.log(betResult);
};

//*********GET BET LIST************//
var betList;

var GetBetList = function () {
    document.getElementById("flashGameContent").GetBetList();
    return betList.toString();
};

var GetBetListResult = function (betListResult) {
    betList = betListResult;
    console.log(betListResult);
};

//********GET BALANCE***********//
var balance;

var GetBalance = function () {
    document.getElementById("flashGameContent").GetBalance();
    return balance.toString();
};

var GetBalanceResult = function (balanceResult) {
    balance = balanceResult;
    console.log(balanceResult);
};

//*********NOTIFY MESSAGE*********//
var QueueMessage = [];

var NotifyMessage = function (message) {
    QueueMessage.push(message);
    console.log('%cMessage: ' + message, 'background: #bada55; color: #222');
}; 

var ShiftQueueMessage = function () {
    if (QueueMessage.length > 0) {
        var message = QueueMessage.shift().toString();
        if (QueueMessage.length == 0) {
            QueueMessage.push(message);
        }
        return message;
    }
    return "NoMessage";
};