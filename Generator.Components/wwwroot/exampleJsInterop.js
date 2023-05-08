// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function showPrompt(message) {
  return prompt(message, 'Type anything here');
}


 
export function changeRowStyle(pointerEventsValue) {
    var table = document.querySelector(".mud-table-root");
    var rows = table.getElementsByClassName("mud-table-row");
    for (var i = 0; i < rows.length; i++) {
        var columns = rows[i].getElementsByTagName("td");
        for (var j = 1; j < columns.length - 1; j++) {
            columns[j].style.pointerEvents = pointerEventsValue;
        }
    }
}

