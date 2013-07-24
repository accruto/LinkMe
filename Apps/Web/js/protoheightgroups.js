// Use a custom HTML attribute, heightgroup="N", to match the heights of DIVs.
// 

function updateHeightGroups() {
    var heightGroups = new Array();

    $$("[heightgroup]").each(function(el) {
        if (el && el.offsetHeight && el.offsetHeight > 0) {
            var i = el.attributes["heightgroup"].value;
            if (!heightGroups[i])
                heightGroups[i] = new Array();
            heightGroups[i].push(el);
            el.style.height = "auto";
        }
    });

    for (var i = 0; i < heightGroups.length; i++) {
        if (heightGroups[i]) {
            var maxHeight = 0;
            for (var j = 0; j < heightGroups[i].length; j++)
                maxHeight = Math.max(heightGroups[i][j].offsetHeight, maxHeight);
            for (var j = 0; j < heightGroups[i].length; j++) {
                if (heightGroups[i])
                    el = heightGroups[i][j];
                if (el) {
                    var elVerticalPadding =
                                    parseInt(el.getStyle("padding-top").replace("px", "")) +
                                    parseInt(el.getStyle("padding-bottom").replace("px", ""));
                    el.style.height = (maxHeight - elVerticalPadding) + "px";
                }
            }
        }
    }
}