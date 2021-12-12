//DataTypes for Requests
var _ShutdownRequestData = {
    Token: ""
}
var _ShutdownRequestResponse = {
    RequestHandled: 0
}
let DataRequest = {
    TypeName: "",
}
let UnitDataResponse = {
    Einheit: "",
    Character: "",
    Name: "",
    PhysID: "",
}
let RemoveRequest = {
    ID: "",
}
//Functions
function NavigateTo(location) {
	window.location.href = (window.location.origin + location);
}

function NavigateToExtern(location) {
	window.location.href = "https://" + location;
}

function AddNewUnitData() {

}

function RemoveUnitData(PhysID) {
    var request = RemoveRequest;
    request.ID = PhysID;
    fetch(window.location.origin + '/GivePLZ/V1/removedata'), {
        method: "POST",
        body: JSON.stringify(request),
    }. then(GetUnitData())
}

function GetUnitData() {
    var request = DataRequest;
    request.TypeName = "Units";
    console.log(JSON.stringify(request));
    fetch(window.location.origin + '/GivePLZ/V1/getdata', {
        method: "POST",
        body: JSON.stringify(request),
    }).then(data => data.json())
    .then(data => {
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var element = document.getElementById("content");
            var inject = "";
            inject += "<tr class='dataBody'>";
            inject += ("<td>" + obj.Name + "</td>\n");
            inject += ("<td>" + obj.Einheit + "</td>\n");
            inject += ("<td>" + obj.Character + "</td>\n");
            inject += ("<td>" + obj.PhysID + "</td>\n");
            inject += "<td><button onClick=" + "NavigateTo('')" + ">Remove</button></td>";
            inject += "</tr>";
            element.innerHTML += inject;
        }
        request = "";
        data = "";
    });
}