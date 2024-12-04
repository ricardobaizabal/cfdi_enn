<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_CFD_Detalle" Codebehind="CFD_Detalle.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=5.1.11.713, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <fieldset>
        <legend style="padding-right: 6px; color: Black">
            <asp:Label ID="lblEstatusCobranza" runat="server" Font-Bold="true" CssClass="item" Text="CFDI Estatus de cobranza"></asp:Label>
        </legend>
        <br />
        
        <table width="90%">
            <tr>
                <td colspan="5" class="item"><strong>Documento: </strong> <asp:Label ID="lblDocumento" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td class="item">Estatus:<br /><br /><asp:DropDownList ID="estatus_cobranzaid" runat="server" CssClass="box"></asp:DropDownList></td>
                <td class="item">Tipo de pago:<br /><br /><asp:DropDownList ID="tipo_pagoid" runat="server" CssClass="box"></asp:DropDownList></td>
                <td class="item">Referencia:<br /><br /><asp:TextBox ID="referencia" runat="server" CssClass="box"></asp:TextBox></td>
                <td class="item">Fecha de pago:<br /><br /><telerik:RadDatePicker ID="fechapago" runat="server"></telerik:RadDatePicker></td>
                <td style="vertical-align:bottom"><br /><asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="boton" /></td>
            </tr>
        </table>
        <br /><br />
           
    </fieldset>
    <br /><br />
    
</asp:Content>

