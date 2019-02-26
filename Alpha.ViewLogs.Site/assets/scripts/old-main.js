/*globals document: false, $: false, top: false*/

function slideToggle(id) {
    var obj = null, rowIndex, p, table;

    if (typeof id === 'object') {
        rowIndex = id.rowIndex;
        p = id.parentNode || id.parentElement;
        table = p.parentNode || p.parentElement;
        var cell = table.rows[rowIndex + 1].cells[0];
        obj = cell.firstElementChild ? cell.firstElementChild : cell.firstChild;
    } else {
        obj = document.getElementById(id);
    }

    if (obj !== null) {
        if (obj.style.display === 'none') {
            obj.style.display = '';
        } else {
            obj.style.display = 'none';
        }
    }
}

function OpenIPInfo(ip) {
    var url = $('#viewIPInfoPath').val() + '?ip=' + ip;
    top.popupManager.open(url, '', 400, 300);
}