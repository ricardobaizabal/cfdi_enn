<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="carteraGraf.aspx.vb" Inherits="LinkiumCFDI.carteraGraf" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
           <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblReportsLegend" runat="server" Font-Bold="true" CssClass="item" Text="Reporte de cartera"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td class="item">
                        <table width="80%" style="line-height:20px;">
                            <tr>
                                <td>
                                    Cliente:<br /><asp:DropDownList ID="clienteid" runat="server" CssClass="box" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
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
        
        <br />
        
        <fieldset>
            <legend style="padding-right: 6px; color: Black; font-weight:bold;" class="item">Acumulado              
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td class="item">
                        <telerik:RadGrid ID="reporteGrid" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" GridLines="None" ShowFooter="false" 
                            PageSize="50" ShowStatusBar="True" ShowHeader="true" 
                            Skin="Office2007" Width="100%">
                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Simple">
                            </HeaderContextMenu>
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False"
                                Name="Cobranza" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas.">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="columna1">
                                        <HeaderTemplate>Sin vencer</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lnkR1" runat="server" Text='<%# formatcurrency(eval("r1"),2) %>' CommandName="cmdR1"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn UniqueName="columna2">
                                        <HeaderTemplate>1 a 15 días</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lnkR2" runat="server" Text='<%# formatcurrency(eval("r2"),2) %>' CommandName="cmdR2"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>16 a 30 días</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lnkR3" runat="server" Text='<%# formatcurrency(eval("r3"),2) %>' CommandName="cmdR3"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>31 - 45 días</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lnkR4" runat="server" Text='<%# formatcurrency(eval("r4"),2) %>' CommandName="cmdR4"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>46 - 60 días</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lnkR5" runat="server" Text='<%# formatcurrency(eval("r5"),2) %>' CommandName="cmdR5"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>61 - 90 días</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lnkR6" runat="server" Text='<%# formatcurrency(eval("r6"),2) %>' CommandName="cmdR6"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>más de 90 días</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lnkR7" runat="server" Text='<%# formatcurrency(eval("r7"),2) %>' CommandName="cmdR7"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <telerik:GridTemplateColumn>
                                        <HeaderTemplate>más de 90 días</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton id="lnkR8" runat="server" Text='<%# formatcurrency(eval("total"),2) %>' CommandName="cmdR8"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    
                                    <%--<telerik:GridBoundColumn DataField="total" DataFormatString="{0:c}" HeaderText="Total" ItemStyle-HorizontalAlign="Right" UniqueName="total">
                                        <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                    </telerik:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                
            </table>
        </fieldset>
        
        <br />
        
        <asp:Panel ID="panelDetalle" runat="server" Visible="false">
            <fieldset class="item">
                <legend style="padding-right: 6px; color: Black; font-weight:bold;">Detalle</legend>
                <table width="100%">
                    <tr>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnExportExcel" runat="server" Text="Exportar a Excel" /><br /><br />
                            <telerik:RadGrid ID="detalleGrid" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" GridLines="None" ShowFooter="true" 
                                PageSize="50" ShowStatusBar="True" ShowHeader="true" AllowSorting="true" 
                                Skin="Office2007" Width="100%" ExportSettings-ExportOnlyData="true">
                                <ExportSettings IgnorePaging="True" FileName="ReporteCobranza.xls">
                                    <Excel Format="ExcelML" />
                                </ExportSettings>
                                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Simple">
                                </HeaderContextMenu>
                                <PagerStyle Mode="NumericPages" />
                                <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" 
                                    Name="Ingresos" Width="100%" NoMasterRecordsText="No existen registros en ese rango de fechas.">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="serie" 
                                            HeaderText="Serie" UniqueName="serie">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>
                                                Folio
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFolio" runat="server" 
                                                    CommandArgument='<%# Eval("id") %>' CommandName="cmdFolio" 
                                                    Text='<%# Eval("folio") %>'></asp:LinkButton>
                                                <asp:Label ID="lblFolio" runat="server" Visible="false"></asp:Label>
                                                <telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="lnkFolio" RelativeTo="Element"
                                                Position="BottomCenter" RenderInPageRoot="true" ManualClose="true">
                                                    <%#Eval("DetalleFactura")%>
                                                </telerik:RadToolTip>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="fecha" DataFormatString="{0:d}" 
                                            HeaderText="Fecha factura" UniqueName="fecha">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="fecha_promesa" DataFormatString="{0:d}" 
                                            HeaderText="Fecha promesa" UniqueName="fecha_promesa">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="dias" SortExpression="dias" 
                                            HeaderText="Días de atraso" UniqueName="dias">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="razonsocial" HeaderText="Razón Social" SortExpression="razonsocial" 
                                            UniqueName="razonsocial">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="importe" DataFormatString="{0:c}" 
                                            HeaderText="Importe" ItemStyle-HorizontalAlign="Right" UniqueName="importe">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="iva" DataFormatString="{0:c}" 
                                            HeaderText="IVA" ItemStyle-HorizontalAlign="Right" UniqueName="iva">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="total" DataFormatString="{0:c}" 
                                            HeaderText="Total" ItemStyle-HorizontalAlign="Right" UniqueName="total">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="estatus_cobranza" HeaderText="Estatus" 
                                            ItemStyle-HorizontalAlign="Left" UniqueName="estatus_cobranza">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPDF" runat="server" Text="pdf" CommandArgument='<%# Eval("id") %>'
                                                    CommandName="cmdPDF"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </fieldset>
       </asp:Panel>
       
       <br />
        
        
        <fieldset class="item" style="padding-left:30px; margin-right:auto;">
            <legend style="padding-right: 6px; color: Black; font-weight:bold;">Gráfica de cartera              
            </legend>
            <br />
            <br />
            <telerik:RadChart ID="RadChart1" runat="server" Width="1000px" Height="400px" Skin="Green">
                
                <Series>
                    <telerik:ChartSeries DataYColumn="total" DefaultLabelValue="#Y{C}" Name="Monto">
                        <Appearance>
                            <FillStyle MainColor="DarkSeaGreen" SecondColor="DarkSeaGreen">
                            </FillStyle>
                            <Border Color="DimGray" />
                        </Appearance>
                    </telerik:ChartSeries>
                </Series>
                <PlotArea>
                    <XAxis DataLabelsColumn="rango" AutoScale="False" MaxValue="7" MinValue="0" Step="1">
                        <Appearance>
                            <MajorGridLines Color="DimGray" Width="0" />
                        </Appearance>
                        <Items>
                            <telerik:ChartAxisItem>
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="1">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="2">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="3">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="4">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="5">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="6">
                            </telerik:ChartAxisItem>
                            <telerik:ChartAxisItem Value="7">
                            </telerik:ChartAxisItem>
                        </Items>
                        <AxisLabel>
                            <TextBlock>
                                <Appearance TextProperties-Font="Verdana, 9.75pt, style=Bold">
                                </Appearance>
                            </TextBlock>
                        </AxisLabel>
                        
                    </XAxis>
                    <YAxis>
                        <Appearance ValueFormat="Currency">
                            <MajorGridLines Color="DimGray" />
                            <TextAppearance TextProperties-Font="Arial, 8.25pt">
                            </TextAppearance>
                        </Appearance>
                        <AxisLabel>
                            <TextBlock>
                                <Appearance TextProperties-Font="Verdana, 9.75pt, style=Bold">
                                </Appearance>
                            </TextBlock>
                        </AxisLabel>
                    </YAxis>
                    <Appearance Dimensions-Margins="13%, 5%, 12%, 10%" Corners="Round, Round, Round, Round, 6">
                        <FillStyle MainColor="White" FillType="Solid">
                        </FillStyle>
                        <Border Color="DimGray" />
                    </Appearance>
                    <YAxis2>
                        <AxisLabel>
                            <TextBlock>
                                <Appearance TextProperties-Font="Verdana, 9.75pt, style=Bold">
                                </Appearance>
                            </TextBlock>
                        </AxisLabel>
                    </YAxis2>
                </PlotArea>
                <ChartTitle>
                    <Appearance Corners="Round, Round, Round, Round, 6" Dimensions-Margins="4%, 10px, 14px, 0%" Position-AlignedPosition="Top">
                        <FillStyle MainColor="224, 224, 224" GammaCorrection="False">
                        </FillStyle>
                        <Border Color="DimGray" />
                    </Appearance>
                    <TextBlock Text="">
                        <Appearance TextProperties-Font="Verdana, 11.25pt">
                        </Appearance>
                    </TextBlock>
                </ChartTitle>
                <Legend Visible="False">
                    <Appearance Dimensions-Margins="18%, 4%, 1px, 1px" Visible="False">
                        <Border Color="DimGray" />
                        <ItemTextAppearance TextProperties-Color="DimGray">
                        </ItemTextAppearance>
                    </Appearance>
                </Legend>
            </telerik:RadChart>
        </fieldset>
</asp:Content>
