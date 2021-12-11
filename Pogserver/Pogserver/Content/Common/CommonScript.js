//DataTypes for Requests
var _ShutdownRequestData = {
    Token: ""
}
var _ShutdownRequestResponse = {
    RequestHandled: 0
}
var _DataRequest = {
    TypeName: "",
}
var _UnitData = {
    Name: "",
    Einheit: "",
    Character: "",
    PhysID: ""
}
//Functions
function NavigateTo(location) {
	window.location.href = (window.location.origin + location);
}

function GetUnitData() {
    _DataRequest.TypeName = "Units";
    fetch('http://localhost:8000/GivePLZ/V1/getdata', {
        method: "POST",
        body: JSON.stringify(_DataRequest),
    }).then(data => { console.log(data.json()) });
}