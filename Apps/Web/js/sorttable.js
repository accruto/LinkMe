addEvent(window, "load", tables_init);

var SORT_COLUMN_INDEX;

function tables_init() {
    // Find all tables with class sortable and make them sortable
    if (!document.getElementsByTagName) return;
    tbls = document.getElementsByTagName("table");
    for (ti=0;ti<tbls.length;ti++) {
        thisTbl = tbls[ti];
        
        // make the table sortable if has sortable class
        if (((' '+thisTbl.className+' ').indexOf("sortable") != -1) && (thisTbl.id)) {
            //initTable(thisTbl.id);
            ts_makeSortable(thisTbl);
        }
				setOddEvenRowClassesOnTable(thisTbl);
    }
}

function ts_makeSortable(table) {
    if (table.rows && table.rows.length > 0) {
        var firstRow = table.rows[0];
    }
    if (!firstRow) return;
    // need to determine which cols are sortable
    for(var indx=0; indx<table.rows.length; indx++)
    {
		var childTDs = thisTbl.rows[indx].getElementsByTagName("TD");
		var arrSortCols = new Array(childTDs.length);
		for(var j=0; j<childTDs.length; j++)
		{
			var tdSortable = childTDs[j].getAttribute('sortable');
			if(tdSortable == null || tdSortable == 'true') {
				arrSortCols[j] = true;
				var cell = childTDs[j];
				var txt = ts_getInnerText(cell);
				cell.innerHTML = '<a href="#" class="tableheader" onclick="ts_resortTable(this);return false;">'+txt+'<span class="sortarrow"></span></a>';				
			} else {
				arrSortCols[j] = false;
			}
		}
    }
}

function ts_getInnerText(el) {
	if (typeof el == "string") return el;
	if (typeof el == "undefined") { return el };
	if (el.innerText) return el.innerText;	//Not needed but it is faster
	var str = "";
	
	var cs = el.childNodes;
	var l = cs.length;
	for (var i = 0; i < l; i++) {
		switch (cs[i].nodeType) {
			case 1: //ELEMENT_NODE
				str += ts_getInnerText(cs[i]);
				break;
			case 3:	//TEXT_NODE
				str += cs[i].nodeValue;
				break;
		}
	}
	return str;
}


function ts_getSortFunction(table, iRow, iColumn) {
	// dont go off end of table rows
	if(iRow >= table.rows.length) {
		return null;
	}
	var sortfn = null; 
	var itm = ts_getInnerText(table.rows[iRow].cells[iColumn]);
	if(itm == null || itm == "") {
		return ts_getSortFunction(table, iRow+1, iColumn);
	}
    if (itm.match(/^\d\d[\/-]\d\d[\/-]\d\d\d\d$/)) sortfn = ts_sort_date;
    if (itm.match(/^\d\d[\/-]\d\d[\/-]\d\d$/)) sortfn = ts_sort_date;
    if (itm.match(/^[£$]/)) sortfn = ts_sort_currency;
    if (itm.match(/^[\d\.]+$/)) sortfn = ts_sort_numeric;
    return sortfn;
}

function ts_resortTable(lnk) {
    // get the span
    var span;
    for (var ci=0;ci<lnk.childNodes.length;ci++) {
        if (lnk.childNodes[ci].tagName && lnk.childNodes[ci].tagName.toLowerCase() == 'span') span = lnk.childNodes[ci];
    }
    var spantext = ts_getInnerText(span);
    var td = lnk.parentNode;
    var column = td.cellIndex;
    var table = getParent(td,'TABLE');
    
    // try and get the seperate table containing the data
    var tableData = document.getElementById(table.id + "Data");
    var startRow = 0;
    if(tableData) {
		// if there is no seperate table data, use the current table, increase startRow to 1 to ignore header row
		startRow = 1;
		table = tableData;
	}
    
    // Work out a type for the column
    if (table.rows.length <= 1) return;
    
	sortfn = ts_getSortFunction(table, startRow, column);
	if(!sortfn) {
		sortfn = ts_sort_caseinsensitive;
	}
    
    SORT_COLUMN_INDEX = column;
    var firstRow = new Array();
    var newRows = new Array();
    for (i=0;i<table.rows[0].length;i++) { firstRow[i] = table.rows[0][i]; }
    for (j=1;j<table.rows.length;j++) { newRows[j-1] = table.rows[j]; }

    newRows.sort(sortfn);

    if (span.getAttribute("sortdir") == 'down') {
        ARROW = '&nbsp;&nbsp;&uarr;';
        newRows.reverse();
        span.setAttribute('sortdir','up');
    } else {
        ARROW = '&nbsp;&nbsp;&darr;';
        span.setAttribute('sortdir','down');
    }
    
    // We appendChild rows that already exist to the tbody, so it moves them rather than creating new ones
    // don't do sortbottom rows
    for (i=0;i<newRows.length;i++) { if (!newRows[i].className || (newRows[i].className && (newRows[i].className.indexOf('sortbottom') == -1))) table.tBodies[0].appendChild(newRows[i]);}
    // do sortbottom rows only
    for (i=0;i<newRows.length;i++) { if (newRows[i].className && (newRows[i].className.indexOf('sortbottom') != -1)) table.tBodies[0].appendChild(newRows[i]);}
    
    // Delete any other arrows there may be showing
    var allspans = document.getElementsByTagName("span");
    for (var ci=0;ci<allspans.length;ci++) {
        if (allspans[ci].className == 'sortarrow') {
            if (getParent(allspans[ci],"table") == getParent(lnk,"table")) { // in the same table as us?
                allspans[ci].innerHTML = '';
            }
        }
    }
        
    span.innerHTML = ARROW;
    
    setOddEvenRowClassesOnTable(table);
}

function getParent(el, pTagName) {
	if (el == null) return null;
	else if (el.nodeType == 1 && el.tagName.toLowerCase() == pTagName.toLowerCase())	// Gecko bug, supposed to be uppercase
		return el;
	else
		return getParent(el.parentNode, pTagName);
}
function ts_sort_date(a,b) {
    // y2k notes: two digit years less than 50 are treated as 20XX, greater than 50 are treated as 19XX
    aa = ts_getInnerText(a.cells[SORT_COLUMN_INDEX]);
    bb = ts_getInnerText(b.cells[SORT_COLUMN_INDEX]);
    if (aa.length == 10) {
        dt1 = aa.substr(6,4)+aa.substr(3,2)+aa.substr(0,2);
    } else {
        yr = aa.substr(6,2);
        if (parseInt(yr) < 50) { yr = '20'+yr; } else { yr = '19'+yr; }
        dt1 = yr+aa.substr(3,2)+aa.substr(0,2);
    }
    if (bb.length == 10) {
        dt2 = bb.substr(6,4)+bb.substr(3,2)+bb.substr(0,2);
    } else {
        yr = bb.substr(6,2);
        if (parseInt(yr) < 50) { yr = '20'+yr; } else { yr = '19'+yr; }
        dt2 = yr+bb.substr(3,2)+bb.substr(0,2);
    }
    if (dt1==dt2) return 0;
    if (dt1<dt2) return -1;
    return 1;
}

function ts_sort_currency(a,b) { 
    aa = ts_getInnerText(a.cells[SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,'');
    bb = ts_getInnerText(b.cells[SORT_COLUMN_INDEX]).replace(/[^0-9.]/g,'');
    return parseFloat(aa) - parseFloat(bb);
}

function ts_sort_numeric(a,b) { 
    aa = parseFloat(ts_getInnerText(a.cells[SORT_COLUMN_INDEX]));
    bb = parseFloat(ts_getInnerText(b.cells[SORT_COLUMN_INDEX]));
    
	if(!isNaN(aa) && aa == 0 && isNaN(bb)) bb = -1;
	else if(!isNaN(bb) && bb == 0 && isNaN(aa)) aa = -1;
	
	if (isNaN(aa)) aa = 0;
	if (isNaN(bb)) bb = 0;
	
    return aa-bb;
}

function ts_sort_caseinsensitive(a,b) {
    aa = ts_getInnerText(a.cells[SORT_COLUMN_INDEX]).toLowerCase();
    bb = ts_getInnerText(b.cells[SORT_COLUMN_INDEX]).toLowerCase();
    if (aa==bb) return 0;
    if (aa<bb) return -1;
    return 1;
}

function ts_sort_default(a,b) {
    aa = ts_getInnerText(a.cells[SORT_COLUMN_INDEX]);
    bb = ts_getInnerText(b.cells[SORT_COLUMN_INDEX]);
    if (aa==bb) return 0;
    if (aa<bb) return -1;
    return 1;
}


function addEvent(elm, evType, fn, useCapture)
// addEvent and removeEvent
// cross-browser event handling for IE5+,  NS6 and Mozilla
// By Scott Andrew
{
  if (elm.addEventListener){
    elm.addEventListener(evType, fn, useCapture);
    return true;
  } else if (elm.attachEvent){
    var r = elm.attachEvent("on"+evType, fn);
    return r;
  } else {
    alert("Handler could not be removed");
  }
} 


function removeTextNodesFromArray(arrayOrig) {
	var newArray = new Array(0);
	for(i = 0; i < arrayOrig.length; i++) {
		if(arrayOrig[i].nodeName != "#text") {
			newArray.push(arrayOrig[i]);
		}
	}
	return newArray;
}

function setOddEvenRowClassesOnTable(tbl) {
    // set row colours if has odd and even row css styles
	var oddRowClass = tbl.getAttribute('OddRowClass');
	var evenRowClass = tbl.getAttribute('EvenRowClass');
	setOddEvenRowClasses(tbl, oddRowClass, evenRowClass);
}

function setOddEvenRowClasses(tbl, oddRowClass, evenRowClass) {
	if (tbl != null && oddRowClass != null && evenRowClass != null) {
		// firefox introduces #text nodes in between elements, we need to remove these to get the real rows
		var arrTR = removeTextNodesFromArray(removeTextNodesFromArray(tbl.childNodes)[0].childNodes); 
		// - total number of TR tags belonging to the TBODY tag
		var l = arrTR.length
		for (i = 1; i < l; i++) { 
			// 0 index start (0 is odd)
			if ((i-1)%2 == 0) {
				if (oddRowClass != null && oddRowClass != "") {
					arrTR[i].className = oddRowClass;
				}
			} 
			else { 
				if (evenRowClass != null && evenRowClass != "")	{
					arrTR[i].className = evenRowClass;
				}
			}
		}
	}
}

