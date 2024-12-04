<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeBehind="agregarorden.aspx.vb" Inherits="LinkiumCFDI.agregarorden" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblEditorOrdenes" runat="server" Font-Bold="true" CssClass="item" Text="Agregar Orden de Compra"></asp:Label>
        </legend>
        <br />
        <table width="100%" cellspacing="2" cellpadding="2" align="center" style="line-height:25px;">
            <tr>
                <td class="item">
                    <strong>Proveedor: </strong><br />
                    <asp:DropDownList ID="proveedorid" runat="server" CssClass="item"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* requerido" ForeColor="Red" CssClass="item" ControlToValidate="proveedorid" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--<tr>
                <td class="item">
                    <strong>Tasa IVA: </strong><br />
                    <asp:DropDownList ID="tasaid" runat="server" CssClass="item"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* requerido" ForeColor="Red" CssClass="item" ControlToValidate="tasaid" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <tr>
                <td class="item">
                    <strong>Comentarios: </strong><br />
                    <telerik:RadTextBox id="txtComentarios" runat="server" TextMode="MultiLine" Width="600px" Height="90px"></telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAddorder" runat="server" CssClass="item" Text="Guardar" />&nbsp;&nbsp;<asp:Button ID="btnCancelar" runat="server" CssClass="item" Text="Cancelar" CausesValidation="false" />
                </td>
            </tr>
        </table>
        <br /><br />
    </fieldset>
</asp:Content>
