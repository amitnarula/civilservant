/// <reference path="jquery-1.4.1-vsdoc.js" />

$(document).ready(function () {
    $(".gridDataRowOdd").live("mouseover", function () { $(this).addClass("highlight"); }).live("mouseout", function () { $(this).removeClass("highlight"); });
    $(".gridDataRowEven").live("mouseover", function () { $(this).addClass("highlight"); }).live("mouseout", function () { $(this).removeClass("highlight"); });
});