<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_testXML" Codebehind="testXML.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    Serie: <asp:TextBox ID="serie" runat="server"></asp:TextBox><br /><br />
    Folio: <asp:TextBox ID="folio" runat="server"></asp:TextBox><br /><br />
    <asp:Button ID="btnValidate" runat="server" Text="Validar" /><br /><br />
    Sello: <asp:Label ID="lblSello" runat="server"></asp:Label><br />
    Cadena: <asp:Label ID="lblCadena" runat="server"></asp:Label><br />
    Certificado: <asp:Label ID="lblCertificado" runat="server"></asp:Label><br />
    No. Aprobación: <asp:label ID="lblNoAprobacion" runat="server"></asp:label><br />
    Año Aprobación: <asp:Label ID="lblAnioAprobacion" runat="server"></asp:Label>
</asp:Content>

