//Helper functions for navigation
function NavigateTo(location) {
	window.location.href = (window.location.origin + location);
}

function NavigateToExtern(location) {
	window.location.href = "https://" + location;
}
//Datatypes for transmissions
let DataRequest = {
    TypeName: "",
}
//Ich geb auf mit Namensgebung
let NewDataRequest = {
    TypeName: "",
    Data: ""
}
let SensorData = {
    Bezeichnung: "",
    Hersteller: "",
    Herstellernummer: "",
    SensorID: "",
    PhysID: "",
    Seriennummer: "",
    StandortID: ""
}
let Measurements = {
    MessungsID: "",
    PhysID: "",
    SensorID: "",
    Wert: "",
    Zeitpunkt: ""
}
let LocationData = {
    Bezeichnung: "",
    KoordinateX: "",
    KoordinateY: "",
    StandortID: ""
}
let UnitData = {
    Einheit: "",
    Zeichen: "",
    Name: "",
    PhysID: "",
}
let RemoveRequest = {
    ID: "",
    Table: "",
    VariableName: "",
}
let StandartResponse = {
    Status: "",
    Message: ""
}

//Send a request to the server to remove the specified datapoint
function RemoveData(tableName, id, varName) {
    var Remrequest = RemoveRequest;
    Remrequest.Table = tableName;
    Remrequest.ID = id;
    console.log(tableName);
    Remrequest.VariableName = varName;
    console.log(JSON.stringify(Remrequest));
    fetch(window.location.origin + '/GivePLZ/V1/removedata', {
        method: "POST",
        body: JSON.stringify(Remrequest),
    }).then( () => { GetData(tableName) });
}

// Send new datapoints to the server
function SendData(type) {
    var childs = document.getElementsByClassName("addInput");
    switch (type) {
        case "Units":
            var obj = UnitData;
            obj.Name = childs[0].value;
            obj.Einheit = childs[1].value;
            obj.Zeichen = childs[2].value;
            obj.PhysID = childs[3].value;

            var req = NewDataRequest;
            req.TypeName = type;
            req.Data = JSON.stringify(obj);
            fetch(window.location.origin + "/GivePLZ/V1/newData", {
                method: "POST",
                body: JSON.stringify(req),
            }).then( () => { GetData(type); });
        break;
        case "Locations":
            var obj = LocationData;
            obj.Bezeichnung = childs[0].value;
            obj.KoordinateX = childs[1].value;
            obj.KoordinateY = childs[2].value;
            obj.StandortID = childs[3].value;

            var req = NewDataRequest;
            req.TypeName = type;
            req.Data = JSON.stringify(obj);
            fetch(window.location.origin + "/GivePLZ/V1/newData", {
                method: "POST",
                body: JSON.stringify(req),
            }).then( () => { GetData(type) });
        break;
        case "Sensors":
            var obj = LocationData;
            obj.Bezeichnung = childs[0].value;
            obj.Hersteller = childs[1].value;
            obj.Herstellernummer = childs[2].value;
            obj.SensorID = childs[3].value;
            obj.Seriennummer = childs[4].value;
            obj.StandortID = childs[5].value;

            var req = NewDataRequest;
            req.TypeName = type;
            req.Data = JSON.stringify(obj);
            fetch(window.location.origin + "/GivePLZ/V1/newData", {
                method: "POST",
                body: JSON.stringify(req),
            }).then( () => { GetData(type) });
        break;
        case "Measurements":
            var obj = LocationData;
            obj.MessungsID = childs[0].value;
            obj.PhysID = childs[1].value;
            obj.SensorID = childs[2].value;
            obj.Wert = childs[3].value;

            var req = NewDataRequest;
            req.TypeName = type;
            req.Data = JSON.stringify(obj);
            fetch(window.location.origin + "/GivePLZ/V1/newData", {
                method: "POST",
                body: JSON.stringify(req),
            }).then( () => { GetData(type) });
        break;
        default:
            console.log("Can't request data");
        break;
    }
}

//Get Data from server
function GetData(type) {
    var request = DataRequest;
    request.TypeName = type;
    fetch(window.location.origin + '/GivePLZ/V1/getdata', {
        method: "POST",
        body: JSON.stringify(request),
    }).then(data => data.json())
    .then(data => {
        switch (type) {
            case "Units":
                ParseUnitData(data);
            break;
            case "Locations":
                ParseLocationData(data);
            break;
            case "Sensors":
                ParseSensorData(data);
            break;
            case "Measurements":
                ParseMeasurementData(data);
            break;
            default:
                console.log("Can't request data");
            break;
        }
    });
}

//Parse incoming data to a HTML Table
//All functions are basically the same
function ParseMeasurementData(data) {
    fetch("/Common/Tables/Measurements.txt").then( table => table.text() )
    .then( table => {
        var inject = table;
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var element = document.getElementById("content");
            inject += "<tr class='dataBody'>";
            inject += `<td>${obj.MessungsID}</td>\n`;
            inject += `<td> ${obj.PhysID}</td>\n`;
            inject += `<td> ${obj.SensorID}</td>\n`;
            inject += `<td> ${obj.Wert}</td>\n`;
            inject += `<td>${obj.Zeitpunkt}</td>\n`;
            inject += `<td><button onclick="RemoveData('Measurements', '${obj.MessungsID}', 'MessungsID')">Remove</button></td>`;
            inject += "</tr>";
        }
        element.innerHTML = inject;
        document.getElementById("loadgifcontainer").setAttribute("hidden", "true");
        document.getElementById("content").attributes.removeNamedItem("hidden");
    });
}

function ParseSensorData(data) {
    fetch("/Common/Tables/Sensors.txt").then( table => table.text() )
    .then( table => {
        var inject = table;
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var element = document.getElementById("content");
            inject += "<tr class='dataBody'>";
            inject += `<td>${obj.Bezeichnung}</td>\n`;
            inject += `<td> ${obj.Hersteller}</td>\n`;
            inject += `<td> ${obj.Herstellernummer}</td>\n`;
            inject += `<td> ${obj.SensorID}</td>\n`;
            inject += `<td>${obj.Seriennummer}</td>\n`;
            inject += `<td> ${obj.StandortID}</td>\n`;
            inject += `<td><button onclick="RemoveData('Sensors', '${obj.SensorID}', 'SensorID')">Remove</button></td>`;
            inject += "</tr>";
        }
        element.innerHTML = inject;
        document.getElementById("loadgifcontainer").setAttribute("hidden", "true");
        document.getElementById("content").attributes.removeNamedItem("hidden");
    });
}

function ParseLocationData(data) {
    fetch("/Common/Tables/Locations.txt").then( table => table.text() )
    .then( table => {
        var inject = table;
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var element = document.getElementById("content");
            inject += "<tr class='dataBody'>";
            inject += `<td>${obj.Bezeichnung}</td>\n`;
            inject += `<td> ${obj.KoordinateX}</td>\n`;
            inject += `<td>${obj.KoordinateY}</td>\n`;
            inject += `<td> ${obj.StandortID}</td>\n`;
            inject += `<td><button onclick="RemoveData('Locations', '${obj.StandortID}', 'StandortID')">Remove</button></td>`;
            inject += "</tr>";
        }
        element.innerHTML = inject;
        document.getElementById("loadgifcontainer").setAttribute("hidden", "true");
        document.getElementById("content").attributes.removeNamedItem("hidden");
    });
}

function ParseUnitData(data) {
    fetch("/Common/Tables/Units.txt").then( table => table.text() )
    .then( table => {
        var inject = table;
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var element = document.getElementById("content");
            inject += "<tr class='dataBody'>";
            inject += `<td>${obj.Name}</td>\n`;
            inject += `<td> ${obj.Einheit}</td>\n`;
            inject += `<td>${obj.Zeichen}</td>\n`;
            inject += `<td> ${obj.PhysID}</td>\n`;
            inject += `<td><button onclick="RemoveData('Units', '${obj.PhysID}', 'PhysID')">Remove</button></td>`;
            inject += "</tr>";
        }
        element.innerHTML = inject;
        document.getElementById("loadgifcontainer").setAttribute("hidden", "true");
        document.getElementById("content").attributes.removeNamedItem("hidden");
    });
}