<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_facturaxion_CreaUsuario" Codebehind="CreaUsuario.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br />
    <strong>
        Generación de código de usuario para facturaxión
    </strong>
    <br /><br />
    <strong>
        Código de proveedor:<br />
    </strong>
    <asp:TextBox ID="codigoProveedor" runat="server" CssClass="box" Width="600px"></asp:TextBox>
    <br /><br />
    <strong>
        Código de usuario con Proveedor:<br />
    </strong>
    <asp:TextBox ID="codigoUsuarioProveedor" runat="server" CssClass="box"></asp:TextBox>
    <br /><br />
    <asp:Button ID="btnCreate" runat="server" Text="Generar código" />
    <br /><br />
    <asp:Label ID="lblNuevoCodigo" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
    
</asp:Content>

