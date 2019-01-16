﻿<%@ Page Title="" Language="C#" MasterPageFile="~/estateblue.master" AutoEventWireup="true" CodeFile="photogallery.aspx.cs" Inherits="photogallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phEmsWebAppHead" Runat="Server">
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
        <script src="galleria.js" type="text/javascript"></script>
        <style type="text/css">

            .content{color:#eee;font:14px/1.4 "helvetica neue", arial,sans-serif;width:620px;margin:20px auto}
            h1{line-height:1.1;letter-spacing:-1px;}
            a {color:#fff;}
            .galleria{height:400px;}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phEmsWebApp" Runat="Server">
<div class="content">
<div id="galleria" runat="server" class="galleria">
            <img alt="Squirrel! That is all." src="http://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Eichh%C3%B6rnchen_D%C3%BCsseldorf_Hofgarten_edit.jpg/800px-Eichh%C3%B6rnchen_D%C3%BCsseldorf_Hofgarten_edit.jpg">
            <img src="http://upload.wikimedia.org/wikipedia/commons/thumb/f/f8/Sea-otter-morro-bay_13.jpg/800px-Sea-otter-morro-bay_13.jpg">
            <img src="http://upload.wikimedia.org/wikipedia/commons/thumb/7/70/Kuznetsk_Alatau_3.jpg/500px-Kuznetsk_Alatau_3.jpg">
            <img alt="Strawberries, yum yum! Shiny red" src="http://upload.wikimedia.org/wikipedia/commons/thumb/d/de/Basket_of_strawberries_red_accent.jpg/500px-Basket_of_strawberries_red_accent.jpg">
            <img src="http://upload.wikimedia.org/wikipedia/commons/2/2d/Ns1-unsharp.jpg">
            <img src="http://upload.wikimedia.org/wikipedia/commons/thumb/6/67/Laser_effects.jpg/500px-Laser_effects.jpg">
            <img src="http://upload.wikimedia.org/wikipedia/commons/thumb/1/1b/PizBernina3.jpg/500px-PizBernina3.jpg">
            <img src="http://upload.wikimedia.org/wikipedia/commons/thumb/4/47/La_Galera_2.jpg/500px-La_Galera_2.jpg">
            <img src="http://upload.wikimedia.org/wikipedia/commons/thumb/9/92/Costa_rica_santa_elena_skywalk.jpg/500px-Costa_rica_santa_elena_skywalk.jpg">
            <img src="http://upload.wikimedia.org/wikipedia/commons/thumb/b/b2/Smoky_forest.jpg/500px-Smoky_forest.jpg">
            <img src="http://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Alcea_rosea_and_blue_sky.jpg/500px-Alcea_rosea_and_blue_sky.jpg">
</div>        </div>
    <script type="text/javascript">
        // Load the classic theme
        Galleria.loadTheme('galleria.classic.js');
        // Initialize Galleria
        $('#<%=galleria.ClientID %>').galleria();
    </script>

</asp:Content>

