(function() {
    var xhr1 = new XMLHttpRequest();
    var xhr2 = new XMLHttpRequest();
    
    xhr1.open('GET', 'http://localhost:10010/');
    xhr2.open('GET', 'http://localhost:10020/');
    
    xhr1.onload = function(e) { console.log(e); }
    xhr2.onload = function(e) { console.log(e); }
    
    xhr1.send();
    xhr2.send();
}());
