(function($) {

    download = function(customArguments) {
        var p = {
            url: null,
            method: "post"
        };

        $.extend(p, customArguments);

        // Remove old iframe.

        if ($("#iframe-download"))
            $("#iframe-download").remove();

        // Create new iframe.
        
        iframeX= $("<iframe src=\"" + p.url + "\" name=\"iframe-download\" id=\"iframe-download\"></iframe>").appendTo("body").hide();
    };

})(jQuery);