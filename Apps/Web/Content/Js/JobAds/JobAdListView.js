(function ($) {

    $(document).ready(function () {

        $(".jobad-list-view").hover(
            function() {
                $(this).addClass("hover");
            },
            function() {
                $(this).removeClass("hover");
            });

        $(".jobad-list-view .column.flag .button").click(function () {
            $(this).toggleClass("expand collapse").closest(".bg").next().toggleClass("expanded collapsed");
        });

    });

})(jQuery);

