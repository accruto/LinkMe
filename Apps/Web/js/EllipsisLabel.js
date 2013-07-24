function activateEllipsisLabels() {
    labels = $$(".ellipsis-label");
    for (i=0; i<labels.length; i++) {   
        label = labels[i];
        
        // Store max display width now, before style changes.
        var labelContainer = label.parentNode;
        maxWidth = labelContainer.offsetWidth;
        
        strDisplayStyle = label.style.display;
        label.style.display = "inline";
        strWhiteSpaceStyle = label.style.whiteSpace;
        label.style.whiteSpace = "nowrap";

        if (label.offsetWidth > maxWidth) {			// Too long? Use HTML to cut

            // Crop the container of the ellipsis-label down to size
            
            labelContainer.style.width = maxWidth - 15 + "px";
            labelContainer.style.overflow = "hidden";

            // Insert the (absolutely positioned) ellipsis DIV
            var newDiv = new Element('div');
            newDiv.className = label.className;
            newDiv.removeClassName("ellipsis-label");
            newDiv.style.position = "absolute";
            newDiv.style.marginLeft = label.parentNode.style.width;
            newDiv.innerHTML = "...";
            label.insert({ 'before': newDiv });
        }        
        label.style.display = strDisplayStyle;
        label.style.whiteSpace = strWhiteSpaceStyle;
	}
}