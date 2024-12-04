<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeBehind="Conceptos.aspx.vb" Inherits="LinkiumCFDI.Conceptos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript">
         function OnRequestStart(target, arguments) {
             if ((arguments.get_eventTarget().indexOf("productslist") > -1) || (arguments.get_eventTarget().indexOf("foto") > -1) || (arguments.get_eventTarget().indexOf("btnAddProduct") > -1)) {
                 arguments.set_enableAjax(false);
             }
         }
         
       </script>
    <style type="text/css">
        .style4 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <asp:Panel ID="searchpanel" DefaultButton="btnSearch" runat="server">
                <table style="width:100%;">
                    <tr>
                        <td style="width:10%">
                            <span class="item" style="width:800px;">Palabra clave:</span>
                        </td>
                        <td style="width:20%">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" Text="Ver todo" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <br />
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListadoProductos_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProductsListLegend" runat="server" Font-Bold="true" CssClass="item">Listado de Conceptos</asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px"></td>
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
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Código" UniqueName="codigo">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Eval("codigo") %>' CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="claveprodserv" HeaderText="Clave SAT" UniqueName="claveprodserv">
                                    </telerik:GridBoundColumn>
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
                                    <telerik:GridBoundColumn DataField="unitario4" HeaderText="Precio Unit. 4" UniqueName="unitario4" DataFormatString="{0:c}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn DataField="ieps" HeaderText="IEPS %" UniqueName="ieps" NumericType="Percent" DataFormatString="{0:P2}">
                                    </telerik:GridNumericColumn>
                                    <telerik:GridBoundColumn DataField="tasa" HeaderText="Tasa" UniqueName="tasa">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="existencia" HeaderText="Existencia" UniqueName="existencia">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete" Exportable="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
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
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAddProduct" runat="server" CausesValidation="False" CssClass="item" TabIndex="6" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelProductRegistration" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/icons/AgregarEditarProducto_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblProductEditLegend" runat="server" Font-Bold="True" CssClass="item">Agregar/Editar Concepto</asp:Label>
                </legend>
                <br />
                <table width="100%" border="0">
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
                            <telerik:RadTextBox ID="txtCode" runat="server" Width="85%">
                            </telerik:RadTextBox>
                        </td>
                        <td class="style4" width="20%">
                            <asp:DropDownList ID="unidadid" runat="server" CssClass="box" Width="85%"></asp:DropDownList>
                        </td>
                        <td class="style4" width="20%">
                            <telerik:RadNumericTextBox ID="txtUnitaryPrice" runat="server" MinValue="0" Skin="Default" Value="0" Width="50%">
                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td class="style4" width="20%">
                            <telerik:RadNumericTextBox ID="txtUnitaryPrice2" runat="server" MinValue="0" Skin="Default" Value="0" Width="50%">
                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td class="style4" width="20%">
                            <telerik:RadNumericTextBox ID="txtUnitaryPrice3" runat="server" MinValue="0" Skin="Default" Value="0" Width="50%">
                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" width="20%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode" CssClass="item" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style4" width="20%">
                            <asp:RequiredFieldValidator ID="valClaveUnidad" runat="server" ControlToValidate="unidadid" CssClass="item" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style4" width="20%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUnitaryPrice" CssClass="item" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
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
                            Skin="Default" Value="0" Width="50%">
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
                        <td class="style4" width="20%">
                        <br />
                            <asp:Label ID="lblDescription" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <telerik:RadTextBox ID="txtDescription" runat="server" Width="95%" Height="80px" TextMode="MultiLine" MaxLength="400">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDescription" CssClass="item" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style4" width="20%">
                            <asp:Label ID="lblClave" runat="server" CssClass="item" Font-Bold="true" Text="Clave producto / servicio:"></asp:Label><br />
                        </td>
                        <td class="style4" width="20%">
                            <asp:Label ID="lblTasa" runat="server" CssClass="item" Font-Bold="true" Text="Tasa:"></asp:Label>
                        </td>
                        <td class="style4" width="20%">
                            <asp:Label ID="lblCostoStd" runat="server" CssClass="item" Font-Bold="true" Text="Costo:"></asp:Label>
                        </td>
                        <td class="style4" width="20%">
                            <asp:Label ID="lblMoneda" runat="server" CssClass="item" Font-Bold="true" Text="Moneda:"></asp:Label>
                        </td>
                        <td class="style4" width="20%">
                            <asp:Label ID="lblIEPS" runat="server" CssClass="item" Font-Bold="true" Text="% IEPS:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" width="20%">
                            <asp:DropDownList ID="cboProductoServ" runat="server" CssClass="box" Width="90%"></asp:DropDownList>
                        </td>
                        <td class="style4" width="20%">
                            <asp:DropDownList ID="tasaid" runat="server" CssClass="box"></asp:DropDownList>
                        </td>
                        <td class="style4" width="20%">
                            <telerik:RadNumericTextBox ID="txtCostoStd" runat="server" MinValue="0" Skin="Default" Value="0" Width="50%">
                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                            </telerik:RadNumericTextBox>
                        </td>
                        <td class="style4" width="20%">
                            <asp:DropDownList ID="monedaid" runat="server" CssClass="box"></asp:DropDownList>
                        </td>
                        <td class="style4" width="20%">
                            <telerik:RadNumericTextBox ID="txtIEPS" runat="server" Type="Percent" MinValue="0" Skin="Default" Value="0" Width="50%">
                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                            </telerik:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" width="20%">
                            <asp:RequiredFieldValidator ID="valClaveServ" runat="server" ControlToValidate="cboProductoServ" CssClass="item" InitialValue="0" Text="Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style4" width="20%">
                            <asp:RequiredFieldValidator ID="valTasa" runat="server" ControlToValidate="tasaid" CssClass="item" ErrorMessage="Requerido" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style4" width="20%">
                            <asp:Label ID="Label2" runat="server" CssClass="item" Font-Bold="True" Text="Objeto de impuesto:"></asp:Label>
                        </td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style4" width="20%">
                            <asp:DropDownList ID="cbmObjetoImpuesto" runat="server" CssClass="box" Width="85%"></asp:DropDownList>
                        </td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style4" width="20%">
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" InitialValue="0" ControlToValidate="cbmObjetoImpuesto" CssClass="item"></asp:RequiredFieldValidator>
                        </td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <strong class="item">Foto del producto:</strong><br />
                            <asp:FileUpload ID="foto" runat="server" /><br /><asp:Image ID="imgProducto" runat="server" Width="200px" Visible="false" />&nbsp;&nbsp; <asp:ImageButton ID="imgBtnEliminarAnexo" runat="server" CausesValidation="false" ImageUrl="~/images/action_delete.gif" Visible="false" />
                            <asp:HiddenField id="hdnFoto" runat="server" Value="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                        <td class="style4" width="20%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <asp:Button ID="btnSaveProduct" runat="server" CssClass="item" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="item" CausesValidation="False" />
                        </td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
                        <td width="20%">&nbsp;</td>
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
    <%--</telerik:RadAjaxPanel>--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>