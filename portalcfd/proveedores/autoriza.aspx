<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="autoriza.aspx.vb" Inherits="LinkiumCFDI.autoriza" %>
<%--<%@ Register src="~/portalcfd/UserControls_PortalCFD/SystemHeader_PortalCFD.ascx" tagname="SystemHeader_PortalCFD" tagprefix="PortalCFDControls" %>--%>
<%--<a href="~/portalcfd/UserControls_PortalCFD/SystemHeader_PortalCFD.ascx">~/portalcfd/UserControls_PortalCFD/SystemHeader_PortalCFD.ascx</a>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Autorización Ordenes de Compra</title>
    <link href="../Styles/Styles.css" rel="stylesheet" type="text/css" />
    <style type="text/css"> 
        .div {
            border: 2px solid #a1a1a1;
            padding: 10px 40px; 
            background: #dddddd;
            width: 60%;
            border-radius: 25px;
        }
        .label
        {
            font-size: 10pt;
	        color: #000000;
	        font-family: Verdana;
	        text-decoration: none;
	        font-weight:600;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="center" width="900px">
            <tr>
                <td>
                    <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/banerComercial.jpg" Width="100%" />
                </td>
            </tr>
            <tr>
                <td style="height:300px;">
                    <div class="div">
                        <asp:Label ID="lblMensaje" runat="server" CssClass="label"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
	                <br />
	                <fieldset class="footer">
	                    <a href="http://www.linkium.mx" class="footer" target="_blank"><asp:Image ID="imgLogoLinkium" runat="server" ImageUrl="~/images/icons/logolinkium.jpg" BorderStyle="None" /> Software Development & IT Consulting</a>
	                </fieldset>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
