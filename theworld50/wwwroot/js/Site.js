(function() {
   /* var ele = $("#nameuser");
    ele.text("Andrii K");

    var main = $("#main");
    main.on('mouseenter', function () {
       // alert("enter");
        main.css("background-color", "yellow");
    });
    main.on('mouseleave', function() {
        main.css("background-color", "");
        //alert("leave");

    });

    var menuItems = $("ul.menu li a");
    menuItems.on("click", function() {
        var me = $(this);
        alert(me.text());
    });*/

    var $divs = $("#sidebar, #wrapper");
    $("#togleSidebar").on("click", function() {
        $divs.toggleClass('hideSidebar');
        if($divs.hasClass("hideSidebar"))
        {
            $(this).text("Show menu");
        } else {
            $(this).text("Hide menu");
        }
    });
})();