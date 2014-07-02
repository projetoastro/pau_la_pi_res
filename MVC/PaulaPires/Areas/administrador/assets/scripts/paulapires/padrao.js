jQuery(document).ready(function () {

    highlightActiveMenuItem();



});


highlightActiveMenuItem = function () {
    var url = window.location.pathname;
    $(".page-sidebar-menu a").each(function() {
        if ($(this).attr("href") == url) {
            var teste = $(this).closest("li");
            //$(this).addClass("active").parent('li').addClass("active");
            $(this).parent('li').addClass("active").closest('ul').parent('li').addClass("active");
        }
    });
};
