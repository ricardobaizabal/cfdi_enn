<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_VerificaCertificado" Codebehind="VerificaCertificado.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <strong>Llave privada:</strong> <asp:Label ID="lblLlave" runat="server"></asp:Label><br /><br />
    <strong>Contraseña:</strong> <asp:Label ID="lblContrasena" runat="server"></asp:Label><br /><br />
    <strong>Certificado:</strong> <asp:Label ID="lblCertificado" runat="server"></asp:Label><br /><br />
    <asp:Button ID="btnVerify" runat="server" Text="Verificar" /><br /><br />
    <asp:label ID="lblResultado" runat="server" Font-Bold="true"></asp:label>
</asp:Content>

