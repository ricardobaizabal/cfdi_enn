<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_administracion_recepcion_facturas" Codebehind="recepcion_facturas.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    Archivo XML: <asp:FileUpload ID="xmlFile" runat="server" />&nbsp;<asp:Button ID="btnUpload" Text="Guardar" runat="server" />
    <br /><br />
    <asp:Label ID="lblUUID" runat="server"></asp:Label>
</asp:Content>


