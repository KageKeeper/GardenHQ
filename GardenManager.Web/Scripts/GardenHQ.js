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
