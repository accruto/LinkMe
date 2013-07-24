// Use a custom HTML attribute, heightgroup="N", to match the heights of DIVs.
// 

function updateHeightGroups() {
    var heightGroups = new Array();

    $("div[heightgroup]").each(function() {
        var el = $(this);
        var i = parseInt(el.attr("heightgroup"));
        if (!heightGroups[i])
            heightGroups[i] = new Array();
        heightGroups[i].push(el);
        el.css("height", "auto");
    });

    for (var i = 0; i < heightGroups.length; i++) {
        if (heightGroups[i]) {
            var maxHeight = 0;
            for (var j = 0; j < heightGroups[i].length; j++)
                maxHeight = Math.max(heightGroups[i][j].height(), maxHeight);
            for (var j = 0; j < heightGroups[i].length; j++) {
                if (heightGroups[i])
                    el = heightGroups[i][j];
                if (el) {
                    var elVerticalPadding = parseInt(el.css("padding-top").replace("px", "")) + parseInt(el.css("padding-bottom").replace("px", ""));
                    el.css("height", (maxHeight - elVerticalPadding) + "px");
                }
            }
        }
    }
}