var c;
var tr;
function moreFun() {
    c = document.querySelector("#more");
    if (c != null) {
        tr = c.parentNode;
        createNode();
    }



    else {
        c = document.querySelector("#less");
        tr = c.parentNode;
        removeNode();
    }

}



function removeNode() {
    var child = tr.parentNode.children;
    var tbody = tr.parentNode;

    var length = child.length;
    for (i = length - 3; i > length - 6; i--) {
        child[i].parentNode.removeChild(child[i]);
    }

    changeLess();
}



function createNode() {
    var n1;
    var n2;
    for (i = 2; i <= 4; i++) {
        n1 = document.createElement("tr")
        n2 = document.createElement("td")
        var txt = document.createTextNode("0" + i + ":00 PM");
        n2.appendChild(txt);
        n1.appendChild(n2);
        tr.parentNode.insertBefore(n1, tr);
    }



    changeMore();
}



function changeMore() {
    c.innerHTML = "less <i class='fa-solid fa-arrow-up'></i>";
    c.setAttribute("id", "less")
}



function changeLess() {
    c.innerHTML = "more <i class='fa-solid fa-arrow-down'></i>";
    c.setAttribute("id", "more")
}