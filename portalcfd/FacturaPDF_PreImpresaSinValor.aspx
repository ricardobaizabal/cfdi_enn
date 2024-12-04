<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeFile="FacturaPDF_PreImpresaSinValor.aspx.vb" Inherits="portalcfd_FacturaPDF_PreImpresaSinValor" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=5.1.11.713, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <asp:Image ID="img4" runat="server" ImageUrl="~/portalCFD/images/arrow.gif" />  <asp:HyperLink ID="lnkPDF" CssClass="item" runat="server" Text="Imprimir PDF"></asp:HyperLink>&nbsp;&nbsp;
    <asp:Image ID="img1" runat="server" ImageUrl="~/portalCFD/images/arrow.gif" />  <asp:HyperLink ID="lnkPDFSinValor" CssClass="item" runat="server" Text="Imprimir PDF sin valor"></asp:HyperLink>&nbsp;&nbsp;
    <asp:Image ID="img2" runat="server" ImageUrl="~/portalCFD/images/arrow.gif" />  <asp:HyperLink ID="lnkPDFPreImpresa" CssClass="item" runat="server" Text="Imprimir PDF forma Pre-Impresa"></asp:HyperLink>&nbsp;&nbsp;
    <asp:Image ID="img3" runat="server" ImageUrl="~/portalCFD/images/arrow.gif" />  <asp:HyperLink ID="lnkPDFPreImpresaSinValor" CssClass="item" runat="server" Text="Imprimir PDF forma Pre-Impresa sin valor"></asp:HyperLink>&nbsp;&nbsp;
    <br /><br />
    <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblCFDPDF" runat="server" Font-Bold="true" CssClass="item" Text="CFD Impresión / PDF"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:ReportViewer id="ReportViewer1" runat="server" Height="700px" ProgressText="Generando CFD" ShowParametersButton="False" Width="900px">
                            <Resources ExportButtonText="Exportar ahora" ExportSelectFormatText="Seleccione el formato para exportar" 
                                ExportToolTip="Exportar" />                                
                        </telerik:ReportViewer>
                    </td>
                </tr>
            </table>
        </fieldset>
</asp:Content>

