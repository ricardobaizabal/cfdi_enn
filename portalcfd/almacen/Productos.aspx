<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" Inherits="LinkiumCFDI.almacen_portalcfd_Productos" MaintainScrollPositionOnPostback="true" Codebehind="Productos.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            height: 17px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
               <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <span class="item">
                Palabra clave: <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>&nbsp;
            <asp:Button ID="btnSearch" runat="server" CssClass="boton" Text="Buscar" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CssClass="boton" Text="Ver todo" />
            </span>
            <br /><br />
        </fieldset>
        <br />
        
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListadoProductos_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProductsListLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="productslist" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
                            Skin="Simple" OnNeedDataSource="productslist_NeedDataSource">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <ExportSettings IgnorePaging="True" FileName="CatalogoProductos">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Products" AllowMultiColumnSorting="False" CommandItemDisplay="Top">
                                <CommandItemSettings ShowRefreshButton="false" ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="" UniqueName="codigo">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Eval("codigo") %>' CommandArgument='<%# Eval("id") %>'
                                                CommandName="cmdEdit" CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unitario" HeaderText="" UniqueName="unitario" DataFormatString="{0:c}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unitario2" HeaderText="" UniqueName="unitario2" DataFormatString="{0:c}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unitario3" HeaderText="" UniqueName="unitario3" DataFormatString="{0:c}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unitario4" HeaderText="" UniqueName="unitario3" DataFormatString="{0:c}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="tasa" HeaderText="Tasa" UniqueName="tasa">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                                CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAddProduct" runat="server" CausesValidation="False" 
                            CssClass="item" TabIndex="6" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">
                    </td>
                </tr>
            </table>
        </fieldset>
    
        <br />
        
        <asp:Panel ID="panelProductRegistration" runat="server" Visible="False">

        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image3" runat="server" ImageUrl="~/images/icons/AgregarEditarProducto_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProductEditLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>

            <br />

            <table width="100%">
                <tr>
                    <td class="style4" width="20%">
                        <asp:Label ID="lblCode" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style4" width="20%">
                        <asp:Label ID="lblUnit" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style4" width="20%">
                        <asp:Label ID="lblUnitaryPrice" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style4" width="20%">
                        <asp:Label ID="lblUnitaryPrice2" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style4" width="20%">
                        <asp:Label ID="lblUnitaryPrice3" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style4" width="20%">
                        <telerik:RadTextBox ID="txtCode" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td class="style4" width="20%">
                        <telerik:RadTextBox ID="txtUnit" Runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td class="style4" width="20%">
                        <telerik:RadNumericTextBox ID="txtUnitaryPrice" runat="server" MinValue="0" 
                            Skin="Default" Value="0" Width="85%">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td class="style4" width="20%">
                        <telerik:RadNumericTextBox ID="txtUnitaryPrice2" runat="server" MinValue="0" 
                            Skin="Default" Value="0" Width="85%">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td class="style4" width="20%">
                        <telerik:RadNumericTextBox ID="txtUnitaryPrice3" runat="server" MinValue="0" 
                            Skin="Default" Value="0" Width="85%">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style4" width="20%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCode" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td class="style4" width="20%">
                        &nbsp;</td>
                    <td class="style4" width="20%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtUnitaryPrice" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td class="style4" width="20%">
                        <asp:Label ID="Label1" Text="Precio Unit. 4" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style4" width="20%">
                        &nbsp;</td>
                    <td class="style4" width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style4" width="20%">
                       <telerik:RadNumericTextBox ID="txtUnitaryPrice4" runat="server" MinValue="0" 
                            Skin="Default" Value="0" Width="85%">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td class="style4" width="20%">
                        &nbsp;</td>
                    <td class="style4" width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                
                
                <tr>
                    <td width="20%">
                        <asp:Label ID="lblDescription" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="20%">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <telerik:RadTextBox ID="txtDescription" Runat="server" Width="95%" Height="80px" TextMode="MultiLine" MaxLength="400">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtDescription" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td width="20%">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                        </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" style="line-height:30px;">
                        <asp:Label ID="lblUso" runat="server" CssClass="item" Font-Bold="true" Text="Uso:"></asp:Label><br />
                        <telerik:RadTextBox ID="txtUso" Runat="server" Width="95%" TextMode="MultiLine">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="line-height:30px;">
                        <asp:Label ID="lblPresentacion" runat="server" CssClass="item" Font-Bold="true" Text="Presentación:"></asp:Label><br />
                        <telerik:RadTextBox ID="txtPresentacion" Runat="server" Width="95%" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="line-height:30px;">
                        <asp:Label ID="lblTasa" runat="server" CssClass="item" Font-Bold="true" Text="Tasa:"></asp:Label>&nbsp;<asp:RequiredFieldValidator ID="valTasa" runat="server" 
                            ControlToValidate="tasaid" CssClass="item" ErrorMessage="Requerido" InitialValue="0"></asp:RequiredFieldValidator><br />
                        <asp:DropDownList ID="tasaid" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                    <td style="line-height:30px;">
                        <asp:Label ID="lblMaximo" runat="server" CssClass="item" Font-Bold="true" Text="Máximo:"></asp:Label><br />
                        <telerik:RadNumericTextBox ID="txtMaximo" runat="server" MinValue="0" 
                            Skin="Default" Value="0">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="line-height:30px;">
                        <asp:Label ID="lblMinimo" runat="server" CssClass="item" Font-Bold="true" Text="Mínimo:"></asp:Label><br />
                        <telerik:RadNumericTextBox ID="txtMinimo" runat="server" MinValue="0" 
                            Skin="Default" Value="0">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="line-height:30px;">
                        <asp:Label ID="lblReorden" runat="server" CssClass="item" Font-Bold="true" Text="Punto reorden:"></asp:Label><br />
                        <telerik:RadNumericTextBox ID="txtReorden" runat="server" MinValue="0" 
                            Skin="Default" Value="0">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="line-height:30px;">
                        <asp:Label ID="lblCostoStd" runat="server" CssClass="item" Font-Bold="true" Text="Costo estándar:"></asp:Label><br />
                        <telerik:RadNumericTextBox ID="txtCostoStd" runat="server" MinValue="0" 
                            Skin="Default" Value="0">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="line-height:30px;">
                        <asp:Label ID="lblCompraMin" runat="server" CssClass="item" Font-Bold="true" Text="Compra mínima:"></asp:Label><br />
                        <telerik:RadNumericTextBox ID="txtCompraMinima" runat="server" MinValue="0" 
                            Skin="Default" Value="0">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td style="line-height:30px;">
                        <asp:Label ID="lblTiempoEntrega" runat="server" CssClass="item" Font-Bold="true" Text="Tiempo de entrega:"></asp:Label><br />
                        <telerik:RadTextBox ID="txtTiempoEntrega" Runat="server">
                        </telerik:RadTextBox>
                    </td>
                    <td style="line-height:30px;">
                        <asp:Label ID="lblMoneda" runat="server" CssClass="item" Font-Bold="true" Text="Moneda:"></asp:Label><br />
                        <asp:DropDownList ID="monedaid" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                    <td style="line-height:30px;">
                        <asp:Label ID="lblTipoCambioStd" runat="server" CssClass="item" Font-Bold="true" Text="Tipo de cambio Std.:"></asp:Label><br />
                        <telerik:RadNumericTextBox ID="txtTipoCambio" runat="server" MinValue="0" 
                            Skin="Default" Value="0">
                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2" style="line-height:30px;">
                        <asp:Label ID="lblProveedor" runat="server" CssClass="item" Font-Bold="true" Text="Proveedor:"></asp:Label><br />
                        <asp:DropDownList ID="proveedorid" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                    <td colspan="3" style="line-height:30px; vertical-align:bottom;">
                        <br />
                        <asp:CheckBox ID="chkInventariableBit" Font-Bold="true" runat="server" Text="Producto Inventariable" CssClass="item" />&nbsp;&nbsp;
                        <%--<asp:CheckBox ID="chkManiobraBit" Font-Bold="true" runat="server" Text="Este producto será considerado como una maniobra" CssClass="item" />--%>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <br />
                        <asp:Button ID="btnSaveProduct" runat="server" CssClass="item" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="item" 
                            CausesValidation="False" />
                    </td>
                    <td width="20%">
                        </td>
                    <td width="20%" valign="bottom">
                    </td>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="20%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Label ID="lblMensaje" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="width: 66%; height: 5px;">
                        <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                        <asp:HiddenField ID="ProductID" runat="server" Value="0" />
                    </td>
                </tr>
            </table>

        </fieldset>

    </asp:Panel>
    
    </telerik:RadAjaxPanel>
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
    
</asp:Content>

