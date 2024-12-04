<%@ Page Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="PepsicoAddenda.aspx.vb" Inherits="LinkiumCFDI.PepsicoAddenda" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
    <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblAddendaEditLegend" runat="server" Text="Configurar Addenda" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>    
            <table width="100%" border="0">
                <tr>
                    <td colspan="4" align="right" style="height:25px">&nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        <asp:Label ID="lblProveedorId" runat="server" CssClass="item" Text="No. de Proveedor:" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="20%">
                        <asp:Label ID="lblVersion" runat="server" CssClass="item" Text="Versión:" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="20%">&nbsp;</td>
                    <td width="40%">&nbsp;</td>
                </tr>
                <tr>
                    <td width="20%">
                        <telerik:RadTextBox ID="txtProvedorId" Runat="server" Width="100px">
                        </telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text=" *" 
                            ControlToValidate="txtProvedorId" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="20%">
                        <telerik:RadTextBox ID="txtVersion" Runat="server" Width="100px">
                        </telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text=" *" 
                            ControlToValidate="txtVersion" SetFocusOnError="true" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="20%">
                        <asp:Button ID="btnAgregarAddenda" runat="server" CausesValidation="True" Text="Guardar" CssClass="item" />
                    </td>
                    <td width="40%">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4" align="left" style="height:25px">
                        <asp:Label ID="lblMensaje" runat="server" CssClass="item" Font-Bold=true ForeColor="Green"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right" style="height:5px">
                        <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                        <asp:HiddenField ID="RegistroID" runat="server" Value="0" />
                        <asp:HiddenField ID="AddendaID" runat="server" Value="0" />
                        <asp:HiddenField ID="ClienteID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </fieldset>
</telerik:RadAjaxPanel>
</asp:Content>
