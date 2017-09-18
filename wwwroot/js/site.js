//site.js

//(function () {
//    //var ele = $("#username");
//    //ele.text("Edward Johnston");

//    //var main = $("#main");
//    //main.on('mouseenter', function () {
//    //    main.style ="background-color:#888;";
//    //});

//    //main.on('mouseleave', function () {
//    //    main.style = "";
//    //});

//    var $sidebarAndWrapper = $("#sidebar, #wrapper");

//    $("#sidebarToggle").on("click", function() {
//        $sidebarAndWrapper.toggleClass("hide-sidebar");

//        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
//            $("#sidebarToggle i").removeClass("fa fa-arrow-circle-left");
//            $("#sidebarToggle i").addClass("fa fa-arrow-circle-right");
//        } else {
//            $("#sidebarToggle i").removeClass("fa fa-arrow-circle-right");
//            $("#sidebarToggle i").addClass("fa fa-arrow-circle-left");
//        }

//    });
//})();


//$(document).ready(function () {

//    $("#DeleteButton").click(function () {
//        alert($("#DeleteButton").attr("data-content"));
//        var url = $("#RedirectTo").val() + '/' + $("#DeleteButton").attr("data-content");
//        location.href = url;
//    });

//});

$(document).ready(function () {

    $('#quantity').on('keyup', function () {
        var tot = $('#price').val() * $('#quantity').val();
        tot = tot.toFixed(2);
        $('#total').val(tot);
    });

    $('#price').on('keyup', function () {
        var tot = $('#price').val() * $('#quantity').val();
        tot = tot.toFixed(2);
        $('#total').val(tot);
    });

});
