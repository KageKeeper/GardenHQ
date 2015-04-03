// Handle showing modal dialogs
// Since many of the buttons will be added dynamically, I needed to do it here.
$(document).on('click', '.show-modal', function (e) {

    e.preventDefault();

    ShowModal(this);
});
$(document).on('click', '#submit-modal-form', function (e) {

    e.preventDefault();
    var parentFormID = '#' + $(this).closest("form").attr("id");
    // Client-side validation
    if (!$(parentFormID).valid()) {
        return false;
    }

    $.ajax({
        type: "POST",
        url: $(this).closest("form").attr("action"),
        data: $(parentFormID).serialize(),
        datatype: "html",
        success: function (data) {
            $('#modal-parent').modal('hide');
            LoadAjaxContent(data);
            ShowCompleteMessage();
            UpdateMainMenu();
            if (mlPushMenu.open) { // if the root menu is showing when a modal is closed...
                mlPushMenu.resetMenu(); // trigger the click to close it..
            }
        }
    }).fail(function (xhr, textStatus, error) {
        ShowErrorMessage(error);
    });

});

// show table actions on row hover
$(document).on({
    mouseenter: function () {
        $(this).find('.table-action-hide a').stop(true).animate({
            opacity: 1
        });
    },
    mouseleave: function () {
        $(this).find('.table-action-hide a').stop(true).animate({
            opacity: 0
        });
    }
}, '.table tbody tr');

// Handle the drop down menu functionality.
//$(document).on('click', '.main-menu').on('click', 'a', function (e) {
//    var parents = $(this).parents('li');
//    var li = $(this).closest('li.dropdown');
//    var otherItems = $('.main-menu li').not(parents);
//    otherItems.find('a').removeClass('active');
//    otherItems.find('a').removeClass('active-parent');
//    if ($(this).hasClass('dropdown-toggle') || $(this).closest('li').find('ul').length == 0) {
//        $(this).addClass('active-parent');
//        var current = $(this).next();
//        if (current.is(':visible')) {
//            li.find("ul.dropdown-menu").slideUp('fast');
//            li.find("ul.dropdown-menu a").removeClass('active')
//        }
//        else {
//            otherItems.find("ul.dropdown-menu").slideUp('fast');
//            current.slideDown('fast');
//        }
//    }
//    else {
//        if (li.find('a.dropdown-toggle').hasClass('active-parent')) {
//            var pre = $(this).closest('ul.dropdown-menu');
//            pre.find("li.dropdown").not($(this).closest('li')).find('ul.dropdown-menu').slideUp('fast');
//        }
//    }
//    if ($(this).hasClass('active') == false) {
//        $(this).parents("ul.dropdown-menu").find('a').removeClass('active');
//        $(this).addClass('active')
//    }
//    if ($(this).hasClass('ajax-link')) {
//        e.preventDefault();
//        if ($(this).hasClass('add-full')) {
//            $('#content').addClass('full-content');
//        }
//        else {
//            $('#content').removeClass('full-content');
//        }
//        var url = $(this).attr('href');
//        //window.location.hash = url;
//        LoadAjaxContent(url);
//    }
//    if ($(this).attr('href') == '#') {
//        e.preventDefault();
//    }
//});

//////////////////////////////////////////////////////
//////////////////////////////////////////////////////
//
//      Main Document Ready script for Garden HQ
//
//////////////////////////////////////////////////////
//////////////////////////////////////////////////////
$(document).ready(function () {
    $('.show-sidebar').on('click', function (e) {
        e.preventDefault();
        $('div#main').toggleClass('sidebar-show');

        return false;
    });
    //var ajax_url = location.hash.replace(/^#/, '');
    //if (ajax_url.length < 1) {
    //    ajax_url = '/';
    //}
    //LoadAjaxContent(ajax_url);

    $('#notification-message-success-close').on('click', function (e) {
        e.preventDefault();
        $('.notification-message-success').doTimeout("notification-message-success-timeout");
        $('.notification-message-success').hide();
    });
    $('#notification-message-error-close').on('click', function (e) {
        e.preventDefault();
        $('.notification-message-error').doTimeout("notification-message-error-timeout");
        $('.notification-message-error').hide();
    });

    //var height = window.innerHeight - 49;
    //$('#main').css('min-height', height)
    //	.on('click', '.expand-link', function (e) {
    //	    var body = $('body');
    //	    e.preventDefault();
    //	    var box = $(this).closest('div.box');
    //	    var button = $(this).find('i');
    //	    button.toggleClass('fa-expand').toggleClass('fa-compress');
    //	    box.toggleClass('expanded');
    //	    body.toggleClass('body-expanded');
    //	    var timeout = 0;
    //	    if (body.hasClass('body-expanded')) {
    //	        timeout = 100;
    //	    }
    //	    setTimeout(function () {
    //	        box.toggleClass('expanded-padding');
    //	    }, timeout);
    //	    setTimeout(function () {
    //	        box.resize();
    //	        box.find('[id^=map-]').resize();
    //	    }, timeout + 50);
    //	})
    //	.on('click', '.collapse-link', function (e) {
    //	    e.preventDefault();
    //	    var box = $(this).closest('div.box');
    //	    var button = $(this).find('i');
    //	    var content = box.find('div.box-content');
    //	    content.slideToggle('fast');
    //	    button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
    //	    setTimeout(function () {
    //	        box.resize();
    //	        box.find('[id^=map-]').resize();
    //	    }, 50);
    //	})
    //	.on('click', '.close-link', function (e) {
    //	    e.preventDefault();
    //	    var content = $(this).closest('div.box');
    //	    content.remove();
    //	});
    //$('#locked-screen').on('click', function (e) {
    //    e.preventDefault();
    //    $('body').addClass('body-screensaver');
    //    $('#screensaver').addClass("show");
    //    ScreenSaver();
    //});
    //$('body').on('click', 'a.close-link', function (e) {
    //    e.preventDefault();
    //    CloseModalBox();
    //});
    //$('#top-panel').on('click', 'a', function (e) {
    //    if ($(this).hasClass('ajax-link')) {
    //        e.preventDefault();
    //        if ($(this).hasClass('add-full')) {
    //            $('#content').addClass('full-content');
    //        }
    //        else {
    //            $('#content').removeClass('full-content');
    //        }
    //        var url = $(this).attr('href');
    //        window.location.hash = url;
    //        LoadAjaxContent(url);
    //    }
    //});
    //$('#search').on('keydown', function (e) {
    //    if (e.keyCode == 13) {
    //        e.preventDefault();
    //        $('#content').removeClass('full-content');
    //        ajax_url = 'ajax/page_search.html';
    //        window.location.hash = ajax_url;
    //        LoadAjaxContent(ajax_url);
    //    }
    //});
    //$('#screen_unlock').on('mouseover', function () {
    //    var header = 'Enter current username and password';
    //    var form = $('<div class="form-group"><label class="control-label">Username</label><input type="text" class="form-control" name="username" /></div>' +
    //				'<div class="form-group"><label class="control-label">Password</label><input type="password" class="form-control" name="password" /></div>');
    //    var button = $('<div class="text-center"><a href="index.html" class="btn btn-primary">Unlock</a></div>');
    //    OpenModalBox(header, form, button);
    //});
});

/*-------------------------------------------
    Main scripts used by theme
---------------------------------------------*/
//
//  Function for load content from url and put in $('.ajax-content') block
//
function LoadAjaxContent(url) {
    $.ajax({
        mimeType: 'text/html; charset=utf-8', // ! Need set mimeType only when run from local file
        url: url,
        type: 'GET',
        success: function (data) {
            $('#ajax-content').html(data);
            if (mlPushMenu.open) { // if the root menu is showing when a modal is closed...
                mlPushMenu.resetMenu(); // trigger the click to close it..
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        dataType: "html",
        async: false
    });

    //window.location.hash = url
}

function ShowModal(clickedElement) {
    var url = $(clickedElement).attr('data-url'); // expected format: Action/ControllerName
    var id = $(clickedElement).attr('data-id'); // the id of the object
    var modalHeaderTitle = $(clickedElement).attr('data-modal-title');
    var modalSubmitText = $(clickedElement).attr('data-modal-submit-text');

    // Construct url
    if (id) {
        url += '/' + id;
    }

    $.get(url, function (data) {
        $('.modal-content').html(data);
        $('#modal-parent').modal('show');
        if (modalSubmitText == "Destroy") // Add additional coloring to indicate a danger operation
        {
            $(".modal-header").addClass("bg-danger");
            $("#submit-modal-form").removeClass("btn-info").addClass("btn-danger");
        }
        $('#modal-header-title').html(modalHeaderTitle);
        $('#submit-modal-form').val(modalSubmitText);
    });
}

function ShowCompleteMessage() {
    $('.notification-message-success').slideToggle(400).doTimeout("notification-message-success-timeout", 2000, function () {
        $('.notification-message-success').slideToggle(400);
    });
}

function ShowErrorMessage(errorMessage) {
    $('.notification-message-error-message').text(errorMessage);
    $('.notification-message-error').slideToggle(400).doTimeout("notification-message-error-timeout", 2000, function () {
        $('.notification-message-error').slideToggle(400);
    });
}

function UpdateMainMenu() {
    $.ajax({
        mimetype: 'text/html; charset=utf-8',
        url: '/Menu/UpdateMainMenu',
        type: 'GET',
        success: function (data) {
            $('#sidebar-left').html(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        dataType: "html"
    });
}
