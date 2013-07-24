function CheckSelectedAtLeastOne(confirmDelete) {
    var checked = false;
    var div = $('divMessageHolder');
    var cBoxes = div.getElementsByTagName('input');
    var selectedCount = 0;
    for(var i = 0; i < cBoxes.length; i++) { 
        var cb = cBoxes[i];
        if(cb.type && cb.type == 'checkbox' && cb.checked == true) {
            checked = true;
            selectedCount++;
        }
    }

    if(!checked) {
        alert('Please select at least one message');
        return false;
    }

    if(confirmDelete) {        
        return confirm((selectedCount > 1 ? "Are you sure want to delete selected messages?" : 
                                            "'Are you sure want to delete selected message?'" ));
    }
    
    return true;
}

function CheckSelectedForForward() {
    var div = $('divMessageHolder');
    var cBoxes = div.getElementsByTagName('input');
    var checked = 0;
    for(var i = 0; i < cBoxes.length; i++) { 
        var cb = cBoxes[i];
        if(cb.type && cb.type == 'checkbox' && cb.checked == true) {
            checked++;
        }
    }

    if(checked == 0) {
        alert('Please select a message to forward');
        return false;
    }

    if(checked > 1) {
        alert('Please select only one message');
        return false;
    }
    
    return true;      
}