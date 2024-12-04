<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.portalcfd_reportes_informeSATmensual" Codebehind="informeSATmensual.aspx.vb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">--%>
        
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td class="item">
                        Para generar el informe seleccione el mes y año y presione el botón generar:<br /><br />
                        Mes: <asp:DropDownList ID="mesid" runat="server" CssClass="box"></asp:DropDownList>&nbsp;&nbsp;Año: <asp:DropDownList ID="annio" runat="server" CssClass="box"></asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btnGenerate" runat="server" Text ="Generar" CssClass="boton" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                        <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
        <br /><br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblHistoricReports" runat="server" Font-Bold="true" Text="Mis informes" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="informeslist" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" GridLines="None" 
                            PageSize="15" ShowStatusBar="True" 
                            Skin="Simple" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" 
                                Name="Clients" Width="100%" NoMasterRecordsText="No existen registros.">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="mes" HeaderText="Mes" 
                                        UniqueName="mes">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="annio" HeaderText="Año" 
                                        UniqueName="annio">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Archivo" 
                                        UniqueName="archivo">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("id") %>' 
                                                CommandName="cmdDownload" Text='<%# Eval("archivo") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" 
                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" 
                                                CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" 
                                                ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </fieldset>
   <%-- </telerik:RadAjaxPanel>--%>
</asp:Content>

