<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PepsicoDetalle.aspx.vb" Inherits="LinkiumCFDI.PepsicoDetalle" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript">

        function getRadWindow() {
            var oWindow = null;

            if (window.radWindow) 
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow) 
                oWindow = window.frameElement.radWindow;

            return oWindow;
        }

        function clientClose(arg) {   
            getRadWindow().close(arg);
        }
        
        function redirectParentPage(url) {
            getRadWindow().BrowserWindow.document.location.href = url;
        }
    </script>
    <link href="Styles/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <fieldset style="padding:5px; margin-top:10px; margin-left:10px; margin-right:10px;">
            <legend style="padding-right:6px; color:Black">
               <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="item" Text="Datos Addenda PEPSICO"></asp:Label>
            </legend>
            <br />
            <table style="width:100%" align="center" border="0" width="100%">
                <tr>
                    <td style="width:33%">
                        <span class="item">Tipo de Documento:</span>
                    </td>
                    <td style="width:33%">
                        <span class="item">No. de Pedido:</span>
                    </td>
                    <td style="width:33%">
                        <span class="item">No. de Solicitud Pago:</span>
                    </td>
                </tr>
                <tr>
                    <td style="width:33%">
                        <asp:DropDownList ID="documentoid" runat="server" AutoPostBack="true" CssClass="box">
                            <asp:ListItem Value="0" Text="--Seleccione--" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Factura"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Nota de Crédito"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Nota de Cargo o Débito"></asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="documentoid" InitialValue="0" runat="server" ErrorMessage=" *" ForeColor="Red" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width:33%">
                        <telerik:RadNumericTextBox ID="txtIdPedido" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MaxLength="10" runat="server"></telerik:RadNumericTextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtIdPedido" runat="server" ErrorMessage=" *" ForeColor="Red" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width:33%">
                        <telerik:RadTextBox ID="txtIdSolicitudPago" MaxLength="10" runat="server">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width:33%">
                        <span class="item">No. de Recepción:</span>
                    </td>
                    <td style="width:33%">
                        <span class="item">Referencia (UUID):</span>
                    </td>
                    <td style="width:33%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width:33%">
                        <telerik:RadNumericTextBox ID="txtIdRecepcion" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MaxLength="10" runat="server"></telerik:RadNumericTextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtIdRecepcion" runat="server" ErrorMessage=" *" ForeColor="Red" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width:33%">
                        <telerik:RadTextBox ID="txtReferencia" Enabled="false" MaxLength="50" Width="95%" runat="server">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width:33%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" align="right">
                        <asp:Button ID="btnGenerarAddenda" CausesValidation="true" runat="server" Text="Generar Addenda" CssClass="boton" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right">
                        <asp:HiddenField runat="server" ID="serieCFD" Value="" />
                        <asp:HiddenField runat="server" ID="folioCFD" Value="" />
                        <asp:HiddenField runat="server" ID="cfdid" Value="0" />
                        <asp:HiddenField runat="server" ID="FolioUUID" Value="" />
                        <asp:HiddenField runat="server" ID="tipodocumentoid" Value="" />
                        <asp:HiddenField runat="server" ID="clienteid" Value="" />
                        <asp:HiddenField runat="server" ID="fhaini" Value="" />
                        <asp:HiddenField runat="server" ID="fhafin" Value="" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    </form>
</body>
</html>
