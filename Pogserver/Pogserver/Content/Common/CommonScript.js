//DataTypes for Requests
var _ShutdownRequestData = {
    Token: ""
}
var _ShutdownRequestResponse = {
    RequestHandled: 0
}
//Functions
function NavigateTo(location) {
	window.location.href = (window.location.origin + location);
}