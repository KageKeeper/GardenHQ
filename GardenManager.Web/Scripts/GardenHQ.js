// Handle showing modal dialogs
// Since many of the buttons will be added dynamically, I needed to do it here.
$(document).on('click', '.show-modal', function (e) {

    e.preventDefault();

    var url = $(this).attr('data-url'); // expected format: Action/ControllerName
    var id = $(this).attr('data-id'); // the id of the object
    $.get(url + '/' + id, function (data) {
        $('.modal-content').html(data);
        $('#modal-parent').modal('show');
    });
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
        }
    });

});

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
        setTimeout(MessagesMenuWidth, 250);
    });
    var ajax_url = location.hash.replace(/^#/, '');
    if (ajax_url.length < 1) {
        ajax_url = '/';
    }
    LoadAjaxContent(ajax_url);

    // Handle the drop down menu functionality.
    $('.main-menu').on('click', 'a', function (e) {
        var parents = $(this).parents('li');
        var li = $(this).closest('li.dropdown');
        var otherItems = $('.main-menu li').not(parents);
        otherItems.find('a').removeClass('active');
        otherItems.find('a').removeClass('active-parent');
        if ($(this).hasClass('dropdown-toggle') || $(this).closest('li').find('ul').length == 0) {
            $(this).addClass('active-parent');
            var current = $(this).next();
            if (current.is(':visible')) {
                li.find("ul.dropdown-menu").slideUp('fast');
                li.find("ul.dropdown-menu a").removeClass('active')
            }
            else {
                otherItems.find("ul.dropdown-menu").slideUp('fast');
                current.slideDown('fast');
            }
        }
        else {
            if (li.find('a.dropdown-toggle').hasClass('active-parent')) {
                var pre = $(this).closest('ul.dropdown-menu');
                pre.find("li.dropdown").not($(this).closest('li')).find('ul.dropdown-menu').slideUp('fast');
            }
        }
        if ($(this).hasClass('active') == false) {
            $(this).parents("ul.dropdown-menu").find('a').removeClass('active');
            $(this).addClass('active')
        }
        if ($(this).hasClass('ajax-link')) {
            e.preventDefault();
            if ($(this).hasClass('add-full')) {
                $('#content').addClass('full-content');
            }
            else {
                $('#content').removeClass('full-content');
            }
            var url = $(this).attr('href');
            //window.location.hash = url;
            LoadAjaxContent(url);
        }
        if ($(this).attr('href') == '#') {
            e.preventDefault();
        }
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
    $('.preloader').show();
    $.ajax({
        mimeType: 'text/html; charset=utf-8', // ! Need set mimeType only when run from local file
        url: url,
        type: 'GET',
        success: function (data) {
            $('#ajax-content').html(data);
            $('.preloader').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);
        },
        dataType: "html",
        async: false
    });
}

function ShowCompleteMessage() {
    $('.notification-message-success').html("Update Completed Successfully!");
    $('.notification-message-success').slideToggle(400).delay(3000).slideToggle(400);
}
