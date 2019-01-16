/// <reference path="jquery-1.4.1-vsdoc.js" />

$(document).ready(function () {
    // Block screen to display info
    blockScreenToDisplayInfo();

    // Unblock on escape
    unblockScreenOnEscape();

    // Minimaz and Maximaz
    minmaxContentContainer();

    // Select all from phEmsWebAppHeader
    checkUncheckAll();

    // Select all from phEmsWebAppHeader
    uncheckFromphEmsWebAppHeader();

});



function blockScreenToDisplayInfo() {
    $(".gridDataRowOdd a, .gridDataRowEven a").live("click", function () {
        $.blockUI();
        return false;
    });
}

function unblockScreenOnEscape() {
    $(document).keyup(function (e) {
        if (e.keyCode == 27) {
            $.unblockUI(); 
        }
    });
}

function minmaxContentContainer() {
    $(".contentphEmsWebAppHeader").live("click", function () { $(this).next().slideToggle("750"); });
}

function checkUncheckAll() {
    $(".gridphEmsWebAppHeader input[type='checkbox']").live("click", function () {
        $(".gridDataRowOdd input[type='checkbox'], .gridDataRowEven input[type='checkbox']").attr("checked", $(this).is(":checked"));
    });
}

function uncheckFromphEmsWebAppHeader() {
    $(".gridDataRowOdd input[type='checkbox'], .gridDataRowEven input[type='checkbox']").live("click", function () {
        if(!$(this).is(':checked'))
             $(".gridphEmsWebAppHeader input[type='checkbox']").attr("checked", false);
    });
}