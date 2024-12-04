<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="recibir.aspx.vb" Inherits="LinkiumCFDI.recibir" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--<title>INTERJOYA</title>--%>
    <link href="../Styles/Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
		<Scripts>
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
		</Scripts>
	</telerik:RadScriptManager>
	    <script language="javascript" type="text/javascript">
	        function GetRadWindow() {
	            var oWindow = null;
	            if (window.radWindow)
	                oWindow = window.radWindow;
	            else if (window.frameElement && window.frameElement.radWindow)
	                oWindow = window.frameElement.radWindow;
	            return oWindow;
	        }
	        function CloseModal() {
	            // GetRadWindow().close();
	            setTimeout(function() {
	                GetRadWindow().close();
	            }, 0);
	        }
    </script>
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
	</telerik:RadAjaxManager>
    <div>
        <br />
        <fieldset class="item">
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblRecibirPartidas" runat="server" Font-Bold="true" CssClass="item" Text="Recibir productos"></asp:Label>
            </legend>
            <br />
            No. Orden: <asp:Label ID="lblCodigo" runat="server"></asp:Label><br />
            Descripción: <asp:Label id="lblDescripcion" runat="server"></asp:Label><br />
            Cantidad solicitada: <asp:Label ID="lblCantidad" runat="server"></asp:Label><br /><br />
            <asp:Label ID="lblPerecederoBit" Visible="False" runat="server"></asp:Label>

            <table width="100%" cellspacing="2" cellpadding="2" border="0" align="left" style="line-height:25px;">
            <tr>
                <td class="item" style="width:25%">
                    Cantidad: <asp:RequiredFieldValidator ID="valCantidad" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCantidad" SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                    <telerik:RadNumericTextBox ID="txtCantidad" Width="100px" runat="server"></telerik:RadNumericTextBox>
                </td>
                <td class="item" style="width:25%">
                    Costo estandar: <asp:RequiredFieldValidator ID="valCostoEstandar" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCostoEstandar" SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                    <telerik:RadNumericTextBox ID="txtCostoEstandar" Width="100px" runat="server"></telerik:RadNumericTextBox>
                </td>
                <td class="item" style="width:25%">
                    <br />
                    <asp:Button ID="btnAdd" runat="server" Text="Aplicar" CssClass="item" />
                </td>
                <td class="item" style="width:25%">&nbsp;</td>
            </tr>
            <tr><td colspan="4" class="item"><span style="color:Red;">Los campos marcados con (*) son requeridos.</span></td></tr>
            <tr>
                <td colspan="4" class="item">
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="item"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4"><br />
                    <telerik:RadGrid ID="conceptosList" runat="server" Width="100%" ShowStatusBar="True"
                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
                        Skin="Default" ShowFooter="false">
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False">
                            <Columns>
                                <telerik:GridBoundColumn DataField="fecha" HeaderText="Fecha" UniqueName="fecha" ItemStyle-HorizontalAlign="Left">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="cantidad" HeaderText="Cantidad Recibida" UniqueName="cantidad" ItemStyle-HorizontalAlign="Center">
                                </telerik:GridBoundColumn>                                
                                <telerik:GridBoundColumn DataField="usuario" HeaderText="Usuario" UniqueName="usuario">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
            </table>
            <br />
        </fieldset>
    </div>
    </form>
</body>
</html>
