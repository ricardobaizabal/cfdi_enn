<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeFile="ImprimeFactura.aspx.vb" Inherits="portalcfd_formatos_ImprimeFactura" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=5.1.11.713, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:ReportViewer id="ReportViewer1" runat="server" Height="700px" ProgressText="Generando factura" ShowParametersButton="False" Width="900px">
        <Resources ExportButtonText="Exportar" ExportSelectFormatText="Seleccione el formato"
            ExportToolTip="Exportar" />
    </telerik:ReportViewer>
</asp:Content>

