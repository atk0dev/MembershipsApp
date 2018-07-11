$(function() {
    $(".panel-carret").click(function(e) {
        $(this).toggleClass("pressed"); 
        $(this).children(".glyphicon-play").toggleClass("gly-rotate-90d");
        e.preventDefault();
    });
});