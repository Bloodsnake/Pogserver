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
    Character: "",
    Name: "",
    PhysID: "",
}
let RemoveRequest = {
    ID: "",
    Table: "",
    VariableName: "",
}
//Functions
function NavigateTo(location) {
	window.location.href = (window.location.origin + location);
}

function NavigateToExtern(location) {
	window.location.href = "https://" + location;
}

function RemoveData(tableName, id, varName) {
    var Remrequest = RemoveRequest;
    Remrequest.ID = id;
    Remrequest.VariableName = varName;
    Remrequest.Table = tableName;
    console.log(JSON.stringify(Remrequest));
    fetch(window.location.origin + '/GivePLZ/V1/removedata', {
        method: "POST",
        body: JSON.stringify(Remrequest),
    });
    window.location.reload();
}

function SendData(type) {
    var childs = document.getElementsByClassName("addInput");
    switch (type) {
        case "Units":
            var obj = UnitData;
            console.log(childs.length);
            obj.Name = childs[0].value;
            obj.Einheit = childs[1].value;
            obj.Character = childs[2].value;
            obj.PhysID = childs[3].value;
            console.log(obj);
            var req = NewDataRequest;
            req.TypeName = type;
            req.Data = JSON.stringify(obj);
            fetch(window.location.origin + "/GivePLZ/V1/newData", {
                method: "POST",
                body: JSON.stringify(req),
            })
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
}

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

function ParseMeasurementData(data) {
    fetch("/Common/Tables/Measurements.txt").then( table => table.text() )
    .then( table => {
        var inject = table;
        console.log(inject);
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var element = document.getElementById("content");
            inject += "<tr class='dataBody'>";
            inject += `<td>${obj.MessungsID}</td>\n`;
            inject += `<td> ${obj.PhysID}</td>\n`;
            inject += `<td> ${obj.SensorID}</td>\n`;
            inject += `<td> ${obj.Wert}</td>\n`;
            inject += `<td>${obj.Zeitpunkt}</td>\n`;
            inject += `<td><button onclick="RemoveData('locations', '${obj.MessungsID}', 'StandortID')">Remove</button></td>`;
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
        console.log(inject);
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
            inject += `<td><button onclick="RemoveData('locations', '${obj.SensorID}', 'StandortID')">Remove</button></td>`;
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
        console.log(inject);
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var element = document.getElementById("content");
            inject += "<tr class='dataBody'>";
            inject += `<td>${obj.Bezeichnung}</td>\n`;
            inject += `<td> ${obj.KoordinateX}</td>\n`;
            inject += `<td>${obj.KoordinateY}</td>\n`;
            inject += `<td> ${obj.StandortID}</td>\n`;
            inject += `<td><button onclick="RemoveData('locations', '${obj.StandortID}', 'StandortID')">Remove</button></td>`;
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
        console.log(inject);
        for (var i = 0; i < data.length; i++) {
            var obj = data[i];
            var element = document.getElementById("content");
            inject += "<tr class='dataBody'>";
            inject += `<td>${obj.Name}</td>\n`;
            inject += `<td> ${obj.Einheit}</td>\n`;
            inject += `<td>${obj.Character}</td>\n`;
            inject += `<td> ${obj.PhysID}</td>\n`;
            inject += `<td><button onclick="RemoveData('units', '${obj.PhysID}', 'StandortID')">Remove</button></td>`;
            inject += "</tr>";
        }
        element.innerHTML = inject;
        document.getElementById("loadgifcontainer").setAttribute("hidden", "true");
        document.getElementById("content").attributes.removeNamedItem("hidden");
    });
}