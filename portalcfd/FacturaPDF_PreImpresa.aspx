<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeFile="FacturaPDF_PreImpresa.aspx.vb" Inherits="portalcfd_FacturaPDF_PreImpresa" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=5.1.11.713, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br />
    <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblCFDPDF" runat="server" Font-Bold="true" CssClass="item" Text="CFDI Impresión"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:ReportViewer id="ReportViewer1" runat="server" Height="800px" ProgressText="Generando CFDI" ShowParametersButton="False" Width="1000px" ShowExportGroup="false" ShowPrintButton="true">
                        </telerik:ReportViewer>
                    </td>
                </tr>
            </table>
        </fieldset>
</asp:Content>

