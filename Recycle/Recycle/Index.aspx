<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Recycle.Index" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Report no recycling</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta name="author" content="Natalya Varshavskaya for Hack4Reno">
    <link href="Content/Site.css" rel="stylesheet" /> 
   
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false" type="text/javascript"></script>

    <%--<script type="text/javascript">
        function initialize() {

            // Create an array of styles.
            var styles = [
             {
                  featureType: "administrative.neighborhood",
                  elementType: "geometry",
                  stylers: [
                    { lightness: 100 },
                    { visibility: "simplified" }
                  ]
              }
            ];

            // Create a new StyledMapType object, passing it the array of styles,
            // as well as the name to be displayed on the map type control.
            var styledMap = new google.maps.StyledMapType(styles,
              { name: "Styled Map" });

            var latlng = new google.maps.LatLng(39.5254004, -119.8135266);
            var myOptions = {
                zoom: 12,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

            //Associate the styled map with the MapTypeId and set it to display.
           map.mapTypes.set('map_style', styledMap);
           map.setMapTypeId('map_style');
        }


    </script>--%>
    <style type="text/css">
        #map_canvas {
            height: 43px;
        }
 
    </style>
</head>

<body onload="initialize()">

<form id="Form1" runat="server">
    <div>
        <table style="text-align: center; width: 1257px;">
             <tr style="align-content:center; width: 100%;">
                 <td style="text-align: center">.::  banner goes here  ::.</td>
             </tr>
        </table>
     </div>

    <br />
       
                <table>
                    <tr>
                        <td style="width:35%; vertical-align: top;">
                            <div style="vertical-align: top;">
                                <table>
                                    <tr>
                                        <td colspan="2"><label>Report NO recycling in your building</label></td>
                                    </tr>
                                    <tr>
                                        <td><span>Enter your address: </span></td>
                                        <td><input type="text" id="txtAddress" runat="server" name="txtAddress" style="width: 213px" /></td>
                                    </tr>
                                    <tr>
                                        <td><span>Comment:</span></td>
                                        <td><textarea class="form-control" name="comment" id="txtComment" runat="server" style="width: 213px"></textarea></td>
                                    </tr>
                                    <tr>
                                        <td class="align-right">
                                           <input type="submit" value="Submit" class="btn" runat="server" id="btnSubmit" />
                                        </td>
                                        <td><label runat="server" id="lblMsg" visible="false">Thank you for your submission!</label></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td style="width:65%;">
                            <asp:Literal ID="js" runat="server"></asp:Literal>
                            <div id="map_canvas" style="width: 100%; height: 728px; margin-bottom:2px;"></div>
                            <label id="lblCount" runat="server"></label>
                        </td>
                    </tr>
        
                </table>
           
     <br />
 
</form>
</body>
</html>
