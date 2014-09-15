(function() {
    var updateP = function(id, response) {
      document.getElementById(id).innerHTML = response;
    };

    var goapi_xhr = new XMLHttpRequest();
    var vnextapi_xhr = new XMLHttpRequest();
    
    goapi_xhr.open('GET', 'http://localhost:10010/');
    vnextapi_xhr.open('GET', 'http://localhost:10020/');
    
    goapi_xhr.onload = function(e) { updateP("goapi-status", e.target.response); }
    vnextapi_xhr.onload = function(e) { updateP("vnextapi-status", e.target.response); }
    
    goapi_xhr.send();
    vnextapi_xhr.send();
}());
