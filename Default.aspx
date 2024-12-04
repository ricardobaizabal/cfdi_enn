<%@ Page Language="VB" AutoEventWireup="false" Inherits="LinkiumCFDI._Default" Codebehind="Default.aspx.vb" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" align="center">
	        <tr>
		        <td align="center">
		            <asp:Image ID="imgBanner" runat="server" />
			    </td>
	        </tr>
	        <tr><td><br /><br /><br /><br /></td></tr>
	        <tr>
	            <td align="center">
	                <fieldset style="padding:5px;">
	                    <legend style="padding:10px; font-size:16px; font-weight:600;">Sistema de Facturación Electrónica</legend>
	                    <br /><br />
	                    <table border="0" cellpadding="1" cellspacing="3" align="center">
	                        <tr>
	                            <td align="right">Email: </td>
	                            <td>
	                                <asp:TextBox ID="email" runat="server" Width="150px"></asp:TextBox>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="valEmail" runat="server" ControlToValidate="email" ErrorMessage="* Requerido" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
	                        </tr>
	                        <tr>
	                            <td align="right">Contraseña: </td>
	                            <td>
	                                <asp:textbox ID="contrasena" runat="server" Width="150px" TextMode="Password"></asp:textbox>&nbsp;&nbsp;<asp:RequiredFieldValidator ID="valContrasena" runat="server" ControlToValidate="contrasena" ErrorMessage="* Requerido" SetFocusOnError="True"></asp:RequiredFieldValidator>
	                            </td>
	                        </tr>
	                        <tr>
	                            <td></td>
	                            <td align="left">
	                                <br />
	                                <asp:Button ID="btnLogin" runat="server" Text="entrar" /><br /><br />
			                        
	                            </td>
	                        </tr>
	                        <tr>
	                            <td></td>
	                            <td align="left"><asp:CheckBox ID="chkRemember" runat="server" Text="Recordar mis datos" /></td>
	                        </tr>
	                        <tr>
	                            <td colspan="2"><br /><asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
	                        </tr>
	                        
	                    </table>
			        
	                </fieldset>
	                
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
