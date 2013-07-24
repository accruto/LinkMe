// Functions used by SubmitAsGetButton
// AS 08.04.2008: Verified, used.
function GetButtonSubmitUrl(baseUrl, queryParameters)
{
    if (!queryParameters || queryParameters.length == 0)
        return baseUrl;

    if (queryParameters.length % 2 != 0)
        throw 'queryParameters must contain an even number of elements';

    var resultUrl = baseUrl;
    var queryStarted = (baseUrl.indexOf('?') != -1);

    for (var i = 0; i < queryParameters.length; i += 2)
    {
        var name = queryParameters[i];
        var value = queryParameters[i + 1] + ''; // Convert to string

        if (name && name.length > 0 && value && value.length > 0)
        {
            resultUrl += (queryStarted ? '&' : '?') + name + '=' + value;
            queryStarted = true;
        }
    }

    return resultUrl;
}

// AS 08.04.2008: Verified, used.
// MF 2010-06-08: Now accommodates RepeatLayout="Table" RepeatColumns="any" (not just RepeatLayout="Flow")
function GetRadioListValue(radioList) {
    if (radioList.nodeName == "TABLE") {
        for (var h = 0; h < radioList.childNodes.length; h++) { 
            var tbody = radioList.childNodes[h];
            if (tbody.tagName == "TBODY") {
                for (var i = 0; i < tbody.childNodes.length; i++) {
                    var tr = tbody.childNodes[i];
                    if (tr.tagName == "TR") {
                        for (var j = 0; j < tr.childNodes.length; j++) {
                            var td = tr.childNodes[j];
                            if (td.tagName == "TD") {
                                for (var k = 0; k < td.childNodes.length; k++) {
                                    var tdChild = td.childNodes[k];
                                    if (tdChild.tagName == "INPUT" && tdChild.checked) {
                                        return tdChild.value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    } else {
        for (var i = 0; i < radioList.childNodes.length; i++) {
            var child = radioList.childNodes[i];
            if (child.tagName == 'INPUT' && child.checked)
                return child.value;
        }
    }
    
    return ""; // No radio button selected.
}

// KT 20.05.2008: Oh god oh god oh god, what am I doing.
// MF 2010-06-08: Now accommodates RepeatLayout="Table" RepeatColumns="any" (not just RepeatLayout="Flow")
function GetCheckBoxListValue(checkBoxList)
{
    var value = 0;
    var n = 0;

    if (checkBoxList.nodeName == "TABLE") {
        for (var h = 0; h < checkBoxList.childNodes.length; h++) { 
            var tbody = checkBoxList.childNodes[h];
            if (tbody.tagName == "TBODY") {
                for (var i = 0; i < tbody.childNodes.length; i++) {
                    var tr = tbody.childNodes[i];
                    if (tr.tagName == "TR") {
                        for (var j = 0; j < tr.childNodes.length; j++) {
                            var td = tr.childNodes[j];
                            if (td.tagName == "TD") {
                                for (var k = 0; k < td.childNodes.length; k++) {
                                    var tdChild = td.childNodes[k];
                                    if (tdChild.tagName == "INPUT") {
                                        value += (tdChild.checked ? Math.pow(2, n) : 0);
                                        n++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    } else {
        for (var i = 0; i < checkBoxList.childNodes.length; i +=2) { 
            if (checkBoxList.childNodes[i].checked) {
                // 1, 2, 4, 8, 16... etc
                value += Math.pow(2, (i/2));
            }   
        }
    }
    
    return value;
}

// AS 08.04.2008: Verified, used.
function GetListBoxValues(listBox)
{
    var value = '';
    
    for (var i = 0; i < listBox.options.length; i++)
    {
        var option = listBox.options[i];
        if (option.selected)
        {
            if (value == '')
            {
                value = option.value;
            }
            else
            {
                value += ',' + option.value;
            }
        }
    }
    
    return value;
}

// AS 08.04.2008: Verified, used.
function GetListBoxFlagsValue(listBox)
{
    var value = 0;
    
    for (var i = 0; i < listBox.options.length; i++)
    {
        var option = listBox.options[i];
        if (option.selected)
        {
            value |= option.value;
        }
    }
    
    return value;
}

// AS 08.04.2008: Verified, used.
function GetDropDownListValue(list)
{
    return list.value;
}

// END functions used by SubmitAsGetButton
